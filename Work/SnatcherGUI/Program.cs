using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnatcherGUI {
  static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.SetHighDpiMode( HighDpiMode.SystemAware );
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault( false );
      Application.Run( new MainForm() );
      /*
       * 
       * private SCAppSettings VAppSettings = new SCAppSettings();
    private SCScripts VScriptsController = new SCScripts();

    private List<String> VParsePagesMasks;

    public MainForm() {
      InitializeComponent();

      ENErrorCode VLoadCode = VAppSettings.FLoad();

      if( VLoadCode != ENErrorCode.EC_OK ) {
        SCGlobal.FShowMessage( "Что-то пошло не так...", "Код - " + VLoadCode.ToString() );
        
        Application.Exit();
      }

      FSetupForm();

      FFIllSettingsControls();
    }

    private void FSetupForm() {
      UInt32[] VDefaultSize = VAppSettings.FGetProperty_DefaultWindowSize();

      MinimumSize = new Size( (Int32) VDefaultSize[0], ( Int32 ) VDefaultSize[ 1] );
      Size = new Size( ( Int32 ) VDefaultSize[ 0 ], ( Int32 ) VDefaultSize[ 1 ] );

      VParsePagesMasks = VAppSettings.FGetProperty_PagesParseMasks();
    }

    private void FFIllSettingsControls() {
      TargetWebSiteComboBox.Items.AddRange( VScriptsController.FGetScripts().ToArray() );

      PagesParseTypeComboBox.Items.AddRange( VAppSettings.FGetProperty_PagesParseTypes().ToArray() );

      if( PagesParseTypeComboBox.Items.Count == 0 ) {
        SCGlobal.FShowMessage( "Что-то пошло не так...", "Код - " + ENErrorCode.EC_HAS_BEEN_INCORRECT_FEELING );

        Application.Exit();
      }

      PagesParseTypeComboBox.SelectedIndex = 0;
    }

    private void FClearBeforeParse() {
      ParseProgressBar.Value = 0;
      JournalList.Items.Clear();
    }

    private void FLockTabsBeforeParse() {
      SettingsTab.Enabled = false;
      DocumentationTab.Enabled = false;
    }

    private void FUnlockTabsAfterParse() {
      SettingsTab.Enabled = true;
      DocumentationTab.Enabled = true;
    }

    private void StartParse_MouseClick( Object sender, MouseEventArgs e ) {
      FClearBeforeParse();
      FLockTabsBeforeParse();
    }

    private void StopParse_MouseClick( Object sender, MouseEventArgs e ) {
      FUnlockTabsAfterParse();
    }

    private void PagesParseTypeComboBox_SelectedIndexChanged( Object sender, EventArgs e ) {
      foreach( UInt32 Index in VAppSettings.FGetProperty_PagesParseDisablesMask() ) {
        if( Index == PagesParseTypeComboBox.SelectedIndex ) {
          PagesParseTypeTextBox.Enabled = false;
          return;
        }
      }

      if( PagesParseTypeComboBox.SelectedIndex != 0 ) {
        PagesParseTypeTextBox.Mask = VParsePagesMasks[ PagesParseTypeComboBox.SelectedIndex - 1 ];
      } else {
        PagesParseTypeTextBox.Mask = "";
      }

      PagesParseTypeTextBox.Enabled = true;
    }
       */
    }
  }
}
