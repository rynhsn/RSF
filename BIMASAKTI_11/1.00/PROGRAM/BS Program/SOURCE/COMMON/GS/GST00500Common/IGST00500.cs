using System;
using System.Collections.Generic;
using System.Text;
using R_APICommonDTO;
using R_CommonFrontBackAPI;

namespace GST00500Common
{
    public interface IGST00500 : R_IServiceCRUDBase<GST00500DTO>
    {
        IAsyncEnumerable<GST00500DTO> ApprovalInboxListStream();
        GST00500UserNameDTO GetUserName();
        GST00500RejectListDTO ReasonRejectList();

    //    IAsyncEnumerable<GST00500ApprovalTransactionDTO> GetError(GST00500ParameterDBDTO loParam);


    }

}
