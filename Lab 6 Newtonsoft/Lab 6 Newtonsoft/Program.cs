using Newtonsoft.Json;

namespace Lab_6_Newtonsoft;
class Program
{
    private static List<Book> books = new List<Book>();
    static void Main(string[] args)
    {
        string jsonString = string.Empty;
        var root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
        var dataFolder = root + Path.DirectorySeparatorChar + "Data";
        var fileNames = new List<string>();
        var serializeFolder = root + Path.DirectorySeparatorChar + "Serialized";
        var writeJSON = serializeFolder + Path.DirectorySeparatorChar + "books.JSON";


        foreach (var file in Directory.GetFiles(dataFolder))
        {
            fileNames.Add(file);
        }
        foreach (var book in ReadBookFile(fileNames))
        {
            Console.WriteLine(book);
        }

        WriteJsonToFile(writeJSON);
        Console.ReadKey();
    }

    static void WriteJsonToFile(string writeJSON)
    {
        string jsonString = JsonConvert.SerializeObject(books,Formatting.Indented);

        using (var sw = new StreamWriter(writeJSON,false))
        {
            sw.WriteLine(jsonString);
        }
    }

    static List<Book> ReadBookFile(List<string> fileNames)
    {
        foreach (var file in fileNames)
        {
            string jsonString = string.Empty;
            using (var sr = new StreamReader(file))
            {
                jsonString = sr.ReadToEnd();
            }

            var book = JsonConvert.DeserializeObject<Book>(jsonString);
            books.Add(book);
        }

        return books;
    }
}

