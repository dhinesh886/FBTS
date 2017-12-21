using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FBTS.Library.Common
{
    public static class DiskIo
    {
        /// <summary>
        ///     Recursively create directory
        /// </summary>
        /// <param name="dirInfo">Folder path to create.</param>
        public static void CreateDirectory(this DirectoryInfo dirInfo)
        {
            if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
            if (!dirInfo.Exists) dirInfo.Create();
        }

        /// <summary>
        ///     Move current instance and rename current instance when needed
        /// </summary>
        /// <param name="fileInfo">Current instance</param>
        /// <param name="destFileName">The Path to move current instance to, which can specify a different file name</param>
        /// <param name="renameWhenExists">Bool to specify if current instance should be renamed when exists</param>
        public static void MoveTo(this FileInfo fileInfo, string destFileName, bool renameWhenExists = false)
        {
            string newFullPath = string.Empty;

            if (renameWhenExists)
            {
                int count = 1;

                string fileNameOnly = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                string extension = Path.GetExtension(fileInfo.FullName);
                newFullPath = Path.Combine(destFileName, fileInfo.Name);

                while (File.Exists(newFullPath))
                {
                    string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                    newFullPath = Path.Combine(destFileName, tempFileName + extension);
                }
            }

            fileInfo.MoveTo(renameWhenExists ? newFullPath : destFileName);
        }

        /// <summary>
        ///     Kilobytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int KB(this int value)
        {
            return value*1024;
        }

        /// <summary>
        ///     Megabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int MB(this int value)
        {
            return value.KB()*1024;
        }

        /// <summary>
        ///     Gigabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GB(this int value)
        {
            return value.MB()*1024;
        }

        /// <summary>
        ///     Terabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long TB(this int value)
        {
            return value.GB()*(long) 1024;
        }

        public static long FolderSize(this DirectoryInfo dir, bool bIncludeSub)
        {
            long totalFolderSize = 0;

            if (!dir.Exists) return 0;

            IEnumerable<FileInfo> files = from f in dir.GetFiles()
                select f;
            foreach (FileInfo file in files) totalFolderSize += file.Length;

            if (bIncludeSub)
            {
                IEnumerable<DirectoryInfo> subDirs = from d in dir.GetDirectories()
                    select d;
                foreach (DirectoryInfo subDir in subDirs) totalFolderSize += FolderSize(subDir, true);
            }

            return totalFolderSize;
        }

        public static long FileSize(this string filePath)
        {
            long bytes = 0;

            var oFileInfo = new FileInfo(filePath);
            bytes = oFileInfo.Length;
            return bytes;
        }

        public static void CopyToFile(this StringBuilder sBuilder, string path)
        {
            File.WriteAllText(path, sBuilder.ToString());
        }

        /// <summary>
        ///     Delete files in a folder that are like the searchPattern, don't include subfolders.
        /// </summary>
        /// <param name="di"></param>
        /// <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        /// <returns>Number of files that have been deleted.</returns>
        public static int DeleteFiles(this DirectoryInfo di, string searchPattern)
        {
            return DeleteFiles(di, searchPattern, false);
        }

        /// <summary>
        ///     Delete files in a folder that are like the searchPattern
        /// </summary>
        /// <param name="di"></param>
        /// <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        /// <param name="includeSubdirs"></param>
        /// <returns>Number of files that have been deleted.</returns>
        /// <remarks>
        ///     This function relies on DirectoryInfo.GetFiles() which will first get all the FileInfo objects in memory. This is
        ///     good for folders with not too many files, otherwise
        ///     an implementation using Windows APIs can be more appropriate. I didn't need this functionality here but if you need
        ///     it just let me know.
        /// </remarks>
        public static int DeleteFiles(this DirectoryInfo di, string searchPattern, bool includeSubdirs)
        {
            int deleted = 0;
            foreach (
                FileInfo fi in
                    di.GetFiles(searchPattern,
                        includeSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                fi.Delete();
                deleted++;
            }

            return deleted;
        }

        public static List<string> ListFiles(this string folderPath)
        {
            if (!Directory.Exists(folderPath)) return null;
            return (from f in Directory.GetFiles(folderPath)
                select Path.GetFileName(f)).
                ToList();
        }
    }
}