Imports R_Common
Imports SAM01000Back
Imports System.ServiceModel.Channels
' NOTE: You can use the "Rename" command on the context menu to change the class name "SAM01000Service" in code, svc and config file together.
Public Class SAM01000Service
    Implements ISAM01000Service

    Public Sub Svc_R_Delete(poEntity As SAM01000Back.SAM01000DTO) Implements R_BackEnd.R_IServicebase(Of SAM01000Back.SAM01000DTO).Svc_R_Delete

    End Sub

    Public Function Svc_R_GetRecord(poEntity As SAM01000Back.SAM01000DTO) As SAM01000Back.SAM01000DTO Implements R_BackEnd.R_IServicebase(Of SAM01000Back.SAM01000DTO).Svc_R_GetRecord
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls
        Dim loRtn As SAM01000DTO = Nothing

        Try
            loRtn = loCls.R_GetRecord(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function Svc_R_Save(poEntity As SAM01000Back.SAM01000DTO, poCRUDMode As R_Common.eCRUDMode) As SAM01000Back.SAM01000DTO Implements R_BackEnd.R_IServicebase(Of SAM01000Back.SAM01000DTO).Svc_R_Save
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls
        Dim loRtn As SAM01000DTO = Nothing

        Try
            loRtn = loCls.R_Save(poEntity, poCRUDMode)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getCmbLOB() As System.Collections.Generic.List(Of SAM01000Back.SAM01000CmbDTO) Implements ISAM01000Service.getCmbLOB
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls
        Dim loRtn As List(Of SAM01000CmbDTO) = Nothing

        Try
            loRtn = loCls.getCmbLOB()
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function GetImage(pcCompId As String) As SAM01000Back.SliceDTO Implements ISAM01000Service.GetImage
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls
        Dim loRtn As SliceDTO = Nothing

        Try
            loRtn = loCls.GetImage(pcCompId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Sub SliceFiles(pcUpdateBy As String, poSlice As SAM01000Back.SliceDTO) Implements ISAM01000Service.SliceFiles
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls

        Try
            loCls.SliceFiles(pcUpdateBy, poSlice)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub SaveImage(pcUpdateBy As String, poSlice As SAM01000Back.SliceDTO, pcCompId As String) Implements ISAM01000Service.SaveImage
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls

        Try
            loCls.SaveImage(pcUpdateBy, poSlice, pcCompId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub SaveDateTime(poNewEntity As SAM01000Back.SAM01000DTO) Implements ISAM01000Service.SaveDateTime
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls

        Try
            loCls.SaveDateTime(poNewEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function getLicenseActivation(pcCompId As String) As SAM01000Back.LicenseDTO Implements ISAM01000Service.getLicenseActivation
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls
        Dim loRtn As LicenseDTO = Nothing

        Try
            loRtn = loCls.getLicenseActivation(pcCompId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Sub SaveTimeout(poNewEntity As SAM01000DTO) Implements ISAM01000Service.SaveTimeout
        Dim loEx As New R_Exception
        Dim loCls As New SAM01000Cls

        Try
            loCls.SaveTimeout(poNewEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub
End Class
