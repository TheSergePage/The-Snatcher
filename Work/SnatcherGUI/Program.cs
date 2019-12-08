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
    }
  }
}
