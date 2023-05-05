Imports R_Common
Imports SAM01200Back
Imports System.ServiceModel.Channels
' NOTE: You can use the "Rename" command on the context menu to change the class name "UserMenuService" in code, svc and config file together.
Public Class UserMenuService
    Implements IUserMenuService

    Public Sub Svc_R_Delete(poEntity As SAM01200Back.UserMenuDTO) Implements R_BackEnd.R_IServicebase(Of SAM01200Back.UserMenuDTO).Svc_R_Delete
        Dim loEx As New R_Exception
        Dim loCls As New UserMenuCls

        Try
            loCls.R_Delete(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_GetRecord(poEntity As SAM01200Back.UserMenuDTO) As SAM01200Back.UserMenuDTO Implements R_BackEnd.R_IServicebase(Of SAM01200Back.UserMenuDTO).Svc_R_GetRecord
        Dim loEx As New R_Exception
        Dim loCls As New UserMenuCls
        Dim loRtn As UserMenuDTO = Nothing

        Try
            loRtn = loCls.R_GetRecord(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function Svc_R_Save(poEntity As SAM01200Back.UserMenuDTO, poCRUDMode As R_Common.eCRUDMode) As SAM01200Back.UserMenuDTO Implements R_BackEnd.R_IServicebase(Of SAM01200Back.UserMenuDTO).Svc_R_Save
        Dim loEx As New R_Exception
        Dim loCls As New UserMenuCls
        Dim loRtn As UserMenuDTO = Nothing

        Try
            loRtn = loCls.R_Save(poEntity, poCRUDMode)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function
End Class
