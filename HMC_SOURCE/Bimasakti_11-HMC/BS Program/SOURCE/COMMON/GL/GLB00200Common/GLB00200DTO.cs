using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GLB00200Common
{
    public class GLB00200DTO
    {
        // CR VFM minta yang dikeluarkan tanggal
        public DateTime? DDOC_DATE { get; set; }
        public DateTime? DREVERSE_DATE { get; set; }
        //
        public bool LSELECTED { get; set; } = false;
        
        public int INO { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CREC_ID { get; set; }
        public bool LVALID { get; set; }
        public string CDEPT_CODE { get; set; }

        public string CDEPT_CODE_NAME { get; set; }
        public string CREF_NO { get; set; } = "";
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CREVERSE_DATE { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NTRANS_AMOUNT { get; set; } =0;
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public decimal NLTRANS_AMOUNT { get;}
        public string CBASE_CURRENCY_CODE { get; set; }
        public decimal NBTRANS_AMOUNT { get; set; }
    }
    public class GLB00200ListDTO : R_APIResultBaseDTO
    {
        public List<GLB00200DTO> Data { get; set; } = new List<GLB00200DTO>();
    }
}
