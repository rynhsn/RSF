Imports System.Data
Imports System.Data.Common
Imports System.Transactions
Imports R_BackEnd
Imports R_Common
Imports R_SecurityTokenCommon

Public Class R_SecurityTokenCls

    'Public Function R_GetUserRefreshToken(poParam As ParameterDTO) As TokenDTO
    '    Dim lcQuery As String
    '    Dim loResult As TokenDTO = Nothing
    '    Dim loDb As New R_Db()
    '    Dim loEx As New R_Exception
    '    Dim loConn As DbConnection = Nothing

    '    Try
    '        loConn = loDb.GetConnection()

    '        lcQuery = "SELECT CTOKEN_ID FROM SAM_USER_COMPANY (NOLOCK) "
    '        lcQuery += "WHERE CUSER_ID = '{0}' AND CCOMPANY_ID = '{1}' "
    '        lcQuery = String.Format(lcQuery, poParam.CUSER_ID, poParam.CCOMPANY_ID)
    '        Dim lcTokenId As String = loDb.SqlExecObjectQuery(Of String)(lcQuery, loConn, False).FirstOrDefault()

    '        If String.IsNullOrWhiteSpace(lcTokenId) Then
    '            lcQuery = "SELECT * FROM SAM_TOKEN (NOLOCK) "
    '            lcQuery += "WHERE CTOKEN_ID = '{0}' "
    '            lcQuery = String.Format(lcQuery, lcTokenId)
    '            loResult = loDb.SqlExecObjectQuery(Of TokenDTO)(lcQuery, loConn, False).FirstOrDefault()

    '            loResult.CUSER_ID = poParam.CUSER_ID
    '            loResult.CCOMPANY_ID = poParam.CCOMPANY_ID
    '        End If
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

    '    Return loResult
    'End Function

    Public Function R_GetUserByRefreshToken(pcRefreshToken As String) As TokenDTO
        Dim lcQuery As String
        Dim loResult As TokenDTO = Nothing
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception
        Dim loConn As DbConnection = Nothing

        Try
            loConn = loDb.GetConnection()

            lcQuery = "SELECT * FROM SAM_TOKEN (NOLOCK) "
            lcQuery += "WHERE CREFRESH_TOKEN = '{0}' "
            lcQuery = String.Format(lcQuery, pcRefreshToken)
            loResult = loDb.SqlExecObjectQuery(Of TokenDTO)(lcQuery, loConn, False).FirstOrDefault()

            If loResult IsNot Nothing Then
                lcQuery = "SELECT CUSER_ID, CCOMPANY_ID FROM SAM_USER_COMPANY (NOLOCK) "
                lcQuery += "WHERE CTOKEN_ID = '{0}' "
                lcQuery = String.Format(lcQuery, loResult.CTOKEN_ID)
                Dim loUser As TokenDTO = loDb.SqlExecObjectQuery(Of TokenDTO)(lcQuery, loConn, False).FirstOrDefault()

                If loUser IsNot Nothing Then
                    loResult.CUSER_ID = loUser.CUSER_ID
                    loResult.CCOMPANY_ID = loUser.CCOMPANY_ID
                Else
                    loResult = Nothing
                End If
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

    Public Function R_UpdateRefreshToken(poParam As ParameterDTO) As String
        Dim lcQuery As String = ""
        Dim loDb As New R_Db()
        Dim loEx As New R_Exception
        Dim loConn As DbConnection = Nothing
        Dim lcTokenId As String = ""

        Try
            Using TransScope As New TransactionScope(TransactionScopeOption.Required)
                loConn = loDb.GetConnection()

                With poParam
                    'check token id dari SAM_USER_COMPANY
                    lcQuery = "SELECT CTOKEN_ID FROM SAM_USER_COMPANY (NOLOCK) "
                    lcQuery += "WHERE CUSER_ID = '{0}' AND CCOMPANY_ID = '{1}' "
                    lcQuery = String.Format(lcQuery, .CUSER_ID, .CCOMPANY_ID)
                    Dim lcUserToken As String = loDb.SqlExecObjectQuery(Of String)(lcQuery, loConn, False).FirstOrDefault()

                    If String.IsNullOrWhiteSpace(lcUserToken) Then
                        lcTokenId = Guid.NewGuid.ToString().ToUpper()

                        lcQuery = "INSERT INTO SAM_TOKEN "
                        lcQuery += "VALUES "
                        lcQuery += "('{0}', '{1}', '{2}', '{3}', '{4}')"
                        lcQuery = String.Format(lcQuery, lcTokenId, .CREFRESH_TOKEN,
                                            .DREFRESH_TOKEN_UTC_CREATED, .DREFRESH_TOKEN_UTC_EXPIRES,
                                            .CTOKEN_IP_ADDRESS)
                        loDb.SqlExecNonQuery(lcQuery, loConn, False)

                        lcQuery = "UPDATE SAM_USER_COMPANY "
                        lcQuery += "SET "
                        lcQuery += "CTOKEN_ID = '{0}' "
                        lcQuery += "WHERE CCOMPANY_ID = '{1}' AND CUSER_ID = '{2}' "
                        lcQuery = String.Format(lcQuery, lcTokenId, .CCOMPANY_ID,
                                            .CUSER_ID)
                        loDb.SqlExecNonQuery(lcQuery, loConn, False)
                    Else
                        lcQuery = "UPDATE SAM_TOKEN "
                        lcQuery += "SET "
                        lcQuery += "CREFRESH_TOKEN = '{0}', "
                        lcQuery += "DREFRESH_TOKEN_UTC_CREATED = '{1}', "
                        lcQuery += "DREFRESH_TOKEN_UTC_EXPIRES = '{2}', "
                        lcQuery += "CTOKEN_IP_ADDRESS = '{3}' "
                        lcQuery += "WHERE CTOKEN_ID = '{4}' "
                        lcQuery = String.Format(lcQuery, .CREFRESH_TOKEN, .DREFRESH_TOKEN_UTC_CREATED,
                                            .DREFRESH_TOKEN_UTC_EXPIRES, .CTOKEN_IP_ADDRESS, lcUserToken)
                        loDb.SqlExecNonQuery(lcQuery, loConn, False)

                        lcTokenId = lcUserToken
                    End If
                End With

                TransScope.Complete()
            End Using
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

        Return lcTokenId
    End Function
End Class
