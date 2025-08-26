using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GSM02500COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GSM02500COMMON.Models
{
    public static class GenerateDataModelGSM02500
    {
        public static GSM02500ResultPrintDTO DefaultData()
        {
            GSM02500ResultPrintDTO loData = new GSM02500ResultPrintDTO();

            List<GSM02500ResultPrintSPDTO> loTempResult = new List<GSM02500ResultPrintSPDTO>();
            int lnDetail;
            for (int i = 1; i <= 2; i++)
            {
                loTempResult.Add(new GSM02500ResultPrintSPDTO
                {
                    CCOMPANY_ID = $"COMP{i:000}",
                    CPROPERTY_ID = $"PROP{i:000}",
                    CPROPERTY_NAME = $"PROPNAME{i:000}",
                    CBUILDING_ID = $"BLD{i:000}",
                    CBUILDING_NAME = $"Building Name {i}",
                    CFLOOR_ID = $"FLR{i:00}",
                    CFLOOR_NAME = $"Floor {i}",
                    CUNIT_ID = $"UNIT{i:0000}",
                    CUNIT_NAME = $"Unit Name {i}",
                    LACTIVE = i % 2 == 0,
                    CUNIT_TYPE_ID = $"TYPE{i:00}",
                    CUNIT_TYPE_NAME = $"Type Name {i}",
                    CUNIT_CATEGORY_ID = $"CAT{i:00}",
                    CUNIT_CATEGORY_NAME = $"Category Name {i}",
                    CSTRATA_STATUS = $"STRATA{i}",
                    CSTRATA_STATUS_DESC = $"Strata Status Desc {i}",
                    CLEASE_STATUS = $"LEASE{i}",
                    CLEASE_STATUS_DESC = $"Lease Status Desc {i}"
                });
            }

            //set header data
            var loHeader = loTempResult.FirstOrDefault();

            loData.Header = string.Format("{0} - {1}", loHeader.CPROPERTY_ID, loHeader.CPROPERTY_NAME);
            loData.Data = loTempResult;

            return loData;
        }

        public static GSM02500ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "FMC",
            };

            GSM02500ResultWithBaseHeaderPrintDTO loRtn = new GSM02500ResultWithBaseHeaderPrintDTO { Column = new GSM02500ColumnDTO() };

            loRtn.BaseHeaderData = loParam;
            loRtn.PropertyProfile = DefaultData();

            return loRtn;
        }
    }

}
