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

using System.Runtime.Serialization;

namespace FBTS.Model.Common
{
    [DataContract]
    public class ObjectContainer
    {
        public ObjectContainer(KeyValuePairItems keyValuePairItems, DataBaseInfo dataBaseInfo)
        {
            KeyValuePairItems = keyValuePairItems;
            DataBaseInfo = dataBaseInfo;
        }
        [DataMember]
        public KeyValuePairItems KeyValuePairItems
        {
            get;
            set;
        }
        [DataMember]
        public DataBaseInfo DataBaseInfo
        {
            get;
            set;
        }
    }
}
