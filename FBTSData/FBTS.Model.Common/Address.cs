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
    [Serializable]
    public class Address
    {
       public string Street { set; get; }
       public string City { set; get; }
       public string State { set; get; }
       public string Country { set; get; }
       public string ZipCode { set; get; }
       public string Mobile { set; get; }
       public string Email { set; get; }
       public string Phone { set; get; }
       public string WebSite { set; get; }
       public string GST { set; get; }
       public string GSTNAReason { set; get; }
        public string ToDisplayFormat()
        {
            return Street + Constants.SpecialCharComma + City + Constants.SpecialCharComma +
                   State + Constants.SpecialCharComma + Country + Constants.SpecialCharHifen + ZipCode;
        }
    }
}