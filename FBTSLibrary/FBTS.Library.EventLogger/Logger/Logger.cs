#region File Header
// ----------------------------------------------------------------------------
// File Name    : Logger.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : Logger
// Description  : 
//                
// Author       : DhineshKumar (dhinesh886@gmail.com)
// Created Date : Monday, May 25, 2009
// Updated By   : -
// Updated Date : -
// Company      : Copyright (c) 2014 Ezy Solutions Pvt Ltd.
//                
// Comments     : 
// ----------------------------------------------------------------------------
#endregion File Header

using System;
using log4net;
using log4net.Ext.EventID;

namespace FBTS.Library.EventLogger.Logger
{
    static public class Logger
    {
// ReSharper disable InconsistentNaming
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
// ReSharper restore InconsistentNaming
        private static readonly IEventIDLog EventLogger = EventIDLogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// static constructor loads log4net configuration from default web or app config
        /// </summary>
        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        /// <summary>
        /// Gets the Logger.Log for this logging instance
        /// </summary>
        static public ILog Log
        {
            get
            {
                return logger;
            }
        }

        /// <summary>
        /// Log Debug Message
        /// </summary>
        /// <param name="message">Message</param>
        static public void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Log Debug Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args"></param>
        static public void DebugFormat(string message, params object[] args)
        {
            logger.DebugFormat(message, args);
        }

        /// <summary>
        /// Log Info Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args"></param>
        static public void InfoFormat(string message, params object[] args)
        {
            logger.InfoFormat(message,args);
        }

        /// <summary>
        /// Log Info Message
        /// </summary>
        /// <param name="message">Message</param>
        static public void Info(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Log Info Message with event id
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="message">Message</param>
        static public void Info(int eventId, string message)
        {
            EventLogger.Info(eventId, message);
        }

        /// <summary>
        /// Log Warn Message
        /// </summary>
        /// <param name="message">Message</param>
        static public void Warn(string message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// Log Warn Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        static public void Warn(string message, Exception ex)
        {
            logger.Warn(message, ex);
        }

        /// <summary>
        /// Log Error Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        static public void Error(string message, Exception ex)
        {
            logger.Error(message, ex);
        }

        /// <summary>
        /// Log Error Message
        /// </summary>
        /// <param name="message">Message</param>
        static public void Error(string message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Log Error Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args"></param>
        static public void ErrorFormat(string message, params object[] args)
        {
            logger.ErrorFormat(message, args);
        }

        /// <summary>
        /// Log Fatal Message
        /// </summary>
        /// <param name="message">Message</param>
        static public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        /// <summary>
        /// Log Fatal Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        static public void Fatal(string message, Exception ex)
        {
            logger.Fatal(message, ex);
        }

        /// <summary>
        /// Logs Fatal message.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        static public void Fatal(int eventId, string message, Exception ex)
        {
            EventLogger.Fatal(eventId, message, ex);
        }

        public static void SetProperty(string name, string value)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return;
            }

            GlobalContext.Properties[name] = value;
        }

        /// <summary>
        /// This method deletes the logs older than <c>days</c> days
        /// </summary>
        /// <param name="days">The number of days that should remain. This should be a negative number.</param>
        public static void DeleteOldLog(int days)
        {
            //XmlConfigurator.Configure();
            var date = DateTime.Now.AddDays(days);
            var task = new LogFileManager();
            task.CleanUp(date);
        }
    }
}

