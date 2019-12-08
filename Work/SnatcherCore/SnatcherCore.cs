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
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows.Forms;
using System.Xml;

using SnatcherSharedFiles;

using MoonSharp.Interpreter;

namespace SnatcherCore
{
  public static class SCGlobal {
    static SCGlobal() {
      VJournal = new SCJournal();
    }

    /*
     * Reusable variables
     */

    public static SCJournal VJournal;

    /*
     * Directories
     */

    public static String VAppDirectoryPath {
      get {
        return Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
      }
    }

    public static String VCoreDirectoryPath {
      get {
        return VAppDirectoryPath + "core" + Path.DirectorySeparatorChar;
      }
    }

    public static String VScriptsDirectoryPath {
      get {
        return VCoreDirectoryPath + "scripts" + Path.DirectorySeparatorChar;
      }
    }

    public static String VJournalStorageDirectoryPath {
      get {
        return VCoreDirectoryPath + "storages" + Path.DirectorySeparatorChar + "journals" + Path.DirectorySeparatorChar;
      }
    }

    /*
     * Files
     */

    public static String VAppSettingsFilePath {
      get {
        return VCoreDirectoryPath + "settings.xml";
      }
    }

    public static String VScriptsCollectionDescriptorFilePath {
      get {
        return VScriptsDirectoryPath + "collection.lua";
      }
    }

    /*
     * Extensions
     */

    public static String VJournalFileExtension {
      get {
        return ".journal";
      }
    }

    /*
     * Utilities
     */

    public static void FShowMessage( String _Title, String _Message ) {
      MessageBox.Show(_Message, _Title, MessageBoxButtons.OK);
    }
  }

  public class SCScripts {
    private List<String> VRawScripts = new List<String>();

    public void FLoadScripts() {
      foreach(String File in Script.RunString( new StreamReader( SCGlobal.VScriptsCollectionDescriptorFilePath ).ReadToEnd() ).ToObject<List<String>>() ) {
        VRawScripts.Add( File );
      }
    }

    public List<String> FGetScripts() {
      return VRawScripts;
    }

    public String FGetScript( String _Name ) {
      foreach( String RawScript in VRawScripts ) {
        if( RawScript == _Name ) {
          return new StreamReader( SCGlobal.VScriptsDirectoryPath + RawScript + Path.DirectorySeparatorChar + "parse.lua" ).ReadToEnd();
        }
      }

      return "";
    }

    public List<Tuple<String, String>> FGetScriptNames() {
      List<Tuple<String, String>> VResult = new List<Tuple<String, String>>();

      foreach( String RawScript in VRawScripts ) {
        VResult.Add( new Tuple<String, String>(RawScript, FGetScriptInformation( RawScript ).Item1) );
      }

      return VResult;
    }

    public List<String> FGetOnlyScriptNames() {
      List<String> VResult = new List<String>();

      foreach( String RawScript in VRawScripts ) {
        VResult.Add( FGetScriptInformation( RawScript ).Item1 );
      }

      return VResult;
    }

    public Tuple<String, String> FGetScriptInformation( String _Name ) {
      foreach( String RawScript in VRawScripts ) {
        if( RawScript == _Name ) {
          DynValue VInformationFunction = Script.RunString( new StreamReader( SCGlobal.VScriptsDirectoryPath + RawScript + Path.DirectorySeparatorChar + "information.lua" ).ReadToEnd() );

          return new Tuple<String, String>( VInformationFunction.Table[ "Name" ].ToString(), VInformationFunction.Table[ "Address" ].ToString() );
        }
      }

      return new Tuple<String, String>("", "");
    }

    public List<Tuple<String, String>> FGetScriptCategories( String _Name ) {
      List<Tuple<String, String>> VResult = new List<Tuple<String, String>>();

      foreach(String RawScript in VRawScripts ) {
        if( RawScript == _Name ) {
          DynValue VInformationFunction = Script.RunString( new StreamReader( SCGlobal.VScriptsDirectoryPath + RawScript + Path.DirectorySeparatorChar + "categories.lua" ).ReadToEnd() );

          for( UInt32 c = 0; c < VInformationFunction.Table.Length; c++ ) {
            VResult.Add( new Tuple<String, String>( VInformationFunction.Table.Get(c).Table["Title"].ToString(), VInformationFunction.Table.Get( c ).Table[ "Address" ].ToString() ) );
          }
        }
      }

      return VResult;
    }

    public List<Tuple<String, String>> FGetScriptContent( String _Name ) {
      List<Tuple<String, String>> VResult = new List<Tuple<String, String>>();

      foreach( String RawScript in VRawScripts ) {
        if( RawScript == _Name ) {
          DynValue VInformationFunction = Script.RunString( new StreamReader( SCGlobal.VScriptsDirectoryPath + RawScript + Path.DirectorySeparatorChar + "content.lua" ).ReadToEnd() );

          for( UInt32 c = 0; c < VInformationFunction.Table.Length + 1; c++ ) {
            VResult.Add( new Tuple<String, String>( VInformationFunction.Table.Get( c ).Table[ "Identifier" ].ToString(), VInformationFunction.Table.Get( c ).Table[ "Name" ].ToString() ) );
          }
        }
      }

      return VResult;
    }
  }

  public class SCAppSettings
  {
    private XmlDocument VSettings = new XmlDocument();
    private XmlElement VRoot;

    public ENErrorCode FLoad() {
      List<Tuple<String, String>>[] VExceptionLists = new List<Tuple<String, String>>[2];
      ENErrorCode VResultCode = ENErrorCode.EC_OK;

      try {
        VSettings.Load( SCGlobal.VAppSettingsFilePath );
      } catch( XmlException _Exception ) {
        /*
         * There is a load or parse error in the XML. In this case, a System.IO.FileNotFoundException is raised.
         */

        VExceptionLists[ 0 ] = SCJournal.FSimpleFormattedEventHeader( GetType().FullName );
        VExceptionLists[ 1 ] = SCJournal.FXmlFormattedEventBody( ( UInt32 ) _Exception.LineNumber, ( UInt32 ) _Exception.LinePosition, _Exception.Message, _Exception.StackTrace, _Exception.Data.ToString() );
        
        VResultCode = ENErrorCode.EC_FILE_HAS_NOT_BEEN_LOADED;
      } catch( ArgumentNullException _Exception ) {
        /*
         * Filename is null.
         */

        VExceptionLists[ 0 ] = SCJournal.FSimpleFormattedEventHeader( GetType().FullName );
        VExceptionLists[ 1 ] = SCJournal.FClassicFormattedEventBody( _Exception.Message, _Exception.StackTrace, _Exception.Data.ToString() );

        VResultCode = ENErrorCode.EC_FILE_HAS_EMPTY_NAME;
      } catch( PathTooLongException _Exception ) {
        /*
         * The specified path, file name, or both exceed the system-defined maximum length.
         */

        VExceptionLists[ 0 ] = SCJournal.FSimpleFormattedEventHeader( GetType().FullName );
        VExceptionLists[ 1 ] = SCJournal.FClassicFormattedEventBody( _Exception.Message, _Exception.StackTrace, _Exception.Data.ToString() );

        VResultCode = ENErrorCode.EC_FILE_HAS_TOO_LONG_NAME;
      } catch( DirectoryNotFoundException _Exception ) {
        /*
         * The specified path is invalid (for example, it is on an unmapped drive).
         */

        VExceptionLists[ 0 ] = SCJournal.FSimpleFormattedEventHeader( GetType().FullName );
        VExceptionLists[ 1 ] = SCJournal.FClassicFormattedEventBody( _Exception.Message, _Exception.StackTrace, _Exception.Data.ToString() );

        VResultCode = ENErrorCode.EC_INVALID_PATH;
      } catch( UnauthorizedAccessException _Exception ) {
        /*
         * Filename specified a file that is read-only or this operation is not supported on the current platform or filename specified a directory or the caller does not have the required permission.
         */

        VExceptionLists[ 0 ] = SCJournal.FSimpleFormattedEventHeader( GetType().FullName );
        VExceptionLists[ 1 ] = SCJournal.FClassicFormattedEventBody( _Exception.Message, _Exception.StackTrace, _Exception.Data.ToString() );

        VResultCode = ENErrorCode.EC_PERHAPS_READ_ONLY_FILE;
      } catch( FileNotFoundException _Exception ) {
        /*
         * The file specified in filename was not found.
         */

        VExceptionLists[ 0 ] = SCJournal.FSimpleFormattedEventHeader( GetType().FullName );
        VExceptionLists[ 1 ] = SCJournal.FClassicFormattedEventBody( _Exception.Message, _Exception.StackTrace, _Exception.Data.ToString() );

        VResultCode = ENErrorCode.EC_FILE_NOT_FOUND;
      } catch( NotSupportedException _Exception ) {
        /*
         * Filename is in an invalid format.
         */

        VExceptionLists[ 0 ] = SCJournal.FSimpleFormattedEventHeader( GetType().FullName );
        VExceptionLists[ 1 ] = SCJournal.FClassicFormattedEventBody( _Exception.Message, _Exception.StackTrace, _Exception.Data.ToString() );

        VResultCode = ENErrorCode.EC_INCORRECT_FILE_CONTENT;
      } catch( SecurityException _Exception ) {
        /*
         * The caller does not have the required permission.
         */

        VExceptionLists[ 0 ] = SCJournal.FSimpleFormattedEventHeader( GetType().FullName );
        VExceptionLists[ 1 ] = SCJournal.FClassicFormattedEventBody( _Exception.Message, _Exception.StackTrace, _Exception.Data.ToString() );

        VResultCode = ENErrorCode.EC_REQUIRED_START_APP_WITH_ADMINISTRATOR_PRIVILEGES;
      }

      if( VExceptionLists[ 0 ] != null && VExceptionLists[ 1 ] != null ) {
        SCGlobal.VJournal.FAppendEvent( VExceptionLists[ 0 ], VExceptionLists[ 1 ] );
      } else {
        SCGlobal.VJournal.FAppendEvent( SCJournal.FSimpleFormattedEventHeader( "SCAppSettings::FLoad" ), SCJournal.FSimpleFormattedEventHeader( "FLoad has been successful passed" ) );
      }

      VRoot = VSettings.DocumentElement;

      return VResultCode;
    }

    public UInt32[] FGetProperty_DefaultWindowSize() {
      return new UInt32[ 2 ] {
        UInt32.Parse(FGetPropertyValueByIdentifier("pDefaultWindowSize_Width", "620")), 
        UInt32.Parse(FGetPropertyValueByIdentifier("pDefaultWindowSize_Height", "280"))
      };
    }

    public List<String> FGetProperty_PagesParseTypes() {
      List<String> VResult = new List<String>();

      foreach( String Format in FGetPropertyValueByIdentifier( "pPagesParse_Types" ).Split( '|' ) ) {
        VResult.Add( Format );
      }

      return VResult;
    }

    public List<String> FGetProperty_PagesParseMasks() {
      List<String> VResult = new List<String>();

      foreach( String Format in FGetPropertyValueByIdentifier( "pPagesParse_Masks" ).Split( '|' ) ) {
        VResult.Add( Format );
      }

      return VResult;
    }

    public List<UInt32> FGetProperty_PagesParseDisablesMask() {
      List<UInt32> VResult = new List<UInt32>();

      foreach( String Format in FGetPropertyValueByIdentifier( "pPagesParse_DisablesMask" ).Split( '|' ) ) {
        if( Format.Length != 0 ) {
          VResult.Add( UInt32.Parse( Format ) );
        }
      }

      return VResult;
    }

    public List<String> FGetProperty_ExportContentFormats() {
      List<String> VResult = new List<String>();

      foreach( String Format in FGetPropertyValueByIdentifier( "pExportFormats" ).Split('|') ) {
        VResult.Add( Format );
      }

      return VResult;
    }

    private String FGetPropertyValueByIdentifier( String _Identifier, String _DefaultValue = "" ) {
      String VResult = _DefaultValue;

      if( _Identifier.Length == 0 ) {
        return VResult;
      }

      foreach( XmlNode Node in VRoot.ChildNodes ) {
        if( Node.Attributes != null  ) {
          if( Node.Attributes[ "id" ].Value == _Identifier ) {
            return Node.Attributes[ "value" ].Value;
          }
        }
      }

      return VResult;
    }
  }

  public class SCJournal {
    private UInt32 VEventSectionIndent = 4, VEventPropertyIndent = 6;

    protected StreamWriter VJournalWriter = new StreamWriter( SCGlobal.VJournalStorageDirectoryPath + "journal " + DateTime.UtcNow.ToString( "[H-m-s] [d-M-yyyy]" ) + SCGlobal.VJournalFileExtension );

    public SCJournal() {
      Console.WriteLine( SCGlobal.VJournalStorageDirectoryPath );
      VJournalWriter.AutoFlush = true;
    }

    public ENErrorCode FAppendEvent( String _Event ) {
      if( _Event.Length == 0 ) {
        return ENErrorCode.EC_EMPTY_EVENT_MESSAGE;
      }

      try {
        VJournalWriter.Write( _Event );
      } catch( IOException ) {
        /*
         * An I/O error occurs.
         */
        return ENErrorCode.EC_COMMON_IO_PROBLEM;
      } catch( ObjectDisposedException ) {
        /*
         * System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer is full, and current writer is closed.
         */
        return ENErrorCode.EC_STREAM_IS_CLOSED;
      } catch( NotSupportedException ) {
        /*
         * System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the System.IO.StreamWriter is at the end the stream.
         */
        return ENErrorCode.EC_STREAM_CURSOR_AT_THE_END;
      }

      return ENErrorCode.EC_OK;
    }

    public ENErrorCode FAppendEvent( List<Tuple<String, String>> _Header, List<Tuple<String, String>> _Body ) {
      String VBuffer = "Event::\n" + FGenerateIndent( VEventSectionIndent ) + "Header [\n" + FExpandSection(_Header) + "]\n" + FGenerateIndent( VEventSectionIndent ) + "Body [\n" + FExpandSection(_Body) + "]\n";

      // VBuffer is equal of "Event::\n" (8)
      if( VBuffer.Length == 8 ) {
        return ENErrorCode.EC_EMPTY_EVENT_MESSAGE;
      }

      try {
        VJournalWriter.Write( VBuffer );
      } catch( IOException ) {
        /*
         * An I/O error occurs.
         */
        return ENErrorCode.EC_COMMON_IO_PROBLEM;
      } catch( ObjectDisposedException ) {
        /*
         * System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer is full, and current writer is closed.
         */
        return ENErrorCode.EC_STREAM_IS_CLOSED;
      } catch( NotSupportedException ) {
        /*
         * System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer is full, and the contents of the buffer cannot be written to the underlying fixed size stream because the System.IO.StreamWriter is at the end the stream.
         */
        return ENErrorCode.EC_STREAM_CURSOR_AT_THE_END;
      }

      return ENErrorCode.EC_OK;
    }

    private String FExpandSection( List<Tuple<String, String>> _Section ) {
      String VBuffer = "";

      if( _Section.Count != 0 ) {
        foreach( Tuple<String, String> Property in _Section ) {
          VBuffer += FGenerateIndent( VEventPropertyIndent ) + Property.Item1 + " -> " + Property.Item2 + "\n";
        }
      }

      return VBuffer;
    }

    public static String FGenerateIndent( UInt32 _Quantity ) {
      String VResult = "";

      for( UInt32 c = 0; c < _Quantity; c++ ) {
        VResult += " ";
      }

      return VResult;
    }

    public static List<Tuple<String, String>> FSimpleFormattedEventHeader( String _Title, [CallerFilePath] String _CallerPath = "", [CallerLineNumber] Int32 _CallerLine = 0 ) {
      return new List<Tuple<String, String>> {
        new Tuple<String, String>( "pTitle", _Title ),

        new Tuple<String, String>( "pCallerPath", _CallerPath ),
        new Tuple<String, String>( "pCallerLine", _CallerLine.ToString() ),

        new Tuple<String, String>( "pTime", DateTime.UtcNow.ToLongDateString() )
      };
    }

    public static List<Tuple<String, String>> FSimpleFormattedEventBody( String _Message ) {
      return new List<Tuple<String, String>> {
        new Tuple<String, String>( "pMessage", _Message )
      };
    }

    public static List<Tuple<String, String>> FClassicFormattedEventBody( String _Message, String _Stack, String _Data ) {
      return new List<Tuple<String, String>> {
        new Tuple<String, String>( "pMessage", _Message ),

        new Tuple<String, String>( "pStack", _Stack ),
        new Tuple<String, String>( "pData", _Data )
      };
    }

    public static List<Tuple<String, String>> FXmlFormattedEventBody( UInt32 _LineNumber, UInt32 _LinePosition, String _Message, String _Stack, String _Data ) {
      return new List<Tuple<String, String>> {
        new Tuple<String, String>( "pLineNumber", _LineNumber.ToString() ),
        new Tuple<String, String>( "pLinePosition", _LinePosition.ToString() ),

        new Tuple<String, String>( "pMessage", _Message ),

        new Tuple<String, String>( "pStack", _Stack ),
        new Tuple<String, String>( "pData", _Data )
      };
    }
  }
}
