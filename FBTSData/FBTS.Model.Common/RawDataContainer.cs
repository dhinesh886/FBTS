#region File Header
// ----------------------------------------------------------------------------
// File Name    : RawDataContainer.cs
// Namespace    : FBTS.Model.Commom
// Class Name   : RawDataContainer
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

using System.Data;
using System.Runtime.Serialization;

namespace FBTS.Model.Common
{
    [DataContract]
    public class RawDataContainer
    {
        public RawDataContainer(DataTable result)
        {
            Result = result;
        }
        [DataMember]
        public DataTable Result
        {
            get;
            set;
        }

        public int TotalResults { get; set; }
        public int CurrentResultsCount { get; set; }
    }
}
