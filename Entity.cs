using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merger
{
    public class Entity
    {
        public string EntityType { get; private set; }
        public string Signature { get; private set; }
        public List<string> Code { get; private set; }

        public Entity(string signature)
        {
            Code = new List<string>() { signature };
            Signature = signature;
        }

        public void Read(StreamReader sr)
        {
            string line;
            var braceCounter = -1;
            while (braceCounter != 0)
            {
                line = sr.ReadLine();
                braceCounter += line.Count('}') - line.Count('{');
                Code.Add(line);
            }
        }
    }
}
