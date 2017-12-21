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

namespace FBTS.Model.Control
{
    public class LoginUserData
    {
        public UserProfile UserProfile { get; set; }
        public CompanyProfile CompanyProfile { get; set; }
        public Menus Menus { get; set; }
        public bool IsAuthenticated { get; set; }
        public DateTime CurrentDate { set; get; }
    }
}