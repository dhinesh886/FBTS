#region File Header
// ----------------------------------------------------------------------------
// File Name    : GuideTaskFactory.cs
// Namespace    : Onvia.Glengarry.Gfx.DataFeed
// Class Name   : GuideTaskFactory
// Description  : 
//                
// Author       : Meenakshi Bisht (meenakshib@aditi.com)
// Created Date : Wednesday, February 06, 2013
// Updated By   : 
// Updated Date : 
// Company      : Copyright (c) 2014 Ezy Solutions Pvt Ltd.
//                
// Comments     : This file is used for cleaning up log files based on creation date.
// ----------------------------------------------------------------------------
#endregion File Header

using System;
using System.IO;
using System.Linq;
using log4net;
using log4net.Appender;

namespace FBTS.Library.EventLogger.Logger
{
    public class LogFileManager
    {
        #region - Constructor -
        public LogFileManager()
        {
        }
        #endregion

        #region - Methods -
        /// <summary>
        /// Cleans up. Auto configures the cleanup based on the log4net configuration
        /// </summary>
        /// <param name="date">Anything prior will not be kept.</param>
        public void CleanUp(DateTime date)
        {
            string directory = string.Empty;
            string filePrefix = string.Empty;

            var repo = LogManager.GetAllRepositories().FirstOrDefault(); ;
            if (repo == null)
                throw new NotSupportedException("Log4Net has not been configured yet.");

            var app = repo.GetAppenders().Where(x => x.GetType() == typeof(RollingFileAppender));            
            if (app != null)
            {
                foreach (var item in app)
                {
                    var appender = item as RollingFileAppender;

                    directory = Path.GetDirectoryName(appender.File);
                    filePrefix = Path.GetFileName(appender.File);

                    CleanUp(directory, filePrefix, date);
                }                
            }
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="logDirectory">The log directory.</param>
        /// <param name="logPrefix">The log prefix. Example: logfile dont include the file extension.</param>
        /// <param name="date">Anything prior will not be kept.</param>
        public void CleanUp(string logDirectory, string logPrefix, DateTime date)
        {
            if (string.IsNullOrEmpty(logDirectory))
                throw new ArgumentException("logDirectory is missing");

            if (string.IsNullOrEmpty(logDirectory))
                throw new ArgumentException("logPrefix is missing");

            var dirInfo = new DirectoryInfo(logDirectory);
            if (!dirInfo.Exists)
                return;

            var fileInfos = dirInfo.GetFiles(string.Format("{0}*.*",logPrefix));
            if (fileInfos.Length == 0)
                return;

            foreach (var info in fileInfos)
            {
                if (string.Compare(info.Name, logPrefix, true) != 0)
                {
                    if (info.LastWriteTime < date)
                    {
                        info.Delete();
                    }
                }
            }

        }
        #endregion
    }
}
