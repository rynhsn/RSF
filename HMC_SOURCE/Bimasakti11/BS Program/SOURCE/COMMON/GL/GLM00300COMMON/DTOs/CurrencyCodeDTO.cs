using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GLM00300Common.DTOs
{

    public class CurrencyCodeListDTO : R_APIResultBaseDTO
    {
        public List<CurrencyCodeDTO>Data { get; set; }
    }
    public class CurrencyCodeDTO
    {
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }

    }
}
