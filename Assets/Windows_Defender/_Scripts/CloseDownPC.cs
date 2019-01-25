using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;

 
class MyClass
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("After Scene is loaded and game is running");
        //System.Diagnostics.Process.Start("shutdown", "/s /t 0"); <-- shutting down windows
        //System.Diagnostics.Process myProcess;
        //myProcess = System.Diagnostics.Process.Start(@"C:\Users\sebastian.zychlewic\AppData\Local\Discord\app-0.0.304\Discord.exe");
        //System.Threading.Thread.Sleep(10000);
        //myProcess.Kill(); <-- starting and killing programs

    //    string path = @"C:\Users\Administratör\Documents\AidanIsGay.txt";

    //    try
    //    {

    //        // Delete the file if it exists.
    //        if (File.Exists(path))
    //        {
    //            // Note that no lock is put on the
    //            // file and the possibility exists
    //            // that another process could do
    //            // something with it between
    //            // the calls to Exists and Delete.
    //            File.Delete(path);
    //        }

    //        // Create the file.
    //        using (FileStream fs = File.Create(path))
    //        {
    //            Byte[] info = new UTF8Encoding(true).GetBytes("Did you know that i can delete your windows?");
    //            // Add some information to the file.
    //            fs.Write(info, 0, info.Length);
    //        }
    //        System.Diagnostics.Process.Start(path);


    //    }

    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //    }
    } 
    //<---- creates text files and writes in them





[RuntimeInitializeOnLoadMethod]
    static void OnSecondRuntimeMethodLoad()
    {
        Debug.Log("SecondMethod After Scene is loaded and game is running.");
    }
}
