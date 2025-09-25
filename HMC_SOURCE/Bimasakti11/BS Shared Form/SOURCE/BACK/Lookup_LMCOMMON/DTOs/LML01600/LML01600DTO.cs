using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01600
{
    public class LML01600DTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; }
        public string? CCALL_TYPE_ID { get; set; }
        public string? CCALL_TYPE_NAME { get; set; }
        public string? CCATEGORY_ID { get; set; }
        public string? CCATEGORY_NAME { get; set; }
        public int IDAYS { get; set; }
        public int IHOURS { get; set; }
        public int IMINUTES { get; set; }
        public bool LPRIOIRTY { get; set; }

        public string? CCATEGORY_ID_AND_NAME
        {
            get => _CCATEGORY_ID_AND_NAME;
            set
            {
                if (string.IsNullOrEmpty(CCATEGORY_NAME) || string.IsNullOrEmpty(CCATEGORY_ID))
                {
                    _CCATEGORY_ID_AND_NAME = "";
                }
                else
                {
                    _CCATEGORY_ID_AND_NAME = CCATEGORY_NAME + " (" + CCATEGORY_ID + ")";
                }
            }
        }
        private string? _CCATEGORY_ID_AND_NAME;
    }
}
