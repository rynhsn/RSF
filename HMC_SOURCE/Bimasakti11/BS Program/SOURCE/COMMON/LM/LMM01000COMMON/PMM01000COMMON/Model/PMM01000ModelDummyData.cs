using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System.Collections.Generic;
using System.Linq;

namespace PMM01000COMMON.Models
{
    public static class GenerateDataModel
    {
        public static PMM01000ResultPrintDTO DefaultDataUtility()
        {
            PMM01000ResultPrintDTO loData = new PMM01000ResultPrintDTO()
            {
                Title = "Utility Charges",
                Header = "JBMPC - Metro Park Residence",
                Column = new PMM01000ColumnPrintDTO()
            };


            int lnTop = 4;
            int lnHeader;
            int lnDetail;
            List<PMM01000PrintDTO> loCollection = new List<PMM01000PrintDTO>();
            for (int i = 1; i <= lnTop; i++)
            {
                lnHeader = (i % 2) + 1;
                for (int j = 1; j <= lnHeader; j++)
                {
                    lnDetail = (j % 3) + 1;
                    for (int k = 1; k <= lnDetail; k++)
                    {
                        loCollection.Add(new PMM01000PrintDTO()
                        {
                            CCHARGES_TYPE_DESCR = string.Format("Type Desc {0}", i),

                            CCHARGES_ID = string.Format("Charges Id {0}", j),
                            CCHARGES_NAME = string.Format("Charges Name {0}", j),
                            CCHARGES_TYPE = string.Format("Type {0}", i),
                            LACTIVE = (j % 2 == 0),
                            LACCRUAL = (j % 2 == 0),
                            CUTILITY_JRNGRP_CODE = string.Format("Journal Code {0}", j),
                            CUTILITY_JRNGRP_NAME = string.Format("Jounal Name {0}", j),
                            LTAX_EXEMPTION = (i % 2 != 0),
                            CTAX_EXEMPTION_CODE = string.Format("Tax Ex {0}", j),
                            NTAX_EXEMPTION_PCT= j,
                            LOTHER_TAX = (i % 2 == 0),
                            COTHER_TAX_ID = string.Format("Other Tax {0}", j),
                            LWITHHOLDING_TAX = (j % 2 == 0),
                            CWITHHOLDING_TAX_TYPE = string.Format("Withholding Tax Type {0}", j),
                            CWITHHOLDING_TAX_ID = string.Format("Withholding Tax id {0}", j),
                            CGOA_CODE = string.Format("GOA Code {0}", k),
                            CGOA_NAME = string.Format("GOA Name {0}", k),
                            LDEPARTMENT_MODE = (k % 2 != 0),
                            CGLACCOUNT_NO = string.Format("GLAccount Code {0}", k),
                            CGLACCOUNT_NAME = string.Format("GLAccount Name {0}", k)
                        });
                    }
                }
            }

            var loTempData = loCollection
                .GroupBy(item => new { item.CCHARGES_TYPE_DESCR, item.CCHARGES_TYPE })
                .Select(group => new PMM01000TopPrintDTO
                {
                    CCHARGES_TYPE_DESCR = group.Key.CCHARGES_TYPE_DESCR,
                    DataCharges = group
                        .GroupBy(headerGroup => new
                        {
                            headerGroup.CCHARGES_TYPE,
                            headerGroup.CCHARGES_ID,
                            headerGroup.CCHARGES_NAME,
                            headerGroup.LACTIVE,
                            headerGroup.LACCRUAL,
                            headerGroup.CUTILITY_JRNGRP_CODE,
                            headerGroup.CUTILITY_JRNGRP_NAME,
                            headerGroup.LTAX_EXEMPTION,
                            headerGroup.LOTHER_TAX,
                            headerGroup.CTAX_EXEMPTION_CODE,
                            headerGroup.NTAX_EXEMPTION_PCT,
                            headerGroup.COTHER_TAX_ID,
                            headerGroup.LWITHHOLDING_TAX,
                            headerGroup.CWITHHOLDING_TAX_TYPE,
                            headerGroup.CWITHHOLDING_TAX_ID
                        })
                        .Select(headerGroup => new PMM01000HeaderPrintDTO
                        {
                            CCHARGES_TYPE = headerGroup.Key.CCHARGES_TYPE,
                            CCHARGES_ID = headerGroup.Key.CCHARGES_ID,
                            CCHARGES_NAME = headerGroup.Key.CCHARGES_NAME,
                            LACTIVE = headerGroup.Key.LACTIVE,
                            LACCRUAL = headerGroup.Key.LACCRUAL,
                            CUTILITY_JRNGRP_CODE = headerGroup.Key.CUTILITY_JRNGRP_CODE,
                            CUTILITY_JRNGRP_NAME = headerGroup.Key.CUTILITY_JRNGRP_NAME,
                            LTAX_EXEMPTION = headerGroup.Key.LTAX_EXEMPTION,
                            LOTHER_TAX = headerGroup.Key.LOTHER_TAX,
                            NTAX_EXEMPTION_PCT= headerGroup.Key.NTAX_EXEMPTION_PCT,
                            LWITHHOLDING_TAX = headerGroup.Key.LWITHHOLDING_TAX,
                            CTAX_EXEMPTION_CODE = headerGroup.Key.CTAX_EXEMPTION_CODE,
                            COTHER_TAX_ID = headerGroup.Key.COTHER_TAX_ID,
                            CWITHHOLDING_TAX_TYPE = headerGroup.Key.CWITHHOLDING_TAX_TYPE,
                            CWITHHOLDING_TAX_ID = headerGroup.Key.CWITHHOLDING_TAX_ID,
                            DetailCharges = headerGroup
                                .Select(detail => new PMM01000DetailPrintDTO
                                {
                                    CGOA_CODE = detail.CGOA_CODE,
                                    CGOA_NAME = detail.CGOA_NAME,
                                    LDEPARTMENT_MODE = detail.LDEPARTMENT_MODE,
                                    CGLACCOUNT_NO = detail.CGLACCOUNT_NO,
                                    CGLACCOUNT_NAME = detail.CGLACCOUNT_NAME
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

            loData.Data = loTempData;

            return loData;
        }

        public static PMM01000ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Utility Charges",
                CUSER_ID = "FMC",
            };

            PMM01000ResultWithBaseHeaderPrintDTO loRtn = new PMM01000ResultWithBaseHeaderPrintDTO();
            loRtn.BaseHeaderData = loParam;
            loRtn.UtilitiesData = GenerateDataModel.DefaultDataUtility();

            return loRtn;
        }
    }

}
