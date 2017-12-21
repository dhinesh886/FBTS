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
using FBTS.Model.Common;

namespace FBTS.Model.Control
{
    public class UserContext
    {
        public Guid UserId { get; set; }
        // USER INFORMATION                        
        public UserProfile UserProfile { set; get; }

        // COMPANY INFORMATION
        public CompanyProfile CompanyProfile { set; get; }

        // DATABASE INFORMATION
        public DataBaseInfo DataBaseInfo { get; set; }

        // CLIENT INFORMATION
        public ClientProfile ClientProfile { get; set; }

        // SMTP INFORMATION
        public SmtpInfo SmtpInfo { get; set; }

        // CURRENT DATE
        public DateTime CurrentDate { get; set; }
        // CURRENT DATE
        public DataViewSetupInfo Stages { get; set; }

        // MENUS
        public Menus Menus { set; get; }
    }
}