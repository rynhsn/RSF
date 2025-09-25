using BaseHeaderReportCOMMON.Models;
using PMT02100REPORTCOMMON.DTOs.PMT02100PDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMT02100COMMON.Models
{
    public class PMT02100InvitationPDFModelDummyData
    {
        public static PMT02100InvitationResultDTO DefaultDataReport()
        {
            PMT02100InvitationResultDTO loData = new PMT02100InvitationResultDTO()
            {
                Column = new PMT02100InvitationColumnDTO()
            };

            int lnHeader = 5;
            int lnDetail;
            PMT02100InvitationDTO loCollection = new PMT02100InvitationDTO()
            {
                CSCHEDULED_HO_DATE = "20240303",
                CSCHEDULED_HO_TIME = "11:30",
                CSCHEDULED_HO_BY = "RAM",
                CPROPERTY_NAME = "Harco Mas",
                CBUILDING_NAME = "Building 001",
                CUNIT_ID = "UNIT001",
                NGROSS_AREA_SIZE = 100,
                CUNIT_TYPE_CATEGORY_NAME = "Category 001",
                CTENANT_NAME = "Tenant 001",
                CTENANT_PHONE_NO = "081234567890",
                CTENANT_EMAIL = "testing@gmail.com",
                IINVOICE_PERIOD = 3,
                CCURRENCY_SYMBOL = "Rp. ",
                NGRAND_TOTAL = 100000
            };

            loCollection.Detail = new List<PMT02100InvitationDetailDTO>();

            for (int i = 1; i < 5; i++)
            {
                loCollection.Detail.Add(new PMT02100InvitationDetailDTO
                {
                    CCHARGES_TYPE_DESCRIPTION = $"Charges-00{i}",
                    CCURRENCY_SYMBOL = "Rp. ",
                    NFEE = 1000*i,
                    LTAXABLE = i%2 == 0,
                    NTOTAL = 1500*i,
                    CDESCRIPTION = $"Description 00{i}"
                });
            }



            loData.Data = loCollection;

            return loData;
        }

        public static PMT02100InvitationResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            PMT02100InvitationResultWithBaseHeaderPrintDTO loRtn = new PMT02100InvitationResultWithBaseHeaderPrintDTO();
            // loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loRtn.ReportData = PMT02100InvitationPDFModelDummyData.DefaultDataReport();

            return loRtn;
        }
    }
}
