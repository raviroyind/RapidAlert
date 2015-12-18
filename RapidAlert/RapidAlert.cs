using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using Newtonsoft.Json;
using RapidAlert.Classes;

namespace RapidAlert
{
    public partial class RapidAlert : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private delegate void invokeMethodDelegate(object sender, FileSystemEventArgs e);

        public static string Kewords = "";

        public static List<RapidFile> FileList=new List<RapidFile>();
        public RapidAlert()
        {
            InitializeComponent();
            CheckFolders();
            LoadConfiguration();
        }

        private void btnAddKeyword_Click(object sender, EventArgs e)
        {
            listBoxKeywords.Items.Add(txtEKeyword.EditValue.ToString());
            txtEKeyword.EditValue = string.Empty;
            txtEKeyword.Focus();
        }

        #region Functions...
         
        private static void InvokeMethod(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created || e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (CheckKeywordsMatch(e))
                    AppendToJsonPost(e);
            }
        }
        private static bool CheckKeywordsMatch(FileSystemEventArgs e)
        {
            var keywordMatch = false;

            foreach (var word in Kewords.Split(',').Where(word =>
            {
                var fileName = Path.GetFileName(e.FullPath);
                return fileName != null && fileName.Contains(word);
            }))
            {
                keywordMatch = true;
                var filename = Path.GetFileName(e.FullPath);
                var duplicate =FileList.FirstOrDefault(x => x.FileName.Equals(filename) && x.Location.Equals(e.FullPath));

                if (duplicate == null)
                {
                    FileList.Add(
                        new RapidFile
                        {
                            FileName = Path.GetFileName(e.FullPath),
                            Location = e.FullPath,
                            ComputerName = Environment.MachineName,
                            DetectionStatus = Status.Detection
                        }
                   );  
                }
            }
             
            return keywordMatch;
        }
        private static void AppendToJsonPost(FileSystemEventArgs e)
        {
             
        }
         
        private void LoadConfiguration()
        {
            var inif =
                new INIFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                            "\\RapidAlert\\config.ini");
            if (inif != null)
            {
                var keywords = inif.Read("KEYWORDS_SETTINGS", "KEYWORDS");

                if (!string.IsNullOrEmpty(keywords))
                {
                    Kewords = keywords;
                    listBoxKeywords.Items.Clear();
                    var keyStrings = keywords.Split(',');
                    foreach (var item in keyStrings)
                    {
                        listBoxKeywords.Items.Add(item);
                    }
                }

                var minutes = inif.Read("INTERVAL_SETTINGS", "MINUTES");
                ddlMinutes.SelectedIndex = !string.IsNullOrEmpty(minutes) ? ddlMinutes.FindStringExact(minutes) : 0;


                var folders = inif.Read("FOLDER_SETTINGS", "FOLDERS");

                if (!string.IsNullOrEmpty(folders))
                {
                    listBoxFolders.Items.Clear();var foldersString = folders.Split(',');
                     
                    foreach (var item in foldersString)
                    {
                        listBoxFolders.Items.Add(item);
                    }
                }
            }
        }

        private void SaveINIFile()
        {
            var inif =
                new INIFile(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                            "\\RapidAlert\\config.ini");

            var keyStrings = listBoxKeywords.Items.Cast<object>()
                .Aggregate(string.Empty, (current, item) => current + (item.ToString() + ","));

            if (keyStrings.EndsWith(","))
                keyStrings = keyStrings.Substring(0, keyStrings.Length - 1);

            inif.Write("KEYWORDS_SETTINGS", "KEYWORDS", keyStrings);

            inif.Write("INTERVAL_SETTINGS", "MINUTES", ddlMinutes.Text);

            var folders = listBoxFolders.Items.Cast<object>()
                .Aggregate(string.Empty, (current, item) => current + (item.ToString() + ","));

            if (folders.EndsWith(","))
                folders = folders.Substring(0, folders.Length - 1);

            inif.Write("FOLDER_SETTINGS", "FOLDERS", folders);
        }

        private void BeginProcess()
        {
            invokeMethodDelegate mymethod = new invokeMethodDelegate(InvokeMethod);

            foreach (var item in listBoxFolders.Items)
            {
                var watcher = new Watcher(item.ToString(), "*.*", mymethod);
                watcher.StartWatch();
            }
        }

        private static void CheckFolders()
        {
            var specialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                    "\\RapidAlert\\";

            if (!Directory.Exists(specialFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(specialFolderPath);
                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion Functions...

        private void btnSaveMinimize_Click(object sender, EventArgs e)
        {
            SaveINIFile();
            LoadConfiguration();
            BeginProcess();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            listBoxKeywords.Items.Clear();
        }

        private void btnClearFolders_Click(object sender, EventArgs e)
        {
            listBoxFolders.Items.Clear();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    listBoxFolders.Items.Add(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void RapidAlert_Resize(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Minimized:
                    appNotifyIcon.BalloonTipTitle = "Rapid Alert";
                    appNotifyIcon.BalloonTipText = "Rapid Alert is running in background.";
                    appNotifyIcon.Visible = true;
                    appNotifyIcon.ShowBalloonTip(500);
                    this.ShowInTaskbar = false;
                    this.Hide();
                    BeginProcess();
                    break;
                case FormWindowState.Normal:
                    appNotifyIcon.Visible = false;
                    break;
            }
        }

        private void RapidAlert_Load(object sender, EventArgs e)
        {}

        private void contextMenuStrip_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void appNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.Show();
            }
        }
    }
}
