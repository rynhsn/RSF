Imports R_BackEnd
Imports R_Common
Imports TelerikMenuBackCls
Imports System.Data.Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "LoginService" in code, svc and config file together.
Public Class LoginService
    Implements ILoginService

    Public Sub SetKey(ByVal pcKey As String) Implements ILoginService.SetKey
        Dim loException As New R_Exception
        Dim loClass As New TelerikMenuBackCls.LoginCls

        Try
            loClass.SetKey(pcKey)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function svc_getUserCompanyBroadcast(ByVal poParameter As TelerikMenuBackCls.SAM_USER_COMPANYDTO) As TelerikMenuBackCls.SAM_USER_COMPANYDTO Implements ILoginService.svc_getUserCompanyBroadcast
        Dim loException As New R_Exception
        Dim loResult As SAM_USER_COMPANYDTO = Nothing

        Try
            Dim loClass As New TelerikMenuBackCls.LoginCls
            loResult = loClass.getUserCompanyBroadcast(poParameter)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return loResult
    End Function

    Public Function Logon(ByVal poParameter As TelerikMenuBackCls.LoginDTO) As TelerikMenuBackCls.LoginDTO Implements ILoginService.Logon
        Dim loException As New R_Exception
        Dim loClass As New TelerikMenuBackCls.LoginCls
        Dim loRtn As LoginDTO = Nothing
        Dim loAuthData As R_AuthenticationData

        Try
            loRtn = loClass.Logon(poParameter)

            'Prepare Internal Auth Data
            loAuthData = New R_AuthenticationData
            With loAuthData
                .TokenType = R_TokenHelper.TOKEN_TYPE_INTERNAL
                .UserID = poParameter.CUSER_ID.Trim
                '.Password = poParameter.CUSER_PASSWORD.Trim
                .ExpiredDays = 2
            End With

            'set Access Token
            R_Context._SetServerContext(R_InternalContextVarEnumerator.ACCESS_TOKEN, R_TokenHelper.GenerateTokenAccess(loAuthData))

            R_Context._SetServerContext(R_Context._GetKey(R_Context.eContextKey.EncryptKey), New String() {KeyEncryptor.KeyEncryptorClass.GetKeyEncryptor, KeyEncryptor.KeyEncryptorClass._Uyah})
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function svc_getCompanyAndUserName(ByVal poParameter As TelerikMenuBackCls.LoginDTO) As TelerikMenuBackCls.LoginDTO Implements ILoginService.svc_getCompanyAndUserName
        Dim loException As New R_Exception
        Dim loClass As New TelerikMenuBackCls.LoginCls
        Dim loRtn As LoginDTO = Nothing

        Try
            loRtn = loClass.getCompanyAndUserName(poParameter)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getLastUpdate(ByVal poParameter As TelerikMenuBackCls.LoginDTO) As Date? Implements ILoginService.getLastUpdate
        Dim loException As New R_Exception
        Dim loClass As New TelerikMenuBackCls.LoginCls
        Dim loRtn As Nullable(Of Date)

        Try
            loRtn = loClass.getLastUpdate(poParameter)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Sub doFlushData(ByVal pcUserId As String, ByVal pcCompanyId As String) Implements ILoginService.doFlushData
        Dim loException As New R_Exception
        Dim loClass As New TelerikMenuBackCls.LoginCls

        Try
            loClass.doFlushData(pcUserId, pcCompanyId)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub Svc_R_UserLocking(ByVal poParameter As TelerikMenuBackCls.LoginDTO) Implements ILoginService.Svc_R_UserLocking
        Dim loException As New R_Exception
        Dim loClass As New TelerikMenuBackCls.LoginCls

        Try
            loClass.R_UserLocking(poParameter)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub Svc_R_UserLockingCompany(ByVal pcCurrentCompanyId As String, ByVal pcNewCompanyId As String, ByVal pcUserId As String) Implements ILoginService.Svc_R_UserLockingCompany
        Dim loException As New R_Exception
        Dim loClass As New TelerikMenuBackCls.LoginCls

        Try
            loClass.R_UserLockingCompany(pcCurrentCompanyId, pcNewCompanyId, pcUserId)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub Svc_R_UserLockingFlush(ByVal pcCurrentCompanyId As String, ByVal pcUserId As String) Implements ILoginService.Svc_R_UserLockingFlush
        Dim loException As New R_Exception
        Dim loClass As New TelerikMenuBackCls.LoginCls

        Try
            loClass.R_UserLockingFlush(pcCurrentCompanyId, pcUserId)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub
End Class
