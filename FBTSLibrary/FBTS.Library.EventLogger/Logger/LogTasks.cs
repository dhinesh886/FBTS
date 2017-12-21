#region File Header
// ----------------------------------------------------------------------------
// File Name    : LogTasks.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : LogTasks
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

using System.Collections.Generic;

namespace FBTS.Library.EventLogger.Logger
{
    public class LogTasks : List<LogTask>
    {
        public LogTasks()
            : base()
        {
        }

        /// <summary>
        /// Serializes this instance.
        /// </summary>
        public void Serialize()
        {
            foreach (LogTask task in this)
            {
                task.Serialize();
            }
        }

        public LogTask CreateNewTask(string name)
        {
            LogTask task = new LogTask(name);
            this.Add(task);
            return task;
        }
    }
}
