using System;
using System.Collections.Generic;
using System.IO;

namespace duplicateFileFinder
{
    class Program
    {
        static void Main()
        {
            //Prompt users to enter the path to the directory for duplicate searching
            Console.WriteLine("Enter the path of the directory you would like to check for duplicates, I would recommend trying the TestDirectory:");
            string startingDirectory = Console.ReadLine();

            //Initialise file List which will store all files for comparison
            List<String> masterFileList = new List<String>();

            //Function call edits the List entered as a parameter
            GetFileList(startingDirectory,masterFileList);

            //Find duplicates and write location and name to console
            List<String> duplicateResults = FindDuplicates(masterFileList);
            Console.WriteLine("The following files are duplicates, here is their name and location:");
            foreach(String copy in duplicateResults){
                Console.WriteLine("NAME: " + Path.GetFileName(copy) +", LOCATION: " + Path.GetDirectoryName(copy));
            }
        }

        //Adds all the file paths of a directory to the fileList passed as a parameter
        static void GetFileList(string startingFolder, List<String> filePathList)
        {
            //Get a list of children folders of the starting folder
            string[] subDir = Directory.GetDirectories(startingFolder);

            //Get array of file paths of current folder to be added to master list
            string[] addingFiles = Directory.GetFiles(startingFolder);
            
            //Go through each new file and add it to the list
            foreach(string file in addingFiles)
            {
                filePathList.Add(file);
            }
            //Go through each child and find their children
            foreach (string child in subDir)
            {
            //Recursive call to go though children of children repeatedly with a Depth-First Search to get all descendent files
                GetFileList(child,filePathList);
            }
        }

        //Returns a list of strings that are duplicates using a hashset
        static List<String> FindDuplicates(List<String> filePathList)
        {
            //Create empty hashset, will know if file is a copy if it already exists in the hashset
            HashSet<String> uniqueFiles = new HashSet<String>();

            //List for outputting the duplicate files
            List<String> duplicates = new List<String>();

            foreach(string file in filePathList)
            {
               //Combine the file size and name into one string, if a file shares these then it is most likely a duplicate
                FileInfo info = new FileInfo(file);
                if(uniqueFiles.Add(info.Length.ToString() + Path.GetFileName(file)))
                {
                }
                //Adding to Hashset returns false because value is not unique, add duplicate to duplicate list
                else
                {
                    duplicates.Add(file);
                }
            }
            return duplicates;
        }
    }
}
