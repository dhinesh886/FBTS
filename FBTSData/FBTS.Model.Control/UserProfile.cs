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
    public class UserProfile : Operations
    {
        public DateTime? ActiveTill{ get; set;}
        public string Address{ get; set;}
        public string Avatar{ get; set;}
        public string Bu{ get; set;}
        public string Branch{ get; set;}
        public string City{ get; set;}
        public string Country{ get; set;}
        public DateTime Created{ get; set;}
        public DateTime? Dob{ get; set;}
        public string DefaultLink{ get; set;}
        public string Dept{ get; set;}
        public Designation Designation { get; set; }
        public string Domain{ get; set;}
        public string DomainUser{ get; set;}
        public string Email{ get; set;}
        public string EmpId{ get; set;}
        public string Gender{ get; set;} 
        public string Mobile{ get; set;}
        public string Name{ get; set;}
        public string Node{ get; set;}
        public string Off{ get; set;}
        public string OffPhone{ get; set;}
        public string Password{ get; set;}
        public Guid? ReportingTo{ get; set;}
        public string ResPhone{ get; set;}
        public string SocialNetwork1{ get; set;}
        public string SocialNetwork2{ get; set;}
        public string SocialNetwork3{ get; set;}
        public string State{ get; set;}
        public bool? Suspend{ get; set;}
        public string LoginId{ get; set;}
        public Guid UCode{ get; set;}
        public string UType{ get; set;}
        public DateTime Updated { get { return DateTime.Now; } }
        public string Wh{ get; set;}
        public string Zip{ get; set;}
        public DateTime LastPasswordChanged { get; set; }
        public string DateFormat { get; set; }
        public UserProfile()
        {
            LoginId = string.Empty;
            UCode = Guid.Empty;
            UType = string.Empty;
            Designation = null;
            Name = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Country = string.Empty;
            Zip = string.Empty;
            Dob = Convert.ToDateTime(Constants.DefaultDate);
            OffPhone = string.Empty;
            ResPhone = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            ReportingTo = Guid.Empty;
            Bu = string.Empty;
            Branch = string.Empty;
            Wh = string.Empty;
            Dept = string.Empty;
            DefaultLink = string.Empty;
            Password = string.Empty;
            Node = string.Empty;
            Domain = string.Empty;
            DomainUser = string.Empty;
            ActiveTill = Convert.ToDateTime(Constants.DefaultDate);
            Created = Convert.ToDateTime(Constants.DefaultDate);            
            LastPasswordChanged = DateTime.MinValue;
            Gender = string.Empty;
            Avatar = string.Empty;
            EmpId = string.Empty;
            SocialNetwork1 = string.Empty;
            SocialNetwork2 = string.Empty;
            SocialNetwork3 = string.Empty;
            Suspend = false;
            Off = string.Empty;
        }

        public string ControlDbServer { get; set; }
        public string DbServer { get; set; }
        public string DbName { get; set; }
        public string DbUserid { get; set; }
        public string DbPassword { get; set; }
    }
}