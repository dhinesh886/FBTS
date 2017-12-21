#region File Header
// ----------------------------------------------------------------------------
// File Name    : DataViewTemplate.cs
// Namespace    : FBTS.Model.Control
// Class Name   : DataViewTemplate
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
    public class DataViewTemplate : Operations
    {
        public DataViewTemplate()
        {
            BasicInfo = new DataViewBasicInfo();
            SetupInfo = new DataViewSetupInfo();
        }
        public DataViewBasicInfo BasicInfo { get; set; }
        public DataViewSetupInfo SetupInfo { get; set; }
       

    }
}
