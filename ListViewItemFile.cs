using System;

namespace FileExplorer
{
    public class ListViewItemFile
    {
        public string Text { get; set; }
        public string FileName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Length { get; set; }
        public bool IsFolder { get; set; }
        public int ImageIndex { get; set; }
    }
}
