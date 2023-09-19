using System.Collections.Generic;
using GSM05000Common.DTOs;
using R_CommonFrontBackAPI;

namespace GSM05000Common
{
    public interface IGSM05000 : R_IServiceCRUDBase<GSM05000DTO>
    {
        IAsyncEnumerable<GSM05000GridDTO> GetTransactionCodeListStream();
        GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList();
        GSM05000ExistDTO CheckExistData(GSM05000TrxCodeParamsDTO poParams);
    }

    public interface IGSM05000Numbering : R_IServiceCRUDBase<GSM05000NumberingGridDTO>
    {
        GSM05000NumberingHeaderDTO GetNumberingHeader(GSM05000TrxCodeParamsDTO poParams);
        IAsyncEnumerable<GSM05000NumberingGridDTO> GetNumberingListStream();
    }

    public interface IGSM05000ApprovalUser : R_IServiceCRUDBase<GSM05000ApprovalUserDTO>
    {
        GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader(GSM05000TrxCodeParamsDTO poParams);
        IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetApprovalListStream();
        string GSM05000ValidationForAction(GSM05000TrxDeptParamsDTO poParams);
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartmentStream();
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000DepartmentChangeSequenceStream(GSM05000TrxCodeParamsDTO poParams);
        IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetUserSequenceDataStream();
        void GSM05000UpdateSequence(List<GSM05000ApprovalUserDTO> poEntity);
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartmentStream(GSM05000DeptCodeParamsDTO poParams);
        void GSM05000CopyToApproval(GSM05000CopyToParamsDTO poParams);
        void GSM05000CopyFromApproval(GSM05000CopyFromParamsDTO poParams);
    }
    
    public interface IGSM05000ApprovalReplacement : R_IServiceCRUDBase<GSM05000ApprovalReplacementDTO>
    {
        IAsyncEnumerable<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementListStream();
    }
}