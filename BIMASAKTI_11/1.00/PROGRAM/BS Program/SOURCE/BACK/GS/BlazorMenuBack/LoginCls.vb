Imports System.Data
Imports System.Data.Common
Imports System.Threading
Imports BlazorMenuCommon
Imports R_BackEnd
Imports R_Common
Imports R_ContextBackEnd
Imports R_ContextEnumAndInterface

Public Class LoginCls

    Public Sub UserLocking(ByVal poParameter As LoginDTO)
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception
        Dim loConn As DbConnection = Nothing

        Try
            loConn = loDb.GetConnection()

            With poParameter
                'SAME USER CANNOT LOGIN TWICE
                Dim loTable As DataTable
                Dim lcLicenseMode As String
                Dim lnLicensee As Integer
                Dim lnCurrentUser As Integer
                Dim lnTimeout As Integer
                Dim llValid As Boolean

                If .CCOMPANY_ID = "" Then
                    lcQuery = "SELECT TOP 1 CCOMPANY_ID "
                    lcQuery += "FROM "
                    lcQuery += "SAM_COMPANIES (NOLOCK) "
                    .CCOMPANY_ID = loDb.SqlExecObjectQuery(Of String)(lcQuery, loConn, False).FirstOrDefault
                End If

                'GET INFO FROM GST_LICENSE
                lcQuery = "SELECT CLICENSE_MODE, NLICENSEE, NTIMEOUT "
                lcQuery += "FROM GST_LICENSE (NOLOCK) "
                lcQuery += "WHERE CCOMPANY_ID = '{0}'"
                lcQuery = String.Format(lcQuery, .CCOMPANY_ID.Trim)
                loTable = loDb.SqlExecQuery(lcQuery, loConn, False)

                If loTable.Rows.Count <= 0 Then
                    loEx.Add(GetError("err001"))
                    Exit Try
                End If

                lcLicenseMode = loTable.Rows(0)("CLICENSE_MODE").ToString.Trim
                lnLicensee = CInt(loTable.Rows(0)("NLICENSEE"))
                lnTimeout = CInt(loTable.Rows(0)("NTIMEOUT"))

                'GET RECORD FROM SAT_LOCKING
                lcQuery = "SELECT "
                lcQuery += "CCOMPUTER_ID "
                lcQuery += "FROM SAT_LOCKING (NOLOCK) "
                lcQuery += "WHERE "
                lcQuery += "CCOMPANY_ID = '{0}' AND "
                lcQuery += "CUSER_ID = '{1}' "
                lcQuery = String.Format(lcQuery, .CCOMPANY_ID.Trim, .CUSER_ID.Trim)

                loTable = loDb.SqlExecQuery(lcQuery, loConn, False)
                If loTable.Rows.Count > 0 Then
                    Dim lcError As String
                    lcError = String.Format(GetMessage("err002"), .CUSER_ID.Trim, lnTimeout.ToString.Trim)
                    loEx.Add(New Exception(lcError))
                    Exit Try
                Else
                    If lcLicenseMode.Equals("USER", StringComparison.InvariantCultureIgnoreCase) Then
                        'GET USER ACTIVE IN SELECTED COMPANY
                        lcQuery = "SELECT "
                        lcQuery += "JUMLAH = (COUNT(CCOMPANY_ID)) "
                        lcQuery += "FROM SAT_LOCKING (NOLOCK) "
                        lcQuery += "WHERE CCOMPANY_ID = '{0}'"
                        lcQuery = String.Format(lcQuery, .CCOMPANY_ID.Trim)

                        loTable = loDb.SqlExecQuery(lcQuery, loConn, False)
                        lnCurrentUser = CInt(loTable.Rows(0)("JUMLAH"))

                        If lnCurrentUser >= lnLicensee Then
                            loEx.Add(GetError("err001"))
                            Exit Try
                        End If
                    ElseIf lcLicenseMode.Equals("USERNAME", StringComparison.InvariantCultureIgnoreCase) Then
                        Dim liUserCount As Integer

                        lcQuery = "SELECT "
                        lcQuery += "JUMLAH = (COUNT(CUSER_ID)) "
                        lcQuery += "FROM SAM_USER_COMPANY (NOLOCK) "
                        lcQuery += "WHERE CCOMPANY_ID = '{0}' AND CUSER_ID <> 'Admin' "
                        lcQuery = String.Format(lcQuery, .CCOMPANY_ID.Trim)

                        loTable = loDb.SqlExecQuery(lcQuery, loConn, False)
                        liUserCount = CInt(loTable.Rows(0)("JUMLAH"))

                        If liUserCount > lnLicensee Then
                            loEx.Add(GetError("err001"))
                            Exit Try
                        End If
                    Else
                        'GET VALIDATION WITH STORED PROCEDURE
                        lcQuery = "EXEC RSP_LicenseValidation '{0}', {1} "
                        lcQuery = String.Format(lcQuery, .CCOMPANY_ID, lcLicenseMode)

                        loTable = loDb.SqlExecQuery(lcQuery, loConn, False)
                        If loTable.Rows.Count > 0 Then
                            llValid = CBool(loTable.Rows(0)(0))

                            If Not llValid Then
                                loEx.Add(GetError("err001"))
                                Exit Try
                            End If
                        End If
                    End If

                    'INSERT SAT_LOCKING
                    lcQuery = "INSERT INTO SAT_LOCKING "
                    lcQuery += "(CCOMPANY_ID, CUSER_ID, DLOGIN_DATE, DLAST_ACTIVITY_DATE, CCOMPUTER_ID) "
                    lcQuery += "VALUES "
                    lcQuery += "('{0}', '{1}', GETDATE(), GETDATE(), '{2}')"
                    lcQuery = String.Format(lcQuery, .CCOMPANY_ID.Trim, .CUSER_ID.Trim, R_Context._GetInternalContext(R_InternalContextVarEnumerator.COMPUTER_ID).Trim)

                    loDb.SqlExecNonQuery(lcQuery, loConn, False)
                End If
            End With
        Catch ex As Exception
            loEx.Add(ex)
        Finally
            If loConn IsNot Nothing Then
                If Not (loConn.State = ConnectionState.Closed) Then
                    loConn.Close()
                End If
                loConn.Dispose()
                loConn = Nothing
            End If
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Sub UserLockingCompany(ByVal pcCurrentCompanyId As String, ByVal pcNewCompanyId As String, ByVal pcUserId As String)
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            'SAME USER CANNOT LOGIN TWICE
            Dim loTable As DataTable
            Dim lcLicenseMode As String
            Dim lnLicensee As Integer
            Dim lnCurrentUser As Integer
            Dim lnTimeout As Integer

            'GET INFO FROM GST_LICENSE
            lcQuery = "SELECT CLICENSE_MODE, NLICENSEE, NTIMEOUT "
            lcQuery += "FROM GST_LICENSE "
            lcQuery += "WHERE CCOMPANY_ID = '{0}'"
            lcQuery = String.Format(lcQuery, pcNewCompanyId.Trim)

            loTable = loDb.SqlExecQuery(lcQuery, loDb.GetConnection(), False)
            If loTable.Rows.Count <= 0 Then
                loEx.Add(GetError("err001"))
                Exit Try
            End If

            lcLicenseMode = loTable.Rows(0)("CLICENSE_MODE").ToString.Trim
            lnLicensee = CInt(loTable.Rows(0)("NLICENSEE"))
            lnTimeout = CInt(loTable.Rows(0)("NTIMEOUT"))

            'GET RECORD FROM SAT_LOCKING
            lcQuery = "SELECT "
            lcQuery += "CCOMPUTER_ID "
            lcQuery += "FROM SAT_LOCKING "
            lcQuery += "WHERE "
            lcQuery += "CCOMPANY_ID = '{0}' AND "
            lcQuery += "CUSER_ID = '{1}' "
            lcQuery = String.Format(lcQuery, pcNewCompanyId.Trim, pcUserId.Trim)

            loTable = loDb.SqlExecQuery(lcQuery, loDb.GetConnection(), False)
            If loTable.Rows.Count > 0 Then
                Dim lcError As String
                lcError = String.Format(GetMessage("err002"), pcUserId.Trim, lnTimeout.ToString.Trim)
                loEx.Add(New Exception(lcError))
                Exit Try
            Else
                If lcLicenseMode.Equals("CONCURRENT USER", StringComparison.InvariantCultureIgnoreCase) Then
                    'GET USER ACTIVE IN SELECTED COMPANY
                    lcQuery = "SELECT "
                    lcQuery += "JUMLAH = (COUNT(CCOMPANY_ID)) "
                    lcQuery += "FROM SAT_LOCKING "
                    lcQuery += "WHERE CCOMPANY_ID = '{0}'"
                    lcQuery = String.Format(lcQuery, pcNewCompanyId.Trim)

                    loTable = loDb.SqlExecQuery(lcQuery, loDb.GetConnection(), False)
                    lnCurrentUser = CInt(loTable.Rows(0)("JUMLAH"))

                    If lnCurrentUser >= lnLicensee Then
                        loEx.Add(GetError("err001"))
                        Exit Try
                    End If
                End If

                'DELETE SAT_LOCKING CURRENT COMPANY
                Dim loParam As New LoginDTO With {.CCOMPANY_ID = pcCurrentCompanyId, .CUSER_ID = pcUserId}
                UserLockingFlush(loParam)
            End If
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Sub UserLockingFlush(poParameter As LoginDTO)
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            'DELETE SAT_LOCKING CURRENT COMPANY
            lcQuery = "DELETE FROM SAT_LOCKING "
            lcQuery += "WHERE "
            lcQuery += "CCOMPANY_ID = '{0}' AND "
            lcQuery += "CUSER_ID = '{1}' "
            lcQuery = String.Format(lcQuery, poParameter.CCOMPANY_ID, poParameter.CUSER_ID)

            loDb.SqlExecNonQuery(lcQuery)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Function Logon(ByVal poParameter As LoginDTO) As LoginDTO
        Dim loEx As New R_Exception
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loResult As LoginDTO
        Dim loRtn As LoginDTO = Nothing

        Try
            'get USER_ID and USER_NAME
            lcQuery = "SELECT CUSER_ID, CUSER_NAME, CUSER_PASSWORD, CCULTURE_ID "
            lcQuery += "FROM SAM_USER (NOLOCK) "
            lcQuery += "WHERE CUSER_ID = '{0}' "
            lcQuery = String.Format(lcQuery, poParameter.CUSER_ID)

            loResult = loDb.SqlExecObjectQuery(Of LoginDTO)(lcQuery).FirstOrDefault

            If loResult IsNot Nothing Then
                Dim loParam As New UserCompanyDTO
                With loParam
                    .CCOMPANY_ID = poParameter.CCOMPANY_ID
                    .CUSER_ID = poParameter.CUSER_ID
                End With

                'Dim loCheckHasMenuAccess As UserCompanyDTO = Me.GetUserCompanyBroadcast(loParam)
                'If loCheckHasMenuAccess Is Nothing Then
                '    loEx.Add(GetError("err006"))
                '    Exit Try
                'End If

                loRtn = New LoginDTO With {.CUSER_ID = loResult.CUSER_ID,
                                           .CUSER_NAME = loResult.CUSER_NAME,
                                           .CCULTURE_ID = loResult.CCULTURE_ID}

                lcQuery = "SELECT A.CCOMPANY_ID, A.CCOMPANY_NAME, A.CREPORT_CULTURE, A.CNUMBER_FORMAT, A.CDATE_LONG_FORMAT, A.CDATE_SHORT_FORMAT, "
                lcQuery += "A.CTIME_LONG_FORMAT, A.CTIME_SHORT_FORMAT, A.IDECIMAL_PLACES, A.IROUNDING_PLACES, A.CROUNDING_METHOD, A.LENABLE_SAVE_CONFIRMATION, "
                lcQuery += "B.CLICENSE_MODE, B.NLICENSEE FROM SAM_COMPANIES A (NOLOCK) "
                lcQuery += "INNER JOIN GST_LICENSE B (NOLOCK) "
                lcQuery += "ON B.CCOMPANY_ID = A.CCOMPANY_ID "
                lcQuery += "WHERE A.CCOMPANY_ID = '{0}'"
                lcQuery = String.Format(lcQuery, poParameter.CCOMPANY_ID)
                loResult = loDb.SqlExecObjectQuery(Of LoginDTO)(lcQuery).FirstOrDefault

                If loResult Is Nothing Then
                    loEx.Add(GetError("err007"))
                    Exit Try
                End If

                With loRtn
                    .CCOMPANY_ID = loResult.CCOMPANY_ID
                    .CCOMPANY_NAME = loResult.CCOMPANY_NAME
                    .CREPORT_CULTURE = loResult.CREPORT_CULTURE
                    .CNUMBER_FORMAT = loResult.CNUMBER_FORMAT
                    .CDATE_LONG_FORMAT = loResult.CDATE_LONG_FORMAT
                    .CDATE_SHORT_FORMAT = loResult.CDATE_SHORT_FORMAT
                    .CTIME_LONG_FORMAT = loResult.CTIME_LONG_FORMAT
                    .CTIME_SHORT_FORMAT = loResult.CTIME_SHORT_FORMAT
                    .IDECIMAL_PLACES = loResult.IDECIMAL_PLACES
                    .IROUNDING_PLACES = loResult.IROUNDING_PLACES
                    .CROUNDING_METHOD = loResult.CROUNDING_METHOD
                    .LENABLE_SAVE_CONFIRMATION = loResult.LENABLE_SAVE_CONFIRMATION
                    .CLICENSE_MODE = loResult.CLICENSE_MODE
                    .NLICENSEE = loResult.NLICENSEE
                End With

                Me.UserLocking(poParameter)

                Me.ClearUserData(loRtn.CUSER_ID, loRtn.CCOMPANY_ID)
            End If

        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return loRtn
    End Function

    Public Function GetUserCompanyBroadcast(ByVal poParameter As UserCompanyDTO) As UserCompanyDTO
        Dim loEx As New R_Exception
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loResult As UserCompanyDTO = Nothing

        Try
            lcQuery = "SELECT CUSER_ID, CCOMPANY_ID, LENABLE_BROADCAST AS LCAN_BROADCAST "
            lcQuery += "FROM "
            lcQuery += "SAM_USER_COMPANY "
            lcQuery += "WHERE "
            lcQuery += "CUSER_ID = '{0}' AND CCOMPANY_ID = '{1}' "
            lcQuery = String.Format(lcQuery, poParameter.CUSER_ID, poParameter.CCOMPANY_ID)

            loResult = loDb.SqlExecObjectQuery(Of UserCompanyDTO)(lcQuery).FirstOrDefault
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return loResult
    End Function

    Public Function GetCompanyAndUserName(ByVal poParameter As LoginDTO) As LoginDTO
        Dim loEx As New R_Exception
        Dim lcQuery As String = ""
        Dim loDb As New R_Db
        Dim loResult As LoginDTO = Nothing
        Dim loRtn As LoginDTO = Nothing
        Dim loConn As DbConnection = Nothing

        Try
            loConn = loDb.GetConnection()

            lcQuery = "SELECT CUSER_ID, CUSER_NAME "
            lcQuery += "FROM SAM_USER "
            lcQuery += "WHERE CUSER_ID = '{0}' "
            lcQuery = String.Format(lcQuery, poParameter.CUSER_ID.ToString.Trim)

            loResult = loDb.SqlExecObjectQuery(Of LoginDTO)(lcQuery, loConn, False).FirstOrDefault

            loRtn = New LoginDTO With {.CUSER_ID = loResult.CUSER_ID,
                                       .CUSER_NAME = loResult.CUSER_NAME}

            lcQuery = "SELECT CCOMPANY_ID, CCOMPANY_NAME "
            lcQuery += "FROM SAM_COMPANIES "
            lcQuery += "WHERE CCOMPANY_ID = '{0}' "
            lcQuery = String.Format(lcQuery, poParameter.CCOMPANY_ID)

            loResult = loDb.SqlExecObjectQuery(Of LoginDTO)(lcQuery, loConn, False).FirstOrDefault

            With loRtn
                .CCOMPANY_ID = loResult.CCOMPANY_ID
                .CCOMPANY_NAME = loResult.CCOMPANY_NAME
            End With

            If loRtn Is Nothing Then
                loEx.Add(GetError("err001"))
                Exit Try
            End If
        Catch ex As Exception
            loEx.Add(ex)
        Finally
            If loConn IsNot Nothing Then
                If Not (loConn.State = ConnectionState.Closed) Then
                    loConn.Close()
                End If
                loConn.Dispose()
                loConn = Nothing
            End If
        End Try

        loEx.ThrowExceptionIfErrors()

        Return loResult
    End Function

    Public Sub ClearUserData(ByVal pcUserId As String, ByVal pcCompanyId As String)
        Dim loEx As New R_Exception
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()

        Try
            lcQuery = "DELETE GST_SPLIT_UPLOAD  "
            lcQuery += "WHERE CUSER_ID = '{0}' "
            lcQuery += "AND CCOMPANY_ID = '{1}' "
            lcQuery = String.Format(lcQuery, pcUserId, pcCompanyId)
            loDb.SqlExecNonQuery(lcQuery)

            lcQuery = "DELETE GST_UPLOAD_ERROR_STATUS   "
            lcQuery += "WHERE CUSER_ID = '{0}' "
            lcQuery += "AND CCOMPANY_ID = '{1}' "
            lcQuery = String.Format(lcQuery, pcUserId, pcCompanyId)
            loDb.SqlExecNonQuery(lcQuery)

            lcQuery = "DELETE GST_UPLOAD_PROCESS_STATUS   "
            lcQuery += "WHERE CUSER_ID = '{0}' "
            lcQuery += "AND CCOMPANY_ID = '{1}' "
            lcQuery = String.Format(lcQuery, pcUserId, pcCompanyId)
            loDb.SqlExecNonQuery(lcQuery)

            lcQuery = "DELETE GST_XML_RESULT    "
            lcQuery += "WHERE CUSER_ID = '{0}' "
            lcQuery += "AND CCOMPANY_ID = '{1}' "
            lcQuery = String.Format(lcQuery, pcUserId, pcCompanyId)
            loDb.SqlExecNonQuery(lcQuery)

            lcQuery = "DELETE SAM_USER_LOCKING   "
            lcQuery += "WHERE CUSER_ID = '{0}' "
            lcQuery += "AND CCOMPANY_ID = '{1}' "
            lcQuery = String.Format(lcQuery, pcUserId, pcCompanyId)
            loDb.SqlExecNonQuery(lcQuery)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Function GetLastUpdate(ByVal poParameter As LoginDTO) As Nullable(Of Date)
        Dim loEx As New R_Exception
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loResult As Nullable(Of Date)
        Dim loRtn As LoginDTO = Nothing

        Try
            lcQuery = "SELECT DLAST_UPDATE_PSWD "
            lcQuery += "FROM "
            lcQuery += "SAM_USER "
            lcQuery += "WHERE "
            lcQuery += "CUSER_ID = '{0}' "
            lcQuery = String.Format(lcQuery, poParameter.CUSER_ID)

            loRtn = loDb.SqlExecObjectQuery(Of LoginDTO)(lcQuery).FirstOrDefault

            If loRtn.DLAST_UPDATE_PSWD IsNot Nothing Then
                loResult = loRtn.DLAST_UPDATE_PSWD
            Else
                loResult = Nothing
            End If
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()

        Return loResult
    End Function

    Public Sub SetEncryptKey(pcEnryptKey As String)
        Dim loEx As New R_Exception
        Dim lcCmd As String = ""
        Dim lcKey As String = ""
        Dim loDb As New R_Db()
        Dim loConn As DbConnection = Nothing

        Try
            lcKey = pcEnryptKey
            loConn = loDb.GetConnection()

            lcCmd = "DELETE R_ClassTable WHERE (SELECT COUNT(*) FROM R_ClassTable) > 1"
            loDb.SqlExecNonQuery(lcCmd, loConn, False)

            lcCmd = "UPDATE R_ClassTable SET R_Key = dbo.FN_EGET('" + lcKey + "') WHERE dbo.FN_EKEYEQUAL('" + lcKey + "') = 0"
            loDb.SqlExecNonQuery(lcCmd, loConn, False)

            lcCmd = "INSERT INTO R_ClassTable SELECT dbo.FN_EGET('" + lcKey + "') WHERE (SELECT COUNT(*) FROM R_ClassTable) = 0"
            loDb.SqlExecNonQuery(lcCmd, loConn, False)

        Catch ex As Exception
            loEx.Add(ex)
        Finally
            If loConn IsNot Nothing Then
                If Not (loConn.State = ConnectionState.Closed) Then
                    loConn.Close()
                End If
                loConn.Dispose()
                loConn = Nothing
            End If
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Sub SetKey(pcKey As String)
        Dim loEx As New R_Exception
        Dim lcCmd As String = ""
        Dim loDb As New R_Db()
        Dim loConn As DbConnection = Nothing

        Try
            loConn = loDb.GetConnection()

            lcCmd = "DELETE R_ClassTable WHERE (SELECT COUNT(*) FROM R_ClassTable) > 1"
            loDb.SqlExecNonQuery(lcCmd, loConn, False)

            lcCmd = "UPDATE R_ClassTable SET "
            lcCmd += "R_Key = dbo.FN_EGET('{0}') "
            lcCmd += "WHERE dbo.FN_EKEYEQUAL('{0}') = 0"
            lcCmd = String.Format(lcCmd, pcKey)
            loDb.SqlExecNonQuery(lcCmd, loConn, False)

            lcCmd = "INSERT INTO R_ClassTable "
            lcCmd += "SELECT dbo.FN_EGET('{0}') "
            lcCmd += "WHERE (SELECT COUNT(*) FROM R_ClassTable) = 0"
            lcCmd = String.Format(lcCmd, pcKey)
            loDb.SqlExecNonQuery(lcCmd, loConn, False)

        Catch ex As Exception
            loEx.Add(ex)
        Finally
            If loConn IsNot Nothing Then
                If Not (loConn.State = ConnectionState.Closed) Then
                    loConn.Close()
                End If
                loConn.Dispose()
                loConn = Nothing
            End If
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Public Function GetCompanyIdByUser() As String
        Dim loEx As New R_Exception
        Dim lcQuery As String = ""
        Dim loDb As New R_Db
        Dim loConn As DbConnection = Nothing
        Dim lcResult As String = ""

        Try
            loConn = loDb.GetConnection()

            lcQuery = "SELECT * "
            lcQuery += "FROM "
            lcQuery += "GST_PARAMETER (NOLOCK) "

            Dim loResultList As List(Of ParameterDTO)
            loResultList = loDb.SqlExecObjectQuery(Of ParameterDTO)(lcQuery, loConn, False)

            Dim llChooseCompanyAtLoginPage As Boolean = False
            Dim lcSecurityAndAccountPolicy As String = ""
            For Each loParameter As ParameterDTO In loResultList
                If loParameter.CPARAMETER_ID = "ChooseCompanyAtLoginPage" Then
                    llChooseCompanyAtLoginPage = IIf(loParameter.CPARAMETER_VALUE = "true", True, False)
                ElseIf loParameter.CPARAMETER_ID = "SecurityAndAccountPolicy" Then
                    lcSecurityAndAccountPolicy = loParameter.CPARAMETER_VALUE
                End If
            Next

            If lcSecurityAndAccountPolicy <> "bycompany" AndAlso Not llChooseCompanyAtLoginPage Then
                lcQuery = "SELECT CCOMPANY_ID "
                lcQuery += "FROM "
                lcQuery += "SAM_COMPANIES (NOLOCK) "
                lcResult = loDb.SqlExecObjectQuery(Of String)(lcQuery, loConn, False).FirstOrDefault()
            End If
        Catch ex As Exception
            loEx.Add(ex)
        Finally
            If loConn IsNot Nothing Then
                If Not (loConn.State = ConnectionState.Closed) Then
                    loConn.Close()
                End If
                loConn.Dispose()
                loConn = Nothing
            End If
        End Try

        loEx.ThrowExceptionIfErrors()

        Return lcResult
    End Function

    Private Function GetError(pcErrorId As String) As R_Error
        Try
            Return R_Utility.R_GetError(GetType(LoginCls), pcErrorId, Thread.CurrentThread.CurrentCulture, "BlazorMenuBackResources_msgrsc")
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function GetMessage(pcMsgId As String) As String
        Try
            Return R_Utility.R_GetMessage(GetType(LoginCls), pcMsgId, Thread.CurrentThread.CurrentCulture, "BlazorMenuBackResources_msgrsc")
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
