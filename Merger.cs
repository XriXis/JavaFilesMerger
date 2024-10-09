using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merger
{
    public class Merger
    {
        public DirectoryInfo Folder { get; }
        public List<SourseFile> SourseFiles { get; }
        public string MainEntityName { get; }
        public Merger(DirectoryInfo folder, string firstEntityName)
        {
            Folder = folder;
            SourseFiles = new List<SourseFile>();
            MainEntityName = firstEntityName;
        }

        public void Read()
        {
            foreach (var item in Folder.EnumerateFiles())
            {
                SourseFiles.Add(new SourseFile(item));
                SourseFiles[SourseFiles.Count - 1].Read();
            }
        }

        public SourseFile Merge(string destinationName)
        {
            using (var fp = File.Create(Path.Combine(Folder.FullName, destinationName)))
            {
                var result = new SourseFile(new FileInfo(fp.Name));

                foreach (var item in SourseFiles)
                {
                    result.Merge(item, MainEntityName);
                }
                return result;
            }
        }
    }


}
