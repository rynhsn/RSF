Imports R_Common

Public Class R_SecurityPolicyService
    Implements IR_SecurityPolicyService

    Public Function Svc_R_GetSecurityPolicy(ByVal poParameter As R_Common.R_SecurityPolicyDTO) As R_Common.R_SecurityPolicyDTO Implements R_Common.IR_SecurityPolicyService.Svc_R_GetSecurityPolicy
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            Return loClass.R_GetSecurityPolicy(poParameter)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return Nothing
    End Function

    Public Function Svc_R_GetSecurityPolicyParameter() As R_Common.R_SecurityPolicyParameterDTO Implements R_Common.IR_SecurityPolicyService.Svc_R_GetSecurityPolicyParameter
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            Return loClass.R_GetSecurityPolicyParameter()
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return Nothing
    End Function

    Public Function Svc_R_CheckPasswordHistory(ByVal pcCompanyId As String, ByVal pcUserId As String, ByVal pcHashPassword As String, ByVal pcSecurityAndAccountPolicy As String) As Boolean Implements R_Common.IR_SecurityPolicyService.Svc_R_CheckPasswordHistory
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            Return loClass.R_CheckPasswordHistory(pcCompanyId, pcUserId, pcHashPassword, pcSecurityAndAccountPolicy)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return False
    End Function

    Public Function Svc_R_GetCurrentDate() As String Implements R_Common.IR_SecurityPolicyService.Svc_R_GetCurrentDate
        Dim loException As New R_Exception
        Dim lcRtn As String = ""

        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            Return loClass.R_GetCurrentDate()
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return lcRtn
    End Function

    Public Sub Svc_R_ResetAccountLockoutDuration(ByVal pcCompanyId As String, ByVal pcUserId As String, ByVal pcSecurityAndAccountPolicy As String) Implements R_Common.IR_SecurityPolicyService.Svc_R_ResetAccountLockoutDuration
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            loClass.R_ResetAccountLockoutDuration(pcCompanyId, pcUserId, pcSecurityAndAccountPolicy)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub Svc_R_ResetAccountLockoutThreshold(ByVal pcCompanyId As String, ByVal pcUserId As String, ByVal pcSecurityAndAccountPolicy As String) Implements R_Common.IR_SecurityPolicyService.Svc_R_ResetAccountLockoutThreshold
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            loClass.R_ResetAccountLockoutThreshold(pcCompanyId, pcUserId, pcSecurityAndAccountPolicy)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub Svc_R_UpdatePassword(ByVal poParameter As R_Common.R_SecurityPolicyDTO, ByVal pcHashPassword As String, ByVal pcHashCurrentPassword As String) Implements R_Common.IR_SecurityPolicyService.Svc_R_UpdatePassword
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            loClass.R_UpdatePassword(poParameter, pcHashPassword, pcHashCurrentPassword)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_SecurityPolicyLogon(ByVal poParameter As R_SecurityPolicyDTO, ByVal pcHashPassword As String) As Boolean Implements R_Common.IR_SecurityPolicyService.Svc_R_SecurityPolicyLogon
        Dim loException As New R_Exception
        Dim lcRtn As String = ""
        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            Return loClass.R_SecurityPolicyLogon(poParameter, pcHashPassword)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return lcRtn
    End Function

    Public Sub Svc_R_LoginValidation(poParam As R_Common.R_SecurityPolicyParameterDTO, pcCompId As String, pcUserId As String, pcPassword As String) Implements R_Common.IR_SecurityPolicyService.Svc_R_LoginValidation
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_SecurityPolicyBack

            loClass.R_LoginValidation(poParam, pcCompId, pcUserId, pcPassword)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub
End Class
