Imports R_Common
Imports GSM00100Back

' NOTE: You can use the "Rename" command on the context menu to change the class name "GST00300Service" in code, svc and config file together.
Public Class GSM00100Service
    Implements IGSM00100Service

    Public Sub Svc_R_Delete(poEntity As GSM00100Back.GSM00100DTO) Implements R_BackEnd.R_IServicebase(Of GSM00100Back.GSM00100DTO).Svc_R_Delete
        Dim loEx As New R_Exception
        Dim loCls As New GSM00100Cls

        Try
            loCls.R_Delete(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_GetRecord(poEntity As GSM00100Back.GSM00100DTO) As GSM00100Back.GSM00100DTO Implements R_BackEnd.R_IServicebase(Of GSM00100Back.GSM00100DTO).Svc_R_GetRecord
        Dim loEx As New R_Exception
        Dim loCls As New GSM00100Cls
        Dim loRtn As GSM00100DTO = Nothing

        Try
            loRtn = loCls.R_GetRecord(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function Svc_R_Save(poEntity As GSM00100Back.GSM00100DTO, poCRUDMode As R_Common.eCRUDMode) As GSM00100Back.GSM00100DTO Implements R_BackEnd.R_IServicebase(Of GSM00100Back.GSM00100DTO).Svc_R_Save
        Dim loEx As New R_Exception
        Dim loCls As New GSM00100Cls
        Dim loRtn As GSM00100DTO = Nothing

        Try
            loRtn = loCls.R_Save(poEntity, poCRUDMode)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function CheckDelete(poParam As GSM00100Back.GSM00100DTO) As Boolean Implements IGSM00100Service.CheckDelete
        Dim loEx As New R_Exception
        Dim loCls As New GSM00100Cls
        Dim loRtn As Boolean

        Try
            loRtn = loCls.CheckDelete(poParam)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Sub TestSendEmail(poParam As GSM00100Back.EmailDTO) Implements IGSM00100Service.TestSendEmail
        Dim loEx As New R_Exception
        Dim loCls As New GSM00100Cls

        Try
            loCls.TestSendEmail(poParam)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    'Public Sub TestErrorHandlingSP(plWithError As Boolean) Implements IGSM00100Service.TestErrorHandlingSP
    '    Dim loEx As New R_Exception
    '    Dim loCls As New GSM00100Cls

    '    Try
    '        loCls.TestErrorHandlingSP(plWithError)
    '    Catch ex As Exception
    '        loEx.Add(ex)
    '    End Try

    '    loEx.ConvertAndThrowToServiceExceptionIfErrors()
    'End Sub
End Class
