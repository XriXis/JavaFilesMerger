using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merger
{
    public class SourseFile
    {
        public FileInfo File_ { get; }
        public HashSet<string> Imports { get; set; }
        public List<Entity> Entities { get; }

        protected static HashSet<string> EntityTypes = new HashSet<string>() { "class", "enum", "interface" };

        private static readonly string importView = "import";

        public SourseFile(FileInfo file)
        {
            File_ = file;
            Imports = new HashSet<string>();
            Entities = new List<Entity>();
        }

        public void Read()
        {
            using (var sr = new StreamReader(File_.OpenRead()))
            {
                string line = "";
                while (!isEntity(line))
                {
                    line = sr.ReadLine();
                    if (line == null) return;
                    if (line.Contains(importView))
                    {
                        Imports.Add(line);
                    }
                }
                while (!sr.EndOfStream)
                {
                    Entities.Add(new Entity(line));
                    Entities[Entities.Count - 1].Read(sr);
                    line = sr.ReadLine();
                    while (!isEntity(line) && !sr.EndOfStream)
                        line = sr.ReadLine();
                }
            }
        }

        public void Merge(SourseFile another, string mainEntityName)
        {
            foreach (var import in another.Imports)
            {
                this.Imports.Add(import);
            }
            foreach (var entity in another.Entities)
            {
                if (entity.Signature.Contains(mainEntityName))
                {
                    this.Entities.Insert(0, entity);
                }
                else
                {
                    this.Entities.Add(entity);
                }
            }
        }

        public void Write()
        {

            using (StreamWriter writter = new StreamWriter(File_.FullName))
            {
                foreach (string import in this.Imports)
                {
                    writter.WriteLine(import);
                }
                foreach (var entity in this.Entities)
                {
                    writter.Write("\n\n");
                    foreach (var line in entity.Code)
                    {
                        writter.WriteLine(line);
                    }
                }
            }
        }

        private static bool isEntity(string line)
        {
            if (line == null) return false;
            foreach (var entity in EntityTypes)
            {
                if (line.Contains(entity)) return true;
            }
            return false;
        }

        public override string ToString()
        {
            return File_.Name;
        }
    }
}
