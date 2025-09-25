using System;
using System.Collections.Generic;
using System.Text;

namespace Global_PMCOMMON.DTOs.Response.Invoice_Type
{
    public  class InvoiceTypeParameterDTO : BaseDTO
    {
        public string CCLASS_APPLICATION { get; set; } = "";
        public string CCLASS_ID { get; set; } = "";
        public string CCLASS_RECID { get; set; } = "";
    }
}
