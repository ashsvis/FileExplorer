using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.Collections.Specialized;

namespace FileExplorer
{
    public partial class FileExplorerForm : Form
    {
        Hashtable icons = new Hashtable();
        ListViewEx mainListView;
        // хранилище виртуальных записей для списка файлов
        List<ListViewItemFile> files = new List<ListViewItemFile>();
        List<ListViewItemFile> cutcopy = new List<ListViewItemFile>();
        bool cutop;
        bool internalop;

        public FileExplorerForm()
        {
            InitializeComponent();
            // подключение расширенного виртуального списка
            mainListView = new ListViewEx()
            {
                Dock = DockStyle.Fill,
                SmallImageList = imageList1,
                FullRowSelect = true,
                ShowItemToolTips = true,
                HideSelection = false,
                View = View.Details,
                LabelEdit = true,
                ContextMenuStrip = contextFolderMenu,
                VirtualMode = true
            };
            // определение списка столбцов
            mainListView.Columns.Add(new ColumnHeader() { Text = "Имя", Width = 220 });
            mainListView.Columns.Add(new ColumnHeader() { Text = "Дата изменения", Width = 110, TextAlign = HorizontalAlignment.Center });
            mainListView.Columns.Add(new ColumnHeader() { Text = "Тип", Width = 110 });
            mainListView.Columns.Add(new ColumnHeader() { Text = "Размер", Width = 100, TextAlign = HorizontalAlignment.Right });
            // исключение мерцания в списке
            mainListView.SetDoubleBuffered(true);
            // подключение событий для списка
            mainListView.DoubleClick += mainListView_DoubleClick;
            mainListView.ColumnClick += mainListView_ColumnClick;
            mainListView.RetrieveVirtualItem += mainListView_RetrieveVirtualItem;
            mainListView.SelectedIndexChanged += mainListView_SelectedIndexChanged;
            mainListView.VirtualItemsSelectionRangeChanged += mainListView_VirtualItemsSelectionRangeChanged;
            mainListView.SearchForVirtualItem += mainListView_SearchForVirtualItem;
            mainListView.BeforeLabelEdit += mainListView_BeforeLabelEdit;
            mainListView.AfterLabelEdit += mainListView_AfterLabelEdit;
            // размещение списка на панели
            panelListPlace.Controls.Add(mainListView);
        }

        /// <summary>
        /// Когда пользователь начинает редактировать имя папки или файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            var index = e.Item;
            var item = files[index];
            var oldName = item.FileName;
            var path = Path.GetDirectoryName(oldName);
            e.CancelEdit = false;
        }

        /// <summary>
        /// Когда пользователь заканчивает редактировать имя папки или файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;
            var index = e.Item;
            var item = files[index];
            var oldName = item.FileName;
            var path = Path.GetDirectoryName(oldName);
            var newName = Path.Combine(path, e.Label);
            if (oldName == newName) return;
            internalop = true;
            if (item.IsFolder)
                Directory.Move(oldName, newName);
            else
                File.Move(oldName, newName);
            FillVirtualList();
            FindLabel(newName);
        }

        /// <summary>
        /// Определение индекса в виртуальном списке при поиске
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_SearchForVirtualItem(object sender, SearchForVirtualItemEventArgs e)
        {
            e.Index = files.FindIndex(item => item.FileName == e.Text);
        }

        /// <summary>
        /// Текущий элемент виртуального списка был изменён
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            tsbOpen.Visible = mainListView.SelectedIndices.Count == 1;
            mainListView.ContextMenuStrip = mainListView.SelectedIndices.Count > 0 ? contextItemsMenu : contextFolderMenu;
            if (mainListView.SelectedIndices.Count == 1)
            {
                var item = files[mainListView.SelectedIndices[0]];
                var what = item.IsFolder ? "Папка:" : "Файл:";
                var size = item.IsFolder ? "" : $". Размер (байт): {item.Length}";
                ShowStatus($"{what} \"{item.FileName}\"{size}");
            }
            else
                if (mainListView.SelectedIndices.Count > 1)
                    ShowStatus($"Выбрано элементов: {mainListView.SelectedIndices.Count}");
        }

        /// <summary>
        /// Виртуальный выбор списка изменён
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_VirtualItemsSelectionRangeChanged(object sender, 
            ListViewVirtualItemsSelectionRangeChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                mainListView.ContextMenuStrip = contextFolderMenu;
                return;
            }
            mainListView.ContextMenuStrip = contextItemsMenu;
        }

        private delegate void ShowErrorProc(string message);

        /// <summary>
        /// Метод для показа сообщений статусной строки
        /// </summary>
        /// <param name="msg"></param>
        private void ShowStatus(string msg)
        {
            if (InvokeRequired)
            {
                var ddd = new ShowErrorProc(ShowStatus);
                Invoke(ddd, msg);
            }
            else
            {
                tsslStatus.BackColor = SystemColors.Control;
                tsslStatus.Text = msg;
                statusStrip1.Refresh();
            }
        }

        /// <summary>
        /// Метод для показа сообщений статусной строки
        /// </summary>
        /// <param name="msg"></param>
        private void ShowErrorStatus(string msg)
        {
            if (InvokeRequired)
            {
                var ddd = new ShowErrorProc(ShowErrorStatus);
                Invoke(ddd, msg);
            }
            else
            {
                tsslStatus.BackColor = Color.Yellow;
                tsslStatus.Text = msg;
                statusStrip1.Refresh();
            }
        }

        /// <summary>
        /// При первоначальной загрузке основной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileExplorerForm_Load(object sender, EventArgs e)
        {
            FillFoldersTree();
        }

        /// <summary>
        /// Заполнение дерева каталогов
        /// </summary>
        private async void FillFoldersTree()
        {
            var logicalDrives = await FileHelper.GetLogicalDrivesAsync();
            if (mainTree.Nodes.Count > 0) return;
            foreach (var drive in logicalDrives)
            {
                var driveNode = new TreeNodeFile(drive) { DirectoryName = drive };
                mainTree.Nodes.Add(driveNode);
                var collections = await FileHelper.GetDirectoriesCollectionAsync(drive);
                var mess = FileHelper.GetErrorMessage();
                if (collections.Length > 0)
                    driveNode.Nodes.Add(new TreeNodeFile()); // add stub
                else
                    driveNode.Text += $"{mess}";
                driveNode.ImageIndex = driveNode.SelectedImageIndex = collections.Length > 0 ? 0 : 1;
            }
            mainListView.ResizeColumns(0);
        }

        /// <summary>
        /// Перед открытием узла дерева каталогов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node as TreeNodeFile;
            if (node == null) return;
            NodeBeforeExpand(node);
        }

        /// <summary>
        /// Выполняется перед разворачиванием содержимого узла
        /// </summary>
        /// <param name="node"></param>
        private async void NodeBeforeExpand(TreeNodeFile node)
        {
            node.Nodes.Clear();
            var collection = FileHelper.GetDirectoriesCollection(node.DirectoryName);
            foreach (var dir in collection)
            {
                var fileName = dir.FullName.Substring(node.DirectoryName.Length);
                if (fileName.StartsWith("$")) continue;
                var dirNode = new TreeNodeFile() { DirectoryName = dir.FullName };
                dirNode.CreationTime = dir.CreationTime;
                dirNode.LastAccessTime = dir.LastAccessTime;
                dirNode.LastWriteTime = dir.LastWriteTime;
                dirNode.Text = Path.GetFileName(dir.FullName);
                node.Nodes.Add(dirNode);
                // уточнение существующей вложенности каталогов
                var collections = await FileHelper.GetDirectoriesCollectionAsync(dir.FullName);
                var mess = FileHelper.GetErrorMessage();
                if (collections.Length > 0)
                {
                    dirNode.Nodes.Add(new TreeNodeFile()); // add stub
                }
                else
                    dirNode.Text += $"{mess}";
                if (!dir.IsEmpty)
                    dirNode.ImageIndex = dirNode.SelectedImageIndex = 1;
            }
        }

        /// <summary>
        /// После выбора узла в дереве каталогов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node as TreeNodeFile;
            if (node == null) return;
            tsslPath.Text = node.DirectoryName;
            FillVirtualList();
            try
            {
                fileSystemWatcher1.Path = node.DirectoryName;
            }
            catch (Exception ex)
            {
                ShowErrorStatus(ex.Message);
            }
        }

        /// <summary>
        /// Получение очередного элемента для виртуального списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem();
            var item = files[e.ItemIndex];
            e.Item.Text = item.Text;
            e.Item.ImageIndex = item.ImageIndex;
            e.Item.SubItems.Add(item.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
            e.Item.SubItems.Add(item.IsFolder ? "Папка с файлами" : $"Файл {Path.GetExtension(item.Text)}");
            e.Item.SubItems.Add(item.IsFolder ? "" : item.Length.ToString("### ### ### ##0"));
        }

        /// <summary>
        /// Заполнение виртуального списка файлов
        /// </summary>
        private async void FillVirtualList(string searchPattern = "")
        {
            tsbOpen.Visible = false;
            mainListView.VirtualListSize = 0;
            files.Clear();
            var searchMode = !string.IsNullOrWhiteSpace(searchPattern);
            if (searchMode) Refresh();
            var node = (TreeNodeFile)mainTree.SelectedNode;
            if (node == null) return;
            // загрузка имён папок
            var list = new List<EntryInfo>();
            var collections = await FileHelper.GetDirectoriesCollectionAsync(node.DirectoryName, searchPattern);
            list.AddRange(collections);
            var folders = list.ToArray();
            foreach (var dir in folders)
            {
                var fileName = node.DirectoryName.Length < dir.FullName.Length
                     ? dir.FullName.Substring(node.DirectoryName.Length)
                     : dir.FullName;
                if (fileName.StartsWith("$")) continue;
                var lvi = new ListViewItemFile()
                {
                    Text = searchMode ? dir.FullName : Path.GetFileName(dir.FullName),
                    ImageIndex = dir.IsEmpty ? 0 : 1,
                    FileName = dir.FullName,
                    CreationTime = dir.CreationTime,
                    LastAccessTime = dir.LastAccessTime,
                    LastWriteTime = dir.LastWriteTime,
                    IsFolder = true
                };
                files.Add(lvi);
            }
            mainListView.VirtualListSize = files.Count;
            // загрузка имён файлов
            list = new List<EntryInfo>();
            list.AddRange(await FileHelper.GetFilesCollectionAsync(node.DirectoryName, searchPattern));
            var dirfiles = list.ToArray();
            foreach (var file in dirfiles)
            {
                var fileName = node.DirectoryName.Length < file.FullName.Length
                     ? file.FullName.Substring(node.DirectoryName.Length)
                     : file.FullName;
                if (fileName.StartsWith("$")) continue;
                var index = 2; // иконка по умолчанию

                // получение иконки для exe файла
                var hash = Path.GetExtension(file.FullName);
                if (!icons.ContainsKey(hash))
                {
                    var icon = Icon.ExtractAssociatedIcon(file.FullName);
                    imageList1.Images.Add(icon);
                    index = imageList1.Images.Count - 1;
                    icons[hash] = index;
                }
                else
                    index = (int)icons[hash];

                var lvi = new ListViewItemFile()
                {
                    Text = searchMode ? file.FullName : Path.GetFileName(file.FullName),
                    ImageIndex = index,
                    FileName = file.FullName,
                    CreationTime = file.CreationTime,
                    LastAccessTime = file.LastAccessTime,
                    LastWriteTime = file.LastWriteTime,
                    Length = file.Length,
                    IsFolder = false
                };
                files.Add(lvi);
            }
            mainListView.VirtualListSize = files.Count;
            files.Sort(FileComparer);
            ShowStatus($"Показано каталогов: {folders.Length} и файлов: {dirfiles.Length}");
            mainListView.ResizeColumns(0);
            mainListView.ContextMenuStrip = contextFolderMenu;
        }

        /// <summary>
        /// Метод для критериев сравнения файлов и папок
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int FileComparer(ListViewItemFile x, ListViewItemFile y)
        {
            if (x.IsFolder && y.IsFolder || !x.IsFolder && !y.IsFolder)
            {
                switch (_lastColumn)
                {
                    case 0:
                        return _reverse ? string.Compare(y.Text, x.Text) : string.Compare(x.Text, y.Text);
                    case 1:
                        return _reverse ? DateTime.Compare(y.LastWriteTime, x.LastWriteTime) : DateTime.Compare(x.LastWriteTime, y.LastWriteTime);
                    case 2:
                        return _reverse ?
                            string.Compare(Path.GetExtension(y.FileName), Path.GetExtension(x.FileName))
                            :
                            string.Compare(Path.GetExtension(x.FileName), Path.GetExtension(y.FileName));
                    case 3:
                        return _reverse ?
                            y.Length > x.Length ? 1 : y.Length < x.Length ? -1 : 0
                            :
                            x.Length > y.Length ? 1 : x.Length < y.Length ? -1 : 0;
                }
                return 0;
            }
            return _reverse ? y.IsFolder && !x.IsFolder ? -1 : 1 : x.IsFolder && !y.IsFolder ? -1 : 1;
        }

        /// <summary>
        /// Двойной клик по списку файлов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_DoubleClick(object sender, EventArgs e)
        {
            OpenFolderOrFile();
        }

        /// <summary>
        /// Открытие папки или запуск файлов
        /// </summary>
        private void OpenFolderOrFile()
        {
            if (mainListView.FocusedItem == null || mainTree.SelectedNode == null) return;
            var item = files[mainListView.FocusedItem.Index];
            if (!item.IsFolder)
            {
                Process.Start(item.FileName);
                return;
            }
            if (!mainTree.SelectedNode.IsExpanded)
                mainTree.SelectedNode.Expand();
            foreach (var node in mainTree.SelectedNode.Nodes.Cast<TreeNodeFile>())
            {
                if (node.DirectoryName == item.FileName)
                {
                    mainTree.SelectedNode = node;
                    break;
                }
            }
        }

        int _lastColumn = 0;
        bool _reverse;

        /// <summary>
        /// Щелчок на заголовках списка для сортировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (_lastColumn != e.Column)
            {
                _lastColumn = e.Column;
                _reverse = false;
            }
            else
                _reverse = !_reverse;

            files.Sort(FileComparer);

            mainListView.Invalidate();
            if (mainListView.FocusedItem != null)
                mainListView.FocusedItem.EnsureVisible();
        }

        private void FileExplorerForm_ResizeEnd(object sender, EventArgs e)
        {
            mainListView.ResizeColumns(0);
        }

        private void panelListPlace_Resize(object sender, EventArgs e)
        {
            mainListView.ResizeColumns(0);
        }

        /// <summary>
        /// Метод проверки соединения с сервером приложения
        /// </summary>
        private void UpdateClock()
        {
            var date = FileHelper.GetDate();
            tsslDateTime.Text = date.ToString("dd.MM.yyyy, ddd, HH.mm.ss");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // получаем текущую дату и время
            UpdateClock();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenFolderOrFile();
            mainListView.Focus();
        }

        /// <summary>
        /// Кнопка "Новая папка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbCreateFolder_Click(object sender, EventArgs e)
        {
            var node = (TreeNodeFile)mainTree.SelectedNode;
            if (node == null) return;
            var newFolderName = FileHelper.AddNewFolderIn(node.DirectoryName);
            node.Nodes.Add(new TreeNodeFile()); // add stub
            internalop = true;
            FillVirtualList();
            FindLabel(newFolderName);
        }

        private void FindLabel(string newFolderName)
        {
            var lvi = mainListView.FindItemWithText(newFolderName);
            if (lvi == null) return;
            mainListView.SelectedIndices.Add(lvi.Index);
            lvi.Focused = true;
            lvi.EnsureVisible();
            mainListView.Focus();
        }

        /// <summary>
        /// Открытие меню "Упорядочить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbArrange_DropDownOpening(object sender, EventArgs e)
        {
            tsmiCopy.Enabled = tsmiCut.Enabled = tsmiRemove.Enabled = mainListView.SelectedIndices.Count > 0;
            tsmiRename.Enabled = mainListView.SelectedIndices.Count == 1;
            tsmiPaste.Enabled = cutcopy.Count > 0;
        }

        /// <summary>
        /// Пункт меню "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRemove_Click(object sender, EventArgs e)
        {
            if (mainListView.FocusedItem == null || mainTree.SelectedNode == null) return;
            if (MessageBox.Show(this, $"{mainListView.SelectedIndices.Count} элементов (файлов, папок) будет удалено в корзину.",
                "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    var folders = false;
                    Cursor = Cursors.WaitCursor;
                    foreach (var index in mainListView.SelectedIndices.Cast<int>())
                    {
                        var item = files[index];
                        if (item.IsFolder)
                            folders = true;
                        try
                        {
                            FileHelper.MoveFileToRecycleBin(item.FileName);
                        }
                        catch (Exception ex)
                        {
                            ShowErrorStatus($"Не удалось удалить файл {item.FileName}: {ex.Message}");
                            continue;
                        }
                    }
                    if (folders)
                    {
                        mainTree.SelectedNode.Collapse();
                        mainTree.SelectedNode.Expand();
                    }
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Переименовать в виртуальном списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRename_Click(object sender, EventArgs e)
        {
            if (mainListView.FocusedItem == null) return;
            mainListView.FocusedItem.BeginEdit();
        }

        /// <summary>
        /// Выбрать всё в виртуальном списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
            mainListView.SelectedIndices.Clear();
            for (var i = 0; i < files.Count; i++)
                mainListView.SelectedIndices.Add(i);
            mainListView.Focus();
        }

        /// <summary>
        /// Подготовка к перемещению выбранных элементов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCut_Click(object sender, EventArgs e)
        {
            cutcopy.Clear();
            foreach (var index in mainListView.SelectedIndices.Cast<int>())
            {
                var item = files[index];
                cutcopy.Add(item);
            }
            cutop = true;
        }

        /// <summary>
        /// Подготовка к копированию выбранных элементов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            cutcopy.Clear();
            foreach (var index in mainListView.SelectedIndices.Cast<int>())
            {
                var item = files[index];
                cutcopy.Add(item);
            }
            cutop = false;
        }

        /// <summary>
        /// Завершение операции по копированию или перемещению элементов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            if (mainTree.SelectedNode == null) return;
            var rootFolder = ((TreeNodeFile)mainTree.SelectedNode).DirectoryName;
            try
            {
                if (cutop)
                    MoveFoldersAndFiles(rootFolder);
                else
                    CopyFoldersAndFiles(rootFolder);
            }
            finally
            {
                cutcopy.Clear();
            }
        }

        /// <summary>
        /// Перемещение файлов и папок
        /// </summary>
        /// <param name="rootFolder"></param>
        private void MoveFoldersAndFiles(string rootFolder)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                foreach (var item in cutcopy)
                {
                    var destName = Path.Combine(rootFolder, Path.GetFileName(item.FileName));
                    ShowStatus($"Перемещаю \"{destName}\"...");
                    try
                    {
                        if (item.IsFolder)
                            FileHelper.MoveFolder(item.FileName, destName);
                        else
                            FileHelper.MoveFile(item.FileName, destName);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorStatus(ex.Message);
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Копирование файлов и папок
        /// </summary>
        /// <param name="rootFolder"></param>
        private void CopyFoldersAndFiles(string rootFolder)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                foreach (var item in cutcopy)
                {
                    var destName = Path.Combine(rootFolder, Path.GetFileName(item.FileName));
                    ShowStatus($"Копирую \"{destName}\"...");
                    try
                    {
                        if (item.IsFolder)
                            FileHelper.CopyFolder(item.FileName, destName);
                        else
                            FileHelper.CopyFile(item.FileName, destName);
                    }
                    catch (Exception ex)
                    {
                        ShowErrorStatus(ex.Message);
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Вид как таблица файлов и папок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbTableView_Click(object sender, EventArgs e)
        {
            mainListView.View = View.Details;
            tsbListView.Checked = false;
        }

        /// <summary>
        /// Вид как список файлов и папок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbListView_Click(object sender, EventArgs e)
        {
            mainListView.View = View.List;
            tsbTableView.Checked = false;
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (internalop)
            {
                internalop = false;
                return;
            }
            FillVirtualList();
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            if (internalop)
            {
                internalop = false;
                return;
            }
            FillVirtualList();
        }

        private void contextFolderMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = cutcopy.Count == 0;
        }

        private void contextItemsMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (files.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            cmiRename.Visible = cmiOpen.Visible = toolStripMenuItem4.Visible =
                cmiProperties.Visible = toolStripMenuItem6.Visible = mainListView.SelectedIndices.Count == 1;
        }

        private void tsbFind_Click(object sender, EventArgs e)
        {
            var sample = tscbFindText.Text.Trim();
            if (string.IsNullOrWhiteSpace(sample)) return;
            if (mainTree.SelectedNode == null) return;
            var rootFolder = ((TreeNodeFile)mainTree.SelectedNode).DirectoryName;
            tsslPath.Text = $"Ищем \"{sample}\" в папке \"{rootFolder}\"";
            ShowStatus($"Ищем \"{sample}\"...");
            statusStrip2.Refresh();
            FillVirtualList(sample);
        }

        private void cmiProperties_Click(object sender, EventArgs e)
        {
            if (mainListView.SelectedIndices.Count != 1 || mainTree.SelectedNode == null) return;
            var item = files[mainListView.SelectedIndices[0]];
            var dir = Path.GetDirectoryName(item.FileName);
            var file = Path.GetFileName(item.FileName);
            ShowFolderFileProperties(dir, file);
        }

        private static void ShowFolderFileProperties(string dir, string file)
        {
            var properties = new NameValueCollection();
            var shellAppType = Type.GetTypeFromProgID("Shell.Application");
            dynamic shell = Activator.CreateInstance(shellAppType);
            var folder = shell.NameSpace(dir);
            if (folder != null)
            {
                var folderItem = folder.ParseName(file);
                var names = new Dictionary<int, string>();
                for (var idx = 0; idx < short.MaxValue; idx++)
                {
                    var key = (string)folder.GetDetailsOf(null, idx);
                    if (!string.IsNullOrEmpty(key))
                    {
                        names.Add(idx, key);
                    }
                }
                foreach (var idx in names.Keys.OrderBy(x => x))
                {
                    var value = (string)folder.GetDetailsOf(folderItem, idx);
                    if (!string.IsNullOrEmpty(value))
                        properties[names[idx]] = value;
                }
            }
            var frm = new PropertiesForm(properties);
            frm.ShowDialog();
        }

        private void contextTreeMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mainTree.SelectedNode == null)
            {
                e.Cancel = true;
                return;
            }
        }

        private void treeProperties_Click(object sender, EventArgs e)
        {
            if (mainTree.SelectedNode == null) return;
            var item = (TreeNodeFile)mainTree.SelectedNode;
            var dir = Path.GetDirectoryName(item.DirectoryName);
            var file = Path.GetFileName(item.DirectoryName);
            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = item.DirectoryName;
                var properties = new NameValueCollection();
                var allDrives = DriveInfo.GetDrives();
                double divider = 1024 * 1024 * 1024;
                foreach (var drive in allDrives)
                {
                    if (drive.Name == dir)
                    {
                        properties["Имя"] = drive.Name;
                        properties["Тип диска"] = drive.DriveType.ToString();
                        if (drive.IsReady == true)
                        {
                            properties["Метка тома"] = drive.VolumeLabel;
                            properties["Файловая система"] = drive.DriveFormat;
                            properties["Доступно пользователю"] = (drive.AvailableFreeSpace / divider).ToString("0.00") + " ГБ";
                            properties["Всего доступно на диске"] = (drive.TotalFreeSpace / divider).ToString("0.00") + " ГБ";
                            properties["Общий размер диска"] = (drive.TotalSize / divider).ToString("0.00") + " ГБ";
                        }
                        break;
                    }
                }
                var frm = new PropertiesForm(properties);
                frm.ShowDialog();
            }
            else
                ShowFolderFileProperties(dir, file);
        }

        private void mainTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                mainTree.SelectedNode = mainTree.GetNodeAt(e.Location);
        }
    }
}
