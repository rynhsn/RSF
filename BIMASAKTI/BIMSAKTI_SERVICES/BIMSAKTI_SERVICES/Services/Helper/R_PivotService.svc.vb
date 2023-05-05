Imports R_Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "R_PivotService" in code, svc and config file together.
Public Class R_PivotService
    Implements IR_PivotService

    Public Sub Svc_R_DeletePivotTemplate(ByVal poPivotPar As R_Common.R_PivotPar) Implements R_Common.IR_PivotService.Svc_R_DeletePivotTemplate
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_PivotBack

            loClass.R_DeletePivotTemplate(poPivotPar)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_LoadPivotTemplate(ByVal poPivotPar As R_Common.R_PivotPar) As R_Common.R_PivotPar Implements R_Common.IR_PivotService.Svc_R_LoadPivotTemplate
        Dim loException As New R_Exception
        Dim loResult As R_PivotPar = Nothing

        Try
            Dim loClass As New R_BackEnd.R_PivotBack

            loResult = loClass.R_LoadPivotTemplate(poPivotPar)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loResult
    End Function

    Public Function Svc_R_LoadPivotTemplateList(ByVal poPivotPar As R_Common.R_PivotPar) As System.Collections.Generic.List(Of R_Common.R_PivotPar) Implements R_Common.IR_PivotService.Svc_R_LoadPivotTemplateList
        Dim loException As New R_Exception
        Dim loResult As List(Of R_PivotPar) = Nothing

        Try
            Dim loClass As New R_BackEnd.R_PivotBack

            loResult = loClass.R_LoadPivotTemplateList(poPivotPar)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loResult
    End Function

    Public Sub Svc_R_SavePivotTemplate(ByVal poPivotPar As R_Common.R_PivotPar, ByVal pcMode As String) Implements R_Common.IR_PivotService.Svc_R_SavePivotTemplate
        Dim loException As New R_Exception

        Try
            Dim loClass As New R_BackEnd.R_PivotBack

            loClass.R_SavePivotTemplate(poPivotPar, pcMode)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub
End Class
