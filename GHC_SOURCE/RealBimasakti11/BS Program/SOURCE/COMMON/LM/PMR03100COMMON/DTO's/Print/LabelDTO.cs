using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03100COMMON.DTO_s.Print
{
    public class LabelDTO
    {
        public string PARAM_PROPERTY { get; set; } = "Property";
        public string PARAM_CUTOFF_YEAR { get; set; } = "Cut Off Year";
        public string HEADER_INVAMTEXCL { get; set; }="Invoice Amt Excl";
        public string HEADER_DPPNETO { get; set; }="DPP(Neto)";
        public string HEADER_CACUTTEDAMT { get; set; }="CA Bukti Potong Amt";
        public string HEADER_CAREFNO { get; set; }="CA Ref No";
        public string HEADER_PPHAMOUNT { get; set; }="PPh Amount";
        public string HEADER_MONTHYEAR { get; set; }="Month Year";
        public string FOOTER_TOTPPHAMT { get; set; }="Total PPh Amount";
    }
}
