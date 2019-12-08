﻿using System;
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
