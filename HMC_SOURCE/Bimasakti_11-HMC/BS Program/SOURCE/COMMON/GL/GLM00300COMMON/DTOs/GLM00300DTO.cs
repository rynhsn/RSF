using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;

namespace GLM00300Common.DTOs
{
    public class GLM00300ListDTO : R_APIResultBaseDTO
    {
        public List<GLM00300DTO> Data { get; set; }
    }

    public class GLM00300DTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
        public string CREC_ID { get; set; }
        public string CBW_NAME_DISPLAY { get; set; }

        public bool LALLOW_APPROVE { get; set; }
        public string CBW_CODE { get; set; }
        public string CBW_NAME { get; set; }
        public decimal NL_AMOUNT01 { get; set; }
        public decimal NL_AMOUNT02 { get; set; }
        public decimal NL_AMOUNT03 { get; set; }
        public decimal NL_AMOUNT04 { get; set; }
        public decimal NL_AMOUNT05 { get; set; }
        public decimal NL_AMOUNT06 { get; set; }
        public decimal NL_AMOUNT07 { get; set; }
        public decimal NL_AMOUNT08 { get; set; }
        public decimal NL_AMOUNT09 { get; set; }
        public decimal NL_AMOUNT10 { get; set; }
        public decimal NL_AMOUNT11 { get; set; }
        public decimal NL_AMOUNT12 { get; set; }
        public decimal NL_AMOUNT13 { get; set; }
        public decimal NL_AMOUNT14 { get; set; }
        public decimal NL_AMOUNT15 { get; set; }
        public decimal NB_AMOUNT01 { get; set; }
        public decimal NB_AMOUNT02 { get; set; }
        public decimal NB_AMOUNT03 { get; set; }
        public decimal NB_AMOUNT04 { get; set; }
        public decimal NB_AMOUNT05 { get; set; }
        public decimal NB_AMOUNT06 { get; set; }
        public decimal NB_AMOUNT07 { get; set; }
        public decimal NB_AMOUNT08 { get; set; }
        public decimal NB_AMOUNT09 { get; set; }
        public decimal NB_AMOUNT10 { get; set; }
        public decimal NB_AMOUNT11 { get; set; }
        public decimal NB_AMOUNT12 { get; set; }
        public decimal NB_AMOUNT13 { get; set; }
        public decimal NB_AMOUNT14 { get; set; }
        public decimal NB_AMOUNT15 { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CUPDATE_DATE { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CCREATE_DATE { get; set; }

    }
}
