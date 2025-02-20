using System;

namespace HDI00100Common.DTOs
{
    public class HDI00100TaskSchedulerDTO
    {
        public string CTASK_CTG_CODE { get; set; } = "";
        public string CTASK_CTG_NAME { get; set; } = "";
        public string CTASK_CTG_DISPLAY => $"{CTASK_CTG_NAME} ({CTASK_CTG_CODE})";
        
        
        public string CTASK_CODE { get; set; } = "";
        public string CTASK_NAME { get; set; } = "";
        public string CTASK_DISPLAY => $"{CTASK_NAME} ({CTASK_CODE})";
        
        
        public string CASSET_CODE { get; set; } = "";
        public string CASSET_NAME { get; set; } = "";
        public string CASSET_DISPLAY => $"{CASSET_NAME} ({CASSET_CODE})";
        
        public string CBUILDING_NAME { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CDEPT_DISPLAY => $"{CDEPT_NAME} ({CDEPT_CODE})";
        
        
        public string CCALL_TYPE_ID { get; set; } = "";
        public string CCALL_TYPE_NAME { get; set; } = "";
        public string CCALL_TYPE_DISPLAY => $"{CCALL_TYPE_NAME} ({CCALL_TYPE_ID})";
        
        
        public string CSCHEDULE_DATE { get; set; } = "";
        public DateTime? DSCHEDULE_DATE { get; set; }
        
        
        public string CSPV_ID { get; set; } = "";
        public string CSPV_NAME { get; set; } = "";
        public string CSPV_DISPLAY => $"{CSPV_NAME} ({CSPV_ID})";
        
        
        public string CSTAFF_ID { get; set; } = "";
        public string CSTAFF_NAME { get; set; } = "";
        public string CSTAFF_DISPLAY => $"{CSTAFF_NAME} ({CSTAFF_ID})";
        
        
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
    }
}