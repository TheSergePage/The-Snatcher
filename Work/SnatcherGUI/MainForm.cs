using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SnatcherCore;
using SnatcherSharedFiles;

using MoonSharp.Interpreter;
using System.Threading;
using AngleSharp;
using AngleSharp.Dom;
using System.IO;
using System.Text.RegularExpressions;

namespace SnatcherGUI {
  public partial class MainForm : Form {
    private SCAppSettings VAppSettings = new SCAppSettings();
    private SCScripts VScriptsController = new SCScripts();

    private Script VScriptEngine;

    private ResourceManager VDocumentationResources = new System.Resources.ResourceManager( "SnatcherCore.Documentation", Assembly.LoadFrom( @"SnatcherCore.dll" ) );

    private List<String> VParsePagesMasks;
    private List<Tuple<String, String>> VWebSites;
    private List<Tuple<String, List<Tuple<String, String>>>> VCategories = new List<Tuple<String, List<Tuple<String, String>>>>();
    private List<Tuple<String, List<Tuple<String, String>>>> VContents = new List<Tuple<String, List<Tuple<String, String>>>>();

    private DynValue VScriptResult;

    private static String VExportDirectoryPath = SCGlobal.VAppDirectoryPath + Path.DirectorySeparatorChar + "export" + Path.DirectorySeparatorChar;

    private class SnatcherFunctional {
      private MainForm VParent;

      private WebBrowser VHiddenBrowser;
      private ListBox VJournalList;
      private ProgressBar VProgressBar;

      private String VWebSiteData = "";
      private String VQueryData = "";
      private List<String> VQueryDataCollection = new List<string>();

      public SnatcherFunctional( MainForm _Parent, WebBrowser _ParentBrowser, ListBox _ParentJournalList, ProgressBar _ParentProgressBar ) {
        VParent = _Parent;

        VHiddenBrowser = _ParentBrowser;
        VHiddenBrowser.ObjectForScripting = this;

        VJournalList = _ParentJournalList;
        VProgressBar = _ParentProgressBar;
      }

      private void FWaitUntilLoading() {
        while( VHiddenBrowser.ReadyState != WebBrowserReadyState.Complete ) {
          Application.DoEvents();
        }
      }
      
      public void FLoadWebSite( DynValue _Address ) {
        if( _Address.String.Length == 0 ) {
          return;
        }

        VHiddenBrowser.Navigate(_Address.String);

        FWaitUntilLoading();

        VWebSiteData = VHiddenBrowser.DocumentText;
      }

      public DynValue FGetWebSiteData() {
        return DynValue.NewString(VWebSiteData);
      }

      public async void FExtractData( DynValue _Data, DynValue _Query, DynValue _QueryType, DynValue _IsOuterData, DynValue _IsTextPresent ) {
        IDocument VDocument = await BrowsingContext.New( Configuration.Default ).OpenAsync( req => req.Content( _Data.String ) );

        try {
          switch( _QueryType.Number ) {
            case 0:
            if( _IsOuterData.Boolean ) {
              if( _IsTextPresent.Boolean ) {
                VQueryData = VDocument.QuerySelector( _Query.String ).TextContent;
              } else {
                VQueryData = VDocument.QuerySelector( _Query.String ).OuterHtml;
              }
            } else {
              if( _IsTextPresent.Boolean ) {
                VQueryData = VDocument.QuerySelector( _Query.String ).TextContent;
              } else {
                VQueryData = VDocument.QuerySelector( _Query.String ).InnerHtml;
              }
            }
            break;

            case 1:
            VQueryDataCollection.Clear();

            foreach( IElement Element in VDocument.QuerySelectorAll( _Query.String ).ToList() ) {
              if( _IsOuterData.Boolean ) {
                if( _IsTextPresent.Boolean ) {
                  VQueryDataCollection.Add( Element.TextContent );
                } else {
                  VQueryDataCollection.Add( Element.OuterHtml );
                }
              } else {
                if( _IsTextPresent.Boolean ) {
                  VQueryDataCollection.Add( Element.TextContent );
                } else {
                  VQueryDataCollection.Add( Element.InnerHtml );
                }
              }
            }
            break;
          }
        } catch(Exception ) {
          VQueryData = "";
          VQueryDataCollection.Clear();
        }
      }

      public async void FWaiting( DynValue _Time ) {
        await Task.Delay( (Int32) _Time.Number );
      }

      public DynValue FExecuteJavaScript( DynValue _ScriptName, DynValue _ScriptBody, DynValue _Type ) {
        HtmlDocument VWebDocument = VHiddenBrowser.Document;

        HtmlElement VHeadSection = VWebDocument.GetElementsByTagName( "head" )[ 0 ];
        HtmlElement VTemporaryScriptElement = VWebDocument.CreateElement( "script" );

        VTemporaryScriptElement.Id = "@" + _ScriptName.String;
        VTemporaryScriptElement.InnerText = _ScriptBody.String;

        VHeadSection.AppendChild( VTemporaryScriptElement );

        DynValue VResult = DynValue.NewString("");

        object VInvokeResult = VHiddenBrowser.Document.InvokeScript( _ScriptName.String );

        if( VInvokeResult == null ) {
          return DynValue.NewString( "" );
        }

        switch( _Type.Number ) {
          case 0:
          VResult = DynValue.NewString( VInvokeResult.ToString() );
          break;

          case 1:
          VResult = DynValue.NewNumber( Int32.Parse( VInvokeResult.ToString() ) );
          break;

          case 2:
          VResult = DynValue.NewBoolean( Boolean.Parse( VInvokeResult.ToString() ) );
          break;
        }

        VWebDocument.GetElementById( VTemporaryScriptElement.Id ).OuterHtml = "";

        return VResult;
      }

      public DynValue FGetQueryData() {
        return DynValue.NewString(VQueryData);
      }

      public List<DynValue> FGetQueryDataCollection() {
        List < DynValue > VResult = new List<DynValue>();

        foreach( String Data in VQueryDataCollection ) {
          VResult.Add( DynValue.NewString( Data ) );
        }

        return VResult;
      }

      public DynValue FIsEmptyQueryDataCollection() {
        if( VQueryDataCollection.Count == 0 ) {
          return DynValue.NewBoolean( true );
        }

        return DynValue.NewBoolean( false );
      }

      public void FAddEvent( DynValue _Message ) {
        VJournalList.Items.Add( _Message.String );
      }

      public void FClearJournalList() {
        VJournalList.Items.Clear();
      }

      public void FSetProgressBarValue( DynValue _Value ) {
        VProgressBar.Value = (Int32) _Value.Number;
      }

      public void FSetMaxBarValue( DynValue _Value ) {
        VProgressBar.Maximum = ( Int32 ) _Value.Number;
      }

      public void FStopParse() {
        VParent.FUnlockTabsAfterParse();

        VParent.VScriptEngine = null;

        VParent.FSetupScriptEngine();
      }

      public void FDebugMessageBox_String( DynValue _Message ) {
        MessageBox.Show( _Message.CastToString(), "Отладочное сообщение", MessageBoxButtons.OK );
      }

      public void FDebugMessageBox_Tuple( List<DynValue> _Messages ) {
        String VResult = "";

        if( _Messages == null ) {
          return;
        }

        foreach( DynValue Message in _Messages ) {
          VResult += Message.CastToString() + "\n";
        }

        MessageBox.Show( VResult, "Отладочное сообщение", MessageBoxButtons.OK );
      }

      private String FSerializeTable( Table _Table ) {
        String VResult = "";

        if( _Table == null ) {
          return VResult;
        }

        foreach( DynValue Message in _Table.Values ) {
          if( Message.Type == DataType.Table ) {
            VResult += FSerializeTable( Message.Table );
          }

          VResult += Message.CastToString() + "\n";
        }

        return VResult;
      }

      public void FDebugMessageBox_Table( Table _Table ) {
        String VResult = "";

        if( _Table == null ) {
          return;
        }

        VResult = FSerializeTable( _Table );

        MessageBox.Show( VResult, "Отладочное сообщение", MessageBoxButtons.OK );
      }
    }

    public MainForm() {
      InitializeComponent();

      UserData.RegisterType<SnatcherFunctional>();

      FSetupScriptEngine();

      ENErrorCode VLoadCode = VAppSettings.FLoad();

      if( VLoadCode != ENErrorCode.EC_OK ) {
        SCGlobal.FShowMessage( "Что-то пошло не так...", "Код - " + VLoadCode.ToString() );

        Application.Exit();
      }

      VScriptsController.FLoadScripts();

      VWebSites = VScriptsController.FGetScriptNames();

      foreach( String Script in VScriptsController.FGetScripts() ) {
        List<Tuple<String, String>> VScriptCategories = new List<Tuple<String, String>>();
        List<Tuple<String, String>> VScriptContents = new List<Tuple<String, String>>();

        foreach( Tuple<String, String> ScriptCategory in VScriptsController.FGetScriptCategories( Script ) ) {
          VScriptCategories.Add( ScriptCategory );
        }

        foreach( Tuple<String, String> ScriptContent in VScriptsController.FGetScriptContent( Script ) ) {
          VScriptContents.Add( ScriptContent );
        }

        VCategories.Add( new Tuple<String, List<Tuple<String, String>>>( Script, VScriptCategories ) );
        VContents.Add( new Tuple<String, List<Tuple<String, String>>>( Script, VScriptContents ) );
      }

      FSetupForm();

      FFillSettingsControls();
    }

    private void FSetupScriptEngine() {
      VScriptEngine = new Script();

      VScriptEngine.Globals.Set("SnatcherFunctional", UserData.Create( new SnatcherFunctional( this, HiddenBrowser, JournalList, ParseProgressBar ) ));
    }

    private void FSetupForm() {
      UInt32[] VDefaultSize = VAppSettings.FGetProperty_DefaultWindowSize();

      MinimumSize = new Size( ( Int32 ) VDefaultSize[ 0 ], ( Int32 ) VDefaultSize[ 1 ] );
      Size = new Size( ( Int32 ) VDefaultSize[ 0 ], ( Int32 ) VDefaultSize[ 1 ] );
    }

    private void FFillSettingsControls() {
      TargetWebSiteComboBox.Items.AddRange( VScriptsController.FGetOnlyScriptNames().ToArray() );
      
      TargetWebSiteComboBox.SelectedIndex = 0;

      PagesParseTypeComboBox.Items.AddRange( VAppSettings.FGetProperty_PagesParseTypes().ToArray() );

      if( PagesParseTypeComboBox.Items.Count == 0 ) {
        SCGlobal.FShowMessage( "Что-то пошло не так...", "Код - " + ENErrorCode.EC_HAS_BEEN_INCORRECT_FEELING );

        Application.Exit();
      }

      PagesParseTypeComboBox.SelectedIndex = 0;

      VParsePagesMasks = VAppSettings.FGetProperty_PagesParseMasks();

      ContentExportTypeComboBox.Items.AddRange( VAppSettings.FGetProperty_ExportContentFormats().ToArray() );

      ContentExportTypeComboBox.SelectedIndex = 0;

      DocumentationBrowser.Navigate( "about:blank" );

      if( DocumentationBrowser.Document != null ) {
        DocumentationBrowser.Document.Write( String.Empty );
      }

      DocumentationBrowser.DocumentText = VDocumentationResources.GetString( "Page_Blank" );
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

    private List<UInt32> FGetPagesRange() {
      List<UInt32> VResult = new List<UInt32>();

      foreach( String VRange in PagesParseTypeTextBox.Text.Split( ' ' ) ) {
        UInt32 VParsedRange;

        if( UInt32.TryParse( VRange, out VParsedRange ) ) {
          VResult.Add( VParsedRange );
        }
      }

      return VResult;
    }

    private void StartParse_MouseClick( Object sender, MouseEventArgs e ) {
      FClearBeforeParse();
      FLockTabsBeforeParse();

      VScriptEngine.DoString(VScriptsController.FGetScript(FGetWebSiteAddress(TargetWebSiteComboBox.SelectedItem.ToString())));

      List<DynValue> VSelectedContent = new List<DynValue>(),
        VChosenPagesRange = new List<DynValue>();

      String VWebSiteHuminizedName = TargetWebSiteComboBox.SelectedItem.ToString();

      foreach( String Content in ParseContentCheckListBox.CheckedItems) {
        String VContentIdentifier = FGetContentIdentifier( VWebSiteHuminizedName, Content );
        VSelectedContent.Add( DynValue.NewString( VContentIdentifier ) );
      }

      if( PagesParseTypeTextBox.Text.Length != 0 ) {
        List<UInt32> VPagesRange = FGetPagesRange();

        if( VPagesRange.Count == 1 ) {
          VChosenPagesRange.Add( DynValue.NewNumber( VPagesRange[0] ) );
        } else if( VPagesRange.Count == 2 ) {
          VChosenPagesRange.Add( DynValue.NewNumber( VPagesRange[0] ) );
          VChosenPagesRange.Add( DynValue.NewNumber( VPagesRange[1] ) );
        }
      }

      VScriptResult = VScriptEngine.Call( VScriptEngine.Globals.Get("FStart"), new DynValue[] { 
        // _BaseUrl
        DynValue.NewString(VScriptsController.FGetScriptInformation(FGetWebSiteAddress(TargetWebSiteComboBox.SelectedItem.ToString())).Item2),

        // _Category
        DynValue.NewString(FGetCategoryAddress(FGetWebSiteAddress(TargetWebSiteComboBox.SelectedItem.ToString()), TargetCategoryComboBox.SelectedItem.ToString())),

        // _PagesRange
        DynValue.NewTable(VScriptEngine, VChosenPagesRange.ToArray()),

        // _Content
        DynValue.NewTable(VScriptEngine, VSelectedContent.ToArray())
      });

      FExportScriptResult();

      FUnlockTabsAfterParse();

      VScriptEngine = null;

      FSetupScriptEngine();
    }

    private void FExportScriptResult() {
      StreamWriter VResultWriter;
      
      switch(ContentExportTypeComboBox.SelectedItem) {
        case "CSV":
        VResultWriter = new StreamWriter( VExportDirectoryPath + "result " + DateTime.UtcNow.ToString( "[H-m-s] [d-M-yyyy]" ) + ".csv" );

        String VHeader = "";

        foreach( String Content in ParseContentCheckListBox.CheckedItems ) {
          VHeader += "\"" + Content + "\",";
        }

        VHeader = VHeader.Remove( VHeader.Length - 1 );
        VHeader += "\n";

        List<String> VDataList = new List<String>();

        for( UInt32 c = 0; c < VScriptResult.Table.Length; c++ ) {
          String VDataRow = "";

          foreach( String Content in ParseContentCheckListBox.CheckedItems ) {
            String VContentIdentifier = FGetContentIdentifier( TargetWebSiteComboBox.SelectedItem.ToString(), Content );

            DynValue VTableValue = VScriptResult.Table.Get( c ).Table.Get( VContentIdentifier );

            if( VTableValue == null || VTableValue.IsNil() ) {
              MessageBox.Show( "Не найдено значение в таблице результата скрипта - " + VContentIdentifier, "Экспорт", MessageBoxButtons.OK );

              FClearData();

              Application.Exit();
            } else if(VTableValue != null && VTableValue.IsNotNil()) {
              VDataRow += "\"" + Regex.Replace( VScriptResult.Table.Get( c ).Table[ VContentIdentifier ].ToString(), @"\s+", " " ) + "\",";
            }
          }

          VDataRow = VDataRow.Remove( VDataRow.Length - 1 );
          VDataRow += "\n";

          VDataList.Add( VDataRow );
        }

        VResultWriter.AutoFlush = true;

        VResultWriter.Write( VHeader );

        foreach( String DataItem in VDataList ) {
          VResultWriter.Write( DataItem );
        }

        VResultWriter.Close();

        MessageBox.Show( "Экспорт окончен.\nРезультат можно посмотреть в папке 'export'", "Экспорт", MessageBoxButtons.OK );

        FClearData();
        break;
      }
    }

    private void FClearData() {
      VScriptResult = null;

      FUnlockTabsAfterParse();

      VScriptEngine = null;

      FSetupScriptEngine();
    }

    private void StopParse_MouseClick( Object sender, MouseEventArgs e ) {
      FUnlockTabsAfterParse();

      VScriptEngine = null;

      FSetupScriptEngine();
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

    private String FGetContentIdentifier( String _WebSiteHumanizedName, String _ContentHumanizedName ) {
      if( _WebSiteHumanizedName.Length == 0 ||  _ContentHumanizedName.Length == 0 ) {
        return "";
      }

      String VWebSizeRawName = FGetWebSiteAddress( _WebSiteHumanizedName );

      foreach( Tuple<String, List<Tuple<String, String>>> WebSiteContent in VContents ) {
        if( VWebSizeRawName == WebSiteContent.Item1 ) {
          foreach( Tuple<String, String> Content in WebSiteContent.Item2 ) {
            if( _ContentHumanizedName == Content.Item2 ) {
              return Content.Item1;
            }
          }

          return "";
        }
      }

      return "";
    }

    private String FGetCategoryAddress( String _WebSiteRawName, String _CategoryHumanizedName ) {
      if( _WebSiteRawName.Length == 0 || _CategoryHumanizedName.Length == 0 ) {
        return "";
      }

      foreach( Tuple<String, List<Tuple<String, String>>> WebSiteCategories in VCategories ) {
        if( WebSiteCategories.Item1 == _WebSiteRawName ) {
          foreach( Tuple<String, String> Category in WebSiteCategories.Item2 ) {
            if( Category.Item1 == _CategoryHumanizedName ) {
              return Category.Item2;
            }
          }
        }
      }

      return "";
    }

    private String FGetWebSiteAddress( String _HumanizedName ) {
      if( _HumanizedName.Length == 0 ) {
        return "";
      }

      foreach( Tuple<String, String> WebSite in VWebSites ) {
        if( WebSite.Item2 == _HumanizedName ) {
          return WebSite.Item1;
        }
      }

      return "";
    }

    private void TargetWebSiteComboBox_SelectedIndexChanged( Object sender, EventArgs e ) {
      TargetCategoryComboBox.Items.Clear();
      ParseContentCheckListBox.Items.Clear();

      String VRawCurrentSiteName = FGetWebSiteAddress(TargetWebSiteComboBox.SelectedItem.ToString());

      Boolean VIsFoundCategory = false, VIsFoundContent = false;

      foreach( Tuple<String, List<Tuple<String, String>>> WebSiteCategories in VCategories ) {
        if( VRawCurrentSiteName == WebSiteCategories.Item1 ) {
          foreach( Tuple<String, String> Category in WebSiteCategories.Item2 ) {
            TargetCategoryComboBox.Items.Add( Category.Item1 );
          }

          if( TargetCategoryComboBox.Items.Count != 0 ) {
            TargetCategoryComboBox.SelectedIndex = 0;
          }

          VIsFoundCategory = true;

          break;
        }
      }

      foreach( Tuple<String, List<Tuple<String, String>>> WebSiteContent in VContents ) {
        if( VRawCurrentSiteName == WebSiteContent.Item1 ) {
          foreach( Tuple<String, String> Content in WebSiteContent.Item2 ) {
            ParseContentCheckListBox.Items.Add( Content.Item2 );
          }

          VIsFoundContent = true;

          break;
        }
      }

      if( VIsFoundCategory == true || VIsFoundContent == true ) {
        return;
      }
    }
  }
}
