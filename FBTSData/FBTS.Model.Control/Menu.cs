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
    public class Menu : Operations
    {
        public string MenuId { get; set; }
        public string MenuCode { get; set; }
        public string MenuName { get; set; }
        public string ModuleName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuTarget { get; set; }
        public int MenuLevel { get; set; } 
        public int MenuSlno { get; set; }
        public int MenuSequence { get; set; }
        public decimal MenuOrder { get; set; }
        public bool MenuAvailable { get; set; }
        public string MenuIcon { get; set; }
        public string MenuSettings { get; set; }
        public Menus SubMenus { get; set; }

        public Menu()
        {
            SubMenus = new Menus();
        }
    }
}