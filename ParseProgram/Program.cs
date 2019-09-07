using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseProgram
{
    class File
    {
        protected string name;
        protected string extension;
        protected string size;
        protected string content;

        public virtual void Parse(string input)
        {
            string name1 = input.Substring(0, input.IndexOf('.'));
            name = name1.Substring(input.IndexOf(':') + 1);

            string extension1 = input.Substring(0, input.IndexOf('('));
            extension = extension1.Substring(extension1.Length - 3);

            string size1 = input.Substring(0, input.IndexOf(')'));
            size = size1.Substring(input.IndexOf('(') + 1);

            content = input.Substring(input.IndexOf(';') + 1);

            Console.WriteLine("{0}.{1}", name, extension);
            Console.WriteLine("Extension: {0}", extension);
            Console.WriteLine("Size: {0}", size);

        }
    }

    class Txt : File
    {
        public override void Parse(string input)
        {
            base.Parse(input);
            Console.WriteLine("Content: {0}", content);
            Console.WriteLine();
        }
    }

    class Image : File
    {
        public override void Parse(string input)
        {
            base.Parse(input);
            Console.WriteLine("Resolution: {0}", content);
            Console.WriteLine();
        }
    }

    class Movie : File
    {
        public override void Parse(string input)
        {
            base.Parse(input);
            string content1 = content.Substring(content.IndexOf(';') + 1);
            content = content.Substring(0, content.IndexOf(';'));
            Console.WriteLine("Resolution: {0}", content);
            Console.WriteLine("Length: {0}", content1);
            Console.WriteLine();
        }
    }

    class Program
    {
        public static string GetExtension(string input)
        {
            string extension1 = input.Substring(0, input.IndexOf('('));
            string extension = extension1.Substring(extension1.Length - 3);
            return extension;
        }

        static void Main()
        {
            string s = @"Text:file.txt(6B);Some string content
Image:img.bmp(19MB); 1920х1080
Text:data.txt(12B); Another string
Text:data1.txt(7B); Yet another string
Movie:logan.2017.mkv(19GB); 1920х1080; 2h12m
";
            string[] separator = { Environment.NewLine };

            string[] t = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in t)
            {
                switch (GetExtension(item))
                {
                    case "txt":
                        Txt txt = new Txt();
                        Console.WriteLine("Text files:");
                        txt.Parse(item);
                        break;
                    case "bmp":
                        Image img = new Image();
                        Console.WriteLine("Images:");
                        img.Parse(item);
                        break;
                    case "mkv":
                        Movie movie = new Movie();
                        Console.WriteLine("Movies:");
                        movie.Parse(item);
                        break;
                    default:
                        Console.WriteLine("any type");
                        break;
                }
            }
            Console.ReadLine();
        }
    }
}
