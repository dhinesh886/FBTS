﻿#region File Header
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
    public class Currency
    {
       public string Code { set; get; }
       public string MajorDenomination { set; get; }
       public string MinorDenomination { set; get; }
       public string Symbol { set; get; }
       public string Factor { set; get; }
       public decimal CurrencyRate { set; get; }
    }
}