using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s
{
    public class CategoryTypeParamDTO 
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPARENT_ID { get; set; } 
        public bool LCHILD_ONLY { get; set; }
        public string CLANGUAGE_ID { get; set; }

        //public string CCATEGORY_TYPE { get; set; }
    }
}
