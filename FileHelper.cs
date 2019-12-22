using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FileExplorer
{
    public static class FileHelper
    {
        private static string _errorMessage = string.Empty;

        public static DateTime GetDate()
        {
            return DateTime.Now;
        }

        public static Task<EntryInfo[]> GetDirectoriesCollectionAsync(string directoryName, string searchPattern = "")
        {
            var task = new Task<EntryInfo[]>(() => GetDirectoriesCollection(directoryName, searchPattern));
            task.Start();
            return task;
        }

        public static EntryInfo[] GetDirectoriesCollection(string directoryName, string searchPattern = "")
        {
            var collection = new DirectoryInfo(directoryName);
            var directories = new List<EntryInfo>();
            try
            {
                var sampleCollection = string.IsNullOrWhiteSpace(searchPattern)
                    ? collection.EnumerateDirectories()
                    : collection.EnumerateDirectories(searchPattern, SearchOption.AllDirectories);
                foreach (var dir in sampleCollection)
                {
                    var subCollection = new DirectoryInfo(dir.FullName);
                    bool isEmpty = CheckEmpty(subCollection);
                    var entry = new EntryInfo
                    {
                        FullName = dir.FullName,
                        CreationTime = dir.CreationTime,
                        LastAccessTime = dir.LastAccessTime,
                        LastWriteTime = dir.LastWriteTime,
                        IsEmpty = isEmpty
                    };
                    directories.Add(entry);
                }
                _errorMessage = string.Empty;
                return directories.ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                _errorMessage = " (нет доступа)";
                return directories.ToArray();
            }
            catch (IOException)
            {
                _errorMessage = " (не готово)";
                return directories.ToArray();
            }
            catch (Exception e)
            {
                _errorMessage = $" ({e.Message})";
                return directories.ToArray();
            }
        }

        private static bool CheckEmpty(DirectoryInfo subSollection)
        {
            try
            {
                return subSollection.EnumerateDirectories().Count() +
                              subSollection.EnumerateFiles().Count() > 0;
            }
            catch
            {
                return true;
            }
        }

        public static string GetErrorMessage()
        {
            return _errorMessage;
        }

        public static Task<EntryInfo[]> GetFilesCollectionAsync(string directoryName, string searchPattern = "")
        {
            var task = new Task<EntryInfo[]>(() => GetFilesCollection(directoryName, searchPattern));
            task.Start();
            return task;
        }

        public static EntryInfo[] GetFilesCollection(string directoryName, string searchPattern = "")
        {
            var collection = new DirectoryInfo(directoryName);
            var directories = new List<EntryInfo>();
            try
            {
                var sampleCollection = string.IsNullOrWhiteSpace(searchPattern)
                    ? collection.EnumerateFiles()
                    : collection.EnumerateFiles(searchPattern, SearchOption.AllDirectories);
                foreach (var file in sampleCollection)
                {
                    var entry = new EntryInfo
                    {
                        FullName = file.FullName,
                        CreationTime = file.CreationTime,
                        LastAccessTime = file.LastAccessTime,
                        LastWriteTime = file.LastWriteTime,
                        Length = file.Length,
                        IsEmpty = file.Length == 0
                    };
                    directories.Add(entry);
                }
                _errorMessage = string.Empty;
                return directories.ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                _errorMessage = " (нет доступа)";
                return directories.ToArray();
            }
            catch (IOException)
            {
                _errorMessage = " (не готово)";
                return directories.ToArray();
            }
            catch (Exception e)
            {
                _errorMessage = $" ({e.Message})";
                return directories.ToArray();
            }
        }

        public static Task<string[]> GetLogicalDrivesAsync()
        {
            var task = new Task<string[]>(GetLogicalDrives);
            task.Start();
            return task;
        }

        public static string[] GetLogicalDrives()
        {
            return Environment.GetLogicalDrives();
        }

        public static string AddNewFolderIn(string directoryName)
        {
            var newFolder = Path.Combine(directoryName, "Новая папка");
            var n = 1;
            while (Directory.Exists(newFolder))
                newFolder = Path.Combine(directoryName, $"Новая папка ({++n})");
            Directory.CreateDirectory(newFolder);
            return newFolder;
        }

        public static void MoveFileToRecycleBin(string path)
        {
            const uint delete = 3;
            const ushort silentNoConfirmationUndo = 84;
            var fileOp = new SHFILEOPSTRUCT
            {
                FileFunc = delete,
                NamesFrom = path + "\0", // Строки NamesFrom, NameTo должны заканчиваться двумя '\0'. Один добавит p/invoke. В итоге получится два.
                Flags = silentNoConfirmationUndo
            };
            int hResult = SHFileOperation(ref fileOp);
            if (hResult != 0)
            {
                throw new Win32Exception(hResult);
            }
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT lpFileOp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public uint FileFunc;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string NamesFrom;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string NameTo;
            public ushort Flags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszProgressTitle;
        }

        public static void MoveFile(string sourceName, string destName)
        {
            File.Move(sourceName, destName);
        }

        public static void MoveFolder(string sourceName, string destName)
        {
            Directory.Move(sourceName, destName);
        }

        public static void CopyFile(string sourceName, string destName)
        {
            File.Copy(sourceName, destName);
        }

        public static void CopyFolder(string sourceName, string destName)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceName, destName);
        }

    }
}
