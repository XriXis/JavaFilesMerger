using System.Text;

namespace Merger
{
    class Programm
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Absolute path:");
            var path = Console.ReadLine();
            Console.WriteLine("Main class name (optional):");
            var mainClass = Console.ReadLine();
            Console.WriteLine("New file name:");
            var name = Console.ReadLine();
            var merger = new Merger(new DirectoryInfo(path), mainClass);
            merger.Read();
            var file = merger.Merge(name);
            file.Write();
        }
    }




    static class StringExtention
    {
        public static int Count(this string line, char charToCount)
        {
            if (line == null) return 0;
            var cnt = 0;
            foreach (var e in line)
            {
                if (e == charToCount) cnt++;
            }
            return cnt;
        }
        public static string Multyply(this string line, int times)
        {
            var sb = new StringBuilder("");
            for (var i = 0; i < times; i++)
            {
                sb.Append(line);
            }
            return sb.ToString();
        }
    }
}