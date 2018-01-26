﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveDomain.Core.Logging
{
    public class NullLogger : ILogger
    {
        public void Flush(TimeSpan? maxTimeToWait = null) { }
        public LogLevel LogLevel => LogLevel.Fatal;
        public void Fatal(string text) { }
        public void Error(string text) { }
        public void Info(string text) { }
        public void Debug(string text) { }
        public void Trace(string text) { }
        public void Fatal(string format, params object[] args) { }
        public void Error(string format, params object[] args) { }
        public void Info(string format, params object[] args) { }
        public void Debug(string format, params object[] args) { }
        public void Trace(string format, params object[] args) { }
        public void FatalException(Exception exc, string text) { }
        public void ErrorException(Exception exc, string text) { }
        public void InfoException(Exception exc, string text) { }
        public void DebugException(Exception exc, string text) { }
        public void TraceException(Exception exc, string text) { }
        public void FatalException(Exception exc, string format, params object[] args) { }
        public void ErrorException(Exception exc, string format, params object[] args) { }
        public void InfoException(Exception exc, string format, params object[] args) { }
        public void DebugException(Exception exc, string format, params object[] args) { }
        public void TraceException(Exception exc, string format, params object[] args) { }

    }
}
