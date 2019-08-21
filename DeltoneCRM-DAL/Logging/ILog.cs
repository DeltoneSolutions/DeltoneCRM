using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
   
    public interface ILog
    {
        void Debug(object message);
        void Debug(object message, Exception exception);
        void DebugFormat(string format, params object[] args);
        void Info(object message);
        void Info(object message, Exception exception);
        void InfoFormat(string format, params object[] args);
        void Warn(object message);
        void Warn(object message, Exception exception);
        void WarnFormat(string format, params object[] args);
        void Error(object message, bool email = false);
        void Error(object message, Exception exception, bool email = false);
        void ErrorFormat(string format, params object[] args);
        void Fatal(object message, bool email = false);
        void Fatal(object message, Exception exception, bool email = false);
        void FatalFormat(string format, params object[] args);
    }
}
