using PMT02100COMMON.DTOs.PMT02100;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PMT02100MODEL.FrontDTOs.PMT02100
{
    public class ScheduleProcessParameterDTO
    {
        public List<PMT02100HandoverDTO> loSelectedHandover { get; set; }
        public string CPROPERTY_ID { get; set; }
    }
}
