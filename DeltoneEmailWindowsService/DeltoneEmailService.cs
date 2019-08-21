using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DeltoneEmailWindowsService
{
    public partial class DeltoneEmailService : ServiceBase
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DeltoneEmailService));
        readonly Timer _timer;
        bool _processing;
        bool _error;
        private DateTime _lastMessageSendTry;
        private readonly int _messageSendInterval;
        private readonly int _maxQueue;
        private readonly int _retryOnErrorInterval;
        public DeltoneEmailService()
        {
            InitializeComponent();
            _lastMessageSendTry = DateTime.Now;
            _messageSendInterval = 30000;
            _retryOnErrorInterval = 60000;

            _processing = false;
            _error = false;

            _timer = new Timer(_messageSendInterval);
            _timer.Elapsed += HandleTimer;
            _timer.Enabled = false;
        }

        protected override void OnStart(string[] args)
        {
            _error = false;
            StartQueueTimer();
            //    _logger.Info("Started service.");
            _logger.Info(" Service started");
            base.OnStart(args);
        }

        protected void HandleTimer(object sender, ElapsedEventArgs e)
        {
            Process();
        }

        private void DoTask()
        {
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                new OrderSendEmailDAL(connString).RunPurchaseOrderEmail();
            }
            catch (Exception ex)
            {
                _logger.Error(" Email send error" + ex.Message);
            }
        }

        void Process()
        {
            if (_processing)
                return;
            _processing = true;
            try
            {
                StopQueueTimers();
                if (!_error)
                    DoTask();


            }
            catch (Exception e)
            {
                _error = true;

                // _logger.Fatal("Error occurred on message sending service", e);
            }
            finally
            {
                _processing = false;
                try { StartQueueTimer(); }
                catch (Exception ex)
                {
                    // _logger.Fatal("Hell is on.... Timer did not start...", ex);
                }
            }
        }

        protected override void OnStop()
        {
            StopQueueTimers();
            //   _logger.Info("Stopped service.");
            _logger.Info(" Service stopped");
            base.OnStop();
        }
        void StartQueueTimer()
        {
            StopQueueTimers();
            _timer.Start();
        }
        void StopQueueTimers()
        {
            _timer.Stop();
        }
    }
}
