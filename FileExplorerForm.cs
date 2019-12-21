using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Diagnostics;

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
            FillList();
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
        }

        /// <summary>
        /// Виртуальный выбор списка изменён
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainListView_VirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs e)
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
            FillDataFiles();
        }

        /// <summary>
        /// Заполнение дерева каталогов
        /// </summary>
        private void FillDataFiles()
        {
            var logicalDrives = FileHelper.GetLogicalDrives();
            var method = new MethodInvoker(() =>
            {
                if (mainTree.Nodes.Count > 0) return;
                mainTree.Nodes.Clear();
                foreach (var drive in logicalDrives)
                {
                    var driveNode = new TreeNodeFile(drive) { DirectoryName = drive };
                    mainTree.Nodes.Add(driveNode);
                    var count = FileHelper.GetDirectoriesCount(drive);
                    var collections = FileHelper.GetDirectoriesCollection(drive, 0, count);
                    var mess = FileHelper.GetErrorMessage();
                    if (collections.Length > 0)
                        driveNode.Nodes.Add(new TreeNodeFile()); // add stub
                    else
                        driveNode.Text += $"{mess}";
                    if (collections.Length > 0)
                        driveNode.ImageIndex = driveNode.SelectedImageIndex = 1;
                }
                mainListView.ResizeColumns(0);
            });
            if (InvokeRequired) BeginInvoke(method);  else  method();
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
            try
            {
                Cursor = Cursors.WaitCursor;
                NodeBeforeExpand(node);
            }
            finally
            {
                mainTree.EndUpdate();
                Cursor = Cursors.Default;
            }
        }

        private void NodeBeforeExpand(TreeNodeFile node)
        {
            mainTree.BeginUpdate();
            node.Nodes.Clear();
            var count = FileHelper.GetDirectoriesCount(node.DirectoryName);
            var collection = FileHelper.GetDirectoriesCollection(node.DirectoryName, 0, count);
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
                count = FileHelper.GetDirectoriesCount(dir.FullName);
                var collections = FileHelper.GetDirectoriesCollection(dir.FullName, 0, count);
                var mess = FileHelper.GetErrorMessage();
                if (collections.Length > 0)
                    dirNode.Nodes.Add(new TreeNodeFile()); // add stub
                else
                    dirNode.Text += $"{mess}";
                if (!dir.IsEmpty)
                    dirNode.ImageIndex = dirNode.SelectedImageIndex = 1;
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
        /// После выбора узла в дереве каталогов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node as TreeNodeFile;
            if (node == null) return;
            tsslPath.Text = node.FullPath;
            FillList();
            try
            {
                fileSystemWatcher1.Path = node.FullPath;
            }
            catch (Exception ex)
            {
                ShowStatus(ex.Message);
            }
        }

        /// <summary>
        /// Заполнение виртуального списка файлов
        /// </summary>
        private void FillList()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                tsbOpen.Visible = false;
                mainListView.BeginUpdate();
                mainListView.VirtualListSize = 0;
                files.Clear();
                var node = (TreeNodeFile)mainTree.SelectedNode;
                if (node == null) return;
                // загрузка имён папок
                var count = FileHelper.GetDirectoriesCount(node.DirectoryName);
                var list = new List<EntryInfo>();
                var collections = FileHelper.GetDirectoriesCollection(node.FullPath, 0, count);
                list.AddRange(collections);
                #region сегментация отключена
                //var offset = 0;
                //while (count > 0)
                //{
                //    list.AddRange(FileHelper.GetDirectoriesCollection(node.DirectoryName, offset, count > 100 ? 100 : count));
                //    if (count > 100)
                //    {
                //        count -= 100;
                //        offset += 100;
                //    }
                //    else
                //        break;
                //}
                #endregion
                var folders = list.ToArray();
                foreach (var dir in folders)
                {
                    var fileName = node.DirectoryName.Length < dir.FullName.Length
                         ? dir.FullName.Substring(node.DirectoryName.Length)
                         : dir.FullName;
                    if (fileName.StartsWith("$")) continue;
                    var lvi = new ListViewItemFile()
                    {
                        Text = Path.GetFileName(dir.FullName),
                        ImageIndex = dir.IsEmpty ? 0 : 1,
                        FileName = dir.FullName,
                        CreationTime = dir.CreationTime,
                        LastAccessTime = dir.LastAccessTime,
                        LastWriteTime = dir.LastWriteTime,
                        IsFolder = true
                    };
                    files.Add(lvi);
                    mainListView.VirtualListSize = files.Count;
                }
                // загрузка имён файлов
                count = FileHelper.GetFilesCount(node.DirectoryName);
                list = new List<EntryInfo>();
                list.AddRange(FileHelper.GetFilesCollection(node.DirectoryName, 0, count));
                #region  сегментация отключена
                //offset = 0;
                //while (count > 0)
                //{
                //    list.AddRange(FileHelper.GetFilesCollection(node.DirectoryName, offset, count > 100 ? 100 : count));
                //    if (count > 100)
                //    {
                //        count -= 100;
                //        offset += 100;
                //    }
                //    else
                //        break;
                //}
                #endregion
                var dirfiles = list.ToArray();
                foreach (var file in dirfiles)
                {
                    var fileName = node.DirectoryName.Length < file.FullName.Length 
                         ? file.FullName.Substring(node.DirectoryName.Length)
                         : file.FullName;
                    if (fileName.StartsWith("$")) continue;
                    var index = 2; // иконка по умолчанию
                    
                    // получение иконки для exe файла
                    var hash = Path.GetExtension(file.FullName);  //icon.GetHashCode();
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
                        Text = Path.GetFileName(file.FullName),
                        ImageIndex = index,
                        FileName = file.FullName,
                        CreationTime = file.CreationTime,
                        LastAccessTime = file.LastAccessTime,
                        LastWriteTime = file.LastWriteTime,
                        Length = file.Length,
                        IsFolder = false
                    };
                    files.Add(lvi);
                    mainListView.VirtualListSize = files.Count;
                }
                files.Sort(FileComparer);
                ShowStatus($"Показано каталогов: {folders.Length} и файлов: {dirfiles.Length}");
                mainListView.ResizeColumns(0);
            }
            finally
            {
                mainListView.EndUpdate();
                Cursor = Cursors.Default;
            }
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
            try
            {
                Cursor = Cursors.WaitCursor;
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
            finally
            {
                Cursor = Cursors.Default;
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
            FillList();
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
                            ShowStatus($"Не удалось удалить файл {item.FileName}: {ex.Message}");
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
                FillList();
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

        private void tsmiPaste_Click(object sender, EventArgs e)
        {

        }

        private void tsbTableView_Click(object sender, EventArgs e)
        {
            mainListView.View = View.Details;
            tsbListView.Checked = false;
        }

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
            FillList();
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            if (internalop)
            {
                internalop = false;
                return;
            }
            FillList();
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
            cmiRename.Visible = cmiOpen.Visible = toolStripMenuItem4.Visible = mainListView.SelectedIndices.Count == 1;
        }
    }
}
