using System;
using System.Collections.Generic;
using FCDLL.Controller;
using FCDLL.Model;

namespace UsingExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating instance.
            CopyController CopyController =
                new CopyController(@"C:\MySourceFolder" /*Target folder full path*/,
                                   new string[] { "192.168.0.2", "192.168.0.3" } /*Destination remote nodes (ip/hostname) array*/,
                                   "TargetPath" /*Set path to destination folder on remote node.*/,
                                   "NewFolder" /*Set new folder name in case if its required*/);

            List<Result> results = CopyController.CopyRun() /*Result list*/;

            // Show result of copy process.
            foreach (Result result in results)
            {
                Console.WriteLine(new string('=', 100));
                Console.WriteLine($@"Hostname: {result.Destination.Name}; Path: {result.Destination.FullPath}; Result: {result.CopyResult}");
            }


            // Or just run instance once.
            new CopyController(@"C:\PathToCopy",
                               @"C:\HostList.txt" /*Destination list.txt file full path*/).CopyRun();
        }
    }
}
