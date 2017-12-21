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
    public class CompanyProfile :Operations
    {
        public string Address{get; set;}
        public string CEmail{get; set;}
        public string CfName{get; set;}
        public string CName{get; set;}
        public string CPhone{get; set;}
        public string CType{get; set;}
        public string City{get; set;}
        public string Country{get; set;}
        public DateTime Created{get; set;}
        public string Email{get; set;}
        public string Fax{get; set;}
        public DateTime? FinancialYearEnd{get; set;}
        public DateTime? FinancialYearStart{get; set;}
        public string Id{get; set;}
        public int? Longitude{get; set;}
        public int? Latitude{get; set;}
        public string Logo{get; set;}
        public string LogoHeight{get; set;}
        public string LogoWidth{get; set;}
        public string Name{get; set;}
        public string Mobile { get; set; }
        public int NoofBranches{get; set;}
        public int NoofDepartments{get; set;}
        public string Off{get; set;}
        public string Phone{get; set;}
        public string ProductKey{get; set;}
        public string SmtpHostIn{get; set;}
        public int? SmtpHostInPort{get; set;}
        public string SmtpHostOut{get; set;}
        public int? SmtpHostOutPort{get; set;}
        public string SmtpPassword{get; set;}
        public string SmtpUserName{get; set;}
        public bool? SslEnabled{get; set;}
        public string State{get; set;}
        public bool Suspend{get; set;}
        public string Tax1{get; set;}
        public string Tax2{get; set;}
        public string Tax3{get; set;}
        public string Tax4{get; set;}
        public string TaxValidity1 { get; set; }
        public string TaxValidity2 { get; set; }
        public string TaxValidity3 { get; set; }
        public string TaxValidity4 { get; set; }
        public string TrnCurrency{get; set;}
        public DateTime Updated{get; set;}
        public string Website{get; set;}
        public string Zip { get; set; }
        public string DateFormat { get; set; }
        public decimal CurRate { get; set; }
        public string CountryName { get; set; }
        public CompanyProfile()
        {
            
            CType = string.Empty;
            Id = string.Empty;
            Name = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Country = string.Empty;
            Zip = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
            Email = string.Empty;
            Website = string.Empty;
            FinancialYearStart = DateTime.MinValue;
            FinancialYearEnd = DateTime.MinValue;
            Created = DateTime.Now;
            Updated = DateTime.Now;
            NoofBranches = 0;
            NoofDepartments = 0;
            CfName = string.Empty;
            ProductKey = string.Empty;
            Tax1 = string.Empty;
            Tax2 = string.Empty;
            Tax3 = string.Empty;
            Tax4 = string.Empty;
            TaxValidity1 = string.Empty;
            TaxValidity2 = string.Empty;
            TaxValidity3 = string.Empty;
            TaxValidity4 = string.Empty;
            Logo = string.Empty;
            LogoHeight = string.Empty;
            LogoWidth = string.Empty;
            Longitude = 0;
            Latitude = 0;
            CName = string.Empty;
            CPhone = string.Empty;
            CEmail = string.Empty;
            TrnCurrency = string.Empty;
            Off = string.Empty;
            Suspend = false;

            SmtpHostIn = string.Empty;
            SmtpHostInPort = 0;
            SmtpHostOut = string.Empty;
            SmtpHostOutPort = 0;
            SmtpUserName = string.Empty;
            SmtpPassword = string.Empty;
            SslEnabled = false;
            CurRate = 0;
            CountryName = string.Empty;
        }
        
    }
}