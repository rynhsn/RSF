using GSM05000Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM05000Common
{
    public interface IGSM05000 : R_IServiceCRUDBase<GSM05000DTO>
    {
        //ambil data dari database
        GSM05000ListDTO<GSM05000GridDTO> GetTransactionCodeList();
        GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList();
    }

    public interface IGSM05000Numbering : R_IServiceCRUDBase<GSM05000NumberingGridDTO>
    {
        GSM05000ListDTO<GSM05000NumberingGridDTO> GetNumberingList();
        GSM05000NumberingHeaderDTO GetNumberingHeader();
    }

    public interface IGSM05000Approval : R_IServiceCRUDBase<GSM05000ApprovalUserDTO>
    {
        GSM05000ListDTO<GSM05000ApprovalUserDTO> GSM05000GetApprovalList();
        GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader();
        GSM05000ApprovalDepartmentDTO GSM05000GetApprovalDepartment();
    }
    
    public interface IGSM05000ApprovalReplacement : R_IServiceCRUDBase<GSM05000ApprovalReplacementDTO>
    {
        GSM05000ListDTO<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementList();
    }
}