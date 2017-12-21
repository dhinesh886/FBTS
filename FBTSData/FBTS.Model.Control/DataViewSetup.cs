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

using FBTS.Model.Common;

namespace FBTS.Model.Control
{
    public class DataViewSetup: Operations
    {
        public DataViewSetup()
        {
            Referances = new WFComponentSubs();
        }
        public string Id { get; set; }
        public string DataType { get; set; }
        public string Stage { get; set; }
        public string Field { get; set; }
        public string FieldWidth { get; set; }
        public string FieldType { get; set; }
        public string FieldDescription { get; set; }
        public string ActionLink { get; set; }
        public string Relation1 { get; set; }
        public string Relation2 { get; set; }
        public bool OpenNewWindow { get; set; }
        public WFComponentSubs Referances { get; set; }
        public bool Suspend { get; set; }
    }
}
