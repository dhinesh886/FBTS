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
using System.Linq;
using log4net.Ext.EventID;

namespace FBTS.Library.EventLogger.Logger
{
    public class LogEvent
    {
        private static LogEvent _instance;
        private static object syncObj = new object();

        private IEventIDLog _logger;
                
        public static LogEvent Instance
        {
            get { return _instance; }            
        }

        public string ApplicationName
        {
            get 
            {
                return ((log4net.Appender.EventLogAppender)
                    _logger.Logger.Repository.GetAppenders().Single(
                        a => a.Name == "EventLogAppender")
                    ).ApplicationName; 
            }
            set
            {
                ((log4net.Appender.EventLogAppender)
                  _logger.Logger.Repository.GetAppenders().Single(
                      a => a.Name == "EventLogAppender")
                  ).ApplicationName = value;
            }
        }

        /// <summary>
        /// static constructor loads log4net configuration from default web or app config
        /// </summary>
        public LogEvent()
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger = EventIDLogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// Initializes a new instance of the <see>
        ///         <cref>InstanceLogger</cref>
        ///     </see>
        ///     class.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        public LogEvent(string loggerName)
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger = EventIDLogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// Instantiates the specified logger name.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        public static void Initialize(string loggerName)
        {
            lock (syncObj)
            {
                if (null == _instance)
                {
                    _instance = new LogEvent(loggerName);
                }
            }
        }

        /// <summary>
        /// Gets the Logger.Log for this logging instance
        /// </summary>
        public IEventIDLog Log
        {
            get
            {
                return _logger;
            }
        }

        /// <summary>
        /// Log Debug Message
        /// </summary>
        /// <param name="message">Message</param>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }


        /// <summary>
        /// Log Debug Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args"></param>
        public void DebugFormat(string message, params object[] args)
        {
            _logger.DebugFormat(message, args);
        }

        /// <summary>
        /// Log Info Message
        /// </summary>
        /// <param name="message">Message</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Infoes the specified event id.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="message">The message.</param>
        public void Info(int eventId, string message)
        {
            _logger.Info(eventId, message);
        }

        /// <summary>
        /// Log Warn Message
        /// </summary>
        /// <param name="message">Message</param>
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Warns the specified event id.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="message">The message.</param>
        public void Warn(int eventId, string message)
        {
            _logger.Warn(eventId, message);
        }

        /// <summary>
        /// Log Warn Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        public void Warn(string message, Exception ex)
        {
            _logger.Warn(message, ex);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Warn(int eventId, string message, Exception ex)
        {
            _logger.Warn(eventId, message, ex);
        }

        /// <summary>
        /// Log Error Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        public void Error(string message, Exception ex)
        {
            _logger.Error(message, ex);
        }

        /// <summary>
        /// Errors the specified event id.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Error(int eventId, string message, Exception ex)
        {
            _logger.Error(eventId, message, ex);
        }

        /// <summary>
        /// Log Error Message
        /// </summary>
        /// <param name="message">Message</param>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(int eventId, string message)
        {
            _logger.Error(eventId, message);
        }

        /// <summary>
        /// Log Fatal Message
        /// </summary>
        /// <param name="message">Message</param>
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        /// <summary>
        /// Fatals the specified event id.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="message">The message.</param>
        public void Fatal(int eventId, string message)
        {
            _logger.Fatal(eventId, message);
        }

        /// <summary>
        /// Log Fatal Message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        public void Fatal(string message, Exception ex)
        {
            _logger.Fatal(message, ex);
        }

        public void Fatal(int eventId, string message, Exception ex)
        {
            _logger.Fatal(eventId, message, ex);
        }

        public static void SetProperty(string name, string value)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            {
                return;
            }

            log4net.GlobalContext.Properties[name] = value;
        }
    }
}

