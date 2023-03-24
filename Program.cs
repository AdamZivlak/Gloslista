namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            WriteTheHelp();
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                    // FIXME: Bryter inte loop
                }
                else if (command == "load") // FIXME: Genererar ingen information, (det gör den visst!)
                {
                    LoadTheDictionary(defaultFile, argument);
                }
                else if (command == "list")
                {
                    ListTheLoadedDictionary();
                }
                else if (command == "new")
                {
                    AddNewWord(argument);
                }
                else if (command == "delete")
                {
                    DeleteTheWordInDictionary(argument);
                }
                else if (command == "translate")
                {
                    if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string TranslateWord = Console.ReadLine();
                        TranslateTheWord(TranslateWord);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static void DeleteTheWordInDictionary(string[] argument)
        {
            if (argument.Length == 3)
            {
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                        index = i;
                }
                dictionary.RemoveAt(index);
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                string SwedishDelete = Console.ReadLine();
                Console.Write("Write word in English: ");
                string EnglishDelet = Console.ReadLine();
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == SwedishDelete && gloss.word_eng == EnglishDelet)
                        index = i;
                }
                dictionary.RemoveAt(index);
            }
        }

        private static void LoadTheDictionary(string defaultFile, string[] argument)
        {
            if (argument.Length == 1)
            {
                using (StreamReader sr = new StreamReader(defaultFile))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            }
        }

        private static void ListTheLoadedDictionary()
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
            }
        }

        private static void AddNewWord(string[] argument)
        {
            if (argument.Length == 3)
            {
                dictionary.Add(new SweEngGloss(argument[1], argument[2]));
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word in Swedish: ");
                string SwedishNew = Console.ReadLine();
                Console.Write("Write word in English: ");
                string EnglishNew = Console.ReadLine();
                dictionary.Add(new SweEngGloss(SwedishNew, EnglishNew));
            }
        }

        private static void TranslateTheWord(string TranslateWord)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == TranslateWord)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == TranslateWord)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }

        private static void WriteTheHelp()
        {
            Console.WriteLine("Welcome to the dictionary app!");
            Console.WriteLine();
            Console.WriteLine("quit  -  quit the dictionary app");
            Console.WriteLine("load  -  load the dictionary");
            Console.WriteLine("list  -  list the dictionary");
            Console.WriteLine("new  -  add a new word to the dictionary");
            Console.WriteLine("delete  -  delete a word in the dictionary");
            Console.WriteLine("translate  -  translate a word");
        }
    }
}