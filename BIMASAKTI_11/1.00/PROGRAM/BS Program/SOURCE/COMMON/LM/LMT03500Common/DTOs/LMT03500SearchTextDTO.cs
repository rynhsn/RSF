﻿namespace LMT03500Common.DTOs
{
    public class LMT03500SearchTextDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CSEARCH_TEXT { get; set; } = "";
    }
}