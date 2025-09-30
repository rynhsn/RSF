using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02580;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs;
using R_BlazorFrontEnd.Helpers;
using GSM02500FrontResources;

namespace GSM02500MODEL.View_Model
{
    public class GSM02580ViewModel : R_ViewModel<GSM02580DTO>
    {
        private GSM02580Model loModel = new GSM02580Model();

        private GSM02500Model loSharedModel = new GSM02500Model();

        public GSM02580DTO loOperationalHourDetail = null;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public async Task GetSelectedPropertyAsync()
        {
            R_Exception loException = new R_Exception();
            SelectedPropertyParameterDTO loParam = null;
            try
            {
                loParam = new SelectedPropertyParameterDTO()
                {
                    Data = SelectedProperty
                };
                SelectedProperty = await loSharedModel.GetSelectedPropertyAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetOperationalHourAsync()
        {
            R_Exception loEx = new R_Exception();
            GSM02580ParameterDTO loResult = null;
            GSM02580ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02580ParameterDTO()
                {
                    //Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };

                loResult = await loModel.R_ServiceGetRecordAsync(loParam);
                loResult.Data.CNOTES = string.IsNullOrWhiteSpace(loResult.Data.CNOTES) ? "" : loResult.Data.CNOTES;
                loOperationalHourDetail = loResult.Data;

                if (loResult.Data != null)
                {
                    loOperationalHourDetail.IMONDAY_OPEN_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CMONDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CMONDAY_OPEN_H);
                    loOperationalHourDetail.IMONDAY_OPEN_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CMONDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CMONDAY_OPEN_M);
                    loOperationalHourDetail.IMONDAY_CLOSE_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CMONDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CMONDAY_CLOSE_H);
                    loOperationalHourDetail.IMONDAY_CLOSE_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CMONDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CMONDAY_CLOSE_M);

                    loOperationalHourDetail.ITUESDAY_OPEN_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CTUESDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CTUESDAY_OPEN_H);
                    loOperationalHourDetail.ITUESDAY_OPEN_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CTUESDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CTUESDAY_OPEN_M);
                    loOperationalHourDetail.ITUESDAY_CLOSE_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CTUESDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CTUESDAY_CLOSE_H);
                    loOperationalHourDetail.ITUESDAY_CLOSE_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CTUESDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CTUESDAY_CLOSE_M);

                    loOperationalHourDetail.IWEDNESDAY_OPEN_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CWEDNESDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CWEDNESDAY_OPEN_H);
                    loOperationalHourDetail.IWEDNESDAY_OPEN_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CWEDNESDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CWEDNESDAY_OPEN_M);
                    loOperationalHourDetail.IWEDNESDAY_CLOSE_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CWEDNESDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CWEDNESDAY_CLOSE_H);
                    loOperationalHourDetail.IWEDNESDAY_CLOSE_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CWEDNESDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CWEDNESDAY_CLOSE_M);

                    loOperationalHourDetail.ITHURSDAY_OPEN_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CTHURSDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CTHURSDAY_OPEN_H);
                    loOperationalHourDetail.ITHURSDAY_OPEN_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CTHURSDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CTHURSDAY_OPEN_M);
                    loOperationalHourDetail.ITHURSDAY_CLOSE_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CTHURSDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CTHURSDAY_CLOSE_H);
                    loOperationalHourDetail.ITHURSDAY_CLOSE_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CTHURSDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CTHURSDAY_CLOSE_M);

                    loOperationalHourDetail.IFRIDAY_OPEN_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CFRIDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CFRIDAY_OPEN_H);
                    loOperationalHourDetail.IFRIDAY_OPEN_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CFRIDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CFRIDAY_OPEN_M);
                    loOperationalHourDetail.IFRIDAY_CLOSE_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CFRIDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CFRIDAY_CLOSE_H);
                    loOperationalHourDetail.IFRIDAY_CLOSE_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CFRIDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CFRIDAY_CLOSE_M);

                    loOperationalHourDetail.ISATURDAY_OPEN_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CSATURDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CSATURDAY_OPEN_H);
                    loOperationalHourDetail.ISATURDAY_OPEN_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CSATURDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CSATURDAY_OPEN_M);
                    loOperationalHourDetail.ISATURDAY_CLOSE_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CSATURDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CSATURDAY_CLOSE_H);
                    loOperationalHourDetail.ISATURDAY_CLOSE_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CSATURDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CSATURDAY_CLOSE_M);

                    loOperationalHourDetail.ISUNDAY_OPEN_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CSUNDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CSUNDAY_OPEN_H);
                    loOperationalHourDetail.ISUNDAY_OPEN_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CSUNDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CSUNDAY_OPEN_M);
                    loOperationalHourDetail.ISUNDAY_CLOSE_H = string.IsNullOrWhiteSpace(loOperationalHourDetail.CSUNDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CSUNDAY_CLOSE_H);
                    loOperationalHourDetail.ISUNDAY_CLOSE_M = string.IsNullOrWhiteSpace(loOperationalHourDetail.CSUNDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CSUNDAY_CLOSE_M);
                }
               
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveOperationalHourAsync(GSM02580DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            GSM02580ParameterDTO loResult = null;
            GSM02580ParameterDTO loParam = null;

            try
            {
                poEntity.CMONDAY_OPEN_H = poEntity.IMONDAY_OPEN_H.ToString("D2");
                poEntity.CMONDAY_OPEN_M = poEntity.IMONDAY_OPEN_M.ToString("D2");
                poEntity.CMONDAY_CLOSE_H = poEntity.IMONDAY_CLOSE_H.ToString("D2");
                poEntity.CMONDAY_CLOSE_M = poEntity.IMONDAY_CLOSE_M.ToString("D2");

                poEntity.CTUESDAY_OPEN_H = poEntity.ITUESDAY_OPEN_H.ToString("D2");
                poEntity.CTUESDAY_OPEN_M = poEntity.ITUESDAY_OPEN_M.ToString("D2");
                poEntity.CTUESDAY_CLOSE_H = poEntity.ITUESDAY_CLOSE_H.ToString("D2");
                poEntity.CTUESDAY_CLOSE_M = poEntity.ITUESDAY_CLOSE_M.ToString("D2");

                poEntity.CWEDNESDAY_OPEN_H = poEntity.IWEDNESDAY_OPEN_H.ToString("D2");
                poEntity.CWEDNESDAY_OPEN_M = poEntity.IWEDNESDAY_OPEN_M.ToString("D2");
                poEntity.CWEDNESDAY_CLOSE_H = poEntity.IWEDNESDAY_CLOSE_H.ToString("D2");
                poEntity.CWEDNESDAY_CLOSE_M = poEntity.IWEDNESDAY_CLOSE_M.ToString("D2");

                poEntity.CTHURSDAY_OPEN_H = poEntity.ITHURSDAY_OPEN_H.ToString("D2");
                poEntity.CTHURSDAY_OPEN_M = poEntity.ITHURSDAY_OPEN_M.ToString("D2");
                poEntity.CTHURSDAY_CLOSE_H = poEntity.ITHURSDAY_CLOSE_H.ToString("D2");
                poEntity.CTHURSDAY_CLOSE_M = poEntity.ITHURSDAY_CLOSE_M.ToString("D2");

                poEntity.CFRIDAY_OPEN_H = poEntity.IFRIDAY_OPEN_H.ToString("D2");
                poEntity.CFRIDAY_OPEN_M = poEntity.IFRIDAY_OPEN_M.ToString("D2");
                poEntity.CFRIDAY_CLOSE_H = poEntity.IFRIDAY_CLOSE_H.ToString("D2");
                poEntity.CFRIDAY_CLOSE_M = poEntity.IFRIDAY_CLOSE_M.ToString("D2");

                poEntity.CSATURDAY_OPEN_H = poEntity.ISATURDAY_OPEN_H.ToString("D2");
                poEntity.CSATURDAY_OPEN_M = poEntity.ISATURDAY_OPEN_M.ToString("D2");
                poEntity.CSATURDAY_CLOSE_H = poEntity.ISATURDAY_CLOSE_H.ToString("D2");
                poEntity.CSATURDAY_CLOSE_M = poEntity.ISATURDAY_CLOSE_M.ToString("D2");

                poEntity.CSUNDAY_OPEN_H = poEntity.ISUNDAY_OPEN_H.ToString("D2");
                poEntity.CSUNDAY_OPEN_M = poEntity.ISUNDAY_OPEN_M.ToString("D2");
                poEntity.CSUNDAY_CLOSE_H = poEntity.ISUNDAY_CLOSE_H.ToString("D2");
                poEntity.CSUNDAY_CLOSE_M = poEntity.ISUNDAY_CLOSE_M.ToString("D2");

                loParam = new GSM02580ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loOperationalHourDetail = loResult.Data;

                loOperationalHourDetail.IMONDAY_OPEN_H = string.IsNullOrEmpty(loOperationalHourDetail.CMONDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CMONDAY_OPEN_H);
                loOperationalHourDetail.IMONDAY_OPEN_M = string.IsNullOrEmpty(loOperationalHourDetail.CMONDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CMONDAY_OPEN_M);
                loOperationalHourDetail.IMONDAY_CLOSE_H = string.IsNullOrEmpty(loOperationalHourDetail.CMONDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CMONDAY_CLOSE_H);
                loOperationalHourDetail.IMONDAY_CLOSE_M = string.IsNullOrEmpty(loOperationalHourDetail.CMONDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CMONDAY_CLOSE_M);

                loOperationalHourDetail.ITUESDAY_OPEN_H = string.IsNullOrEmpty(loOperationalHourDetail.CTUESDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CTUESDAY_OPEN_H);
                loOperationalHourDetail.ITUESDAY_OPEN_M = string.IsNullOrEmpty(loOperationalHourDetail.CTUESDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CTUESDAY_OPEN_M);
                loOperationalHourDetail.ITUESDAY_CLOSE_H = string.IsNullOrEmpty(loOperationalHourDetail.CTUESDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CTUESDAY_CLOSE_H);
                loOperationalHourDetail.ITUESDAY_CLOSE_M = string.IsNullOrEmpty(loOperationalHourDetail.CTUESDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CTUESDAY_CLOSE_M);

                loOperationalHourDetail.IWEDNESDAY_OPEN_H = string.IsNullOrEmpty(loOperationalHourDetail.CWEDNESDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CWEDNESDAY_OPEN_H);
                loOperationalHourDetail.IWEDNESDAY_OPEN_M = string.IsNullOrEmpty(loOperationalHourDetail.CWEDNESDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CWEDNESDAY_OPEN_M);
                loOperationalHourDetail.IWEDNESDAY_CLOSE_H = string.IsNullOrEmpty(loOperationalHourDetail.CWEDNESDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CWEDNESDAY_CLOSE_H);
                loOperationalHourDetail.IWEDNESDAY_CLOSE_M = string.IsNullOrEmpty(loOperationalHourDetail.CWEDNESDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CWEDNESDAY_CLOSE_M);

                loOperationalHourDetail.ITHURSDAY_OPEN_H = string.IsNullOrEmpty(loOperationalHourDetail.CTHURSDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CTHURSDAY_OPEN_H);
                loOperationalHourDetail.ITHURSDAY_OPEN_M = string.IsNullOrEmpty(loOperationalHourDetail.CTHURSDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CTHURSDAY_OPEN_M);
                loOperationalHourDetail.ITHURSDAY_CLOSE_H = string.IsNullOrEmpty(loOperationalHourDetail.CTHURSDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CTHURSDAY_CLOSE_H);
                loOperationalHourDetail.ITHURSDAY_CLOSE_M = string.IsNullOrEmpty(loOperationalHourDetail.CTHURSDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CTHURSDAY_CLOSE_M);

                loOperationalHourDetail.IFRIDAY_OPEN_H = string.IsNullOrEmpty(loOperationalHourDetail.CFRIDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CFRIDAY_OPEN_H);
                loOperationalHourDetail.IFRIDAY_OPEN_M = string.IsNullOrEmpty(loOperationalHourDetail.CFRIDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CFRIDAY_OPEN_M);
                loOperationalHourDetail.IFRIDAY_CLOSE_H = string.IsNullOrEmpty(loOperationalHourDetail.CFRIDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CFRIDAY_CLOSE_H);
                loOperationalHourDetail.IFRIDAY_CLOSE_M = string.IsNullOrEmpty(loOperationalHourDetail.CFRIDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CFRIDAY_CLOSE_M);

                loOperationalHourDetail.ISATURDAY_OPEN_H = string.IsNullOrEmpty(loOperationalHourDetail.CSATURDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CSATURDAY_OPEN_H);
                loOperationalHourDetail.ISATURDAY_OPEN_M = string.IsNullOrEmpty(loOperationalHourDetail.CSATURDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CSATURDAY_OPEN_M);
                loOperationalHourDetail.ISATURDAY_CLOSE_H = string.IsNullOrEmpty(loOperationalHourDetail.CSATURDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CSATURDAY_CLOSE_H);
                loOperationalHourDetail.ISATURDAY_CLOSE_M = string.IsNullOrEmpty(loOperationalHourDetail.CSATURDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CSATURDAY_CLOSE_M);

                loOperationalHourDetail.ISUNDAY_OPEN_H = string.IsNullOrEmpty(loOperationalHourDetail.CSUNDAY_OPEN_H) ? 0 : int.Parse(loOperationalHourDetail.CSUNDAY_OPEN_H);
                loOperationalHourDetail.ISUNDAY_OPEN_M = string.IsNullOrEmpty(loOperationalHourDetail.CSUNDAY_OPEN_M) ? 0 : int.Parse(loOperationalHourDetail.CSUNDAY_OPEN_M);
                loOperationalHourDetail.ISUNDAY_CLOSE_H = string.IsNullOrEmpty(loOperationalHourDetail.CSUNDAY_CLOSE_H) ? 0 : int.Parse(loOperationalHourDetail.CSUNDAY_CLOSE_H);
                loOperationalHourDetail.ISUNDAY_CLOSE_M = string.IsNullOrEmpty(loOperationalHourDetail.CSUNDAY_CLOSE_M) ? 0 : int.Parse(loOperationalHourDetail.CSUNDAY_CLOSE_M);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteOperationalHourAsync(GSM02580DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02580ParameterDTO loParam = null;

            try
            {
                loParam = new GSM02580ParameterDTO()
                {
                    Data = poEntity,
                    CPROPERTY_ID = SelectedProperty.CPROPERTY_ID
                };

                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
