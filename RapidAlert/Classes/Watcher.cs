﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidAlert.Classes
{
      public class Watcher
      {
 
          public string Directory { get; set; }
          public string  Filter { get; set; }
 
 
          private Delegate _changeMethod;
 
         public Delegate ChangeMethod
         {
             get { return _changeMethod; }
             set { _changeMethod = value; }
         }

         FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

         public Watcher(string directory, string filter, Delegate invokeMethod)
         {
             this._changeMethod = invokeMethod;
             this.Directory = directory;
             this.Filter = Filter;
         }


         public void StartWatch()
         {
             fileSystemWatcher.Filter = this.Filter;
             fileSystemWatcher.Path = this.Directory;
             fileSystemWatcher.EnableRaisingEvents = true;

             fileSystemWatcher.Changed +=
               new FileSystemEventHandler(fileSystemWatcher_Changed);
         }

         void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
         {
             if (_changeMethod != null)
             {
                 _changeMethod.DynamicInvoke(sender, e);
             }
         }
     }
}
