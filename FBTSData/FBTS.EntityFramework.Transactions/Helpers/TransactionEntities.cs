#region File Header
// ----------------------------------------------------------------------------
// File Name    : TransactionEntities.cs
// Namespace    : FBTS.EntityFramework.Transactions
// Class Name   : TransactionEntities
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
namespace FBTS.EntityFramework.Transactions
{
   
    public partial class TransactionEntities
    {
        public TransactionEntities(string connectionString)
            : base(connectionString)
        {
        }
    }
}
