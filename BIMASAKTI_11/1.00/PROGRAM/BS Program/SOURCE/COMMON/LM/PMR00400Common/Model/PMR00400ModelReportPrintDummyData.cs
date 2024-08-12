using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BaseHeaderReportCOMMON;
using PMR00400Common.DTOs;
using PMR00400Common.DTOs.Print;

namespace PMR00400Common.Model
{
    public static class PMR00400ModelReportPrintDummyData
    {
        public static PMR00400ReportResultDTO DefaultData()
        {
            int Data1 = 4;
            int Data2 = 3;
            int Data3 = 3;
            
            var loCollection = new List<PMR00400DataDTO>();

            for (int a = 1; a < Data1; a++)
            {
                for (int b = 1; b < Data2; b++)
                {
                    for (int c = 1; c < Data3; c++)

                    {
                        loCollection.Add(new PMR00400DataDTO()
                        {
                            CBUILDING_ID = $"AE09-{a}",
                            CBUILDING_NAME = $"Building {a}",
                            NBUILDING_SPACE = (a * 2m),
                            NBUILDING_GROSS_AREA_SIZE = (a * 2m),
                            NBUILDING_NET_AREA_SIZE = (a * 2m),
                            NBUILDING_COMMON_AREA_SIZE = (a * 2m),
                            NBUILDING_EMPTY_SPACE = (a * 2m),


                            CFLOOR_ID = $"FloorID {b}",
                            CFLOOR_NAME = $"Floor {b}",
                            NFLOOR_SPACE = (a * 2m),
                            NFLOOR_GROSS_AREA_SIZE = (a * 2m),
                            NFLOOR_COMMON_AREA_SIZE = (a * 2m),


                            CUNIT_ID = $"UnitID {c}",
                            CUNIT_NAME = $"Unit {c}",
                            CUNIT_TYPE_CATEGORY_ID = $"Unit Type Category {c}",
                            CUNIT_TYPE_CATEGORY_NAME = $"Unit Type Category Name {c}",
                            CUNIT_TYPE_NAME = $"Unit Type Name {c}",
                            CUNIT_VIEW_NAME = $"Unit View Name {c}",
                            CUNIT_CATEGORY_NAME = $"Unit Category Name {c}",
                            NUNIT_GROSS_AREA_SIZE = (a * 2m),
                            NUNIT_NET_AREA_SIZE = (a * 2m),
                            NUNIT_COMMON_AREA_SIZE = (a * 2m),
                            CSTRATA_STATUS_NAME = $"Strata Status Name {c}",
                            COWNER_ID = $"Owner ID {c}",
                            COWNER_NAME = $"Owner Name {c}",
                            CLEASE_STATUS_NAME = $"Lease Status Name {c}",
                            CREF_NO = $"Ref No {c}",
                            CSTART_DATE = DateTime.Now.AddMonths(c).ToString("yyyyMMdd"),
                            CEND_DATE = DateTime.Now.AddMonths(c).ToString("yyyyMMdd"),
                            CTENANT_ID = $"Tenant ID {c}",
                            CTENANT_NAME = $"Tenant Name {c}",
                        });
                    }
                }
            }

            var loData = new PMR00400ReportResultDTO
            {
                Title = "PMR00400 Unit List",
                Label = new PMR00400LabelDTO(),
                Header = new PMR00400ParamDTO
                {
                    CCOMPANY_ID = "Company ID",
                    CPROPERTY_ID = "Property ID",
                    CTYPE = "Type",
                    CFROM_BUILDING_ID = "From Building ID",
                    CTO_BUILDING_ID = "To Building ID",
                    CFROM_UNIT_TYPE_ID = "From Unit Type ID",
                    CTO_UNIT_TYPE_ID = "To Unit Type ID",
                    CLANG_ID = "Lang ID",
                    // CPROPERTY_NAME = "Property Name",
                    // CFROM_BUILDING_NAME = "From Building Name",
                    // CTO_BUILDING_NAME = "To Building Name",
                    // CFROM_UNIT_TYPE_NAME = "From Unit Type Name",
                    // CTO_UNIT_TYPE_NAME = "To Unit Type Name",

                },

                Data = loCollection,
            };
            return loData;
        }
        
        public static PMR00400ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Unit List",
                CUSER_ID = "rhc"
            };
            
            var loData = new PMR00400ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}