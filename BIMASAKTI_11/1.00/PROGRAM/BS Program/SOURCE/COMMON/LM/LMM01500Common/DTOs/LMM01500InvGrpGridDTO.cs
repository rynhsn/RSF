using System;

namespace LMM01500Common.DTOs
{
    public class LMM01500InvGrpGridDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CINVGRP_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }

        private string _CINVGRP { get; set; }
        public string CINVGRP
        {
            get => _CINVGRP;
            set => _CINVGRP = $"{CINVGRP_CODE} - {CINVGRP_NAME}";
        }
    }
}