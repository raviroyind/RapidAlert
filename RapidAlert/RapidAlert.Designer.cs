namespace RapidAlert
{
    partial class RapidAlert
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Ribbon.ReduceOperation reduceOperation2 = new DevExpress.XtraBars.Ribbon.ReduceOperation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RapidAlert));
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
            this.txtEKeyword = new DevExpress.XtraEditors.TextEdit();
            this.groupKeywords = new DevExpress.XtraEditors.GroupControl();
            this.listBoxKeywords = new DevExpress.XtraEditors.ListBoxControl();
            this.btnAddKeyword = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.ddlMinutes = new System.Windows.Forms.ComboBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnClearFolders = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectFolder = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveMinimize = new DevExpress.XtraEditors.SimpleButton();
            this.groupFolders = new DevExpress.XtraEditors.GroupControl();
            this.listBoxFolders = new DevExpress.XtraEditors.ListBoxControl();
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.postTimer = new System.Windows.Forms.Timer(this.components);
            this.btnSelectAll = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEKeyword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupKeywords)).BeginInit();
            this.groupKeywords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxKeywords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupFolders)).BeginInit();
            this.groupFolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFolders)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            reduceOperation2.Behavior = DevExpress.XtraBars.Ribbon.ReduceOperationBehavior.Single;
            reduceOperation2.Group = null;
            reduceOperation2.ItemLinkIndex = 0;
            reduceOperation2.ItemLinksCount = 0;
            reduceOperation2.Operation = DevExpress.XtraBars.Ribbon.ReduceOperationType.Gallery;
            this.ribbonPage1.ReduceOperations.Add(reduceOperation2);
            this.ribbonPage1.Text = "Log Files";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barHeaderItem1});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbonControl1.MaxItemId = 2;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl1.Size = new System.Drawing.Size(1340, 155);
            // 
            // barHeaderItem1
            // 
            this.barHeaderItem1.Caption = "barHeaderItem1";
            this.barHeaderItem1.Id = 1;
            this.barHeaderItem1.Name = "barHeaderItem1";
            // 
            // txtEKeyword
            // 
            this.txtEKeyword.Location = new System.Drawing.Point(137, 524);
            this.txtEKeyword.MenuManager = this.ribbonControl1;
            this.txtEKeyword.Name = "txtEKeyword";
            this.txtEKeyword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtEKeyword.Properties.Appearance.Options.UseFont = true;
            this.txtEKeyword.Size = new System.Drawing.Size(244, 30);
            this.txtEKeyword.TabIndex = 0;
            // 
            // groupKeywords
            // 
            this.groupKeywords.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupKeywords.Appearance.Options.UseFont = true;
            this.groupKeywords.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(10)));
            this.groupKeywords.AppearanceCaption.Options.UseFont = true;
            this.groupKeywords.CaptionImagePadding = new System.Windows.Forms.Padding(5);
            this.groupKeywords.Controls.Add(this.btnSelectAll);
            this.groupKeywords.Controls.Add(this.listBoxKeywords);
            this.groupKeywords.Location = new System.Drawing.Point(137, 68);
            this.groupKeywords.Name = "groupKeywords";
            this.groupKeywords.Size = new System.Drawing.Size(374, 445);
            this.groupKeywords.TabIndex = 2;
            this.groupKeywords.Text = "Keywords";
            // 
            // listBoxKeywords
            // 
            this.listBoxKeywords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxKeywords.Location = new System.Drawing.Point(2, 29);
            this.listBoxKeywords.Name = "listBoxKeywords";
            this.listBoxKeywords.Size = new System.Drawing.Size(370, 414);
            this.listBoxKeywords.TabIndex = 11;
            // 
            // btnAddKeyword
            // 
            this.btnAddKeyword.Image = ((System.Drawing.Image)(resources.GetObject("btnAddKeyword.Image")));
            this.btnAddKeyword.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnAddKeyword.Location = new System.Drawing.Point(387, 517);
            this.btnAddKeyword.Name = "btnAddKeyword";
            this.btnAddKeyword.Size = new System.Drawing.Size(124, 40);
            this.btnAddKeyword.TabIndex = 5;
            this.btnAddKeyword.Text = "Add Keyword";
            this.btnAddKeyword.Click += new System.EventHandler(this.btnAddKeyword_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 155);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.ddlMinutes);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnAddKeyword);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtEKeyword);
            this.splitContainerControl1.Panel1.Controls.Add(this.groupKeywords);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnClearFolders);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnSelectFolder);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnSaveMinimize);
            this.splitContainerControl1.Panel2.Controls.Add(this.groupFolders);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1340, 662);
            this.splitContainerControl1.SplitterPosition = 666;
            this.splitContainerControl1.TabIndex = 9;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ddlMinutes
            // 
            this.ddlMinutes.Font = new System.Drawing.Font("Tahoma", 11F);
            this.ddlMinutes.FormattingEnabled = true;
            this.ddlMinutes.Items.AddRange(new object[] {
            "5 minutes",
            "10  minutes",
            "15  minutes",
            "20  minutes",
            "25 minutes",
            "30 minutes",
            "35 minutes",
            "40 minutes",
            "45 minutes",
            "50 minutes",
            "55 minutes",
            "60 minutes"});
            this.ddlMinutes.Location = new System.Drawing.Point(207, 575);
            this.ddlMinutes.Name = "ddlMinutes";
            this.ddlMinutes.Size = new System.Drawing.Size(121, 30);
            this.ddlMinutes.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Location = new System.Drawing.Point(137, 575);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 24);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Update";
            // 
            // btnClearFolders
            // 
            this.btnClearFolders.Image = ((System.Drawing.Image)(resources.GetObject("btnClearFolders.Image")));
            this.btnClearFolders.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClearFolders.Location = new System.Drawing.Point(147, 22);
            this.btnClearFolders.Name = "btnClearFolders";
            this.btnClearFolders.Size = new System.Drawing.Size(139, 40);
            this.btnClearFolders.TabIndex = 11;
            this.btnClearFolders.Text = "Clear Folders";
            this.btnClearFolders.Click += new System.EventHandler(this.btnClearFolders_Click);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFolder.Image")));
            this.btnSelectFolder.Location = new System.Drawing.Point(147, 518);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(372, 39);
            this.btnSelectFolder.TabIndex = 11;
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // btnSaveMinimize
            // 
            this.btnSaveMinimize.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnSaveMinimize.Appearance.Options.UseFont = true;
            this.btnSaveMinimize.Location = new System.Drawing.Point(259, 575);
            this.btnSaveMinimize.Name = "btnSaveMinimize";
            this.btnSaveMinimize.Size = new System.Drawing.Size(187, 58);
            this.btnSaveMinimize.TabIndex = 10;
            this.btnSaveMinimize.Text = "Save && Minimize";
            this.btnSaveMinimize.Click += new System.EventHandler(this.btnSaveMinimize_Click);
            // 
            // groupFolders
            // 
            this.groupFolders.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.groupFolders.Appearance.Options.UseFont = true;
            this.groupFolders.Controls.Add(this.listBoxFolders);
            this.groupFolders.Location = new System.Drawing.Point(145, 68);
            this.groupFolders.Name = "groupFolders";
            this.groupFolders.Size = new System.Drawing.Size(374, 445);
            this.groupFolders.TabIndex = 3;
            this.groupFolders.Text = "Folders";
            // 
            // listBoxFolders
            // 
            this.listBoxFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFolders.Location = new System.Drawing.Point(2, 24);
            this.listBoxFolders.Name = "listBoxFolders";
            this.listBoxFolders.Size = new System.Drawing.Size(370, 419);
            this.listBoxFolders.TabIndex = 0;
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.appNotifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.appNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("appNotifyIcon.Icon")));
            this.appNotifyIcon.Visible = true;
            this.appNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.appNotifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(109, 30);
            this.contextMenuStrip.Click += new System.EventHandler(this.contextMenuStrip_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(108, 26);
            this.toolStripMenuItem1.Text = "Exit";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.Location = new System.Drawing.Point(310, 3);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(59, 23);
            this.btnSelectAll.TabIndex = 11;
            this.btnSelectAll.Text = "Clear";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // RapidAlert
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 817);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RapidAlert";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rapid Alert 1.0";
            this.Load += new System.EventHandler(this.RapidAlert_Load);
            this.Resize += new System.EventHandler(this.RapidAlert_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEKeyword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupKeywords)).EndInit();
            this.groupKeywords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxKeywords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupFolders)).EndInit();
            this.groupFolders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxFolders)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraEditors.TextEdit txtEKeyword;
        private DevExpress.XtraEditors.GroupControl groupKeywords;
        private DevExpress.XtraEditors.SimpleButton btnAddKeyword;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox ddlMinutes;
        private DevExpress.XtraEditors.SimpleButton btnSaveMinimize;
        private DevExpress.XtraEditors.GroupControl groupFolders;
        private DevExpress.XtraEditors.SimpleButton btnSelectFolder;
        private DevExpress.XtraEditors.ListBoxControl listBoxKeywords;
        private DevExpress.XtraEditors.SimpleButton btnClearFolders;
        private DevExpress.XtraEditors.ListBoxControl listBoxFolders;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Timer postTimer;
        private DevExpress.XtraEditors.SimpleButton btnSelectAll;

    }
}

