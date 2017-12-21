#region File Header
// ----------------------------------------------------------------------------
// File Name    : ExceptionLog.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : ExceptionLog
// Description  : 
//                
// Author       : DhineshKumar (dhinesh886@gmail.com)
// Created Date : Monday, December 07, 2014
// Updated By   : -
// Updated Date : -
// Company      : Copyright (c) 2014 Ezy Solutions Pvt Ltd.
//                
// Comments     : 
// ----------------------------------------------------------------------------
#endregion File Header
using System;

namespace FBTS.Model.Common
{
    public class ClientProfile
    {
        public string SessionId { get; set; }
        public Guid UserId { get; set; }
        public string ClientApplication { get; set; }
        public string ClientMachineName { get; set; }
        public string ClientIpAddress { get; set; }
    }
}