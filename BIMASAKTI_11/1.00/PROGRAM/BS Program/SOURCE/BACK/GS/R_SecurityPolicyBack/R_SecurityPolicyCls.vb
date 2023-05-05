Imports R_BackEnd
Imports R_Common
Imports R_SecurityPolicyCommon

Public Class R_SecurityPolicyCls

    Private Class _ParameterDTO
        Public Property CPARAMETER_ID As String
        Public Property CPARAMETER_VALUE As String
    End Class

    Public Function R_GetSecurityPolicy(ByVal poParameter As R_SecurityPolicyParameterDTO) As R_SecurityPolicyDTO
        Dim lcQuery As String = ""
        Dim loUser As R_SecurityPolicyDTO = Nothing
        Dim loUserException As R_SecurityPolicyDTO = Nothing
        Dim loReturn As R_SecurityPolicyDTO = Nothing
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            If poParameter.CSECURITY_AND_ACCOUNT_POLICY = "byuser" Then
                lcQuery = "SELECT * "
                lcQuery += "FROM "
                lcQuery += "GST_GENERAL_SECURITY_POLICY (NOLOCK) "

                loReturn = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                If loReturn IsNot Nothing Then
                    lcQuery = "SELECT CONVERT(VARCHAR, DLAST_UPDATE_PSWD, 112) AS CLAST_UPDATE_PASSWORD, CONVERT(VARCHAR, GETDATE(), 120) AS CCURRENT_DATE, CONVERT(varchar, DACCOUNT_LOCKOUT, 120) AS CACCOUNT_LOCKOUT_DATE, CONVERT(varchar, DFAILED_ATTEMPT, 120) AS CFAILED_ATTEMPT_DATE, NFAILED_ATTEMPT as NFAILED_ATTEMPT "
                    lcQuery += "FROM "
                    lcQuery += "SAM_USER (NOLOCK) "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery = String.Format(lcQuery, poParameter.CUSER_ID)

                    loUser = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                    If loUser IsNot Nothing Then
                        loReturn.CLAST_UPDATE_PASSWORD = loUser.CLAST_UPDATE_PASSWORD
                        loReturn.CCURRENT_DATE = loUser.CCURRENT_DATE
                        loReturn.CACCOUNT_LOCKOUT_DATE = loUser.CACCOUNT_LOCKOUT_DATE
                        loReturn.CFAILED_ATTEMPT_DATE = loUser.CFAILED_ATTEMPT_DATE
                        loReturn.NFAILED_ATTEMPT = loUser.NFAILED_ATTEMPT
                    End If
                End If
            ElseIf poParameter.CSECURITY_AND_ACCOUNT_POLICY = "bycompany" Then
                lcQuery = "SELECT * "
                lcQuery += "FROM "
                lcQuery += "GST_COMPANY_SECURITY_POLICY_EXCEPTION (NOLOCK) "
                lcQuery += "WHERE CCOMPANY_ID = '{0}' "
                lcQuery = String.Format(lcQuery, poParameter.CCOMPANY_ID)

                loReturn = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                If loReturn Is Nothing Then
                    lcQuery = "SELECT * "
                    lcQuery += "FROM "
                    lcQuery += "GST_GENERAL_SECURITY_POLICY (NOLOCK) "

                    loReturn = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault
                End If

                If loReturn IsNot Nothing Then
                    lcQuery = "SELECT CONVERT(VARCHAR, DLAST_UPDATE_PSWD, 112) AS CLAST_UPDATE_PASSWORD, CONVERT(VARCHAR, GETDATE(), 120) AS CCURRENT_DATE, CONVERT(varchar, DACCOUNT_LOCKOUT, 120) AS CACCOUNT_LOCKOUT_DATE, CONVERT(varchar, DFAILED_ATTEMPT, 120) AS CFAILED_ATTEMPT_DATE, NFAILED_ATTEMPT as NFAILED_ATTEMPT "
                    lcQuery += "FROM "
                    lcQuery += "SAM_USER_COMPANY (NOLOCK) "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery += "AND CCOMPANY_ID = '{1}' "
                    lcQuery = String.Format(lcQuery, poParameter.CUSER_ID, poParameter.CCOMPANY_ID)

                    loUser = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                    If loUser IsNot Nothing Then
                        loReturn.CLAST_UPDATE_PASSWORD = loUser.CLAST_UPDATE_PASSWORD
                        loReturn.CCURRENT_DATE = loUser.CCURRENT_DATE
                        loReturn.CACCOUNT_LOCKOUT_DATE = loUser.CACCOUNT_LOCKOUT_DATE
                        loReturn.CFAILED_ATTEMPT_DATE = loUser.CFAILED_ATTEMPT_DATE
                        loReturn.NFAILED_ATTEMPT = loUser.NFAILED_ATTEMPT
                    End If
                End If
            End If

            If loReturn IsNot Nothing Then
                lcQuery = "SELECT * "
                lcQuery += "FROM "
                lcQuery += "GST_USER_SECURITY_POLICY_EXCEPTION (NOLOCK) "
                lcQuery += "WHERE CUSER_ID = '{0}' "
                lcQuery = String.Format(lcQuery, poParameter.CUSER_ID)

                loUserException = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                If loUserException IsNot Nothing Then
                    loReturn.LMAXIMUM_PASSWORD_AGE = loUserException.LMAXIMUM_PASSWORD_AGE
                    loReturn.CMAXIMUM_PASSWORD_AGE_OPTION = loUserException.CMAXIMUM_PASSWORD_AGE_OPTION
                    loReturn.IMAXIMUM_PASSWORD_AGE_DAYS = loUserException.IMAXIMUM_PASSWORD_AGE_DAYS
                    loReturn.IMAXIMUM_PASSWORD_AGE_CERTAIN_DAY_OF_MONTH = loUserException.IMAXIMUM_PASSWORD_AGE_CERTAIN_DAY_OF_MONTH
                    loReturn.IMAXIMUM_PASSWORD_AGE_MONTH = loUserException.IMAXIMUM_PASSWORD_AGE_MONTH
                    loReturn.CMAXIMUM_PASSWORD_AGE_MONTH_EFFECTIVE_DATE = loUserException.CMAXIMUM_PASSWORD_AGE_MONTH_EFFECTIVE_DATE
                End If

                If loReturn.LMAXIMUM_PASSWORD_AGE Then
                    If loReturn.CMAXIMUM_PASSWORD_AGE_OPTION = "days" Then
                        If Not String.IsNullOrWhiteSpace(loReturn.CLAST_UPDATE_PASSWORD) Then 'berarti password yang digunakan adalah password reset
                            loReturn.CNEXT_UPDATE_PASSWORD = DateAdd(DateInterval.Day, loReturn.IMAXIMUM_PASSWORD_AGE_DAYS, Date.ParseExact(loReturn.CLAST_UPDATE_PASSWORD, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture)).ToString("yyyy-MM-dd HH:mm:ss")
                        End If
                    ElseIf loReturn.CMAXIMUM_PASSWORD_AGE_OPTION = "daysofmonth" Then
                        loReturn.CNEXT_UPDATE_PASSWORD = loReturn.CMAXIMUM_PASSWORD_AGE_MONTH_EFFECTIVE_DATE
                    End If
                End If
            End If
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return loReturn
    End Function

    Public Function R_GetSecurityPolicyParameter() As R_SecurityPolicyParameterDTO
        Dim lcQuery As String = ""
        Dim loResult As New R_SecurityPolicyParameterDTO
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            'If R_BackGlobalVar.ISMULTI_TENANT Then
            '    loResult.lChooseCompanyAtLoginPage = True
            '    loResult.cSecurityAndAccountPolicy = "bycompany"
            'Else
            lcQuery = "SELECT * "
            lcQuery += "FROM "
            lcQuery += "GST_PARAMETER (NOLOCK) "

            Dim loResultList As List(Of _ParameterDTO)
            loResultList = loDb.SqlExecObjectQuery(Of _ParameterDTO)(lcQuery)

            For Each loParameter As _ParameterDTO In loResultList
                If loParameter.CPARAMETER_ID = "ChooseCompanyAtLoginPage" Then
                    loResult.LCHOOSE_COMPANY_AT_LOGIN_PAGE = IIf(loParameter.CPARAMETER_VALUE = "true", True, False)
                ElseIf loParameter.CPARAMETER_ID = "SecurityAndAccountPolicy" Then
                    loResult.CSECURITY_AND_ACCOUNT_POLICY = loParameter.CPARAMETER_VALUE
                End If
            Next
            'End If
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return loResult
    End Function

    Public Function R_CheckPasswordHistory(ByVal poParameter As R_SecurityPolicyParameterDTO) As Boolean
        Dim lcQuery As String = ""
        Dim llResult As Boolean = False
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception
        Dim liENFORCE_PASSWORD_HISTORY As Integer = 0

        Try
            With poParameter
                'get IENFORCE_PASSWORD_HISTORY untuk tau berapa password terakhir yang disimpan
                lcQuery = "SELECT IENFORCE_PASSWORD_HISTORY "
                lcQuery += "FROM GST_GENERAL_SECURITY_POLICY (NOLOCK) "
                liENFORCE_PASSWORD_HISTORY = loDb.SqlExecObjectQuery(Of Integer)(lcQuery).FirstOrDefault
                liENFORCE_PASSWORD_HISTORY += 1 'karena kalo hanya 1 yang diambil password yg sekarang

                lcQuery = "SELECT TOP {0} CUSER_PASSWORD "
                lcQuery += "FROM "
                lcQuery = String.Format(lcQuery, liENFORCE_PASSWORD_HISTORY)

                If .CSECURITY_AND_ACCOUNT_POLICY = "bycompany" Then
                    lcQuery += "GST_COMPANY_SECURITY_HISTORY (NOLOCK) "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery += "AND CCOMPANY_ID = '{1}' ORDER BY DCREATE_DATE DESC "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID, .CHASH_PASSWORD)

                ElseIf poParameter.CSECURITY_AND_ACCOUNT_POLICY = "byuser" Then
                    lcQuery += "GST_GENERAL_SECURITY_HISTORY (NOLOCK) "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID)
                End If

                Dim loTempResult As List(Of R_SecurityPolicyDTO)
                loTempResult = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery)

                Dim loTempHashPass As R_SecurityPolicyDTO
                loTempHashPass = loTempResult.Where(Function(x) x.CUSER_PASSWORD = .CHASH_PASSWORD).Select(Function(x) x).FirstOrDefault

                llResult = loTempHashPass IsNot Nothing
            End With
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return llResult
    End Function

    Public Function R_GetCurrentDate() As String
        Dim lcQuery As String = ""
        Dim loResult As String = ""
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            lcQuery = "SELECT CONVERT(VARCHAR, GETDATE(), 120) AS cCurrentDate "
            lcQuery += "FROM "
            lcQuery += "GST_PARAMETER (NOLOCK) "

            loResult = loDb.SqlExecObjectQuery(Of String)(lcQuery).FirstOrDefault
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return loResult
    End Function

    Public Sub R_ResetAccountLockoutDuration(ByVal poParameter As R_SecurityPolicyParameterDTO)
        Dim lcQuery As String = ""
        Dim loResult As R_SecurityPolicyParameterDTO = Nothing
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            With poParameter
                If .CSECURITY_AND_ACCOUNT_POLICY = "bycompany" Then
                    lcQuery = "UPDATE SAM_USER_COMPANY "
                    lcQuery += "SET "
                    lcQuery += "NFAILED_ATTEMPT = 0, "
                    lcQuery += "DACCOUNT_LOCKOUT = NULL, "
                    lcQuery += "DFAILED_ATTEMPT = NULL "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery += "AND CCOMPANY_ID = '{1}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)
                Else
                    lcQuery = "UPDATE SAM_USER "
                    lcQuery += "SET "
                    lcQuery += "NFAILED_ATTEMPT = 0, "
                    lcQuery += "DACCOUNT_LOCKOUT = NULL, "
                    lcQuery += "DFAILED_ATTEMPT = NULL "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)
                End If
            End With

            loDb.SqlExecNonQuery(lcQuery)

            lcQuery = "SELECT * "
            lcQuery += "FROM "
            lcQuery += "GST_PARAMETER (NOLOCK) "

            Dim loResultList As List(Of _ParameterDTO)
            loResultList = loDb.SqlExecObjectQuery(Of _ParameterDTO)(lcQuery)

            loResult = New R_SecurityPolicyParameterDTO
            For Each loParameter As _ParameterDTO In loResultList
                If loParameter.CPARAMETER_ID = "ChooseCompanyAtLoginPage" Then
                    loResult.LCHOOSE_COMPANY_AT_LOGIN_PAGE = IIf(loParameter.CPARAMETER_VALUE = "true", True, False)
                ElseIf loParameter.CPARAMETER_ID = "SecurityAndAccountPolicy" Then
                    loResult.CSECURITY_AND_ACCOUNT_POLICY = loParameter.CPARAMETER_VALUE
                End If
            Next
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Sub R_ResetAccountLockoutThreshold(ByVal poParameter As R_SecurityPolicyParameterDTO)
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            With poParameter
                If .CSECURITY_AND_ACCOUNT_POLICY = "bycompany" Then
                    lcQuery = "UPDATE SAM_USER_COMPANY "
                    lcQuery += "SET "
                    lcQuery += "NFAILED_ATTEMPT = 0, "
                    lcQuery += "DFAILED_ATTEMPT = NULL "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery += "AND CCOMPANY_ID = '{1}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)
                Else
                    lcQuery = "UPDATE SAM_USER "
                    lcQuery += "SET "
                    lcQuery += "NFAILED_ATTEMPT = 0, "
                    lcQuery += "DFAILED_ATTEMPT = NULL "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)
                End If
            End With

            loDb.SqlExecNonQuery(lcQuery)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Sub R_UpdatePassword(ByVal poParameter As R_SecurityPolicyParameterDTO)
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            With poParameter
                If .CSECURITY_AND_ACCOUNT_POLICY = "bycompany" Then
                    If .CHASH_CURRENT_PASSWORD <> "" Then
                        lcQuery = "SELECT CUSER_ID "
                        lcQuery += "FROM "
                        lcQuery += "SAM_USER_COMPANY (NOLOCK) "
                        lcQuery += "WHERE CUSER_ID = '{0}' "
                        lcQuery += "AND CCOMPANY_ID = '{1}' "
                        lcQuery += "AND CUSER_PASSWORD = '{2}' "
                        lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID, .CHASH_CURRENT_PASSWORD)

                        Dim loResult As R_SecurityPolicyDTO = Nothing
                        loResult = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                        If loResult Is Nothing Then
                            loEx.Add(New Exception("Wrong old password"))
                        End If
                    End If

                    lcQuery = "UPDATE SAM_USER_COMPANY "
                    lcQuery += "SET "
                    lcQuery += "CUSER_PASSWORD = '{0}', "
                    lcQuery += "DLAST_UPDATE_PSWD = GETDATE() "
                    lcQuery += "WHERE CUSER_ID = '{1}' "
                    lcQuery += "AND CCOMPANY_ID = '{2}' "
                    lcQuery = String.Format(lcQuery, .CHASH_PASSWORD, .CUSER_ID, .CCOMPANY_ID)
                    loDb.SqlExecNonQuery(lcQuery)

                    lcQuery = "UPDATE SAM_USER "
                    lcQuery += "SET "
                    lcQuery += "CUSER_PASSWORD = '{0}', "
                    lcQuery += "DLAST_UPDATE_PSWD = GETDATE() "
                    lcQuery += "WHERE CUSER_ID = '{1}' "
                    lcQuery = String.Format(lcQuery, .CHASH_PASSWORD, .CUSER_ID)
                    loDb.SqlExecNonQuery(lcQuery)

                    If .LENFORCE_PASSWORD_HISTORY Then
                        lcQuery = "SELECT CUSER_ID as cUserId "
                        lcQuery += "FROM GST_COMPANY_SECURITY_HISTORY (NOLOCK) "
                        lcQuery += "WHERE CUSER_ID = '{0}' "
                        lcQuery += "AND CCOMPANY_ID = '{1}' "
                        lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)

                        Dim loTempResultList As List(Of R_SecurityPolicyDTO)
                        loTempResultList = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery)

                        If loTempResultList IsNot Nothing Then
                            If loTempResultList.Count >= .IENFORCE_PASSWORD_HISTORY Then
                                lcQuery = "DELETE GST_COMPANY_SECURITY_HISTORY "
                                lcQuery += "WHERE CUSER_ID = '{0}' "
                                lcQuery += "AND CCOMPANY_ID = '{1}' "
                                lcQuery += "AND DCREATE_DATE = (SELECT MIN(DCREATE_DATE) FROM GST_COMPANY_SECURITY_HISTORY) "
                                lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)

                                loDb.SqlExecNonQuery(lcQuery)
                            End If
                        End If

                        lcQuery = "INSERT INTO GST_COMPANY_SECURITY_HISTORY "
                        lcQuery += "(CUSER_ID, CCOMPANY_ID, CUSER_PASSWORD) "
                        lcQuery += "VALUES "
                        lcQuery += "('{0}', '{1}', '{2}')"
                        lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID, .CHASH_PASSWORD)

                        loDb.SqlExecNonQuery(lcQuery)
                    End If
                Else 'byuser atau none
                    If .CHASH_CURRENT_PASSWORD <> "" Then
                        lcQuery = "SELECT CUSER_ID "
                        lcQuery += "FROM "
                        lcQuery += "SAM_USER (NOLOCK) "
                        lcQuery += "WHERE CUSER_ID = '{0}' "
                        lcQuery += "AND CUSER_PASSWORD = '{1}' "
                        lcQuery = String.Format(lcQuery, .CUSER_ID, .CHASH_CURRENT_PASSWORD)

                        Dim loResult As R_SecurityPolicyDTO
                        loResult = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                        If loResult Is Nothing Then
                            loEx.Add(New Exception("Wrong old password"))
                        End If
                    End If

                    lcQuery = "UPDATE SAM_USER "
                    lcQuery += "SET "
                    lcQuery += "CUSER_PASSWORD = '{0}', "
                    lcQuery += "DLAST_UPDATE_PSWD = GETDATE() "
                    lcQuery += "WHERE CUSER_ID = '{1}' "
                    lcQuery = String.Format(lcQuery, .CHASH_PASSWORD, .CUSER_ID)

                    loDb.SqlExecNonQuery(lcQuery)

                    If .CSECURITY_AND_ACCOUNT_POLICY = "byuser" Then
                        If .LENFORCE_PASSWORD_HISTORY Then
                            lcQuery = "SELECT CUSER_ID "
                            lcQuery += "FROM GST_GENERAL_SECURITY_HISTORY (NOLOCK) "
                            lcQuery += "WHERE CUSER_ID = '{0}' "
                            lcQuery = String.Format(lcQuery, .CUSER_ID)

                            Dim loTempResultList As List(Of R_SecurityPolicyDTO)
                            loTempResultList = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery)

                            If loTempResultList IsNot Nothing Then
                                If loTempResultList.Count >= .IENFORCE_PASSWORD_HISTORY Then
                                    lcQuery = "DELETE GST_GENERAL_SECURITY_HISTORY "
                                    lcQuery += "WHERE CUSER_ID = '{0}' "
                                    lcQuery += "AND DCREATE_DATE = (SELECT MIN(DCREATE_DATE) FROM GST_GENERAL_SECURITY_HISTORY) "
                                    lcQuery = String.Format(lcQuery, .CUSER_ID)

                                    loDb.SqlExecNonQuery(lcQuery)
                                End If
                            End If

                            lcQuery = "INSERT INTO GST_GENERAL_SECURITY_HISTORY "
                            lcQuery += "(CUSER_ID, CUSER_PASSWORD) "
                            lcQuery += "VALUES "
                            lcQuery += "('{0}', '{1}')"
                            lcQuery = String.Format(lcQuery, .CUSER_ID, .CHASH_PASSWORD)

                            loDb.SqlExecNonQuery(lcQuery)
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Function R_SecurityPolicyLogon(ByVal poParameter As R_SecurityPolicyDTO) As Boolean
        Dim lcQuery As String
        Dim llRtn As Boolean = False
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            Dim loSecurityParameter As R_SecurityPolicyParameterDTO = R_GetSecurityPolicyParameter()

            With poParameter
                If loSecurityParameter.CSECURITY_AND_ACCOUNT_POLICY = "bycompany" Then
                    lcQuery = "SELECT CUSER_ID "
                    lcQuery += "FROM "
                    lcQuery += "SAM_USER_COMPANY (NOLOCK) "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery += "AND CCOMPANY_ID = '{1}' "
                    lcQuery += "AND CUSER_PASSWORD = '{2}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID, .CUSER_PASSWORD)

                    Dim loResult As Object
                    loResult = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                    If loResult Is Nothing Then
                        If .LACCOUNT_LOCKOUT_THRESHOLD Then
                            lcQuery = "SELECT CUSER_ID, NFAILED_ATTEMPT as nFailedAttempt "
                            lcQuery += "FROM "
                            lcQuery += "SAM_USER_COMPANY (NOLOCK) "
                            lcQuery += "WHERE CUSER_ID = '{0}' "
                            lcQuery += "AND CCOMPANY_ID = '{1}' "
                            lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)

                            Dim loResultUser As Object
                            loResultUser = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                            If loResultUser IsNot Nothing Then
                                lcQuery = "UPDATE SAM_USER_COMPANY "
                                lcQuery += "SET "
                                If loResultUser.nFailedAttempt >= .NFAILED_ATTEMPT Then
                                    lcQuery += "DACCOUNT_LOCKOUT = GETDATE(), "
                                End If
                                lcQuery += "NFAILED_ATTEMPT = NFAILED_ATTEMPT + 1, "
                                lcQuery += "DFAILED_ATTEMPT = GETDATE() "
                                lcQuery += "WHERE CUSER_ID = '{0}' "
                                lcQuery += "AND CCOMPANY_ID = '{1}' "
                                lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)

                                loDb.SqlExecNonQuery(lcQuery)
                            End If
                        End If

                        loEx.Add(New Exception("Wrong user id and password combination"))
                    End If
                Else 'byuser atau none
                    If .CUSER_PASSWORD <> "" Then
                        lcQuery = "SELECT CUSER_ID "
                        lcQuery += "FROM "
                        lcQuery += "SAM_USER (NOLOCK) "
                        lcQuery += "WHERE CUSER_ID = '{0}' "
                        lcQuery += "AND CUSER_PASSWORD = '{1}' "
                        lcQuery = String.Format(lcQuery, .CUSER_ID, .CUSER_PASSWORD)

                        Dim loResult As Object
                        loResult = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                        If loResult Is Nothing Then
                            If .LACCOUNT_LOCKOUT_THRESHOLD Then
                                lcQuery = "SELECT CUSER_ID , NFAILED_ATTEMPT as nFailedAttempt "
                                lcQuery += "FROM "
                                lcQuery += "SAM_USER (NOLOCK) "
                                lcQuery += "WHERE CUSER_ID = '{0}' "
                                lcQuery = String.Format(lcQuery, .CUSER_ID)

                                Dim loResultUser As Object
                                loResultUser = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                                If loResultUser IsNot Nothing Then
                                    lcQuery = "UPDATE SAM_USER "
                                    lcQuery += "SET "
                                    If loResultUser.nFailedAttempt >= .NFAILED_ATTEMPT Then
                                        lcQuery += "DACCOUNT_LOCKOUT = GETDATE(), "
                                    End If
                                    lcQuery += "NFAILED_ATTEMPT = NFAILED_ATTEMPT + 1, "
                                    lcQuery += "DFAILED_ATTEMPT = GETDATE() "
                                    lcQuery += "WHERE CUSER_ID = '{0}' "
                                    lcQuery = String.Format(lcQuery, .CUSER_ID)

                                    loDb.SqlExecNonQuery(lcQuery)
                                End If
                            End If

                            loEx.Add(New Exception("Wrong user id and password combination"))
                        End If
                    End If
                End If
            End With

            If loEx.Haserror Then
                Exit Try
            End If

            llRtn = True
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return llRtn
    End Function

    Public Sub R_LoginValidation(ByVal poParam As R_SecurityPolicyParameterDTO)
        Dim loEx As New R_Exception
        Dim lcQuery As String
        Dim loDb As New R_Db()
        Dim loResult As R_SecurityPolicyDTO = Nothing

        Try
            With poParam
                If .CSECURITY_AND_ACCOUNT_POLICY = "bycompany" Then
                    lcQuery = "SELECT CUSER_ID "
                    lcQuery += "FROM "
                    lcQuery += "SAM_USER_COMPANY (NOLOCK) "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery += "AND CCOMPANY_ID = '{1}' "
                    lcQuery += "AND CUSER_PASSWORD = '{2}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID, R_Utility.HashPassword(.CUSER_PASSWORD, .CUSER_ID.ToLower))

                Else 'byuser atau none
                    lcQuery = "SELECT CUSER_ID "
                    lcQuery += "FROM "
                    lcQuery += "SAM_USER (NOLOCK) "
                    lcQuery += "WHERE CUSER_ID = '{0}' "
                    lcQuery += "AND CUSER_PASSWORD = '{1}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, R_Utility.HashPassword(.CUSER_PASSWORD, .CUSER_ID.ToLower))

                End If

                loResult = loDb.SqlExecObjectQuery(Of R_SecurityPolicyDTO)(lcQuery).FirstOrDefault

                If loResult Is Nothing Then
                    loEx.Add(New Exception("Wrong user id and password combination"))
                End If
            End With
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub
End Class
