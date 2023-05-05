Imports R_Common
Imports SAM01100Back
Imports System.ServiceModel.Channels
' NOTE: You can use the "Rename" command on the context menu to change the class name "MenuProgramService" in code, svc and config file together.
Public Class MenuProgramService
    Implements IMenuProgramService

    Public Sub Svc_R_Delete(poEntity As SAM01100Back.SAM01100MenuProgramDTO) Implements R_BackEnd.R_IServicebase(Of SAM01100Back.SAM01100MenuProgramDTO).Svc_R_Delete
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100MenuProgramCls

        Try
            loCls.R_Delete(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_GetRecord(poEntity As SAM01100Back.SAM01100MenuProgramDTO) As SAM01100Back.SAM01100MenuProgramDTO Implements R_BackEnd.R_IServicebase(Of SAM01100Back.SAM01100MenuProgramDTO).Svc_R_GetRecord
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100MenuProgramCls
        Dim loRtn As SAM01100MenuProgramDTO = Nothing

        Try
            loRtn = loCls.R_GetRecord(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function Svc_R_Save(poEntity As SAM01100Back.SAM01100MenuProgramDTO, poCRUDMode As R_Common.eCRUDMode) As SAM01100Back.SAM01100MenuProgramDTO Implements R_BackEnd.R_IServicebase(Of SAM01100Back.SAM01100MenuProgramDTO).Svc_R_Save
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100MenuProgramCls
        Dim loRtn As SAM01100MenuProgramDTO = Nothing

        Try
            loRtn = loCls.R_Save(poEntity, poCRUDMode)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Sub saveButtonAccess(poParam As SAM01100Back.SAM01100MenuProgramDTO) Implements IMenuProgramService.saveButtonAccess
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100MenuProgramCls

        Try
            loCls.saveButtonAccess(poParam)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub saveGeneralAccess(poparam As SAM01100Back.SAM01100MenuProgramDTO) Implements IMenuProgramService.saveGeneralAccess
        Dim loEx As New R_Exception
        Dim loCls As New SAM01100MenuProgramCls

        Try
            loCls.saveGeneralAccess(poparam)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub
End Class
