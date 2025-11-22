using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> GetData(string file)
    {
        List<string> textStructure = new List<string>();
        using (StreamReader rFile = new StreamReader(file))
        {
            string linesFromText = rFile.ReadLine();
            textStructure.Add(linesFromText);
        }
        return textStructure;
    }

    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("No file");
            return;
        }
        else
        {
            string source = args[0];
            Console.WriteLine(string.Join("\n", GetData(source)));
        }
    }
}

