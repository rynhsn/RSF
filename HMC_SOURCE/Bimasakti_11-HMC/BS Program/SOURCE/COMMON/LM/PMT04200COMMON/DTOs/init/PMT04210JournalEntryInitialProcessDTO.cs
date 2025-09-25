using System.Collections.Generic;

namespace PMT04200Common.DTOs;

public class PMT04210JournalEntryInitialProcessDTO
{
    public PMT04200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; }
    public PMT04200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
    public PMT04200TodayDateDTO VAR_TODAY { get; set; }
    public PMT04200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; }
    public PMT04200PMSystemParamDTO VAR_PM_SYSTEM_PARAM { get; set; }
    public PMT04200GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; }
    public List<PropertyListDTO> VAR_PROPERTY_LIST { get; set; }
}