using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
    public class LogManager
    {
        public static ILog GetLogger(Type type)
        {
            return new Logger(type);
        }

        public static ILog GetLogger(string name)
        {
            return new Logger(name);
        }
    }

    public class Logger : ILog
    {
        readonly log4net.ILog _logger;
        static string _header = string.Empty;
        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public Logger(string name)
        {
            _logger = log4net.LogManager.GetLogger(name);
        }

        public Logger(Type type)
        {
            _logger = log4net.LogManager.GetLogger(type);
        }

        #region ILog Members



        public void Debug(object message)
        {
            message = _header + message;
            _logger.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            message = _header + message;
            _logger.Debug(message, exception);
        }

        public void DebugFormat(string format, params object[] args)
        {
            format = _header + format;
            _logger.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            message = _header + message;
            _logger.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            message = _header + message;
            _logger.Info(message, exception);
        }

        public void InfoFormat(string format, params object[] args)
        {
            format = _header + format;
            _logger.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            message = _header + message;
            _logger.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            message = _header + message;
            _logger.Warn(message, exception);
        }

        public void WarnFormat(string format, params object[] args)
        {
            format = _header + format;
            _logger.WarnFormat(format, args);
        }

        public void Error(object message, bool email = false)
        {
            message = _header + message;
            _logger.Error(message);
        }

        public void Error(object message, Exception exception, bool email = false)
        {
            message = _header + message;
            _logger.Error(message, exception);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            format = _header + format;
            _logger.ErrorFormat(format, args);
        }

        public void Fatal(object message, bool email = false)
        {
            message = _header + message;
            _logger.Fatal(message);
        }

        public void Fatal(object message, Exception exception, bool email = false)
        {
            message = _header + message;
            _logger.Fatal(message, exception);
        }

        public void FatalFormat(string format, params object[] args)
        {
            format = _header + format;
            _logger.FatalFormat(format, args);
        }

     


        #endregion


       

    }

}
