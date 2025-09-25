Imports System.Data
Imports System.Data.Common
Imports GSM00100Common
Imports R_BackEnd
Imports R_Common
Imports R_CommonFrontBackAPI

Public Class GSM00100Cls
    Inherits R_BusinessObject(Of GSM00100DTO)
    'Implements R_IAttachFile

    Protected Overrides Sub R_Saving(poNewEntity As GSM00100DTO, poCRUDMode As eCRUDMode)
        Dim loEx As New R_Exception()
        Dim loDb As New R_Db()
        Dim loConn As DbConnection = Nothing
        Dim lcQuery As String
        Dim loResult As GSM00100DTO
        Dim loCmd = loDb.GetCommand
        Dim loPar = loDb.GetParameter

        Try
            loConn = loDb.GetConnection()

            With poNewEntity
                If poCRUDMode = eCRUDMode.AddMode Then
                    lcQuery = "SELECT * "
                    lcQuery += "FROM "
                    lcQuery += "GSM_SMTP_CONFIG (NOLOCK) "
                    lcQuery += "WHERE CSMTP_ID = N'{0}' "
                    lcQuery = String.Format(lcQuery, poNewEntity.CSMTP_ID)

                    loResult = loDb.SqlExecObjectQuery(Of GSM00100DTO)(lcQuery, loConn, False).FirstOrDefault
                    If loResult IsNot Nothing Then
                        loEx.Add("PS001", String.Format("SMTP ID {0} Already Exist", poNewEntity.CSMTP_ID))
                        Exit Try
                    End If

                    lcQuery = "INSERT INTO GSM_SMTP_CONFIG (CSMTP_ID, CSMTP_SERVER, CSMTP_PORT, LSUPPORT_SSL, CSMTP_CREDENTIAL_USER, CSMTP_CREDENTIAL_PASSWORD, CGENERAL_EMAIL_ADDRESS, "
                    lcQuery += "CUPDATE_BY, DUPDATE_DATE, CCREATE_BY, DCREATE_DATE, LAPI) "
                    lcQuery += "VALUES(N'{0}', N'{1}', N'{2}', '{3}', N'{4}', N'{5}', N'{6}', N'{7}', {8}, N'{7}', {8}, '{9}')"
                    lcQuery = String.Format(lcQuery, .CSMTP_ID, .CSMTP_SERVER, .CSMTP_PORT, .LSUPPORT_SSL, .CSMTP_CREDENTIAL_USER, .CSMTP_CREDENTIAL_PASSWORD, .CGENERAL_EMAIL_ADDRESS,
                                            .CUSER_ID, GetDate(.CDATENOW), .LAPI)
                    loDb.SqlExecNonQuery(lcQuery, loConn, False)

                ElseIf poCRUDMode = eCRUDMode.EditMode Then
                    If Not .LEDIT_CREDENTIAL Then
                        lcQuery = "UPDATE GSM_SMTP_CONFIG "
                        lcQuery += "SET "
                        lcQuery += "CSMTP_SERVER = @CSMTP_SERVER, "
                        lcQuery += "CSMTP_PORT = N'{1}', "
                        lcQuery += "LSUPPORT_SSL = '{2}', "
                        lcQuery += "CGENERAL_EMAIL_ADDRESS = N'{6}', "
                        lcQuery += "CUPDATE_BY = N'{7}', "
                        lcQuery += "DUPDATE_DATE = {8}, "
                        lcQuery += "LAPI = '{9}' "
                        lcQuery += "WHERE CSMTP_ID = N'{5}'"
                        lcQuery = String.Format(lcQuery, .CSMTP_SERVER, .CSMTP_PORT, .LSUPPORT_SSL, .CSMTP_CREDENTIAL_USER, .CSMTP_CREDENTIAL_PASSWORD, .CSMTP_ID, .CGENERAL_EMAIL_ADDRESS,
                                                .CUSER_ID, GetDate(.CDATENOW), .LAPI)
                    Else
                        lcQuery = "UPDATE GSM_SMTP_CONFIG "
                        lcQuery += "SET "
                        lcQuery += "CSMTP_SERVER = @CSMTP_SERVER, "
                        lcQuery += "CSMTP_PORT = N'{1}', "
                        lcQuery += "LSUPPORT_SSL = '{2}', "
                        lcQuery += "CSMTP_CREDENTIAL_USER = N'{3}', "
                        lcQuery += "CSMTP_CREDENTIAL_PASSWORD = N'{4}', "
                        lcQuery += "CGENERAL_EMAIL_ADDRESS = N'{6}', "
                        lcQuery += "CUPDATE_BY = N'{7}', "
                        lcQuery += "DUPDATE_DATE = {8}, "
                        lcQuery += "LAPI = '{9}' "
                        lcQuery += "WHERE CSMTP_ID = N'{5}'"
                        lcQuery = String.Format(lcQuery, .CSMTP_SERVER, .CSMTP_PORT, .LSUPPORT_SSL, .CSMTP_CREDENTIAL_USER, .CSMTP_CREDENTIAL_PASSWORD, .CSMTP_ID, .CGENERAL_EMAIL_ADDRESS,
                                                .CUSER_ID, GetDate(.CDATENOW), .LAPI)
                    End If

                    loCmd.CommandText = lcQuery
                    loPar = loDb.GetParameter()

                    With loPar
                        .ParameterName = "@CSMTP_SERVER"
                        .DbType = DbType.String
                        .Value = IIf(poNewEntity.CSMTP_SERVER Is Nothing, DBNull.Value, poNewEntity.CSMTP_SERVER)
                    End With

                    loCmd.Parameters.Add(loPar)
                    loDb.SqlExecNonQuery(loConn, loCmd, False)
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

    Protected Overrides Sub R_Deleting(poEntity As GSM00100DTO)
        Dim lcQuery As String
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            lcQuery = "DELETE GSM_SMTP_CONFIG "
            lcQuery += "WHERE "
            lcQuery += "CSMTP_ID = N'{0}' "
            lcQuery = String.Format(lcQuery, poEntity.CSMTP_ID)

            loDb.SqlExecNonQuery(lcQuery)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
    End Sub

    Protected Overrides Function R_Display(poEntity As GSM00100DTO) As GSM00100DTO
        Dim lcQuery As String
        Dim loResult As GSM00100DTO = Nothing
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            lcQuery = "SELECT CSMTP_ID, CSMTP_SERVER, CSMTP_PORT, LSUPPORT_SSL, CGENERAL_EMAIL_ADDRESS, LAPI, "
            lcQuery += "CUPDATE_BY, DUPDATE_DATE, CCREATE_BY, DCREATE_DATE "
            lcQuery += "FROM "
            lcQuery += "GSM_SMTP_CONFIG (NOLOCK) "
            lcQuery += "WHERE CSMTP_ID = N'{0}' "
            lcQuery = String.Format(lcQuery, poEntity.CSMTP_ID)

            loResult = loDb.SqlExecObjectQuery(Of GSM00100DTO)(lcQuery).FirstOrDefault
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
        Return loResult
    End Function

    'Public Sub R_AttachFile(poAttachFile As R_AttachFilePar) Implements R_IAttachFile.R_AttachFile
    '    Dim loEx As New R_Exception
    '    Dim lcQuery As String
    '    Dim loConn As DbConnection = Nothing
    '    Dim loDb As New R_Db()
    '    Dim loSender As R_Sender
    '    Dim lcEmailID As String
    '    Dim poParam As New EmailDTO

    '    Try
    '        loConn = loDb.GetConnection()

    '        lcEmailID = Guid.NewGuid().ToString("N")

    '        lcQuery = "INSERT INTO GST_EMAIL_OUTBOX(CEMAIL_ID, CEMAIL_FROM, CEMAIL_TO, CEMAIL_SUBJECT, CEMAIL_BODY, LFLAG_PRIORITY, "
    '        lcQuery += "CSTATUS, LFLAG_HTML, CCOMPANY_ID, CUSER_ID, CPROGRAM_ID, CSMTP_ID, LFLAG_ACTIVE, CVALID_UNTIL) "
    '        lcQuery += "VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', 'True', "
    '        lcQuery += "'00', 'True', N'{5}', N'{6}', N'{7}', N'{8}', 'True', '')"

    '        With poParam
    '            lcQuery = String.Format(lcQuery, lcEmailID, .CEMAIL_FROM, .CEMAIL_TO, .CEMAIL_SUBJECT, .CEMAIL_BODY,
    '                                  .CCOMPANY_ID, .CUSER_ID, .CPROGRAM_ID, .CSMTP_ID)
    '        End With

    '        'Insert email outbox
    '        loDb.SqlExecNonQuery(lcQuery, loConn, False)

    '        loSender = New R_Sender
    '        loSender.R_SendById(lcEmailID)
    '    Catch ex As Exception
    '        loEx.Add(ex)
    '    Finally
    '        If loConn IsNot Nothing Then
    '            If Not (loConn.State = ConnectionState.Closed) Then
    '                loConn.Close()
    '            End If
    '            loConn.Dispose()
    '            loConn = Nothing
    '        End If
    '    End Try

    '    loEx.ThrowExceptionIfErrors()
    'End Sub

    Public Function GetSMTPList() As List(Of GSM00100DTOList)
        Dim lcQuery As String
        Dim loResult As List(Of GSM00100DTOList) = Nothing
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception

        Try
            lcQuery = "SELECT * "
            lcQuery += "FROM "
            lcQuery += "GSM_SMTP_CONFIG (NOLOCK)"

            loResult = loDb.SqlExecObjectQuery(Of GSM00100DTOList)(lcQuery)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
        Return loResult
    End Function

    Public Function CheckDelete(poParam As GSM00100DTO) As Boolean
        Dim lcQuery As String
        Dim loResult As GSM00100DTO
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception
        Dim loRtn As Boolean

        Try
            lcQuery = "SELECT CSMTP_ID "
            lcQuery += "FROM "
            lcQuery += "SAM_COMPANIES (NOLOCK) "
            lcQuery += "WHERE CSMTP_ID = N'{0}' "
            lcQuery = String.Format(lcQuery, poParam.CSMTP_ID)

            loResult = loDb.SqlExecObjectQuery(Of GSM00100DTO)(lcQuery).FirstOrDefault
            If loResult IsNot Nothing Then
                loRtn = True
            End If
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ThrowExceptionIfErrors()
        Return loRtn
    End Function

    Public Sub TestSendEmail(poParam As EmailDTO)
        'Dim loEx As New R_Exception
        'Dim lcQuery As String
        'Dim loConn As DbConnection = Nothing
        'Dim loDb As New R_Db()
        'Dim loSender As R_Sender
        'Dim lcEmailID As String

        'Try
        '    Using TransScope As New TransactionScope(TransactionScopeOption.Required)
        '        loConn = loDb.GetConnection()

        '        lcEmailID = Guid.NewGuid().ToString("N")

        '        lcQuery = "INSERT INTO GST_EMAIL_OUTBOX(CEMAIL_ID, CEMAIL_FROM, CEMAIL_TO, CEMAIL_SUBJECT, CEMAIL_BODY, LFLAG_PRIORITY, "
        '        lcQuery += "CSTATUS, LFLAG_HTML, CCOMPANY_ID, CUSER_ID, CPROGRAM_ID, CSMTP_ID, LFLAG_ACTIVE, CVALID_UNTIL) "
        '        lcQuery += "VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', 'True', "
        '        lcQuery += "'00', 'True', N'{5}', N'{6}', N'{7}', N'{8}', 'True', '')"
        '        With poParam
        '            lcQuery = String.Format(lcQuery, lcEmailID, .CEMAIL_FROM, .CEMAIL_TO, .CEMAIL_SUBJECT, .CEMAIL_BODY,
        '                                  .CCOMPANY_ID, .CUSER_ID, .CPROGRAM_ID, .CSMTP_ID)
        '        End With

        '        'Insert email outbox
        '        loDb.SqlExecNonQuery(lcQuery, loConn, False)

        '        loSender = New R_Sender
        '        loSender.R_SendById(lcEmailID)

        '        TransScope.Complete()
        '    End Using
        'Catch ex As Exception
        '    loEx.Add(ex)
        'Finally
        '    If loConn IsNot Nothing Then
        '        If Not (loConn.State = ConnectionState.Closed) Then
        '            loConn.Close()
        '        End If
        '        loConn.Dispose()
        '        loConn = Nothing
        '    End If
        'End Try

        'loEx.ThrowExceptionIfErrors()
    End Sub

    Public Sub CheckSupportedEmailProvider()
        'Dim lcQuery As String
        'Dim loPlatformType As List(Of PlatformDTO) = Nothing
        'Dim loDb As New R_Db()
        'Dim loEx As New R_Exception

        'Try
        '    lcQuery = "SELECT * FROM SAB_PLATFORM_TYPE (NOLOCK) "
        '    loPlatformType = loDb.SqlExecObjectQuery(Of PlatformDTO)(lcQuery)

        '    If loPlatformType.Count = 0 Then
        '        loEx.Add(New Exception("Email provider is not found in Platform Type Master"))
        '        Exit Try
        '    Else
        '        Dim loExistOnEnum As PlatformDTO = loPlatformType.Where(Function(x) HandleEnum(x.CPLATFORM_TYPE)).FirstOrDefault

        '        If loExistOnEnum Is Nothing Then
        '            loEx.Add(New Exception("Email provider in Platform Type Master is not supported"))
        '            Exit Try
        '        End If
        '    End If
        'Catch ex As Exception
        '    loEx.Add(ex)
        'End Try

        'loEx.ThrowExceptionIfErrors()
    End Sub

    Private Function GetDate(pcDate As String) As String
        Return String.Format("CONVERT(DATETIME, '{0}')", pcDate)
    End Function

    'Private Function HandleEnum(pcPlatformType As String) As Boolean
    '    If System.Enum.IsDefined(GetType(R_eEmailProvider), pcPlatformType.ToUpper) Then
    '        Return True
    '    End If

    '    Return False
    'End Function
End Class
