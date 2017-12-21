using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.HR_Payroll
{
    public class EmployeeDetail : Operations
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime JoiningDate { get; set; }
        public string EmployeeType { get; set; }
        public string EmployeeGrade { get; set; }
        public string EmployeeDepartment { get; set; }
        public string EmployeeLocation { get; set; }
        public string UserAccess { get; set; }
        public string EmployeeCTC { get; set; }
        public string Remark { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeMiddleName { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeGender { get; set; }
        public string EmpFatherName { get; set; }
        public string EmployeeDesignation { get; set; }
        public DateTime EmpDOB { get; set; }
        public string EmpAge { get; set; }
        public string EmpMaritalStatus { get; set; }
        public string EmpNationality { get; set; }
        public string EmpAddress { get; set; }
        public string EmpState { get; set; }
        public string EmpCountry { get; set; }
        public string PinCode { get; set; }
        public string LandLine { get; set; }
        public string Mobile { get; set; }
        public string EmpMailId { get; set; }
        public string PassPortNo { get; set; }
        public string ImmediateSupervisor { get; set; }
        public DateTime PassPortValidFrom { get; set; }
        public DateTime PassPortValidTo { get; set; }
        public string EmpPanNo { get; set; }
        public string EmpDLNo { get; set; }
        public string EmpHobbies { get; set; }
        public string IdCard { get; set; }
        public string BankAccNo { get; set; }
        public string BankName { get; set; }
        public string PFno { get; set; }
        public string ESIno { get; set; }
        public string NoticePeriod { get; set; }
        public bool OTEligible { get; set; }
        public string PersonalInfo { get; set; }
        public DateTime NextIncrementDate { get; set; }
        public string HRInfo { get; set; }  

        public EmployeeDetail()
        {
            employeeSkills = new EmployeeSkills();
        }
        public EmployeeSkills employeeSkills { get; set; }
    }
}
