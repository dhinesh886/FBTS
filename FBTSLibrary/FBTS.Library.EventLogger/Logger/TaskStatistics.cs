#region File Header
// ----------------------------------------------------------------------------
// File Name    : TaskStatistics.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : TaskStatistics
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

using System;

namespace FBTS.Library.EventLogger.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskStatistics
    {
        private string _taskName;
        private double _elapsedTime;
        private DateTime _finishedAt;

        public string TaskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }

        public double ElapsedTime
        {
            get { return _elapsedTime; }
            set { _elapsedTime = value; }
        }
        
        public DateTime FinishedAt
        {
            get { return _finishedAt; }
            set { _finishedAt = value; }
        }

        public TaskStatistics()
        {
            TaskName = string.Empty;
            FinishedAt = DateTime.MinValue;
            ElapsedTime = 0.0;

        }
    }
}
