' NOTE: You can use the "Rename" command on the context menu to change the class name "SAM01000PlatformService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select SAM01000PlatformService.svc or SAM01000PlatformService.svc.vb at the Solution Explorer and start debugging.
Imports R_BackEnd
Imports R_Common
Imports SAM01000Back

Public Class SAM01000PlatformService
    Implements ISAM01000PlatformService

    Public Sub Svc_R_Delete(poEntity As PlatformDTO) Implements R_IServicebase(Of PlatformDTO).Svc_R_Delete
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000PlatformCls

        Try
            loCls.R_Delete(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_GetRecord(poEntity As PlatformDTO) As PlatformDTO Implements R_IServicebase(Of PlatformDTO).Svc_R_GetRecord
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000PlatformCls
        Dim loRtn As PlatformDTO = Nothing

        Try
            loRtn = loCls.R_GetRecord(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function Svc_R_Save(poEntity As PlatformDTO, poCRUDMode As eCRUDMode) As PlatformDTO Implements R_IServicebase(Of PlatformDTO).Svc_R_Save
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000PlatformCls
        Dim loRtn As PlatformDTO = Nothing

        Try
            loRtn = loCls.R_Save(poEntity, poCRUDMode)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function
End Class
