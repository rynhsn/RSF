using System.Collections.Generic;

namespace GLR00300Common.GLR00300Print
{
    #region DTO E - H
    public class GLR00300AccountTrialBalanceResultFormat_EtoH_DTO
    {
        public string Title { get; set; }
        public GLR00300HeaderAccountTrialBalanceDTO Header { get; set; }
        public AccountTrialBalanceColumnDTO Column { get; set; }
        public GLR00300LabelDTO? Label { get; set; }
        public List<GLRR00300DataAccountTrialBalance> Data { get; set; }

    }
    public class GLR00300AccountTrialBalanceResult_FormatEtoH_WithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLR00300AccountTrialBalanceResultFormat_EtoH_DTO GLR00300AccountTrialBalanceResult_FormatEtoH_DataFormat { get; set; }
    }
    #endregion

    #region DTO A - D

    public class GLR00300AccountTrialBalanceResultFormat_AtoD_DTO
    {
        public string Title { get; set; }
        public GLR00300HeaderAccountTrialBalanceDTO Header { get; set; }
        public AccountTrialBalanceColumnDTO Column { get; set; }
        public GLR00300LabelDTO? Label { get; set; }
        public List<GLR00300DataAccountTrialBalanceAD> Data { get; set; }
    }
    public class GLR00300AccountTrialBalanceResult_FormatAtoD_WithBaseHeaderDTO : BaseHeaderReportCOMMON.BaseHeaderResult
    {
        public GLR00300AccountTrialBalanceResultFormat_AtoD_DTO GLR00300AccountTrialBalanceResult_FormatAtoD_DataFormat { get; set; }
    }

    #endregion
}