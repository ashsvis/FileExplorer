using System;
using System.Windows.Forms;

namespace FileExplorer
{
    public class TreeNodeFile : TreeNode
    {
        public TreeNodeFile() : base() { }
        public TreeNodeFile(string name) : base(name) {}
        public string DirectoryName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
    }

    public class TreeNodeStub : TreeNode { }
}
