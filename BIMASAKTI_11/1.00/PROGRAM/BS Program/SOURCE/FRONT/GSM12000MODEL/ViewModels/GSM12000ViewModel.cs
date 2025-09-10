using GSM12000COMMON;
using GSM12000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml.Linq;
using R_BlazorFrontEnd.Enums;

namespace GSM12000MODEL
{
    public class GSM12000ViewModel : R_ViewModel<GSM12000DTO>
    {
        private GSM12000Model _GSM12000Model = new GSM12000Model();

        #region Property Class

        public List<GSM12000GSBCodeDTO> MessageTypeList { get; set; } = new List<GSM12000GSBCodeDTO>();
        public List<GSM12000DTO> MessageList { get; set; } = new List<GSM12000DTO>();
        public ObservableCollection<GSM12000DTO> MessageGrid { get; set; } = new ObservableCollection<GSM12000DTO>();
        public GSM12000PrintParamDTO PrintParam { get; set; } = new GSM12000PrintParamDTO();
        public GSM12000DTO Message { get; set; } = new GSM12000DTO();
        public string MessageTypeCode { get; set; }
        public string messageTypeValue = "";
        public string messageNoFromValue = "";
        public string messageNoToValue = "";
        public bool lActiveSave = true;
        public R_IFileConverter? _fileConverter;
        public bool activeInactiveMessage = false;
        public string FileTypeValue { get; set; }
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };
        #endregion

        public async Task GetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM12000Model.GetListMessageTypeAsync();
                MessageTypeList = loResult;
                if (MessageTypeList.Count > 0)
                {
                    messageTypeValue = MessageTypeList[0].CCODE;
                }
                else
                {
                    messageTypeValue = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetListMessage()
        {
            var loEx = new R_Exception();
            try
            {
                MessageTypeCode = messageTypeValue;
                var loResult = await _GSM12000Model.GetListMessageAsync(MessageTypeCode);

                MessageGrid = new ObservableCollection<GSM12000DTO>(loResult);
                MessageList = loResult;
                if (MessageList.Count > 0)
                {
                    messageNoFromValue = MessageList[0].CMESSAGE_NO;
                    messageNoToValue = MessageList[0].CMESSAGE_NO;
                    
                }
                else
                {
                    messageNoFromValue = "";
                    messageNoToValue = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetMessage(GSM12000DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GSM12000Model.R_ServiceGetRecordAsync(poEntity);
                Message = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveMessage(GSM12000DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {

                if (string.IsNullOrWhiteSpace(poEntity.CMESSAGE_TYPE))
                {
                    poEntity.CMESSAGE_TYPE = messageTypeValue;
                }

                poEntity.CADDITIONAL_INFO = poEntity.CADDITIONAL_DESCRIPTION;
                var loResult = await _GSM12000Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                Message = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteMessage(GSM12000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CADDITIONAL_INFO = poEntity.CADDITIONAL_DESCRIPTION;
                poEntity.TMESSAGE_DESCR_RTF = poEntity.TMESSAGE_DESCRIPTION;
                poEntity.TADDITIONAL_DESCR_RTF = poEntity.CADDITIONAL_DESCRIPTION;
                await _GSM12000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ChangeStatusActive()
        {
            var loEx = new R_Exception();

            try
            {
            var poParam = new GSM12000DTO();
            poParam.CMESSAGE_TYPE = Message.CMESSAGE_TYPE;
            poParam.LACTIVE = !Message.LACTIVE;
            poParam.CMESSAGE_NO = Message.CMESSAGE_NO;
                await _GSM12000Model.ActiveInActiveMessageAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}