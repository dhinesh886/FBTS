#region File Header
// ----------------------------------------------------------------------------
// File Name    : SyncLogger.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : SyncLogger
// Description  : 
//                
// Author       : DhineshKumar (dhinesh886@gmail.com)
// Created Date : Friday, October 23, 2009
// Updated By   : -
// Updated Date : -
// Company      : Copyright (c) 2014 Ezy Solutions Pvt Ltd.
//                
// Comments     : 
// ----------------------------------------------------------------------------
#endregion File Header

using System;
using System.Collections.Generic;
using ReportsOnly;

namespace FBTS.Library.EventLogger.Logger
{
    public class SyncLogger
    {
        public static object SyncObject = new object();
        public static bool EnableReportLog { get; set; }
        public static bool EnableExceptionLog { get; set; }
        public static bool EnableDumpStatistics { get; set; }

        protected ClientInfo ClientProfile { get; set; }
        
        private DateTime _startTime;
        private DateTime _endTime;
        private Exception _exception;
        private LogTasks _tasks;
        private LogFields _fields;
        private LogStatistics _statistics;

        static SyncLogger()
        {
            EnableReportLog = false;
            EnableExceptionLog = false;
            EnableDumpStatistics = false;
        }

        

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        /// <summary>
        /// Gets or sets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public LogTasks Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public LogFields Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }

        /// <summary>
        /// Gets or sets the statistics.
        /// </summary>
        /// <value>The statistics.</value>
        public LogStatistics Statistics
        {
            get { return _statistics; }
            set { _statistics = value; }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _startTime = DateTime.Now;
        }

        /// <summary>
        /// Ends this instance.
        /// </summary>
        public void End()
        {
            _endTime = DateTime.Now;
        }


        /// <summary>
        /// Initializes a new instance of the <see>
        ///         <cref>VceSearchLog</cref>
        ///     </see>
        ///     class.
        /// </summary>
        public SyncLogger(ClientInfo clientProfile) 
        {
            ClientProfile = clientProfile;
            Tasks = new LogTasks();
            Fields = new LogFields();
            Statistics = new LogStatistics();
            Start();
        }

        /// <summary>
        /// Creates the task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns></returns>
        public virtual LogTask CreateTask(string taskName)
        {
            LogTask task = new LogTask(taskName);
            Tasks.Add(task);
            return task;
        }

        /// <summary>
        /// Adds the field.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void AddField(string name, string value)
        {
            Fields.Add(new LogField(name, value));
        }
        /// <summary>
        /// Serializes this instance.
        /// </summary>
        public virtual void Serialize()
        {
            TimeSpan elapsed = EndTime.Subtract(StartTime);
            // log only if available
            if (!string.IsNullOrEmpty(ClientProfile.SessionId))
            {
                Logger.Info(string.Format("{0,-18} : {1}", "Id", ClientProfile.SessionId));
            }
            if (!string.IsNullOrEmpty(ClientProfile.UserId.ToString()))
            {
                Logger.Info(string.Format("{0,-18} : {1}", "UserId", ClientProfile.UserId));
            }
            if (!string.IsNullOrEmpty(ClientProfile.UserName))
            {
                Logger.Info(string.Format("{0,-18} : {1}", "UserName", ClientProfile.UserName));
            }
            if (!string.IsNullOrEmpty(ClientProfile.ClientApplication))
            {
                Logger.Info(string.Format("{0,-18} : {1}", "ClientApplication", ClientProfile.ClientApplication));
            }
            if (!string.IsNullOrEmpty(ClientProfile.ClientMachineName))
            {
                Logger.Info(string.Format("{0,-18} : {1}", "ClientMachineName", ClientProfile.ClientMachineName));
            } 

            if (!LogTask.ShowOnlyElapsedTime)
            {
                Logger.Info(string.Format("{0,-18} : {1}", "Start Time", StartTime.ToLongTimeString()));
                Logger.Info(string.Format("{0,-18} : {1}", "End Time", EndTime.ToLongTimeString()));
            }
            Logger.Info(string.Format("{0,-18} : {1} ms", "Elapsed Time", elapsed.TotalMilliseconds));

            Fields.ForEach(item =>
            {
                if (null != item)
                {
                    Logger.Info(string.Format("{0,-18} : {1}", item.Name, item.Value));
                }
            }
            );

            if (EnableReportLog)
            {
                // Serialize the logs for Test Reports
                LogField nameField = Fields.Find(item => String.Compare(item.Name, "UserName", StringComparison.OrdinalIgnoreCase) == 0);
                string userName = (null != nameField) ? nameField.Value : ClientProfile.SessionId;

                string reportLog = string.Format("{0,-40} | {1,-12} | {2,-12} | {3,-12}", userName,
                                                                    StartTime.ToLongTimeString(),
                                                                    EndTime.ToLongTimeString(),
                                                                    elapsed.TotalMilliseconds);
                ReportLog.Dump(" ");
                ReportLog.Dump(" ");
                ReportLog.Dump(reportLog);
            }

            if (EnableDumpStatistics)
            {
                CollectStatistics();
            }

            SerializeTasks();
        }

        private void CollectStatistics()
        {
            var tasks = new List<TaskStatistics>();
            
            // Collect statistics data for each task
            foreach (LogTask task in Tasks)
            {
                task.GetTasksStatistics(tasks);
            }

            if (tasks.Count != 0)
            {
                tasks.ForEach(item => Statistics.AddTask(item));
            }
        }


        /// <summary>
        /// Serializes the tasks.
        /// </summary>
        public virtual void SerializeTasks()
        {
            // Dump all tasks
            Tasks.ForEach(item => item.Serialize());
        }

        /// <summary>
        /// Dumps this instance.
        /// </summary>
        public void Dump()
        {
            End();

            lock (SyncObject)
            {
                Logger.Info(" ");
                Logger.Info("------------ Begin Status -------------");
                Serialize();
                Logger.Info("------------- End Status --------------");
                Logger.Info(" ");
                Logger.Info(" ");
            }
        }

        /// <summary>
        /// Dumps the specified statictics.
        /// </summary>
        /// <param name="statictics">The statictics.</param>
        public void Dump(LogStatistics statictics)
        {
            Statistics = statictics;
            Dump();
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Log(string message)
        {
            lock (SyncObject)
            {
                Logger.Info(message);
            }
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public static void LogException(string message, Exception ex)
        {
            lock (SyncObject)
            {
                Logger.Error(message, ex);
            }
        }
    }
}
