using System;
using System.IO;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace GhostNutters.LanguageGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select Language-Version: 1) Normal, 2) Beta Edition");
            char version = char.Parse(Console.ReadLine());

            string _folderPath = "";
            string _localVersion = "";
            string _betaVersion = "";
            string _serverVersion = "";

            switch (version)
            {
                case '1':
                    Console.WriteLine("Checking for version...");

                    _folderPath = File.ReadAllText($"./path.txt");

                    if (_folderPath != string.Empty)
                    {
                        using var wc = new WebClient();

                        var fileContent = File.ReadAllText($"./localVersion.json");
                        var c = JsonConvert.DeserializeObject<VersionModel>(fileContent);

                        if (c != null)
                        {
                            _localVersion = c.Version;
                        }

                        var ver = wc.DownloadString("https://enkdev.xyz/cdn/mods/ghc/ghostnutters/languageFile.json");
                        var content = JsonConvert.DeserializeObject<VersionModel>(ver);

                        if (content != null)
                        {
                            _serverVersion = content.Version;
                        }

                        if (_localVersion != _serverVersion)
                        {
                            Console.Clear();
                            Console.WriteLine("Grabbing new Language-File...");

                            var serverFile =
                                wc.DownloadString("https://enkdev.xyz/cdn/mods/ghc/ghostnutters/GhostNutters.json");
                            using var fw = File.CreateText($"./File/GhostNutters.json");
                            fw.Write(serverFile);
                            fw.Dispose();

                            Console.Clear();
                            Console.WriteLine("Updating local version...");

                            using var fw2 = File.CreateText("./localVersion.json");
                            fw2.Write(ver);
                            fw2.Dispose();

                            Console.Clear();
                            Console.WriteLine("Moving Language-File to GHC's language directory...");

                            if (File.Exists($"{_folderPath}/GhostNutters.json"))
                            {
                                File.Delete($"{_folderPath}/GhostNutters.json");
                            }

                            File.Move($"./File/GhostNutters.json", $"{_folderPath}/GhostNutters.json");
                            File.Delete($"./File/GhostNutters.json");


                            Console.Clear();
                            Console.WriteLine("Done");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Language-File is already up-to-date.");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(
                            "Your path to Ghost Hunters Corps language directory is not set. Please save it inside path.txt before running this app.");
                    }

                    break;
                case '2':
                    Console.WriteLine("Checking for version...");

                    _folderPath = File.ReadAllText($"./path.txt");

                    if (_folderPath != string.Empty)
                    {
                        using var wc = new WebClient();

                        var fileContent = File.ReadAllText($"./localVersion.json");
                        var c = JsonConvert.DeserializeObject<VersionModel>(fileContent);

                        if (c != null)
                        {
                            _betaVersion = c.BetaVersion;
                        }

                        var ver = wc.DownloadString("https://enkdev.xyz/cdn/mods/ghc/ghostnutters/languageFile.json");
                        var content = JsonConvert.DeserializeObject<VersionModel>(ver);

                        if (content != null)
                        {
                            _serverVersion = content.BetaVersion;
                        }

                        if (_betaVersion != _serverVersion)
                        {
                            Console.Clear();
                            Console.WriteLine("Grabbing new Language-File...");

                            var serverFile =
                                wc.DownloadString("https://enkdev.xyz/cdn/mods/ghc/ghostnutters/GhostNuttersBetaEdition.json");
                            using var fw = File.CreateText($"./File/GhostNuttersBetaEdition.json");
                            fw.Write(serverFile);
                            fw.Dispose();

                            Console.Clear();
                            Console.WriteLine("Updating local version...");

                            using var fw2 = File.CreateText("./localVersion.json");
                            fw2.Write(ver);
                            fw2.Dispose();

                            Console.Clear();
                            Console.WriteLine("Moving Language-File to GHC's language directory...");

                            if (File.Exists($"{_folderPath}/GhostNuttersBetaEdition.json"))
                            {
                                File.Delete($"{_folderPath}/GhostNuttersBetaEdition.json");
                            }

                            File.Move($"./File/GhostNuttersBetaEdition.json", $"{_folderPath}/GhostNuttersBetaEdition.json");
                            File.Delete($"./File/GhostNuttersBetaEdition.json");


                            Console.Clear();
                            Console.WriteLine("Done");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Language-File is already up-to-date.");
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(
                            "Your path to Ghost Hunters Corps language directory is not set. Please save it inside path.txt before running this app.");
                    }

                    break;
            }
            

            Console.ReadKey();
        }
    }
}