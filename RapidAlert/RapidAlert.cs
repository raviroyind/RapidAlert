using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using RapidAlert.Classes;
using RapidAlert.Properties;
using log4net;
using log4net.Config;

namespace RapidAlert
{
    public partial class RapidAlert : RibbonForm
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RapidAlert));
        private delegate void invokeMethodDelegate(object sender, FileSystemEventArgs e);

        public static string Kewords = "";

        public static List<RapidFile> FileList=new List<RapidFile>();
        public RapidAlert()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
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
         
        private static void InvokeMethod(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created || e.ChangeType == WatcherChangeTypes.Changed)
            {
                CheckKeywordsMatch(e);
            }
        }
        private static bool CheckKeywordsMatch(FileSystemEventArgs e)
        {
            var keywordMatch = false;


            foreach (var word in Kewords.Split(','))
            {
                var filename = Path.GetFileNameWithoutExtension(e.FullPath).ToLower();
                var extenion = Path.GetExtension(e.FullPath).ToLower();

                if (word.ToLower().Contains(filename) || word.ToLower().StartsWith(filename) || word.ToLower().EndsWith(filename) || word.Contains(extenion) || filename.ToLower().Contains(word.ToLower()) ||filename.ToLower().StartsWith(word) || filename.ToLower().EndsWith(word))
                {
                    keywordMatch = true;
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
                        Logger.Info("Detected " + e.Name + " at location -" +
                                    e.FullPath.Substring(0, e.FullPath.LastIndexOf("\\", StringComparison.Ordinal))+"\\");
                        

                    }
                }

            }
            //foreach (var word in Kewords.Split(',').Where(word =>
            //{
            //    var fileName = Path.GetFileName(e.FullPath);
            //    return fileName != null && fileName.ToLower().Contains(word.ToLower());
            //}))
            //{
            //    keywordMatch = true;
            //    var filename = Path.GetFileName(e.FullPath);
            //    var duplicate = FileList.FirstOrDefault(x => x.location.ToLower().Contains(filename.ToLower()) && x.location.ToLower().Equals(e.FullPath.ToLower()));

            //    if (duplicate == null)
            //    {
            //        FileList.Add(
            //            new RapidFile
            //            {
            //                status = DetectionStatus.Detection,
            //                location = e.FullPath,
            //                computer_name = Environment.MachineName,
            //            }
            //       );
  
            //        //Make entry to log file.
            //        Logger.Log("Detected "+filename+ " at location -"+e.FullPath.Substring(0,e.FullPath.LastIndexOf("\\", StringComparison.Ordinal)));
                     
            //    }
            //}
             
            return keywordMatch;
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
            invokeMethodDelegate mymethod = new invokeMethodDelegate(InvokeMethod);

            foreach (var item in listBoxFolders.Items)
            {
                var watcher = new Watcher(item.ToString(), "*.*", mymethod);
                watcher.StartWatch();
            }
            
            postTimer.Interval = GetInterval();
            postTimer.Start();


            appNotifyIcon.BalloonTipTitle = Resources.RapidAlert_BeginProcess_Rapid_Alert;
            appNotifyIcon.BalloonTipText = Resources.RapidAlert_BeginProcess_Rapid_Alert_Process_is_running_in_background_;
            appNotifyIcon.Visible = true;
            appNotifyIcon.ShowBalloonTip(500);this.ShowInTaskbar = false;
            this.Hide();
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
                "\\RapidAlert\\Logs\\";

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
        {this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
            this.appNotifyIcon.Icon.Dispose();}
        private async void postTimer_Tick(object sender, EventArgs e)
        { 
           var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(FileList));
           stringPayload = stringPayload.ToLower();
           var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
           using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.PostAsync(Resources.API_URL, httpContent);
               if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();FileList.Clear();
                }
            }
        }
        private void barButtonViewLog_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\RapidAlert\\Logs\\");
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
                this.Hide();
                BeginProcess();e.Cancel = true;
            }
        }
    }
}
