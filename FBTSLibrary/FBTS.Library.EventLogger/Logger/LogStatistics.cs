#region File Header
// ----------------------------------------------------------------------------
// File Name    : LogStatistics.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : LogStatistics
// Description  : 
//                
// Author       : DhineshKumar (dhinesh886@gmail.com)
// Created Date : Monday, May 21, 2010
// Updated By   : -
// Updated Date : -
// Company      : Copyright (c) 2014 Ezy Solutions Pvt Ltd.
//                
// Comments     : 
// ----------------------------------------------------------------------------
#endregion File Header

using System.Collections.Generic;

namespace FBTS.Library.EventLogger.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class LogStatistics : Dictionary<string, TaskStatistics>
    {
        private static object SyncObject = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="LogStatistics"/> class.
        /// </summary>
        public LogStatistics()
        {
        }
        
        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="task">The task.</param>
        public void AddTask(TaskStatistics task)
        {
            TaskStatistics existing = null;

            lock (LogStatistics.SyncObject)
            {
                if (this.TryGetValue(task.TaskName, out existing))
                {
                    existing.ElapsedTime += task.ElapsedTime;
                    if (task.FinishedAt > existing.FinishedAt)
                    {
                        existing.FinishedAt = task.FinishedAt;
                    }
                }
                else
                {
                    this.Add(task.TaskName, task);
                }
            }
        }

        /// <summary>
        /// Dumps the statistics.
        /// </summary>
        public void Dump()
        {
            if (this.Count == 0)
            {
                return;
            }

            Logger.Info("---------- Overall Run Status -------------");
            foreach (KeyValuePair<string, TaskStatistics> item in this)
            {
                Logger.Info(string.Format("{0,-18} : {1,-8:0.00}mts ({2})", item.Key, item.Value.ElapsedTime, item.Value.FinishedAt.ToShortTimeString()));
            }
            Logger.Info("-------------------------------------------");
        }
    }
}
