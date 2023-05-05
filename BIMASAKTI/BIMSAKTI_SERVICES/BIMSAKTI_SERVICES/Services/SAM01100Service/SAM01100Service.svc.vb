Imports R_Common
Imports SAM01100Back
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the class name "SAM01100Service" in code, svc and config file together.
Public Class SAM01100Service
    Implements ISAM01100Service

    Public Sub Svc_R_Delete(poEntity As SAM01100Back.SAM01100DTO) Implements R_BackEnd.R_IServicebase(Of SAM01100Back.SAM01100DTO).Svc_R_Delete
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100Cls

        Try
            loCls.R_Delete(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_GetRecord(poEntity As SAM01100Back.SAM01100DTO) As SAM01100Back.SAM01100DTO Implements R_BackEnd.R_IServicebase(Of SAM01100Back.SAM01100DTO).Svc_R_GetRecord
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100Cls
        Dim loRtn As SAM01100DTO = Nothing

        Try
            loRtn = loCls.R_GetRecord(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function Svc_R_Save(poEntity As SAM01100Back.SAM01100DTO, poCRUDMode As R_Common.eCRUDMode) As SAM01100Back.SAM01100DTO Implements R_BackEnd.R_IServicebase(Of SAM01100Back.SAM01100DTO).Svc_R_Save
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100Cls
        Dim loRtn As SAM01100DTO = Nothing

        Try
            loRtn = loCls.R_Save(poEntity, poCRUDMode)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Sub saveGeneralAccess(poNewEntity As System.Collections.Generic.Dictionary(Of String, String)) Implements ISAM01100Service.saveGeneralAccess
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100Cls

        Try
            loCls.saveGeneralAccess(poNewEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

End Class
