using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LMM02500Common.DTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace LMM02500Model.ViewModel
{
    public class LMM02500ViewModel : R_ViewModel<LMM02500ProfileDTO>

    {
        private readonly LMM02500Model _modelLMM02500 = new LMM02500Model();

        //public string lcCPROPERTY_ID = "";

        public ObservableCollection<LMM02500ProfileDTO> loGridListLMM02500 = new ObservableCollection<LMM02500ProfileDTO>();

        public string PropertyValueContext = "";
        public List<LMM02500ParameterDTO> loPropertyList { get; set; } = new List<LMM02500ParameterDTO>();

        public bool _comboBoxEnabled = true;

        public async Task GetAllTenantGroupList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _modelLMM02500.GetAllTenantGroupStreamAsync(PropertyValueContext);

                loGridListLMM02500 = new ObservableCollection<LMM02500ProfileDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                LMM02500ListDTO<LMM02500ParameterDTO> loResult = await _modelLMM02500.GetParameterTenantGroupAsync();

                PropertyValueContext = (PropertyValueContext != "" ? PropertyValueContext : loResult.Data.FirstOrDefault()?.CPROPERTY_ID) ?? throw new InvalidOperationException();

                loPropertyList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}