using System;
using System.Collections.Generic;
using System.Text;

namespace APR00100COMMON.DTO_s
{
    public class APR00100SpParamDTO
    {
        public string CCOMPANY_ID { set; get; }
        public string CPROPERTY_ID { set; get; }
        public string CFROM_SUPPLIER_ID { set; get; }
        public string CTO_SUPPLIER_ID { set; get; }
        public string CFROM_JRNGRP_CODE { set; get; }
        public string CTO_JRNGRP_CODE { set; get; }
        public string CREMAINING_BASED_ON { set; get; }
        public string CCUT_OFF { set; get; }
        public string CPERIOD { set; get; }
        public string CREPORT_TYPE { set; get; }
        public string CSORT_BY { set; get; }
        public string CCURRENCY_TYPE_CODE { set; get; }
        public string CFROM_DEPT_CODE { set; get; }
        public string CTO_DEPT_CODE { set; get; }
        public bool LALLOCATION { set; get; }
        public string CTRANSACTION_TYPE_CODE { set; get; }
        public string CTRANSACTION_TYPE_CODE_NAME { set; get; }
        public string CSUPPLIER_CATEGORY_CODE { set; get; }
        public string CSUPPLIER_CATEGORY_CODE_NAME { set; get; }
        public string CLANG_ID { set; get; }
    }
}
