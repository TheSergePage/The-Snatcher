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
using System.Text;

namespace SnatcherSharedFiles {
  public enum ENErrorCode {
    EC_FILE_HAS_NOT_BEEN_LOADED = Int32.MinValue,
    EC_FILE_HAS_EMPTY_NAME,
    EC_FILE_HAS_TOO_LONG_NAME,
    EC_FILE_NOT_FOUND,
    EC_INCORRECT_FILE_CONTENT,
    EC_PERHAPS_READ_ONLY_FILE,

    EC_INVALID_PATH,

    EC_REQUIRED_START_APP_WITH_ADMINISTRATOR_PRIVILEGES,

    EC_EMPTY_EVENT_MESSAGE,

    EC_STREAM_IS_CLOSED,
    EC_STREAM_CURSOR_AT_THE_END,

    EC_COMMON_IO_PROBLEM,

    EC_HAS_BEEN_INCORRECT_FEELING,

    EC_OK = 0
  }
}
