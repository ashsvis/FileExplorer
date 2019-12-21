using System;

namespace FileExplorer
{
    public struct EntryInfo
    {
        public string FullName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Length { get; set; }
        public bool IsEmpty { get; set; }
    }
}
