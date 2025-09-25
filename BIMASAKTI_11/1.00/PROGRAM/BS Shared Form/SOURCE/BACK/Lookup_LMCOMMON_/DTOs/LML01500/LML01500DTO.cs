using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.LML01500
{
    public class LML01500DTO
    {
        public string? COMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CCATEGORY_ID { get; set; }
        public string? CCATEGORY_NAME { get; set; }
        public string? CPARENT_CATEGORY_ID { get; set; }
        public string? CPARENT_CATEGORY_NAME { get; set; }
        public int ILEVEL { get; set; }
        public string? CNOTES { get; set; }

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

        public string? CPARENT_CATEGORY_ID_AND_NAME
        {
            get => _CPARENT_CATEGORY_ID_AND_NAME;
            set
            {
                if (string.IsNullOrEmpty(CPARENT_CATEGORY_NAME) || string.IsNullOrEmpty(CPARENT_CATEGORY_ID))
                {
                    _CPARENT_CATEGORY_ID_AND_NAME = "";
                }
                else
                {
                    _CPARENT_CATEGORY_ID_AND_NAME = CPARENT_CATEGORY_NAME + " (" + CPARENT_CATEGORY_ID + ")";
                }
            }
        }
        private string? _CPARENT_CATEGORY_ID_AND_NAME;
    }
}
