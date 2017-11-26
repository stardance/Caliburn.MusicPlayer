using MusicPlayer.InfraStructure.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Core.Utils
{
    public class FileHelper
    {
        private static List<FileInfo> infos = new List<FileInfo>();
        public static List<Audio> GetAllAudioFiles(string directory)
        {
            
            if(!Directory.Exists(directory))
            {
                return null;
            }

            List<Audio> result = new List<Audio>();
            string[] audiosExtions = Enum.GetNames(typeof(Enums.AudioFile));
            foreach(var item in GetAllFiles(directory))
            {
                if(audiosExtions.Any(n => n.ToString() == item.Extension.Replace(".","")))
                {
                    result.Add(new Audio {AudioName = item.Name,FullPath = item.FullName });
                }
            }
            return new List<Audio>(result);
        }

        private static List<FileInfo> GetAllFiles(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
            FileInfo[] files = dirInfo.GetFiles();

            infos.AddRange(files);

            foreach(var item in dirInfos)
            {
                GetAllFiles(item.FullName);
            }
            return infos;
        }
    }
}
