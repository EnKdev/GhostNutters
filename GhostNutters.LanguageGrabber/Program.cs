using System;
using System.IO;
using System.Net;
using Ionic.Zip;
using Newtonsoft.Json;

namespace GhostNutters.LanguageGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select Language-Version: 1) Normal, 2) Beta Edition");
            char version = char.Parse(Console.ReadLine());

            string[] _folderPaths = new string[] { };
            string _localVersion = "";
            string _betaVersion = "";
            string _serverVersion = "";

            switch (version)
            {
                case '1':
                    Console.WriteLine("Checking for version...");

                    _folderPaths = File.ReadAllLines($"./path.txt");

                    if (_folderPaths[0] != string.Empty && _folderPaths[1] != string.Empty)
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
                            Console.WriteLine("Grabbing new Language-Files...");

                            var tempZip = new ZipFile();
                            tempZip.Save($"./download/language.zip");

                            wc.DownloadFile("https://enkdev.xyz/cdn/mods/ghc/ghostnutters/packages/GhostNutters.zip",
                                $"./download/language.zip");

                            var zip = ZipFile.Read($"./download/language.zip");

                            foreach (var entry in zip)
                                entry.Extract($"./language", ExtractExistingFileAction.OverwriteSilently);

                            tempZip.Dispose();
                            zip.Dispose();
                            File.Delete($"./download/language.zip");

                            Console.Clear();
                            Console.WriteLine("Updating local version...");

                            using var fw = File.CreateText("./localVersion.json");
                            fw.Write(ver);
                            fw.Dispose();

                            Console.Clear();
                            Console.WriteLine("Moving Language-File to GHC's language directory...");

                            if (File.Exists($"{_folderPaths[0]}/GhostNutters.json"))
                            {
                                File.Delete($"{_folderPaths[0]}/GhostNutters.json");
                            }

                            File.Move($"./language/language/GhostNutters.json", $"{_folderPaths[0]}/GhostNutters.json");
                            File.Delete($"./language/language/GhostNutters.json");

                            Console.Clear();
                            Console.WriteLine("Moving Vocal-File to GHC's language directory...");

                            if (File.Exists($"{_folderPaths[1]}/GhostNutters.json"))
                            {
                                File.Delete($"{_folderPaths[1]}/GhostNutters.json");
                            }

                            File.Move($"./language/vocal/GhostNutters.json", $"{_folderPaths[1]}/GhostNutters.json");
                            File.Delete($"./language/vocal/GhostNutters.json");

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
                            "Your path to Ghost Hunters Corps language and vocal directory is not set. Please save it inside path.txt before running this app.\n" +
                            "If you need help: The first entry should be going to the language directory, the second to the vocal directory");
                    }

                    break;
                case '2':
                    Console.WriteLine("Checking for version...");

                    _folderPaths = File.ReadAllLines($"./path.txt");

                    if (_folderPaths[0] != string.Empty && _folderPaths[1] != string.Empty)
                    {
                        using var wc = new WebClient();

                        var fileContent = File.ReadAllText($"./localVersion.json");
                        var c = JsonConvert.DeserializeObject<VersionModel>(fileContent);

                        if (c != null)
                        {
                            _betaVersion = c.Version;
                        }

                        var ver = wc.DownloadString("https://enkdev.xyz/cdn/mods/ghc/ghostnutters/languageFile.json");
                        var content = JsonConvert.DeserializeObject<VersionModel>(ver);

                        if (content != null)
                        {
                            _serverVersion = content.Version;
                        }

                        if (_betaVersion != _serverVersion)
                        {
                            Console.Clear();
                            Console.WriteLine("Grabbing new Language-Files...");

                            var tempZip = new ZipFile();
                            tempZip.Save($"./download/language.zip");

                            wc.DownloadFile("https://enkdev.xyz/cdn/mods/ghc/ghostnutters/packages/GhostNuttersBetaEdition.zip",
                                $"./download/language.zip");

                            var zip = ZipFile.Read($"./download/language.zip");

                            foreach (var entry in zip)
                                entry.Extract($"./language", ExtractExistingFileAction.OverwriteSilently);

                            tempZip.Dispose();
                            zip.Dispose();
                            File.Delete($"./download/language.zip");

                            Console.Clear();
                            Console.WriteLine("Updating local version...");

                            using var fw = File.CreateText("./localVersion.json");
                            fw.Write(ver);
                            fw.Dispose();

                            Console.Clear();
                            Console.WriteLine("Moving Language-File to GHC's language directory...");

                            if (File.Exists($"{_folderPaths[0]}/GhostNuttersBetaEdition.json"))
                            {
                                File.Delete($"{_folderPaths[0]}/GhostNuttersBetaEdition.json");
                            }

                            File.Move($"./language/language/GhostNuttersBetaEdition.json", $"{_folderPaths[0]}/GhostNuttersBetaEdition.json");
                            File.Delete($"./language/language/GhostNuttersBetaEdition.json");

                            Console.Clear();
                            Console.WriteLine("Moving Vocal-File to GHC's language directory...");

                            if (File.Exists($"{_folderPaths[1]}/GhostNuttersBetaEdition.json"))
                            {
                                File.Delete($"{_folderPaths[1]}/GhostNuttersBetaEdition.json");
                            }

                            File.Move($"./language/vocal/GhostNuttersBetaEdition.json", $"{_folderPaths[1]}/GhostNuttersBetaEdition.json");
                            File.Delete($"./language/vocal/GhostNuttersBetaEdition.json");

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
                            "Your path to Ghost Hunters Corps language and vocal directory is not set. Please save it inside path.txt before running this app.\n" +
                            "If you need help: The first entry should be going to the language directory, the second to the vocal directory");
                    }

                    break;
            }


            Console.ReadKey();
        }
    }
}