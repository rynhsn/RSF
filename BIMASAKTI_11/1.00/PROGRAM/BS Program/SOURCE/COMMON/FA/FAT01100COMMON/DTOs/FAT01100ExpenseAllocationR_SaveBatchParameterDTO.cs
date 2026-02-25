using System;
using System.Collections.Generic;

namespace FAT01100Common.DTOs
{
    public class FAT01100ExpenseAllocationR_SaveBatchParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public FAT01100ExpenseAllocationR_SaveBatchUserParameterDTO UserParameters { get; set; } = new FAT01100ExpenseAllocationR_SaveBatchUserParameterDTO();
        public List<FAT01100ExpenseAllocationBatchListDisplayDTO>? Data { get; set; }
    }
}
