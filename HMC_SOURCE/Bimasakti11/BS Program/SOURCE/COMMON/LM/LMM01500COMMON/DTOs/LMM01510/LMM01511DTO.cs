﻿using System;

namespace LMM01500COMMON
{
    public class LMM01511DTO
    {
        // parameter
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }
        public string CDEPT_CODE { get; set; }

        // + result
        public string CDEPT_NAME { get; set; }

        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileNameExtension { get; set; }
        public Byte[] Data { get; set; }
        public string CINVOICE_TEMPLATE { get; set; } = "";

        public string CBANK_CODE { get; set; }
        public string CBANK_NAME { get; set; }
        public string CBANK_ACCOUNT { get; set; }

        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
    }
}
