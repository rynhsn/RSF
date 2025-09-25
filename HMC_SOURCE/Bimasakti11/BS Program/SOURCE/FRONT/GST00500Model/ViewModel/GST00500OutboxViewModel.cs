using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GST00500Common;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace GST00500Model.ViewModel
{
    public class GST00500OutboxViewModel : R_ViewModel<GST00500DTO>
    {
        private GST00500OutboxModel _modelGST00500Outbox = new GST00500OutboxModel();
        public  ObservableCollection<GST00500DTO> OutboxTransactionList = 
            new ObservableCollection<GST00500DTO>();
        public  ObservableCollection<GST00500ApprovalStatusDTO> OutboxApprovalStatusTransactionList = 
            new ObservableCollection<GST00500ApprovalStatusDTO>();
        public GST00500ParamToOthersProgramDTO _currentRecord = new GST00500ParamToOthersProgramDTO();
        public GST00500InboxViewModel _viewModelInbox = new GST00500InboxViewModel();

        public async Task GetAllOutboxTransaction()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _modelGST00500Outbox.GetOutboxListAsyncModel();
                OutboxTransactionList = new ObservableCollection<GST00500DTO>(loResult);

                if (OutboxTransactionList.Any())
                {
                    foreach (var item in OutboxTransactionList)
                    {
                        item.DREF_DATE = _viewModelInbox.ConvertStringToDateTimeFormat(item.CREF_DATE);
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetAllApprovalStatus(GST00500DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, poEntity.CTRANS_CODE);
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poEntity.CDEPT_CODE);
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poEntity.CREF_NO); ;
                
                var loResult = await _modelGST00500Outbox.GetApprovalStatusListAsyncModel();
                OutboxApprovalStatusTransactionList = new ObservableCollection<GST00500ApprovalStatusDTO>(loResult);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        
    }
}
