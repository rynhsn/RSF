using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GLR00300Common
{
    public interface IGLR00300 : R_IServiceCRUDBase<GLR00300DTO>
    {
        GLR00300InitialProcess InitialProcess();
        GenericList<GLR00300GetPeriod> GetPeriod(GLR00300DBParameterDTO poParam);
        GenericList<GLR00300DTO> GetTrialBalanceType();
        GenericList<GLR00300DTO> GetPrintMethodType();
        GenericList<GLR00300BudgetNoDTO> GetBudgetNo(GLR00300DBParameterDTO loParam);

    }
}