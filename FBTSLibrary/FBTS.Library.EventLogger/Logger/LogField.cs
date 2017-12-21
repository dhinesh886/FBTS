#region File Header
// ----------------------------------------------------------------------------
// File Name    : LogField.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : LogField
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

namespace FBTS.Library.EventLogger.Logger
{
    public class LogField
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public LogField()
        {
        }

        public LogField(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
