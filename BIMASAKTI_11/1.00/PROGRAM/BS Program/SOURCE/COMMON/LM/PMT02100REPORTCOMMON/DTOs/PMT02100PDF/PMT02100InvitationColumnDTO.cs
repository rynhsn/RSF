using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100REPORTCOMMON.DTOs.PMT02100PDF
{
    public class PMT02100InvitationColumnDTO
    {
        public string CUNIT_INFORMATION { get; set; } = "Unit Information";
        public string CHANDOVER_INFORMATION { get; set; } = "Handover Information";
        public string CTENANT_INFORMATION { get; set; } = "Tenant Information";
        public string CPAYMENT_INFORMATION { get; set; } = "Payment Information";
        public string CTYPE { get; set; } = "Type";
        public string CDESCRIPTION { get; set; } = "Description";
        public string CTAXABLE { get; set; } = "Taxable";
        public string CHERE_IS_THE_DETAILED_BREAKDOWN { get; set; } = "Here is the detailed breakdown";
        public string CPAYMENT_INFORMATION_LINE_1 { get; set; } = "Please note that, prior to the handover, you are required to make an advance payment of charges for";
        public string CPAYMENT_INFORMATION_LINE_2 { get; set; } = "months";
        public string CPAYMENT_INFORMATION_LINE_3 { get; set; } = "totaling";
        public string CPAYMENT_INFORMATION_LINE_4 { get; set; } = "(before tax)";
        public string CFEE { get; set; } = "Fee (per Area sqm)";
        public string CTOTAL { get; set; } = "Total";
        public string CGRAND_TOTAL { get; set; } = "Grand Total (before tax)";
        public string CPROPERTY { get; set; } = "Property";
        public string CUNIT { get; set; } = "Unit";
        public string CAREA_SIZE { get; set; } = "Area Size";
        public string CUNIT_TYPE { get; set; } = "Unit Type";
        public string CCONFIRMED_DATE { get; set; } = "Confirmed Date";
        public string CCONFIRMED_BY { get; set; } = "Confirmed By";
        public string CNAME { get; set; } = "Name";
        public string CPHONE_NO { get; set; } = "Phone No.";
        public string CEMAIL { get; set; } = "Email";
        public string CMESSAGE_1 { get; set; } = "* Each item's total in the detailed breakdown is calculated as:";
        public string CMESSAGE_2 { get; set; } = "Fee × Area Size × Total Months to be Paid in Advance.";
        public string CFINAL_MESSAGE { get; set; } = "Please proceed with the required payment so that the handover can be completed on the confirmed date. Thank you for your attention and cooperation.";
    }
}
