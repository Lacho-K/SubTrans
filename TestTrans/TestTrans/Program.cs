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
        public static Dictionary<string, string> languagePairs;
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
            languagePairs = new Dictionary<string, string>
            {
                {"af", "Afrikaans"},
                {"ar", "Arabic"},
                {"az", "Azerbaijani"},
                {"be", "Belarusian"},
                {"bg", "Bulgarian"},
                {"bn", "Bengali"},
                {"bs", "Bosnian"},
                {"ca", "Catalan"},
                {"ceb", "Cebuano"},
                {"cs", "Czech"},
                {"cy", "Welsh"},
                {"da", "Danish"},
                {"de", "German"},
                {"el", "Greek"},
                {"en", "English"},
                {"eo", "Esperanto"},
                {"es", "Spanish"},
                {"et", "Estonian"},
                {"eu", "Basque"},
                {"fa", "Persian"},
                {"fi", "Finnish"},
                {"fr", "French"},
                {"ga", "Irish"},
                {"gl", "Galician"},
                {"gu", "Gujarati"},
                {"ha", "Hausa"},
                {"hi", "Hindi"},
                {"hmn", "Hmong"},
                {"hr", "Croatian"},
                {"ht", "Haitian Creole"},
                {"hu", "Hungarian"},
                {"hy", "Armenian"},
                {"id", "Indonesian"},
                {"ig", "Igbo"},
                {"is", "Icelandic"},
                {"it", "Italian"},
                {"iw", "Hebrew"},
                {"ja", "Japanese"},
                {"jw", "Javanese"},
                {"ka", "Georgian"},
                {"kk", "Kazakh"},
                {"km", "Khmer"},
                {"kn", "Kannada"},
                {"ko", "Korean"},
                {"la", "Latin"},
                {"lo", "Lao"},
                {"lt", "Lithuanian"},
                {"lv", "Latvian"},
                {"ma", "Punjabi"},
                {"mg", "Malagasy"},
                {"mi", "Maori"},
                {"mk", "Macedonian"},
                {"ml", "Malayalam"},
                {"mn", "Mongolian"},
                {"mr", "Marathi"},
                {"ms", "Malay"},
                {"mt", "Maltese"},
                {"my", "Myanmar (Burmese)"},
                {"ne", "Nepali"},
                {"nl", "Dutch"},
                {"no", "Norwegian"},
                {"ny", "Chichewa"},
                {"pl", "Polish"},
                {"pt", "Portuguese"},
                {"ro", "Romanian"},
                {"ru", "Russian"},
                {"si", "Sinhala"},
                {"sk", "Slovak"},
                {"sl", "Slovenian"},
                {"so", "Somali"},
                {"sq", "Albanian"},
                {"sr", "Serbian"},
                {"st", "Sesotho"},
                {"su", "Sudanese"},
                {"sv", "Swedish"},
                {"sw", "Swahili"},
                {"ta", "Tamil"},
                {"te", "Telugu"},
                {"tg", "Tajik"},
                {"th", "Thai"},
                {"tl", "Filipino"},
                {"tr", "Turkish"},
                {"uk", "Ukrainian"},
                {"ur", "Urdu"},
                {"uz", "Uzbek"},
                {"vi", "Vietnamese"},
                {"yi", "Yiddish"},
                {"yo", "Yoruba"},
                {"zh-CN", "Chinese Simplified"},
                {"zh-TW", "Chinese Traditional"},
                {"zu", "Zulu"}               
            };
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Print supported languages? Y/N");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" Default: \"N\".");

                string answer = Console.ReadLine().ToLower();
                if (string.IsNullOrWhiteSpace(answer))
                {
                    answer = "n";
                }
                else if (answer.Length == 1)
                {
                    if (answer == "y")
                    {

                        Console.WriteLine("Supported languages are:");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        foreach (KeyValuePair<string, string> langPair in languagePairs)
                        {
                            Console.WriteLine($"{langPair.Key} - {langPair.Value}");
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input!");
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please type the language code we're translating from (source language), or if you don't know the code just type in the name of the language itself. ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("DEFAULT: \"en\".");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Input: ");

                string fromLang = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fromLang))
                {
                    fromLang = "en";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Selected language: " + "English");
                }
                else
                {
                    if (!CheckLang(fromLang))
                    {
                        continue;
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please type the language code we're translating to (desired language), or if you don't know the code just type in the name of the language itself. ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Default: \"bg\".");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Input: ");

                string toLang = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(toLang))
                {
                    toLang = "bg";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Selected language: " + "Bulgarian");
                }
                else
                {
                    if (!CheckLang(toLang))
                    {
                        continue;
                    }
                }
                
                Console.ForegroundColor = ConsoleColor.White;
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

                string directory = string.Empty;
                try
                {
                   directory = new FileInfo(Subs4Trans).DirectoryName;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid path!");
                    continue;
                }
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

                    double currentIterationNum = 1;
                    double targetIterations = SubGroups.Count;

                    foreach (string SubChunk in SubGroups)
                    {
                        TranslatedText += t.translate(fromLang, toLang, SubChunk);
                        Console.WriteLine($"Translated: {(currentIterationNum/targetIterations)*100:F0}%");
                        currentIterationNum++;
                    }

                    TranslateSubs(toLang.ToUpper());

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Huh it actually worked! Check your translated subs in: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(directory);
                    Console.WriteLine();
                }
            }
        }

        private static bool CheckLang(string inputLang)
        {
            if (!languagePairs.ContainsKey(inputLang))
            {
                string language = languagePairs.FirstOrDefault(l => l.Value.ToLower() == inputLang.ToLower()).Key;

                if (language != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Selected language: " + languagePairs[language]);
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid language!");
                    return false;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Selected language: " + languagePairs[inputLang]);
                return true;
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

                    while (line != null)
                    {
                        if (subNum.ToString().Equals(line))
                        {
                            Text4Trans += " . ~~ - ";
                            subNum++;
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
        public static void TranslateSubs(string transLangCode)
        {
            using (StreamReader reader = new StreamReader(Subs4Trans))
            {
                String line = reader.ReadLine();
                int subNum = InitialNum;
                int index = 0;
                bool isPrinted = false;
                string[] orderedBGSubs = TranslatedText.Split('~').Select(w => w.Trim()).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();                
                using (StreamWriter writer = new StreamWriter(NameOfSubs + $"-{transLangCode}.srt"))
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
                                            filteredSub = filteredSub.Trim();
                                            line = filteredSub;
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
                            Console.WriteLine($"Done {subNum}");
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
    }
}