using PMT01500Common.DTO._1._AgreementList;
using PMT01500Common.DTO._1._AgreementList.Upload;
using PMT01500Common.Utilities;
using System.Collections.Generic;

namespace PMT01500Common.Interface
{
    public interface IPMT01500AgreementList
    {
        IAsyncEnumerable<PMT01500AgreementListOriginalDTO> GetAgreementList();
        IAsyncEnumerable<PMT01500PropertyListDTO> GetPropertyList();
        IAsyncEnumerable<PMT01500VarGsmTransactionCodeDTO> GetVarGsmTransactionCode();
        PMT01500SelectedAgreementGetUnitDescriptionDTO GetSelectedAgreementGetUnitDescription(PMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<PMT01500UnitListOriginalDTO> GetUnitList();
        PMT01500ChangeStatusDTO GetChangeStatus(PMT01500GetHeaderParameterDTO poParameter);
        IAsyncEnumerable<PMT01500ComboBoxDTO> GetComboBoxDataCTransStatus();
        PMT01500ProcessResultDTO ProcessChangeStatus(PMT01500ChangeStatusParameterDTO poEntity);
        PMT01500UploadFileDTO DownloadTemplateFile();
    }
}
