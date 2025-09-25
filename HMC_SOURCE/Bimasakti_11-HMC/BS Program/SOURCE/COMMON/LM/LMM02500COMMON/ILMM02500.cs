using System.Collections.Generic;
using LMM02500Common.DTO;
using R_CommonFrontBackAPI;

namespace LMM02500Common
{
    public interface ILMM02500 : R_IServiceCRUDBase<LMM02500ProfileDTO>
    {
        IAsyncEnumerable<LMM02500ProfileDTO> GetAllTenantGroupStream();
        LMM02500ListDTO<LMM02500ParameterDTO> GetParameterTenantGroup();
        //LMM02500ProfileDTO GetProfileTenantGroup();
        //LMM02500TaxInfoDTO GetTaxInfoTenantGroup();

        LMM02500ListDTO<LMM02500ComboBoxType> GetTaxTypeComboBox();
        LMM02500ListDTO<LMM02500ComboBoxType> GetIdTypeComboBox();
        LMM02500ListDTO<LMM02500ComboBoxType> GetTaxCodeComboBox();
    }
}