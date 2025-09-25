using System;
using System.Collections.Generic;
using System.Globalization;
using BaseHeaderReportCOMMON;
using GSM04000Common.DTO_s.Print;

namespace GSM04000Common.Model
{
    public class GenerateDataModel
    {
        public static GSM04000ReportResultDTO DefaultData()
        {
            
            var loData = new GSM04000ReportResultDTO
            {
                Title = "Supplier Statement",
                Label = new GSM04000ReportLabelDTO(),
                Data = new List<GSM04000ReportDataDTO>()
            };
            
            var loDepartments = new List<DepartmentDTO>();
            for (var i = 1; i <= 5; i++)
            {
                var loDepartment = new DepartmentDTO
                {
                    CCOMPANY_ID = "COMPANY" + i.ToString("D2"),
                    CDEPT_CODE = "DEPT" + i.ToString("D2"),
                    CDEPT_NAME = "Department " + i.ToString(),
                    CCENTER_CODE = "CENTER" + i.ToString("D2"),
                    CCENTER_NAME = "Center " + i.ToString(),
                    CMANAGER_CODE = "MANAGER" + i.ToString("D2"),
                    CMANAGER_NAME = "Manager " + i.ToString(),
                    CBRANCH_CODE = "BRANCH" + i.ToString("D2"),
                    CBRANCH_NAME = "Branch " + i.ToString(),
                    LEVERYONE = i % 2 == 0, // Example logic for LEVERYONE
                    LACTIVE = i % 2 == 1, // Example logic for LACTIVE
                    CEMAIL1 = "email" + i.ToString("D2") + "@example.com",
                    CEMAIL2 = "email" + i.ToString("D2") + "@example.org",
                    CUPDATE_BY = "User" + i.ToString("D2"),
                    DUPDATE_DATE = DateTime.Now.AddDays(-i),
                    CCREATE_BY = "Creator" + i.ToString("D2"),
                    DCREATE_DATE = DateTime.Now.AddDays(-i * 2),
                    CUSER_ID = "User" + i.ToString("D2")
                };
                loDepartments.Add(loDepartment);
            }

            foreach (var department in loDepartments)
            {
                var loDataResult = new GSM04000ReportDataDTO
                {
                    Department = department,
                    Users = new List<UserDepartmentDTO>()
                };

                // Simulate users for each department
                for (var j = 1; j <= 3; j++)
                {
                    var loUser = new UserDepartmentDTO
                    {
                        CUSER_ID = "User" + j.ToString("D2"),
                        CUSER_NAME = "User Name " + j.ToString()
                    };
                    loDataResult.Users.Add(loUser);
                }
                loData.Data.Add(loDataResult);
            }

            return loData;
        }

        public static GSM04000ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Department",
                CUSER_ID = "RHC"
            };

            var loData = new GSM04000ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}