namespace SnatcherGUI {
  partial class MainForm {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing ) {
      if( disposing && ( components != null ) ) {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.Tabs = new System.Windows.Forms.TabControl();
      this.ParserTab = new System.Windows.Forms.TabPage();
      this.ParserTabControlsLayout = new System.Windows.Forms.TableLayoutPanel();
      this.ParserProgressLabel = new System.Windows.Forms.Label();
      this.ParseProgressBar = new System.Windows.Forms.ProgressBar();
      this.JournalList = new System.Windows.Forms.ListBox();
      this.StartParse = new System.Windows.Forms.Button();
      this.StopParse = new System.Windows.Forms.Button();
      this.SettingsTab = new System.Windows.Forms.TabPage();
      this.SettingsTabControlsLayout = new System.Windows.Forms.TableLayoutPanel();
      this.TargetWebSiteLabel = new System.Windows.Forms.Label();
      this.TargetWebSiteComboBox = new System.Windows.Forms.ComboBox();
      this.TargetCategoryLabel = new System.Windows.Forms.Label();
      this.TargetCategoryComboBox = new System.Windows.Forms.ComboBox();
      this.PagesParseLabel = new System.Windows.Forms.Label();
      this.PagesParseTypeComboBox = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.ParseContentCheckListBox = new System.Windows.Forms.CheckedListBox();
      this.ContentExportTypeLabel = new System.Windows.Forms.Label();
      this.ContentExportTypeComboBox = new System.Windows.Forms.ComboBox();
      this.PagesParseTypeTextBox = new System.Windows.Forms.MaskedTextBox();
      this.DocumentationTab = new System.Windows.Forms.TabPage();
      this.DocumentationBrowser = new System.Windows.Forms.WebBrowser();
      this.HiddenBrowser = new System.Windows.Forms.WebBrowser();
      this.Tabs.SuspendLayout();
      this.ParserTab.SuspendLayout();
      this.ParserTabControlsLayout.SuspendLayout();
      this.SettingsTab.SuspendLayout();
      this.SettingsTabControlsLayout.SuspendLayout();
      this.DocumentationTab.SuspendLayout();
      this.SuspendLayout();
      // 
      // Tabs
      // 
      this.Tabs.Controls.Add(this.ParserTab);
      this.Tabs.Controls.Add(this.SettingsTab);
      this.Tabs.Controls.Add(this.DocumentationTab);
      this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Tabs.HotTrack = true;
      this.Tabs.Location = new System.Drawing.Point(5, 5);
      this.Tabs.Multiline = true;
      this.Tabs.Name = "Tabs";
      this.Tabs.SelectedIndex = 0;
      this.Tabs.Size = new System.Drawing.Size(594, 231);
      this.Tabs.TabIndex = 0;
      // 
      // ParserTab
      // 
      this.ParserTab.Controls.Add(this.ParserTabControlsLayout);
      this.ParserTab.Location = new System.Drawing.Point(4, 22);
      this.ParserTab.Name = "ParserTab";
      this.ParserTab.Padding = new System.Windows.Forms.Padding(3);
      this.ParserTab.Size = new System.Drawing.Size(586, 205);
      this.ParserTab.TabIndex = 0;
      this.ParserTab.Text = "Парсер";
      this.ParserTab.UseVisualStyleBackColor = true;
      // 
      // ParserTabControlsLayout
      // 
      this.ParserTabControlsLayout.AutoScroll = true;
      this.ParserTabControlsLayout.BackColor = System.Drawing.Color.Transparent;
      this.ParserTabControlsLayout.ColumnCount = 2;
      this.ParserTabControlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.ParserTabControlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.ParserTabControlsLayout.Controls.Add(this.ParserProgressLabel, 0, 0);
      this.ParserTabControlsLayout.Controls.Add(this.ParseProgressBar, 0, 1);
      this.ParserTabControlsLayout.Controls.Add(this.JournalList, 0, 3);
      this.ParserTabControlsLayout.Controls.Add(this.StartParse, 0, 2);
      this.ParserTabControlsLayout.Controls.Add(this.StopParse, 1, 2);
      this.ParserTabControlsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ParserTabControlsLayout.Location = new System.Drawing.Point(3, 3);
      this.ParserTabControlsLayout.Name = "ParserTabControlsLayout";
      this.ParserTabControlsLayout.RowCount = 4;
      this.ParserTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.ParserTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.ParserTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
      this.ParserTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
      this.ParserTabControlsLayout.Size = new System.Drawing.Size(580, 199);
      this.ParserTabControlsLayout.TabIndex = 0;
      // 
      // ParserProgressLabel
      // 
      this.ParserProgressLabel.AutoSize = true;
      this.ParserTabControlsLayout.SetColumnSpan(this.ParserProgressLabel, 2);
      this.ParserProgressLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ParserProgressLabel.Location = new System.Drawing.Point(3, 0);
      this.ParserProgressLabel.Name = "ParserProgressLabel";
      this.ParserProgressLabel.Size = new System.Drawing.Size(574, 30);
      this.ParserProgressLabel.TabIndex = 0;
      this.ParserProgressLabel.Text = "Прогресс парсинга";
      this.ParserProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ParseProgressBar
      // 
      this.ParserTabControlsLayout.SetColumnSpan(this.ParseProgressBar, 2);
      this.ParseProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ParseProgressBar.Location = new System.Drawing.Point(3, 33);
      this.ParseProgressBar.Name = "ParseProgressBar";
      this.ParseProgressBar.Size = new System.Drawing.Size(574, 14);
      this.ParseProgressBar.Step = 1;
      this.ParseProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.ParseProgressBar.TabIndex = 1;
      // 
      // JournalList
      // 
      this.JournalList.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.ParserTabControlsLayout.SetColumnSpan(this.JournalList, 2);
      this.JournalList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.JournalList.Location = new System.Drawing.Point(3, 97);
      this.JournalList.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
      this.JournalList.Name = "JournalList";
      this.JournalList.Size = new System.Drawing.Size(574, 99);
      this.JournalList.TabIndex = 2;
      // 
      // StartParse
      // 
      this.StartParse.Dock = System.Windows.Forms.DockStyle.Fill;
      this.StartParse.Location = new System.Drawing.Point(3, 53);
      this.StartParse.Name = "StartParse";
      this.StartParse.Size = new System.Drawing.Size(284, 28);
      this.StartParse.TabIndex = 3;
      this.StartParse.Text = "Старт";
      this.StartParse.UseVisualStyleBackColor = true;
      this.StartParse.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StartParse_MouseClick);
      // 
      // StopParse
      // 
      this.StopParse.Dock = System.Windows.Forms.DockStyle.Fill;
      this.StopParse.Location = new System.Drawing.Point(293, 53);
      this.StopParse.Name = "StopParse";
      this.StopParse.Size = new System.Drawing.Size(284, 28);
      this.StopParse.TabIndex = 4;
      this.StopParse.Text = "Стоп";
      this.StopParse.UseVisualStyleBackColor = true;
      this.StopParse.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StopParse_MouseClick);
      // 
      // SettingsTab
      // 
      this.SettingsTab.Controls.Add(this.SettingsTabControlsLayout);
      this.SettingsTab.Location = new System.Drawing.Point(4, 22);
      this.SettingsTab.Name = "SettingsTab";
      this.SettingsTab.Padding = new System.Windows.Forms.Padding(3);
      this.SettingsTab.Size = new System.Drawing.Size(586, 205);
      this.SettingsTab.TabIndex = 1;
      this.SettingsTab.Text = "Настройки";
      this.SettingsTab.UseVisualStyleBackColor = true;
      // 
      // SettingsTabControlsLayout
      // 
      this.SettingsTabControlsLayout.AutoScroll = true;
      this.SettingsTabControlsLayout.BackColor = System.Drawing.Color.Transparent;
      this.SettingsTabControlsLayout.ColumnCount = 2;
      this.SettingsTabControlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.SettingsTabControlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.SettingsTabControlsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
      this.SettingsTabControlsLayout.Controls.Add(this.TargetWebSiteLabel, 0, 0);
      this.SettingsTabControlsLayout.Controls.Add(this.TargetWebSiteComboBox, 0, 1);
      this.SettingsTabControlsLayout.Controls.Add(this.TargetCategoryLabel, 1, 0);
      this.SettingsTabControlsLayout.Controls.Add(this.TargetCategoryComboBox, 1, 1);
      this.SettingsTabControlsLayout.Controls.Add(this.PagesParseLabel, 0, 2);
      this.SettingsTabControlsLayout.Controls.Add(this.PagesParseTypeComboBox, 0, 3);
      this.SettingsTabControlsLayout.Controls.Add(this.label2, 0, 4);
      this.SettingsTabControlsLayout.Controls.Add(this.ParseContentCheckListBox, 0, 5);
      this.SettingsTabControlsLayout.Controls.Add(this.ContentExportTypeLabel, 0, 6);
      this.SettingsTabControlsLayout.Controls.Add(this.ContentExportTypeComboBox, 0, 7);
      this.SettingsTabControlsLayout.Controls.Add(this.PagesParseTypeTextBox, 1, 3);
      this.SettingsTabControlsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SettingsTabControlsLayout.Location = new System.Drawing.Point(3, 3);
      this.SettingsTabControlsLayout.Name = "SettingsTabControlsLayout";
      this.SettingsTabControlsLayout.RowCount = 7;
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.SettingsTabControlsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.SettingsTabControlsLayout.Size = new System.Drawing.Size(580, 199);
      this.SettingsTabControlsLayout.TabIndex = 1;
      // 
      // TargetWebSiteLabel
      // 
      this.TargetWebSiteLabel.AutoSize = true;
      this.TargetWebSiteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TargetWebSiteLabel.Location = new System.Drawing.Point(3, 0);
      this.TargetWebSiteLabel.Name = "TargetWebSiteLabel";
      this.TargetWebSiteLabel.Size = new System.Drawing.Size(284, 30);
      this.TargetWebSiteLabel.TabIndex = 0;
      this.TargetWebSiteLabel.Text = "Веб-сайт:";
      this.TargetWebSiteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // TargetWebSiteComboBox
      // 
      this.TargetWebSiteComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TargetWebSiteComboBox.FormattingEnabled = true;
      this.TargetWebSiteComboBox.Location = new System.Drawing.Point(3, 33);
      this.TargetWebSiteComboBox.Name = "TargetWebSiteComboBox";
      this.TargetWebSiteComboBox.Size = new System.Drawing.Size(284, 21);
      this.TargetWebSiteComboBox.Sorted = true;
      this.TargetWebSiteComboBox.TabIndex = 1;
      this.TargetWebSiteComboBox.SelectedIndexChanged += new System.EventHandler(this.TargetWebSiteComboBox_SelectedIndexChanged);
      // 
      // TargetCategoryLabel
      // 
      this.TargetCategoryLabel.AutoSize = true;
      this.TargetCategoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TargetCategoryLabel.Location = new System.Drawing.Point(293, 0);
      this.TargetCategoryLabel.Name = "TargetCategoryLabel";
      this.TargetCategoryLabel.Size = new System.Drawing.Size(284, 30);
      this.TargetCategoryLabel.TabIndex = 2;
      this.TargetCategoryLabel.Text = "Категория:";
      this.TargetCategoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // TargetCategoryComboBox
      // 
      this.TargetCategoryComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TargetCategoryComboBox.FormattingEnabled = true;
      this.TargetCategoryComboBox.Location = new System.Drawing.Point(293, 33);
      this.TargetCategoryComboBox.Name = "TargetCategoryComboBox";
      this.TargetCategoryComboBox.Size = new System.Drawing.Size(284, 21);
      this.TargetCategoryComboBox.Sorted = true;
      this.TargetCategoryComboBox.TabIndex = 4;
      // 
      // PagesParseLabel
      // 
      this.PagesParseLabel.AutoSize = true;
      this.SettingsTabControlsLayout.SetColumnSpan(this.PagesParseLabel, 3);
      this.PagesParseLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PagesParseLabel.Location = new System.Drawing.Point(3, 60);
      this.PagesParseLabel.Name = "PagesParseLabel";
      this.PagesParseLabel.Size = new System.Drawing.Size(574, 30);
      this.PagesParseLabel.TabIndex = 6;
      this.PagesParseLabel.Text = "Парсинг страниц:";
      this.PagesParseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // PagesParseTypeComboBox
      // 
      this.PagesParseTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PagesParseTypeComboBox.FormattingEnabled = true;
      this.PagesParseTypeComboBox.Location = new System.Drawing.Point(3, 93);
      this.PagesParseTypeComboBox.Name = "PagesParseTypeComboBox";
      this.PagesParseTypeComboBox.Size = new System.Drawing.Size(284, 21);
      this.PagesParseTypeComboBox.TabIndex = 7;
      this.PagesParseTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.PagesParseTypeComboBox_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.SettingsTabControlsLayout.SetColumnSpan(this.label2, 3);
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(3, 120);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(574, 30);
      this.label2.TabIndex = 9;
      this.label2.Text = "Парсинг контента:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ParseContentCheckListBox
      // 
      this.SettingsTabControlsLayout.SetColumnSpan(this.ParseContentCheckListBox, 3);
      this.ParseContentCheckListBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ParseContentCheckListBox.FormattingEnabled = true;
      this.ParseContentCheckListBox.Location = new System.Drawing.Point(3, 153);
      this.ParseContentCheckListBox.Name = "ParseContentCheckListBox";
      this.ParseContentCheckListBox.Size = new System.Drawing.Size(574, 164);
      this.ParseContentCheckListBox.TabIndex = 10;
      // 
      // ContentExportTypeLabel
      // 
      this.ContentExportTypeLabel.AutoSize = true;
      this.SettingsTabControlsLayout.SetColumnSpan(this.ContentExportTypeLabel, 3);
      this.ContentExportTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ContentExportTypeLabel.Location = new System.Drawing.Point(3, 320);
      this.ContentExportTypeLabel.Name = "ContentExportTypeLabel";
      this.ContentExportTypeLabel.Size = new System.Drawing.Size(574, 30);
      this.ContentExportTypeLabel.TabIndex = 11;
      this.ContentExportTypeLabel.Text = "Экспорт контента:";
      this.ContentExportTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ContentExportTypeComboBox
      // 
      this.SettingsTabControlsLayout.SetColumnSpan(this.ContentExportTypeComboBox, 3);
      this.ContentExportTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ContentExportTypeComboBox.FormattingEnabled = true;
      this.ContentExportTypeComboBox.Location = new System.Drawing.Point(3, 353);
      this.ContentExportTypeComboBox.Name = "ContentExportTypeComboBox";
      this.ContentExportTypeComboBox.Size = new System.Drawing.Size(574, 21);
      this.ContentExportTypeComboBox.TabIndex = 12;
      // 
      // PagesParseTypeTextBox
      // 
      this.PagesParseTypeTextBox.AsciiOnly = true;
      this.PagesParseTypeTextBox.BeepOnError = true;
      this.PagesParseTypeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PagesParseTypeTextBox.Location = new System.Drawing.Point(293, 93);
      this.PagesParseTypeTextBox.Name = "PagesParseTypeTextBox";
      this.PagesParseTypeTextBox.Size = new System.Drawing.Size(284, 20);
      this.PagesParseTypeTextBox.TabIndex = 13;
      // 
      // DocumentationTab
      // 
      this.DocumentationTab.Controls.Add(this.DocumentationBrowser);
      this.DocumentationTab.Location = new System.Drawing.Point(4, 22);
      this.DocumentationTab.Name = "DocumentationTab";
      this.DocumentationTab.Padding = new System.Windows.Forms.Padding(3);
      this.DocumentationTab.Size = new System.Drawing.Size(586, 205);
      this.DocumentationTab.TabIndex = 2;
      this.DocumentationTab.Text = "Документация";
      this.DocumentationTab.UseVisualStyleBackColor = true;
      // 
      // DocumentationBrowser
      // 
      this.DocumentationBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DocumentationBrowser.Location = new System.Drawing.Point(3, 3);
      this.DocumentationBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.DocumentationBrowser.Name = "DocumentationBrowser";
      this.DocumentationBrowser.Size = new System.Drawing.Size(580, 199);
      this.DocumentationBrowser.TabIndex = 0;
      this.DocumentationBrowser.Url = new System.Uri("", System.UriKind.Relative);
      // 
      // HiddenBrowser
      // 
      this.HiddenBrowser.AllowWebBrowserDrop = false;
      this.HiddenBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
      this.HiddenBrowser.IsWebBrowserContextMenuEnabled = false;
      this.HiddenBrowser.Location = new System.Drawing.Point(5, 5);
      this.HiddenBrowser.MinimumSize = new System.Drawing.Size(20, 20);
      this.HiddenBrowser.Name = "HiddenBrowser";
      this.HiddenBrowser.ScriptErrorsSuppressed = true;
      this.HiddenBrowser.Size = new System.Drawing.Size(594, 231);
      this.HiddenBrowser.TabIndex = 1;
      this.HiddenBrowser.TabStop = false;
      this.HiddenBrowser.Visible = false;
      this.HiddenBrowser.WebBrowserShortcutsEnabled = false;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(604, 241);
      this.Controls.Add(this.Tabs);
      this.Controls.Add(this.HiddenBrowser);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.MinimumSize = new System.Drawing.Size(620, 280);
      this.Name = "MainForm";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "The Snatcher";
      this.Tabs.ResumeLayout(false);
      this.ParserTab.ResumeLayout(false);
      this.ParserTabControlsLayout.ResumeLayout(false);
      this.ParserTabControlsLayout.PerformLayout();
      this.SettingsTab.ResumeLayout(false);
      this.SettingsTabControlsLayout.ResumeLayout(false);
      this.SettingsTabControlsLayout.PerformLayout();
      this.DocumentationTab.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl Tabs;
    private System.Windows.Forms.TabPage ParserTab;
    private System.Windows.Forms.TabPage SettingsTab;
    private System.Windows.Forms.TabPage DocumentationTab;
    private System.Windows.Forms.TableLayoutPanel ParserTabControlsLayout;
    private System.Windows.Forms.Label ParserProgressLabel;
    private System.Windows.Forms.TableLayoutPanel SettingsTabControlsLayout;
    private System.Windows.Forms.Label TargetWebSiteLabel;
    private System.Windows.Forms.ComboBox TargetWebSiteComboBox;
    private System.Windows.Forms.Label TargetCategoryLabel;
    private System.Windows.Forms.ComboBox TargetCategoryComboBox;
    private System.Windows.Forms.ProgressBar ParseProgressBar;
    private System.Windows.Forms.ListBox JournalList;
    private System.Windows.Forms.Label PagesParseLabel;
    private System.Windows.Forms.ComboBox PagesParseTypeComboBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckedListBox ParseContentCheckListBox;
    private System.Windows.Forms.Label ContentExportTypeLabel;
    private System.Windows.Forms.ComboBox ContentExportTypeComboBox;
    private System.Windows.Forms.MaskedTextBox PagesParseTypeTextBox;
    private System.Windows.Forms.WebBrowser DocumentationBrowser;
    private System.Windows.Forms.Button StartParse;
    private System.Windows.Forms.Button StopParse;
    private System.Windows.Forms.WebBrowser HiddenBrowser;
  }
}

