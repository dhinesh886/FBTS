#region File Header
// ----------------------------------------------------------------------------
// File Name    : LogSerializer.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : LogSerializer
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

namespace FBTS.Library.EventLogger.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class LogSerializer
    {
        private static object SyncObject = new object();

        /// <summary>
        /// Serializes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Serialize(string message)
        {
            lock (LogSerializer.SyncObject)
            {
                Logger.Info(message);
            }
        }

        /// <summary>
        /// Serializes the specified messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public static void Serialize(string[] messages)
        {
            lock (LogSerializer.SyncObject)
            {
                foreach (string log in messages)
                {
                    Logger.Info(log);
                }
            }
        }

        /// <summary>
        /// Serializes the specified session id.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <param name="messages">The messages.</param>
        public static void Serialize(string sessionId, string[] messages)
        {
            lock (LogSerializer.SyncObject)
            {
                Logger.Info(string.Format("Messages from {0}", sessionId));
                foreach (string log in messages)
                {
                    Logger.Info(log);
                }
            }
        }
    }
}
