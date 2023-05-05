Imports R_Common
Imports R_BackEnd

' NOTE: You can use the "Rename" command on the context menu to change the class name "R_ProcessAndUploadService" in code, svc and config file together.
Public Class R_ProcessAndUploadService
    Implements IR_ProcessAndUploadService

    Public Sub Svc_R_DeleteUpload(poData As R_Common.R_UploadData) Implements R_Common.IR_ProcessAndUploadService.Svc_R_DeleteUpload
        Dim loException As New R_Exception

        Try
            R_ProcessAndUploadBack.R_DeleteUpload(poData)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub Svc_R_ProcessData(podata As R_Common.R_UploadCommand) Implements R_Common.IR_ProcessAndUploadService.Svc_R_ProcessData
        Dim loException As New R_Exception

        Try
            R_ProcessAndUploadBack.R_ProcessData(podata)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub Svc_R_UploadData(poData As R_Common.R_UploadData) Implements R_Common.IR_ProcessAndUploadService.Svc_R_UploadData
        Dim loException As New R_Exception

        Try
            R_ProcessAndUploadBack.R_UploadData(poData)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_GetErrorProcess(poErrorStatusParam As R_Common.R_UploadAndProcessKey) As System.Collections.Generic.List(Of R_Common.R_ErrorStatusReturn) Implements R_Common.IR_ProcessAndUploadService.Svc_R_GetErrorProcess
        Dim loException As New R_Exception
        Dim loRtn As List(Of R_ErrorStatusReturn) = Nothing

        Try
            loRtn = R_ProcessAndUploadBack.R_GetErrorProcess(poErrorStatusParam)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function Svc_R_MonitoringProcess(poMonitorParam As R_Common.R_UploadAndProcessKey) As R_Common.R_MonitoringReturn Implements R_Common.IR_ProcessAndUploadService.Svc_R_MonitoringProcess
        Dim loException As New R_Exception
        Dim loRtn As R_MonitoringReturn = Nothing

        Try
            loRtn = R_ProcessAndUploadBack.R_MonitoringProcess(poMonitorParam)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function
End Class
