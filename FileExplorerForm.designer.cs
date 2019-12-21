namespace FileExplorer
{
    partial class FileExplorerForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExplorerForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelListPlace = new System.Windows.Forms.Panel();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbArrange = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbCreateFolder = new System.Windows.Forms.ToolStripButton();
            this.tsbListView = new System.Windows.Forms.ToolStripButton();
            this.tsbTableView = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbBack = new System.Windows.Forms.ToolStripButton();
            this.tsbForward = new System.Windows.Forms.ToolStripButton();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbFindText = new System.Windows.Forms.ToolStripComboBox();
            this.tsbFind = new System.Windows.Forms.ToolStripButton();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.tsslPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.contextItemsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmiCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiRename = new System.Windows.Forms.ToolStripMenuItem();
            this.contextFolderMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmiProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.treeProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.contextItemsMenu.SuspendLayout();
            this.contextFolderMenu.SuspendLayout();
            this.contextTreeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 3);
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus,
            this.tsslDateTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1036, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(932, 17);
            this.tsslStatus.Spring = true;
            this.tsslStatus.Text = "Статус";
            this.tsslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslDateTime
            // 
            this.tsslDateTime.Name = "tsslDateTime";
            this.tsslDateTime.Size = new System.Drawing.Size(89, 17);
            this.tsslDateTime.Text = "Время сервера";
            // 
            // splitContainer1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.splitContainer1, 3);
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(4, 53);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mainTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1028, 488);
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.TabIndex = 0;
            // 
            // mainTree
            // 
            this.mainTree.ContextMenuStrip = this.contextTreeMenu;
            this.mainTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTree.FullRowSelect = true;
            this.mainTree.HideSelection = false;
            this.mainTree.ImageIndex = 0;
            this.mainTree.ImageList = this.imageList1;
            this.mainTree.Location = new System.Drawing.Point(0, 0);
            this.mainTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.mainTree.Name = "mainTree";
            this.mainTree.SelectedImageIndex = 0;
            this.mainTree.ShowNodeToolTips = true;
            this.mainTree.Size = new System.Drawing.Size(277, 488);
            this.mainTree.TabIndex = 0;
            this.mainTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.mainTree_BeforeExpand);
            this.mainTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.mainTree_AfterSelect);
            this.mainTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainTree_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Black;
            this.imageList1.Images.SetKeyName(0, "notemptyfolder.png");
            this.imageList1.Images.SetKeyName(1, "emptyfolder.png");
            this.imageList1.Images.SetKeyName(2, "commonfile.png");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panelListPlace);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelPreview);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(747, 488);
            this.splitContainer2.SplitterDistance = 202;
            this.splitContainer2.TabIndex = 1;
            // 
            // panelListPlace
            // 
            this.panelListPlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListPlace.Location = new System.Drawing.Point(0, 0);
            this.panelListPlace.Name = "panelListPlace";
            this.panelListPlace.Size = new System.Drawing.Size(747, 488);
            this.panelListPlace.TabIndex = 0;
            this.panelListPlace.Resize += new System.EventHandler(this.panelListPlace_Resize);
            // 
            // panelPreview
            // 
            this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview.Location = new System.Drawing.Point(0, 0);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(150, 46);
            this.panelPreview.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toolStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 3);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbArrange,
            this.tsbOpen,
            this.tsbCreateFolder,
            this.tsbListView,
            this.tsbTableView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1036, 25);
            this.toolStrip1.TabIndex = 1;
            // 
            // tsbArrange
            // 
            this.tsbArrange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbArrange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCut,
            this.tsmiCopy,
            this.tsmiPaste,
            this.toolStripMenuItem3,
            this.tsmiSelectAll,
            this.toolStripMenuItem2,
            this.tsmiRemove,
            this.tsmiRename,
            this.toolStripMenuItem1,
            this.tsmiExit});
            this.tsbArrange.Image = ((System.Drawing.Image)(resources.GetObject("tsbArrange.Image")));
            this.tsbArrange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrange.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsbArrange.Name = "tsbArrange";
            this.tsbArrange.Size = new System.Drawing.Size(92, 22);
            this.tsbArrange.Text = "Упорядочить";
            this.tsbArrange.DropDownOpening += new System.EventHandler(this.tsbArrange_DropDownOpening);
            // 
            // tsmiCut
            // 
            this.tsmiCut.Enabled = false;
            this.tsmiCut.Name = "tsmiCut";
            this.tsmiCut.Size = new System.Drawing.Size(180, 22);
            this.tsmiCut.Text = "Переместить";
            this.tsmiCut.Click += new System.EventHandler(this.tsmiCut_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Enabled = false;
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(180, 22);
            this.tsmiCopy.Text = "Копировать";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Enabled = false;
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(180, 22);
            this.tsmiPaste.Text = "Вставить";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiSelectAll
            // 
            this.tsmiSelectAll.Name = "tsmiSelectAll";
            this.tsmiSelectAll.Size = new System.Drawing.Size(180, 22);
            this.tsmiSelectAll.Text = "Выделить все";
            this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiRemove
            // 
            this.tsmiRemove.Enabled = false;
            this.tsmiRemove.Name = "tsmiRemove";
            this.tsmiRemove.Size = new System.Drawing.Size(180, 22);
            this.tsmiRemove.Text = "Удалить";
            this.tsmiRemove.Click += new System.EventHandler(this.tsmiRemove_Click);
            // 
            // tsmiRename
            // 
            this.tsmiRename.Enabled = false;
            this.tsmiRename.Name = "tsmiRename";
            this.tsmiRename.Size = new System.Drawing.Size(180, 22);
            this.tsmiRename.Text = "Переименовать";
            this.tsmiRename.Click += new System.EventHandler(this.tsmiRename_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(180, 22);
            this.tsmiExit.Text = "Закрыть";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(58, 22);
            this.tsbOpen.Text = "Открыть";
            this.tsbOpen.Visible = false;
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbCreateFolder
            // 
            this.tsbCreateFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCreateFolder.Image = ((System.Drawing.Image)(resources.GetObject("tsbCreateFolder.Image")));
            this.tsbCreateFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreateFolder.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsbCreateFolder.Name = "tsbCreateFolder";
            this.tsbCreateFolder.Size = new System.Drawing.Size(80, 22);
            this.tsbCreateFolder.Text = "Новая папка";
            this.tsbCreateFolder.ToolTipText = "Создание новой пустой папки.";
            this.tsbCreateFolder.Click += new System.EventHandler(this.tsbCreateFolder_Click);
            // 
            // tsbListView
            // 
            this.tsbListView.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbListView.CheckOnClick = true;
            this.tsbListView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbListView.Image = ((System.Drawing.Image)(resources.GetObject("tsbListView.Image")));
            this.tsbListView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbListView.Name = "tsbListView";
            this.tsbListView.Size = new System.Drawing.Size(23, 22);
            this.tsbListView.Text = "Просмотр списком";
            this.tsbListView.Click += new System.EventHandler(this.tsbListView_Click);
            // 
            // tsbTableView
            // 
            this.tsbTableView.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbTableView.Checked = true;
            this.tsbTableView.CheckOnClick = true;
            this.tsbTableView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbTableView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTableView.Image = ((System.Drawing.Image)(resources.GetObject("tsbTableView.Image")));
            this.tsbTableView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTableView.Name = "tsbTableView";
            this.tsbTableView.Size = new System.Drawing.Size(23, 22);
            this.tsbTableView.Text = "Проосмотр таблицей";
            this.tsbTableView.Click += new System.EventHandler(this.tsbTableView_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1036, 566);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBack,
            this.tsbForward});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(5, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbBack
            // 
            this.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBack.Enabled = false;
            this.tsbBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbBack.Image")));
            this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBack.Name = "tsbBack";
            this.tsbBack.Size = new System.Drawing.Size(23, 20);
            this.tsbBack.Text = "Назад";
            this.tsbBack.Visible = false;
            // 
            // tsbForward
            // 
            this.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbForward.Enabled = false;
            this.tsbForward.Image = ((System.Drawing.Image)(resources.GetObject("tsbForward.Image")));
            this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForward.Name = "tsbForward";
            this.tsbForward.Size = new System.Drawing.Size(23, 20);
            this.tsbForward.Text = "Вперёд";
            this.tsbForward.Visible = false;
            // 
            // toolStrip4
            // 
            this.toolStrip4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbFindText,
            this.tsbFind});
            this.toolStrip4.Location = new System.Drawing.Point(842, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(194, 25);
            this.toolStrip4.TabIndex = 4;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel1.Text = "Поиск:";
            // 
            // tscbFindText
            // 
            this.tscbFindText.Name = "tscbFindText";
            this.tscbFindText.Size = new System.Drawing.Size(121, 25);
            this.tscbFindText.Text = "*.*";
            // 
            // tsbFind
            // 
            this.tsbFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFind.Image = ((System.Drawing.Image)(resources.GetObject("tsbFind.Image")));
            this.tsbFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFind.Name = "tsbFind";
            this.tsbFind.Size = new System.Drawing.Size(23, 22);
            this.tsbFind.Text = "toolStripButton1";
            this.tsbFind.Click += new System.EventHandler(this.tsbFind_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.BackColor = System.Drawing.SystemColors.Window;
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslPath});
            this.statusStrip2.Location = new System.Drawing.Point(5, 0);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(837, 25);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 5;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // tsslPath
            // 
            this.tsslPath.BackColor = System.Drawing.SystemColors.Window;
            this.tsslPath.Name = "tsslPath";
            this.tsslPath.Size = new System.Drawing.Size(822, 20);
            this.tsslPath.Spring = true;
            this.tsslPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            // 
            // contextItemsMenu
            // 
            this.contextItemsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiOpen,
            this.toolStripMenuItem4,
            this.cmiCut,
            this.cmiCopy,
            this.toolStripMenuItem5,
            this.cmiDelete,
            this.cmiRename,
            this.toolStripMenuItem6,
            this.cmiProperties});
            this.contextItemsMenu.Name = "contextItemsMenu";
            this.contextItemsMenu.Size = new System.Drawing.Size(162, 154);
            this.contextItemsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextItemsMenu_Opening);
            // 
            // cmiOpen
            // 
            this.cmiOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cmiOpen.Name = "cmiOpen";
            this.cmiOpen.Size = new System.Drawing.Size(161, 22);
            this.cmiOpen.Text = "Открыть";
            this.cmiOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(158, 6);
            // 
            // cmiCut
            // 
            this.cmiCut.Name = "cmiCut";
            this.cmiCut.Size = new System.Drawing.Size(161, 22);
            this.cmiCut.Text = "Переместить";
            this.cmiCut.Click += new System.EventHandler(this.tsmiCut_Click);
            // 
            // cmiCopy
            // 
            this.cmiCopy.Name = "cmiCopy";
            this.cmiCopy.Size = new System.Drawing.Size(161, 22);
            this.cmiCopy.Text = "Копировать";
            this.cmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(158, 6);
            // 
            // cmiDelete
            // 
            this.cmiDelete.Name = "cmiDelete";
            this.cmiDelete.Size = new System.Drawing.Size(161, 22);
            this.cmiDelete.Text = "Удалить";
            this.cmiDelete.Click += new System.EventHandler(this.tsmiRemove_Click);
            // 
            // cmiRename
            // 
            this.cmiRename.Name = "cmiRename";
            this.cmiRename.Size = new System.Drawing.Size(161, 22);
            this.cmiRename.Text = "Переименовать";
            this.cmiRename.Click += new System.EventHandler(this.tsmiRename_Click);
            // 
            // contextFolderMenu
            // 
            this.contextFolderMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiPaste});
            this.contextFolderMenu.Name = "contextFolderMenu";
            this.contextFolderMenu.Size = new System.Drawing.Size(123, 26);
            this.contextFolderMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextFolderMenu_Opening);
            // 
            // cmiPaste
            // 
            this.cmiPaste.Name = "cmiPaste";
            this.cmiPaste.Size = new System.Drawing.Size(122, 22);
            this.cmiPaste.Text = "Вставить";
            this.cmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(158, 6);
            // 
            // cmiProperties
            // 
            this.cmiProperties.Name = "cmiProperties";
            this.cmiProperties.Size = new System.Drawing.Size(161, 22);
            this.cmiProperties.Text = "Свойства";
            this.cmiProperties.Click += new System.EventHandler(this.cmiProperties_Click);
            // 
            // contextTreeMenu
            // 
            this.contextTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.treeProperties});
            this.contextTreeMenu.Name = "contextTreeMenu";
            this.contextTreeMenu.Size = new System.Drawing.Size(126, 26);
            this.contextTreeMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextTreeMenu_Opening);
            // 
            // treeProperties
            // 
            this.treeProperties.Name = "treeProperties";
            this.treeProperties.Size = new System.Drawing.Size(180, 22);
            this.treeProperties.Text = "Свойства";
            this.treeProperties.Click += new System.EventHandler(this.treeProperties_Click);
            // 
            // FileExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FileExplorerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Файловый проводник";
            this.Load += new System.EventHandler(this.FileExplorerForm_Load);
            this.ResizeEnd += new System.EventHandler(this.FileExplorerForm_ResizeEnd);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.contextItemsMenu.ResumeLayout(false);
            this.contextFolderMenu.ResumeLayout(false);
            this.contextTreeMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView mainTree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panelListPlace;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel tsslDateTime;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbCreateFolder;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripDropDownButton tsbArrange;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbBack;
        private System.Windows.Forms.ToolStripButton tsbForward;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripComboBox tscbFindText;
        private System.Windows.Forms.ToolStripButton tsbFind;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel tsslPath;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRename;
        private System.Windows.Forms.ToolStripMenuItem tsmiCut;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripButton tsbListView;
        private System.Windows.Forms.ToolStripButton tsbTableView;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ContextMenuStrip contextItemsMenu;
        private System.Windows.Forms.ToolStripMenuItem cmiOpen;
        private System.Windows.Forms.ToolStripMenuItem cmiCopy;
        private System.Windows.Forms.ContextMenuStrip contextFolderMenu;
        private System.Windows.Forms.ToolStripMenuItem cmiPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem cmiCut;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem cmiDelete;
        private System.Windows.Forms.ToolStripMenuItem cmiRename;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem cmiProperties;
        private System.Windows.Forms.ContextMenuStrip contextTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem treeProperties;
    }
}

