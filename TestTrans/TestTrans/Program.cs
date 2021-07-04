using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestTrans
{
    class Program
    {
        public static String Text4Trans;
        public static String TranslatedText;
        public static String Subs4Trans;
        public static String NameOfSubs;
        public static int InitialNum;
        static void Main(string[] args)
        {
            Translator t = new Translator();
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('>', Console.WindowWidth));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(new string(' ', Console.WindowWidth / 2) + "Welcome to TransSubs!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('<', Console.WindowWidth));
            Console.WriteLine();
            Console.Write("This is a tool for translating subtitles in the format: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(".srt ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Instructions:");
            Console.Write("Step 1: Put the ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(".srt ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("file in the same directory as the");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" .exe ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("of the program.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" (Optional)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Step 2: Type in the console the ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("name ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("of the desired ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(".srt ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("file. Or if the ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(".srt ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("file is not in the same directory as the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(".exe ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("of the program, type in the whole directory in which your ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(".srt ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" file is in.");
            Console.Write("Step 3: Your translated file will be in the same directory as the");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" .exe ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("of the program.");
            Console.WriteLine();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please type the language we're translating from (source language).");
                Console.Write("Input: ");
                string fromLang = Console.ReadLine();
                Console.WriteLine("Please type the language we're translating to (desired language).");
                Console.Write("Input: ");
                string toLang = Console.ReadLine();
                Console.Write("Please type in the name of your ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(".srt ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("file or the full path of the directory it's in, or type");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" exit ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("to stop the program.");
                Console.Write("Input: ");
                Subs4Trans = Console.ReadLine();
                Text4Trans = String.Empty;
                TranslatedText = String.Empty;
                NameOfSubs = String.Empty;
                string directory = new FileInfo(Subs4Trans).FullName;
                if (Subs4Trans == "exit")
                {
                    break;
                }
                else if (!Subs4Trans.Contains(".srt"))
                {
                    Subs4Trans += ".srt";
                }
                // if the input of the user is a directory extract the name of the .srt file
                if (Subs4Trans.Contains('\\'))
                {
                    string[] subInfo = Subs4Trans.Split('\\');
                    NameOfSubs = subInfo[subInfo.Length - 1];
                }
                else
                {
                    NameOfSubs = Subs4Trans;
                }
                if (ReadSubs())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("Just a moment...");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;

                    List<string> SubGroups = (from Match m in Regex.Matches(Text4Trans, @".{1,3450}")
                                              select m.Value).ToList();

                    foreach (string SubChunk in SubGroups)
                    {
                        TranslatedText += t.translate(fromLang, toLang, SubChunk);
                    }
                    TranslateSubs();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Huh it actually worked! Check your translated subs in: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(directory);
                    Console.WriteLine();
                }
            }
        }
        // Read the .srt file and put all of its text in a variable "Text4Trans"
        public static bool ReadSubs()
        {
            if (File.Exists(Subs4Trans))
            {
                using (StreamReader reader = new StreamReader(Subs4Trans))
                {
                    String line = reader.ReadLine();
                    int subNum = 0;
                    bool isStartNUm = int.TryParse(line, out InitialNum);
                    while (!isStartNUm)
                    {
                        line = reader.ReadLine();
                        isStartNUm = int.TryParse(line, out InitialNum);
                        if (line != string.Empty && !isStartNUm)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("File is Invalid!");
                            return false;
                        }
                    }
                    subNum = InitialNum;
                    if (line.Contains('"'))
                    {
                        line = RemoveUnwantedSign(line);
                    }
                    while (line != null)
                    {
                        if (subNum.ToString().Equals(line))
                        {
                            Text4Trans += " . ~~ - ";
                            subNum++;
                        }
                        else if (line.Contains('"'))
                        {
                            line = RemoveUnwantedSign(line);
                        }

                        if (line.Contains("<i>") || line.Contains("</i>"))
                        {
                            RemoveForeignSigns(ref line);
                        }

                        if (line != string.Empty)
                        {
                            if (!Char.IsDigit(line[0]))
                            {
                                Text4Trans += line;
                            }
                        }

                        line = reader.ReadLine();
                    }
                }
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File not found!");
                Console.ResetColor();
                return false;
            }
        }

        // Make a new .srt file with the name of the original .srt file + "-BG" and replace the original language text with the translated one.
        public static void TranslateSubs()
        {
            using (StreamReader reader = new StreamReader(Subs4Trans))
            {
                String line = reader.ReadLine();
                int subNum = InitialNum;
                int index = 0;
                bool isPrinted = false;
                string[] orderedBGSubs = TranslatedText.Split('~').Select(w => w.Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
                using (StreamWriter writer = new StreamWriter(NameOfSubs + "-BG" + ".srt"))
                {
                    while (line != null)
                    {
                        if (line != string.Empty)
                        {
                            if (!Char.IsDigit(line[0]))
                            {
                                if (!isPrinted)
                                {
                                    if (orderedBGSubs[index].Contains('-'))
                                    {
                                        // removes dash at begining of the string, also removes dot at end of the string, fixes formating and puts that string in the .srt file  
                                        if (index < orderedBGSubs.Length)
                                        {
                                            string filteredSub = orderedBGSubs[index].Substring(orderedBGSubs[index].IndexOf('-') + 1);
                                            filteredSub = filteredSub.Remove(filteredSub.Length - 1);
                                            line = FixFormating(filteredSub);
                                        }
                                    }
                                    isPrinted = true;
                                }
                                else
                                {
                                    line = reader.ReadLine();
                                    continue;
                                }
                            }
                        }
                        if ((subNum).ToString().Equals(line))
                        {
                            Console.WriteLine($"Done {index + 1}");
                            subNum++;
                            isPrinted = false;
                            index++;
                        }
                        writer.WriteLine(line);
                        line = reader.ReadLine();
                    }
                }
            }
            Console.WriteLine();
        }

        public static string FixFormating(string line)
        {
            if (line != "")
            {
                if (line[0] == ' ')
                    line = line.Remove(0, 1);

                for (int i = 0; i < line.Length - 1; i++)
                {
                    if (line[i] == '.' || line[i] == '?' || line[i] == '!')
                    {
                        if (line[i + 1] != ' ' && line[i + 1] != '.' && line[i + 1] != '?' && line[i + 1] != '!')
                        {
                            line = line.Insert(i + 1, " ");
                        }
                    }
                }
            }
            return line;
        }  

        // Removes " sign because it prevets the "BGText" from being formated properly
        public static String RemoveUnwantedSign(string line)
        {
            List<char> temp = line.ToList();
            temp.RemoveAll(x => x == '"');
            String result = String.Empty;
            for (int i = 0; i < temp.Count; i++)
            {
                result += temp[i].ToString();
            }
            return result;
        }
        public static void RemoveForeignSigns(ref string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '<')
                {
                    if (line[i + 1] == '/')
                    {
                        line = line.Remove(i, 4);
                    }
                    else
                    {
                        line = line.Remove(i, 3);
                    }
                }
            }
        }
    }
}

