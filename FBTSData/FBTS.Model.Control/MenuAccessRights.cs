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
    [Serializable]
    public class MenuAccessRights : Operations
    {
        public Guid UserId{ get; set;}
        public string AccessLevelId{ get; set;}
        public Menus AccessRights { get; set; }
        public MenuAccessRights()
        {
           AccessRights = new Menus();
        }
 
    }
}