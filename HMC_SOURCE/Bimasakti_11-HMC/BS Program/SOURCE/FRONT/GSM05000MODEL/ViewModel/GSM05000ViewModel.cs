using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000Model.ViewModel
{
    public class GSM05000ViewModel : R_ViewModel<GSM05000DTO>
    {
        private GSM05000Model _GSM05000Model = new();
        public ObservableCollection<GSM05000GridDTO> GridList = new();
        public GSM05000DTO Entity = new();
        public GSM05000DTO TempEntity = new();

        // Delimiter List
        public List<GSM05000DelimiterDTO> DelimiterListPrefix = new();
        public List<GSM05000DelimiterDTO> DelimiterListNumber = new();
        public List<GSM05000DelimiterDTO> DelimiterListDepartment = new();
        public List<GSM05000DelimiterDTO> DelimiterListTransCode = new();
        public List<GSM05000DelimiterDTO> DelimiterListPeriodMode = new();

        // For get Sequence
        public int DeptSequence;
        public int TrxSequence;
        public int PeriodSequence;
        public int LenSequence;
        public int PrefixSequence;

        #region Combo Box Helper List

        public List<KeyValuePair<string, string>> CPERIOD_MODE { get; } = new()
        {
            new("N", "None"),
            new("P", "Periodically"),
            new("Y", "Yearly")
        };

        public List<KeyValuePair<string, string>> CAPPROVAL_MODE { get; } = new()
        {
            new("1", "Hierarchy"),
            new("2", "Flat And"),
            new("3", "Flat Or"),
            new("", ""),
        };

        public List<KeyValuePair<string, string>> CYEAR_FORMAT { get; set; } = new()
        {
            new("1", "YY"),
            new("2", "YYYY"),
        };

        #endregion

        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05000Model.GetAllStreamAsync();
                GridList = new ObservableCollection<GSM05000GridDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetEntity(GSM05000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                Entity = await _GSM05000Model.R_ServiceGetRecordAsync(poEntity);
                TempEntity = R_FrontUtility.ConvertObjectToObject<GSM05000DTO>(Entity);
                GetSequence();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveEntity(GSM05000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CSEQUENCE01 = Entity.CSEQUENCE01;
                poNewEntity.CSEQUENCE02 = Entity.CSEQUENCE02;
                poNewEntity.CSEQUENCE03 = Entity.CSEQUENCE03;
                poNewEntity.CSEQUENCE04 = Entity.CSEQUENCE04;

                Entity = await _GSM05000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<bool> CheckExistData(GSM05000DTO poEntity, GSM05000eTabName pcEtabName)
        {
            var loEx = new R_Exception();
            bool llReturn = false;

            try
            {
                GSM05000TrxCodeParamsDTO loParams = new();
                loParams.CTRANS_CODE = poEntity.CTRANS_CODE;
                loParams.ETAB_NAME = pcEtabName;
                var loResult = await _GSM05000Model.CheckExistDataAsync(loParams);
                llReturn = R_FrontUtility.ConvertObjectToObject<bool>(loResult.EXIST);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return llReturn;
        }

        public async Task GetDelimiterList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05000Model.GetDelimiterAsync();

                foreach (var VARIABLE in loReturn.Data)
                {
                    VARIABLE.CCODE = VARIABLE.CCODE.Trim();
                }

                DelimiterListPrefix = loReturn.Data;
                DelimiterListNumber = loReturn.Data;
                DelimiterListDepartment = loReturn.Data;
                DelimiterListTransCode = loReturn.Data;
                DelimiterListPeriodMode = loReturn.Data;
                DelimiterListNumber = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Parse sequence

        public void GetSequence()
        {
            DeptSequence = GetSequenceIndex("01");
            TrxSequence = GetSequenceIndex("02");
            PeriodSequence = GetSequenceIndex("03");
            LenSequence = GetSequenceIndex("04");
            PrefixSequence = GetSequenceIndex("05");
        }

        private int GetSequenceIndex(string pcIndex)
        {
            if (Entity.CSEQUENCE01 == pcIndex) return 1;
            if (Entity.CSEQUENCE02 == pcIndex) return 2;
            if (Entity.CSEQUENCE03 == pcIndex) return 3;
            if (Entity.CSEQUENCE04 == pcIndex) return 4;

            return 0;
        }

        #endregion

        #region REF NO / Sample Code

        public string REFNO = "";
        public string REFNO_LENGTH = "";

        public void getRefNo()
        {
            var loEx = new R_Exception();

            try
            {
                string[] sequenceCodes = new string[4];

                for (int i = 0; i < 4; i++)
                {
                    if (DeptSequence == i + 1)
                    {
                        sequenceCodes[i] = "01";
                    }
                    else if (TrxSequence == i + 1)
                    {
                        sequenceCodes[i] = "02";
                    }
                    else if (PeriodSequence == i + 1)
                    {
                        sequenceCodes[i] = "03";
                    }
                    else if (LenSequence == i + 1)
                    {
                        sequenceCodes[i] = "04";
                    }
                    else if (PrefixSequence == i + 1)
                    {
                        sequenceCodes[i] = "05";
                    }
                    else
                    {
                        sequenceCodes[i] = "0";
                    }
                }

                Data.CSEQUENCE01 = sequenceCodes[0];
                Data.CSEQUENCE02 = sequenceCodes[1];
                Data.CSEQUENCE03 = sequenceCodes[2];
                Data.CSEQUENCE04 = sequenceCodes[3];

                var loResult = string.Concat(
                    Data.CPREFIX.Trim() +
                    (string.IsNullOrEmpty(Data.CPREFIX) ? "" : Data.CPREFIX_DELIMITER) +
                    (
                        Data.CSEQUENCE01 == "01" ? "DDDDDDDD" :
                        Data.CSEQUENCE01 == "02" ? "TTTTTT" :
                        Data.CSEQUENCE01 == "03" ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        Data.CSEQUENCE01 == "04" ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    ) +
                    (string.IsNullOrEmpty(Data.CSEQUENCE01)
                        ? ""
                        : (
                            Data.CSEQUENCE01 == "01" ? Data.CDEPT_DELIMITER :
                            Data.CSEQUENCE01 == "02" ? Data.CTRANSACTION_DELIMITER :
                            Data.CSEQUENCE01 == "03" ? Data.CPERIOD_DELIMITER :
                            Data.CSEQUENCE01 == "04" ? Data.CNUMBER_DELIMITER :
                            ""
                        )) +
                    (
                        Data.CSEQUENCE02 == "01" ? "DDDDDDDD" :
                        Data.CSEQUENCE02 == "02" ? "TTTTTT" :
                        Data.CSEQUENCE02 == "03" ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        Data.CSEQUENCE02 == "04" ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    ) +
                    (string.IsNullOrEmpty(Data.CSEQUENCE02)
                        ? ""
                        : (
                            Data.CSEQUENCE02 == "01" ? Data.CDEPT_DELIMITER :
                            Data.CSEQUENCE02 == "02" ? Data.CTRANSACTION_DELIMITER :
                            Data.CSEQUENCE02 == "03" ? Data.CPERIOD_DELIMITER :
                            Data.CSEQUENCE02 == "04" ? Data.CNUMBER_DELIMITER :
                            ""
                        )) +
                    (
                        Data.CSEQUENCE03 == "01" ? "DDDDDDDD" :
                        Data.CSEQUENCE03 == "02" ? "TTTTTT" :
                        Data.CSEQUENCE03 == "03" ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        Data.CSEQUENCE03 == "04" ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    ) +
                    (string.IsNullOrEmpty(Data.CSEQUENCE03)
                        ? ""
                        : (
                            Data.CSEQUENCE03 == "01" ? Data.CDEPT_DELIMITER :
                            Data.CSEQUENCE03 == "02" ? Data.CTRANSACTION_DELIMITER :
                            Data.CSEQUENCE03 == "03" ? Data.CPERIOD_DELIMITER :
                            Data.CSEQUENCE03 == "04" ? Data.CNUMBER_DELIMITER :
                            ""
                        )) +
                    (
                        Data.CSEQUENCE04 == "01" ? "DDDDDDDD" :
                        Data.CSEQUENCE04 == "02" ? "TTTTTT" :
                        Data.CSEQUENCE04 == "03" ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        Data.CSEQUENCE04 == "04" ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    ) +
                    (string.IsNullOrEmpty(Data.CSEQUENCE04)
                        ? ""
                        : (
                            Data.CSEQUENCE04 == "01" ? Data.CDEPT_DELIMITER :
                            Data.CSEQUENCE04 == "02" ? Data.CTRANSACTION_DELIMITER :
                            Data.CSEQUENCE04 == "03" ? Data.CPERIOD_DELIMITER :
                            Data.CSEQUENCE04 == "04" ? Data.CNUMBER_DELIMITER :
                            ""
                        )) +
                    Entity.CSUFFIX.Trim()
                );

                REFNO = loResult;
                REFNO_LENGTH = REFNO.Length.ToString();
                // REFNO_LENGTH = Regex.Replace(REFNO, "[^a-zA-Z0-9]", "").Length.ToString();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        public bool IsDuplicateSequence(GSM05000DTO poEntity)
        {
            bool llReturn = false;
            List<int> list = new List<int>
            {
                PeriodSequence, LenSequence
            };

            if (poEntity.LDEPT_MODE)
            {
                list.Insert(0, DeptSequence);
            }

            if (poEntity.LTRANSACTION_MODE)
            {
                list.Insert(0, TrxSequence);
            }

            // var isZero = list.Any(x => x == 0);

            var isDuplicate = list.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            if (isDuplicate.Count > 0)
            {
                llReturn = true;
            }

            return llReturn;
        }

        public bool IsValidSequence(GSM05000DTO poEntity, out int pnSequence)
        {
            bool llReturn = true;
            List<int> list = new List<int>
            {
                PeriodSequence, LenSequence
            };

            if (poEntity.LDEPT_MODE)
            {
                list.Insert(0, DeptSequence);
            }

            if (poEntity.LTRANSACTION_MODE)
            {
                list.Insert(0, TrxSequence);
            }

            pnSequence = list.Count();

            bool llValid;

            for (var i = 1; i <= list.Count; i++)
            {
                llValid = list.Any(x => x == i);
                if (!llValid)
                {
                    llReturn = false;
                    break;
                }
            }

            return llReturn;
        }

        //parameter bool didapat dari kondisi yang mengaktifkan mode
        public void AutoSequence(eNumericList peMode, bool poState)
        {
            // buat array untuk menampung sequence key dan value
            var loSequence = new Dictionary<eNumericList, int>
            {
                { eNumericList.DeptMode, this.DeptSequence },
                { eNumericList.TrxMode, this.TrxSequence },
                { eNumericList.PeriodMode, this.PeriodSequence },
                { eNumericList.LenMode, this.LenSequence }
            };

            if (poState)
            {
                //buat agar sequence yang diaktifkan menjadi angka tertinggi di array, bukan jumlah data
                // loSequence[peMode] = loSequence.Values.Max() + 1;
                
                //looping unutk mengecek dari angka 1 - 4
                for (var i = 1; i <= loSequence.Count; i++)
                {
                    //jika sequence yang diaktifkan tidak ada di array, maka sequence yang diaktifkan menjadi angka tertinggi
                    if (!loSequence.ContainsValue(i))
                    {
                        loSequence[peMode] = i;
                        break;
                    }
                }
                
            }
            else
            {
                if (peMode == eNumericList.DeptMode)
                    this.Data.CDEPT_DELIMITER = "";
                else if (peMode == eNumericList.TrxMode)
                    this.Data.CTRANSACTION_DELIMITER = "";
                else if (peMode == eNumericList.PeriodMode)
                    this.Data.CPERIOD_DELIMITER = "";
                else if (peMode == eNumericList.LenMode) 
                    this.Data.CNUMBER_DELIMITER = "";

                // loSequence = loSequence.Where(x => x.Value > loSequence[peMode]).ToDictionary(x => x.Key, x => x.Value - 1);
                
                foreach (var kvp in loSequence.Where(x => x.Value > loSequence[peMode]))
                {
                    loSequence[kvp.Key] -= 1;
                }
                
                loSequence[peMode] = 0;
            }

            this.DeptSequence = loSequence[eNumericList.DeptMode];
            this.TrxSequence = loSequence[eNumericList.TrxMode];
            this.PeriodSequence = loSequence[eNumericList.PeriodMode];
            this.LenSequence = loSequence[eNumericList.LenMode];
        }
    }

    public enum eNumericList
    {
        DeptMode,
        TrxMode,
        PeriodMode,
        LenMode,
        // PrefixMode
    }
}