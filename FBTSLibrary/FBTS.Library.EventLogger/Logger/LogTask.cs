#region File Header
// ----------------------------------------------------------------------------
// File Name    : LogTask.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : LogTask
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
using System.Threading;
using ExceptionsOnly;
using ReportsOnly;

namespace FBTS.Library.EventLogger.Logger
{
    public class LogTask
    {
        public static bool ShowOnlyElapsedTime = true;
        private static long _exceptionId;

        private string _name;
        private bool _isStarted;
        private DateTime _startTime;
        private DateTime _endTime;
        private LogFields _fields;
        private List<Exception> _exceptions;
        private LogTasks _subTasks;
        private List<string> _messages;

        public int EventIdForException { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        private LogFields Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public List<string> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }


        public List<Exception> Exceptions
        {
            get { return _exceptions; }
            set { _exceptions = value; }
        }

        public LogTasks SubTasks
        {
            get { return _subTasks; }
            set { _subTasks = value; }
        }

        public TimeSpan ElapsedTime
        {
            get
            {
                if (!_isStarted)
                {
                    return new TimeSpan(0);
                }

                return EndTime.Subtract(StartTime);
            }
        }

        private static long UniqueExceptionId
        {
            get { return Interlocked.Increment(ref _exceptionId); }
        }

        public virtual void AddField(string name, string field)
        {
            Fields.Add(new LogField(name, field));
        }

        public virtual void AddMessage(string name)
        {
            Messages.Add(name);
        }

        public virtual void AddException(Exception ex)
        {
            Exceptions.Add(ex);
        }

        public virtual void AddSubTask(LogTask task)
        {
            SubTasks.Add(task);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            _isStarted = true;
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
        /// Fails this instance.
        /// </summary>
        public void Fail()
        {
            _endTime = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogTask"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LogTask(string name)
        {
            Name = name;
            Messages = new List<string>();
            SubTasks = new LogTasks();
            Fields = new LogFields();
            Exceptions = new List<Exception>();
            Start();
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        public virtual void Serialize()
        {
            if (!_isStarted)
            {
                return;
            }

            TimeSpan elapsed = EndTime.Subtract(StartTime);

            Logger.Info(".......................................");
            Logger.Info(string.Format("{0,-18} : {1}", "Task Name", Name));
            if (!ShowOnlyElapsedTime)
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

            SerializeMessages();

            if (SyncLogger.EnableReportLog)
            {
                // Serialize the logs for Test Reports
                string reportLog = string.Format("{0,-40} | {1,-12} | {2,-12} | {3,-12}", Name,
                                                                    StartTime.ToLongTimeString(),
                                                                    EndTime.ToLongTimeString(),
                                                                    elapsed.TotalMilliseconds);
                ReportLog.Dump(reportLog);
            }

            SubTasks.ForEach(item => item.Serialize());
        }

        /// <summary>
        /// Serializes the messages.
        /// </summary>
        public virtual void SerializeMessages()
        {
            if (!_isStarted)
            {
                return;
            }

            // Log all Project Ids
            foreach (string msg in Messages)
            {
                Logger.Info(string.Format("{0,-18} : {1}", " ", msg));
            }

            // Log Exception if any
            if (null != Exceptions)
            {
                foreach (Exception ex in Exceptions)
                {
                    if (SyncLogger.EnableExceptionLog)
                    {
                        long exceptionId = UniqueExceptionId;
                        string exception = string.Format("**Exception({0})**", exceptionId);
                        Logger.Info(string.Format("{0,-18} : {1}", exception, ex.Message));
                        ExceptionLog.Dump(exceptionId, ex);
                    }
                    else
                    {
                        Logger.Info(string.Format("{0,-18} : {1})", "**Exception**", ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// Finds the specified task name.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns></returns>
        public virtual LogTask Find(string taskName)
        {
            foreach (LogTask task in SubTasks)
            {
                if (String.Compare(task.Name, taskName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return task;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the tasks statistics.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        public void GetTasksStatistics(List<TaskStatistics> tasks)
        {
            if (!_isStarted)
            {
                return;
            }
            var thisTask = new TaskStatistics
            {
                TaskName = Name,
                FinishedAt = EndTime,
                ElapsedTime = ElapsedTime.TotalMinutes
            };
            tasks.Add(thisTask);

            SubTasks.ForEach(item => item.GetTasksStatistics(tasks));
        }
    }
}
