using System;
using System.Collections.Generic;
using System.Text;

namespace PMB04000COMMON.DTO.DTOs
{
    public class PMB04000DTO : PMB04000BaseDTO
    {
        public bool LSELECTED { get; set; }
        public int INO { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CTRANSACTION_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public string? CINVGRP_NAME { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CTRANS_DESC { get; set; }
        public int IPRINT { get; set; }
        public int IDISTRIBUTE { get; set; }

        //ADDTITIONAL
        //public string? CINVOICE_TYPE { get; set; }
        //public string? CINVOICE_TYPE_NAME { get; set; }
        //public string? CINVGRP_CODE { get; set; }
        //public string? CTENANT_NAME { get; set; }
        //public decimal NTRANS_AMOUNT { get; set; }
        public DateTime? DREF_DATE { get; set; }
    }
}
