using System.Collections.Generic;
using GSM05000Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM05000Common
{
    public interface IGSM05000 : R_IServiceCRUDBase<GSM05000DTO>
    {
        //ambil data dari database
        GSM05000ListDTO<GSM05000GridDTO> GetTransactionCodeList();
        GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList();
        GSM005000ExistDTO CheckExistData();
    }

    public interface IGSM05000Numbering : R_IServiceCRUDBase<GSM05000NumberingGridDTO>
    {
        GSM05000NumberingHeaderDTO GetNumberingHeader();
        GSM05000ListDTO<GSM05000NumberingGridDTO> GetNumberingList();
    }

    public interface IGSM05000ApprovalUser : R_IServiceCRUDBase<GSM05000ApprovalUserDTO>
    {
        GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader();
        GSM05000ListDTO<GSM05000ApprovalUserDTO> GSM05000GetApprovalList();
        string GSM05000ValidationForAction();
        GSM05000ListDTO<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartment();
        GSM05000ListDTO<GSM05000ApprovalDepartmentDTO> GSM05000DepartmentChangeSequence();
        GSM05000ListDTO<GSM05000ApprovalUserDTO> GSM05000GetUserSequenceData();
        void GSM05000UpdateSequence(List<GSM05000ApprovalUserDTO> poEntity);
        GSM05000ListDTO<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartment();
        void GSM05000CopyToApproval();
        void GSM05000CopyFromApproval();
    }
    
    public interface IGSM05000ApprovalReplacement : R_IServiceCRUDBase<GSM05000ApprovalReplacementDTO>
    {
        GSM05000ListDTO<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementList();
    }
}