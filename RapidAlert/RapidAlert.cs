using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using RapidAlert.Classes;
using RapidAlert.Properties;

namespace RapidAlert
{
    public partial class RapidAlert : RibbonForm
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RapidAlert));
        public static string Kewords = "";
        public static List<RapidFile> FileList=new List<RapidFile>();

        public RapidAlert()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
            splitContainerControl1.SplitterPosition = this.splitContainerControl1.Width / 2;
            this.splitContainerControl1.Panel1.Width = this.splitContainerControl1.Width / 2;
            this.splitContainerControl1.Panel2.Width = this.splitContainerControl1.Width / 2;
            CheckFolders();
            LoadConfiguration();
        }

        private void btnAddKeyword_Click(object sender, EventArgs e)
        {
            if (txtEKeyword.EditValue==null)
            {
                XtraMessageBox.Show("Plese enter a keyword!");
                return;
            }

            listBoxKeywords.Items.Add(txtEKeyword.EditValue.ToString());
            txtEKeyword.EditValue = string.Empty;txtEKeyword.Focus();
        }

        #region Functions...
         
        
        private static void CheckKeywordsMatch(FileSystemEventArgs e)
        {
            foreach (var word in Kewords.Split(','))
            {
                var filename = Path.GetFileNameWithoutExtension(e.FullPath).ToLower();

                var extension = Path.GetExtension(e.FullPath);

                if (word.ToLower().Contains(filename) || filename.ToLower().Contains(word.ToLower()) || filename.ToLower().StartsWith(word.ToLower()) || filename.ToLower().EndsWith(word.ToLower()) || CheckExtension(extension,word))
                {
                    var duplicate = FileList.FirstOrDefault(x => x.location.ToLower().Contains(filename.ToLower()) && x.location.ToLower().Equals(e.FullPath.ToLower()));

                    if (duplicate == null)
                    {
                        FileList.Add(new RapidFile
                            {
                                status = DetectionStatus.Detection,
                                location = e.FullPath,
                                computer_name = Environment.MachineName,
                            }
                       );
                        //Make entry to log file.
                        Logger.Info("Detected " + e.Name + " at location -" + e.FullPath.Substring(0, e.FullPath.LastIndexOf("\\", StringComparison.Ordinal))+"\\");
                   }
                }
            }
        }

        private static bool CheckExtension(string extension, string word)
        {
            if (word.Contains("*"))
                word = word.Replace("*", "");

            if (extension.ToLower().Equals(word))
                return true;
            else
                return false;
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

        private void SaveIniFile()
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
            
            foreach (var item in listBoxFolders.Items)
            {
                var myWatcher = new FileSystemWatcher(item.ToString(), "*.*");

                myWatcher.Changed += this.myWatcher_Changed;
                myWatcher.Created += this.myWatcher_Created;
                myWatcher.Renamed += this.myWatcher_Renamed;
                myWatcher.IncludeSubdirectories = true;
                myWatcher.EnableRaisingEvents = true;
                 
            }
            
            postTimer.Interval = GetInterval();
            postTimer.Start();
            appNotifyIcon.BalloonTipTitle = Resources.RapidAlert_BeginProcess_Rapid_Alert;
            appNotifyIcon.BalloonTipText = Resources.RapidAlert_BeginProcess_Rapid_Alert_Process_is_running_in_background_;
            appNotifyIcon.Visible = true;appNotifyIcon.ShowBalloonTip(500);this.ShowInTaskbar = false;
            this.Hide();
        
        }
        private void myWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            CheckKeywordsMatch(e);
        }
        private void myWatcher_Created(object sender, FileSystemEventArgs e)
        {
            CheckKeywordsMatch(e);
        }

        private void myWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            CheckKeywordsMatch(e);
        }

        private int GetInterval()
        { 
            return Convert.ToInt32(TimeSpan.FromMinutes(
                Convert.ToInt32(ddlMinutes.Text.Substring(0, ddlMinutes.Text.IndexOf(' '))))
                .TotalMilliseconds);

        }

        private static void CheckFolders()
        {
            var specialFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\RapidAlert\\";

            var logFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\RapidAlert\\Logs";

            if (!Directory.Exists(specialFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(specialFolderPath);
                }
                catch
                {
                    // ignored
                }
            }

            if (!Directory.Exists(logFolderPath))
                {
                    try
                    {
                        Directory.CreateDirectory(logFolderPath);
                    }
                    catch
                    {
                       // ignored
                    }
                }
         }

        #endregion Functions...

        private void btnSaveMinimize_Click(object sender, EventArgs e){
            SaveIniFile();
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
                    appNotifyIcon.BalloonTipTitle = Resources.RapidAlert_BeginProcess_Rapid_Alert;
                    appNotifyIcon.BalloonTipText = Resources.RapidAlert_BeginProcess_Rapid_Alert_Process_is_running_in_background_;
                    appNotifyIcon.Icon = Resources.Gear_icon_291x300;appNotifyIcon.Visible = true;
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
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
            this.appNotifyIcon.Icon.Dispose();
        }

        private async void postTimer_Tick(object sender, EventArgs e)
        {
           if (FileList.Count == 0)
                return;
           var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(FileList));
             
           var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
           using (var httpClient = new HttpClient())
           {
                var httpResponse = await httpClient.PostAsync(ConfigurationManager.AppSettings["API_URL"], httpContent);
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();FileList.Clear();
                }
           }
        }

        private void barButtonViewLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +"\\RapidAlert\\Logs\\");
        }

        private void ribbonControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RapidAlert_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                appNotifyIcon.BalloonTipTitle = Resources.RapidAlert_BeginProcess_Rapid_Alert;
                appNotifyIcon.BalloonTipText = Resources.RapidAlert_BeginProcess_Rapid_Alert_Process_is_running_in_background_;
                appNotifyIcon.Icon = Resources.Gear_icon_291x300; appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(500);
                this.ShowInTaskbar = false;
                this.Hide();BeginProcess();
                e.Cancel = true;
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }
    }
}
