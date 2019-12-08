/*
  © 2019, Dark Orb.


  This license is hereby grants to any person who obtained a copy of this software the next rights to:

  - Use a compiled version of this software at no cost, without any restrictions, in non-commercial and commercial purposes
  - Use source codes of this software at no cost but with the limitations - source codes available only for academic and / or scientific purposes
  - Copy and distribute without any fee


  This license is require to:

  - Keep the full license text without any changes
  - The license text must be included once in a file called 'License' which placed in the root directory of the software and in all source files of the software


  This license is deny to:

  - Change license of the derivative software
  - Use the copyright holder name and name of any contributor of this software for advertising derivative software without legally certified permission
  - Sell this software
  - Do reverse-engineering
  - Create own derivative software


  The product is an original source codes and original compiled files which made by the original author and provided only under the grants and restrictions of this license.
*/



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SnatcherCore;
using SnatcherSharedFiles;

namespace SnatcherGUI {
  public partial class MainForm : Form {
    private SCAppSettings VAppSettings = new SCAppSettings();
    private SCScripts VScriptsController = new SCScripts();

    private List<String> VParsePagesMasks;
    private List<Tuple<String, String>> VWebSites;
    private List<Tuple<String, List<Tuple<String, String>>>> VCategories = new List<Tuple<String, List<Tuple<String, String>>>>(); 

    public MainForm() {
      InitializeComponent();

      ENErrorCode VLoadCode = VAppSettings.FLoad();

      if( VLoadCode != ENErrorCode.EC_OK ) {
        SCGlobal.FShowMessage( "Что-то пошло не так...", "Код - " + VLoadCode.ToString() );

        Application.Exit();
      }

      VScriptsController.FLoadScripts();

      VWebSites = VScriptsController.FGetScriptNames();

      foreach( String Script in VScriptsController.FGetScripts() ) {
        List<Tuple<String, String>> VScriptCategories = new List<Tuple<String, String>>();

        foreach( Tuple<String, String> ScriptCategory in VScriptsController.FGetScriptCategories( Script ) ) {
          VScriptCategories.Add( ScriptCategory );
        }

        VCategories.Add( new Tuple<String, List<Tuple<String, String>>>( Script, VScriptCategories ) );
      }

      FSetupForm();

      FFIllSettingsControls();
    }

    private void FSetupForm() {
      UInt32[] VDefaultSize = VAppSettings.FGetProperty_DefaultWindowSize();

      MinimumSize = new Size( ( Int32 ) VDefaultSize[ 0 ], ( Int32 ) VDefaultSize[ 1 ] );
      Size = new Size( ( Int32 ) VDefaultSize[ 0 ], ( Int32 ) VDefaultSize[ 1 ] );

      VParsePagesMasks = VAppSettings.FGetProperty_PagesParseMasks();
    }

    private void FFIllSettingsControls() {
      TargetWebSiteComboBox.Items.AddRange( VScriptsController.FGetOnlyScriptNames().ToArray() );
      
      TargetWebSiteComboBox.SelectedIndex = 0;

      foreach( Tuple<String, String> WebSiteCategories in VCategories[0].Item2 ) {
        TargetCategoryComboBox.Items.Add( WebSiteCategories.Item1 );
      }

      TargetCategoryComboBox.SelectedIndex = 0;

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

    private void TargetWebSiteComboBox_SelectedIndexChanged( Object sender, EventArgs e ) {

    }
  }
}
