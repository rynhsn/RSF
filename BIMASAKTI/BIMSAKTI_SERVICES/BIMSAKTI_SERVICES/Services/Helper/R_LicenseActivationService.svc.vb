Imports R_Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "R_LicenseActivationService" in code, svc and config file together.
Public Class R_LicenseActivationService
    Implements IR_LicenseActivationService

    'Public Function fSelectActivation(ByVal pcCompanyId As String) As R_BackEnd.R_ActivationDTO Implements IR_LicenseActivationService.fSelectActivation
    '    Dim loException As New R_Exception
    '    Dim loResult As R_BackEnd.R_ActivationDTO = Nothing

    '    Try
    '        Dim loClass As New R_BackEnd.R_LicenseActivationBack

    '        loResult = loClass.fSelectActivation(pcCompanyId)
    '    Catch ex As Exception
    '        loException.Add(ex)
    '    End Try
    '    loException.ConvertAndThrowToServiceExceptionIfErrors()
    '    Return loResult
    'End Function

    'Public Function fSelectLicense(ByVal pcCompanyId As String) As R_BackEnd.R_LicenseDTO Implements IR_LicenseActivationService.fSelectLicense
    '    Dim loException As New R_Exception
    '    Dim loResult As R_BackEnd.R_LicenseDTO = Nothing

    '    Try
    '        Dim loClass As New R_BackEnd.R_LicenseActivationBack

    '        loResult = loClass.fSelectLicense(pcCompanyId)
    '    Catch ex As Exception
    '        loException.Add(ex)
    '    End Try
    '    loException.ConvertAndThrowToServiceExceptionIfErrors()
    '    Return loResult
    'End Function

    'Public Sub sUpdateActivation(ByVal poParameter As R_BackEnd.R_ActivationDTO) Implements IR_LicenseActivationService.sUpdateActivation
    '    Dim loException As New R_Exception

    '    Try
    '        Dim loClass As New R_BackEnd.R_LicenseActivationBack

    '        loClass.sUpdateActivation(poParameter)
    '    Catch ex As Exception
    '        loException.Add(ex)
    '    End Try
    '    loException.ConvertAndThrowToServiceExceptionIfErrors()
    'End Sub

    'Public Sub sUpdateLicense(ByVal poParameter As R_BackEnd.R_LicenseDTO) Implements IR_LicenseActivationService.sUpdateLicense
    '    Dim loException As New R_Exception

    '    Try
    '        Dim loClass As New R_BackEnd.R_LicenseActivationBack

    '        loClass.sUpdateLicense(poParameter)
    '    Catch ex As Exception
    '        loException.Add(ex)
    '    End Try
    '    loException.ConvertAndThrowToServiceExceptionIfErrors()
    'End Sub

    Public Function Svc_R_CheckActivation(ByVal poParameter As R_Common.R_LicenseActivationDTO) As R_LicenseActivationResult Implements R_Common.IR_LicenseActivationService.Svc_R_CheckActivation
        Dim loException As New R_Exception
        Dim loRetun As R_LicenseActivationResult = Nothing

        Try
            Dim loClass As New R_BackEnd.R_LicenseActivationBack

            loRetun = loClass.fCheckActivation(poParameter)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return loRetun
    End Function

    Public Function Svc_R_CheckLicense(ByVal poParameter As R_Common.R_LicenseActivationDTO) As R_LicenseActivationResult Implements R_Common.IR_LicenseActivationService.Svc_R_CheckLicense
        Dim loException As New R_Exception
        Dim loRetun As R_LicenseActivationResult = Nothing

        Try
            Dim loClass As New R_BackEnd.R_LicenseActivationBack

            loRetun = loClass.fCheckLicense(poParameter)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRetun
    End Function
End Class
