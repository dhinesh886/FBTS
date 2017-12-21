#region File Header
// ----------------------------------------------------------------------------
// File Name    : QueryArgument.cs
// Namespace    : FBTS.Model.Commom
// Class Name   : QueryArgument
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
    public class QueryArgument
    {
        public QueryArgument()
        {
            Filters = new KeyValuePairItems();
            DataBaseInfo = null;
            FilterKey = string.Empty;
            Keyword = string.Empty;
            SubFilterKey = string.Empty; 
            Sort= new StringList();
            Key = string.Empty;
            filter1 = string.Empty;
            filter2 = string.Empty;
            filter3 = string.Empty;
            filter4 = string.Empty;
            filter5 = string.Empty;
            filterDate = Convert.ToDateTime(Constants.DefaultDate);
        }
        public QueryArgument(DataBaseInfo dataBaseInfo)
        {
            Filters = new KeyValuePairItems();
            DataBaseInfo = dataBaseInfo;
            FilterKey = string.Empty;
            Keyword = string.Empty;
            SubFilterKey = string.Empty; 
            Sort = new StringList();
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FilterKey { get; set; }
        public string SubFilterKey { get; set; }
        public string SecondaryFilterKey { get; set; }
        public string Keyword { get; set; } 
        public KeyValuePairItems Filters { get; set; }
        public StringList Sort { get; set; }
        public BindType BindType { get; set; }
        public DDLTypes DdlType { get; set; }
        public string QueryType { get; set; }
        public DataBaseInfo DataBaseInfo { get; set; }
        public string Key { get; set; }
        public string filter1 { get; set; }
        public string filter2 { get; set; }
        public string filter3 { get; set; }
        public string filter4 { get; set; }
        public string filter5 { get; set; }
        public string filter6 { get; set; }
        public string filterType { get; set; }
        public string Action { get; set; }
        public DateTime filterDate { get; set; }
        public Guid UserId { get; set; }
    }
}
