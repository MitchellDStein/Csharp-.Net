using System;
using System.IO;            // types for managing the filesystem
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using static System.Console;

namespace Working_With_File_Systems
{
    class Program
    {
        static void Main(string[] args)
        {
            // OutputFileSystemInfo();
            // WorkWithDrives();
            // WorkWithDirectories();
            WorkWithFiles();
        }

        static void OutputFileSystemInfo()
        {
            WriteLine("{0,-33} {1}", "Path.PathSeparator", PathSeparator);
            WriteLine("{0,-33} {1}", "Path.DirectorySeparatorChar", DirectorySeparatorChar);
            WriteLine("{0,-33} {1}", "Directory.GetCurrentDirectory()", GetCurrentDirectory());
            WriteLine("{0,-33} {1}", "Environment.CurrentDirectory", CurrentDirectory);
            WriteLine("{0,-33} {1}", "Environment.SystemDirectory", SystemDirectory);
            WriteLine("{0,-33} {1}", "Path.GetTempPath()", GetTempPath());
            WriteLine("GetFolderPath(SpecialFolder");
            WriteLine("{0,-33} {1}", " .System)", GetFolderPath(SpecialFolder.System));
            WriteLine("{0,-33} {1}", " .ApplicationData)", GetFolderPath(SpecialFolder.ApplicationData));
            WriteLine("{0,-33} {1}", " .MyDocuments)", GetFolderPath(SpecialFolder.MyDocuments));
            WriteLine("{0,-33} {1}", " .Personal)", GetFolderPath(SpecialFolder.Personal));
        }

        static void WorkWithDrives()
        {
            WriteLine("{0,-30} | {1,-10} | {2,-7} | {3,18} | {4,18}", "NAME", "TYPE", "FORMAT", "SIZE (BYTES)", "FREE SPACE");
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    WriteLine(
                        "{0,-30} | {1,-10} | {2,-7} | {3,18:N0} | {4,18:N0}",
                        drive.Name,
                        drive.DriveType,
                        drive.DriveFormat,
                        drive.TotalSize,
                        drive.AvailableFreeSpace);
                }
                else
                {
                    WriteLine(
                        "{0,-30} | {1,-10}",
                        drive.Name,
                        drive.DriveType);
                }
            }
        }

        static void WorkWithDirectories()
        {
            //define a directory path for a new folder
            // starting in the user's folder.
            var newFolder = Combine(GetFolderPath(SpecialFolder.Personal), "Code", "Chapter09", "NewFolder");

            WriteLine($"Working with: {newFolder}");

            //check if the file exists already
            WriteLine($"Does it exist? {Exists(newFolder)}");
            if (Exists(newFolder))
            {
                WriteLine("Folder already exists!");
            }
            else
            {
                //create the directory
                WriteLine("Creating directory...");
                CreateDirectory(newFolder);

                //check to see if it has been created
                WriteLine($"Has the folder been created: {Exists(newFolder)}");
                WriteLine("If it has been created please hit 'enter' to delete it.");
                ReadLine();

                //delete directory
                WriteLine("deleting directory...");
                Delete(newFolder, recursive: true);

                //check to see if the directory has been deleted
                WriteLine($"Does the file exist?: {Exists(newFolder)}");
            }
        }

        static void WorkWithFiles()
        {
            var dir = Combine(GetFolderPath(SpecialFolder.Personal), "Code", "Chapter09", "OutputFiles");   // setup path for directory
            if (Exists(dir))                                                                                // check if directory exists
            {
                WriteLine($"Directory: {dir} already exists!");
            }
            else
            {
                CreateDirectory(dir);
            }

            // create text file
            string textFile = Combine(dir, "Dummy.txt");    // setup paths for text files
            WriteLine($"Working with: {textFile}.");
            if (File.Exists(textFile))                      // check if textFile exists 
            {
                WriteLine($"File: {textFile} already exists!");
            }
            else
            {
                StreamWriter textWriter = File.CreateText(textFile);    // create text file
                textWriter.WriteLine("Hello, I was created in a lab!"); // write a line of text to the file
                textWriter.Close();                                     // close and release system resources
            }


            // create backup file
            string backupFile = Combine(dir, "Dummy.bak");

            // copy the file, and overwrite if it already exists
            File.Copy(
                sourceFileName: textFile,
                destFileName: backupFile,
                overwrite: true);

            WriteLine($"\nDoes {backupFile} exist? {File.Exists(backupFile)}");
            Write("Confirm the files exist, and then press ENTER to delete original: ");
            ReadLine();

            // delete original file
            WriteLine($"\nDeleting {textFile} ...");
            File.Delete(textFile);
            WriteLine($"Does original file exist? {File.Exists(textFile)}");

            // read from the text file backup
            WriteLine($"\nReading contents of {backupFile}:");
            StreamReader textReader = File.OpenText(backupFile);
            WriteLine(textReader.ReadToEnd());
            textReader.Close();

            // Managing paths
            WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
            WriteLine($"File Name: {GetFileName(textFile)}");
            WriteLine($"File Name without Extension: {GetFileNameWithoutExtension(textFile)}");
            WriteLine($"File Extension: {GetExtension(textFile)}");
            WriteLine($"Random File Name: {GetRandomFileName()}");  // just returns a filename; it doesn't create the file
            WriteLine($"Temporary File Name: {GetTempFileName()}\n"); // creates a zero-byte file and returns its name, ready for you to use

            // File information:
            var info = new FileInfo(backupFile);
            WriteLine($"{backupFile}:");
            WriteLine($"Contains {info.Length} bytes");
            WriteLine($"Last accessed {info.LastAccessTime}");
            WriteLine($"Has readonly set to {info.IsReadOnly}");
        }
    }
}
