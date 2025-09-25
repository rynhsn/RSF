using Lookup_GSCOMMON.DTOs;
using PMT02600COMMON.DTOs.PMT02610;
using PMT02600COMMON.DTOs.PMT02640;
using PMT02600COMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using PMT02600COMMON.DTOs.Helper;

namespace PMT02600MODEL.ViewModel
{
    public class PMT02640ViewModel : R_ViewModel<PMT02640DTO>
    {
        private PMT02640Model loModel = new PMT02640Model();
        private PMT02610Model loAgreementModel = new PMT02610Model();

        #region Property Class
        public TabParameterDTO loTabParameter = new TabParameterDTO();
        public PMT02610DTO loHeader { get; set; } = new PMT02610DTO();
        public PMT02640DTO loDocument { get; set; } = new PMT02640DTO();
        public ObservableCollection<PMT02640DTO> loDocumentList { get; set; } = new ObservableCollection<PMT02640DTO>();
        //public List<GSL00300DTO> VAR_CURRENCY_LIST = new List<GSL00300DTO>();
        #endregion

        //public async Task GetInitialVar()
        //{
        //    var loEx = new R_Exception();
        //    try
        //    {
        //        var loResult = await loGSLModel.GSL00300GetCurrencyListAsync();

        //        VAR_CURRENCY_LIST = loResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        public async Task GetHeaderAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await loAgreementModel.R_ServiceGetRecordAsync(new PMT02610ParameterDTO()
                {
                    Data = new PMT02610DTO()
                    {
                        CPROPERTY_ID = loTabParameter.CPROPERTY_ID,
                        CDEPT_CODE = loTabParameter.CDEPT_CODE,
                        CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE,
                        CREF_NO = loTabParameter.CREF_NO
                    }
                });

                loResult.Data.DSTART_DATE = DateTime.ParseExact(loResult.Data.CSTART_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                loResult.Data.DEND_DATE = DateTime.ParseExact(loResult.Data.CEND_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                loHeader = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAgreementDocumentList(PMT02640DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                var loResult = await loModel.GetAgreementDocumentStreamAsync(poEntity);
                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocumentDate))
                    {
                        x.DDOC_DATE = ldDocumentDate;
                    }
                    else
                    {
                        x.DDOC_DATE = null;
                    }
                    if (DateTime.TryParseExact(x.CEXPIRED_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldExpiredDate))
                    {
                        x.DEXPIRED_DATE = ldExpiredDate;
                    }
                    else
                    {
                        x.DEXPIRED_DATE = null;
                    }
                });

                loDocumentList = new ObservableCollection<PMT02640DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAgreementDocument(PMT02640DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;
                poEntity.CDEPT_CODE = loHeader.CDEPT_CODE;
                var loResult = await loModel.R_ServiceGetRecordAsync(poEntity);
                if (DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocumentDate))
                {
                    loResult.DDOC_DATE = ldDocumentDate;
                }
                else
                {
                    loResult.DDOC_DATE = null;
                }
                if (DateTime.TryParseExact(loResult.CEXPIRED_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldExpiredDate))
                {
                    loResult.DEXPIRED_DATE = ldExpiredDate;
                }
                else
                {
                    loResult.DEXPIRED_DATE = null;
                }

                loDocument = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveAgreementDocument(PMT02640DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CDOC_DATE = poEntity.DDOC_DATE.Value.ToString("yyyyMMdd");
                poEntity.CEXPIRED_DATE = poEntity.DEXPIRED_DATE.Value.ToString("yyyyMMdd");
                poEntity.CTRANS_CODE = ConstantVariable.VAR_TRANS_CODE;

                var loResult = await loModel.R_ServiceSaveAsync(poEntity, poCRUDMode);

                if (DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocumentDate))
                {
                    loResult.DDOC_DATE = ldDocumentDate;
                }
                else
                {
                    loResult.DDOC_DATE = null;
                }
                if (DateTime.TryParseExact(loResult.CEXPIRED_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldExpiredDate))
                {
                    loResult.DEXPIRED_DATE = ldExpiredDate;
                }
                else
                {
                    loResult.DEXPIRED_DATE = null;
                }

                loDocument = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteAgreementDocument(PMT02640DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await loModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
