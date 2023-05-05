Imports R_Common
Imports R_BackEnd.EmailEngine
' NOTE: You can use the "Rename" command on the context menu to change the class name "R_EmailEngineService" in code, svc and config file together.
Public Class R_EmailEngineService
    Implements IR_EmailEngineService

    Public Sub Svc_R_EmailEngineLoadData(poData As R_Common.R_EmailEngineDataPar) Implements R_Common.IR_EmailEngineService.Svc_R_EmailEngineLoadData
        Dim loException As New R_Exception
        Dim loSvc As New R_EmailEngineBack

        Try
            R_EmailEngineLogger.Log.Info("Start Service Svc_R_EmailEngineLoadData")
            loSvc.R_EmailEngineLoadData(poData)

        Catch ex As Exception
            loException.Add(ex)
            R_EmailEngineLogger.Log.Error(ex)
        End Try
        R_EmailEngineLogger.Log.Info("End Service Svc_R_EmailEngineLoadData")
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_EmailEngineProcessData(podata As R_Common.R_EmailEngineCommandPar) As String Implements R_Common.IR_EmailEngineService.Svc_R_EmailEngineProcessData
        Dim loException As New R_Exception
        Dim loSvc As New R_EmailEngineBack
        Dim lcRtn As String = ""

        Try
            R_EmailEngineLogger.Log.Info("Start Service Svc_R_EmailEngineProcessData")
            lcRtn = loSvc.R_EmailEngineProcessData(podata)

        Catch ex As Exception
            loException.Add(ex)
            R_EmailEngineLogger.Log.Error(ex)
        End Try
        R_EmailEngineLogger.Log.Info("End Service Svc_R_EmailEngineProcessData")
        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return lcRtn
    End Function
End Class
