using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLB00200Common
{
    public interface IGLB00200 : R_IServiceCRUDBase<GLB00200DTO>
    {
        GLB00200InitalProcessDTO GetInitialProcess();
        IAsyncEnumerable<GLB00200DTO> ReversingJournalProcessListStream();
        GLB00200JournalDetailListDTO DetailReversingJournalProcessList(GLB00200DTO loParam);
    }
}