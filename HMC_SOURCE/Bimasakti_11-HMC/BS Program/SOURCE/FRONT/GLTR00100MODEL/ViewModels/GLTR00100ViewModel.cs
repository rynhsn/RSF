using GLTR00100COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml;

namespace GLTR00100MODEL
{
    public class GLTR00100ViewModel : R_ViewModel<GLTR00100DTO>
    {
        private GLTR00100Model _GLTR00100Model = new GLTR00100Model();

        public GLTR00100InitialDTO InitialVar = new GLTR00100InitialDTO();
        public GLTR00100DTO DataJournal = new GLTR00100DTO();

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLTR00100Model.GetInitialVarAsync();

                InitialVar = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalTransaction(GLTR00100DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLTR00100Model.GetGLJournalAsync(poParam);

                DataJournal = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
