Imports R_Common
Imports SAM01200Back
Imports System.ServiceModel.Channels
Imports TelerikMenuService
' NOTE: You can use the "Rename" command on the context menu to change the class name "SAM01200Service" in code, svc and config file together.
Public Class SAM01200Service
    Implements ISAM01200Service

    Public Sub Svc_R_Delete(poEntity As SAM01200Back.SAM01200UserDTO) Implements R_BackEnd.R_IServicebase(Of SAM01200Back.SAM01200UserDTO).Svc_R_Delete
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls

        Try
            loCls.R_Delete(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function Svc_R_GetRecord(poEntity As SAM01200Back.SAM01200UserDTO) As SAM01200Back.SAM01200UserDTO Implements R_BackEnd.R_IServicebase(Of SAM01200Back.SAM01200UserDTO).Svc_R_GetRecord
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As SAM01200UserDTO = Nothing

        Try
            loRtn = loCls.R_GetRecord(poEntity)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function Svc_R_Save(poEntity As SAM01200Back.SAM01200UserDTO, poCRUDMode As R_Common.eCRUDMode) As SAM01200Back.SAM01200UserDTO Implements R_BackEnd.R_IServicebase(Of SAM01200Back.SAM01200UserDTO).Svc_R_Save
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As SAM01200UserDTO = Nothing

        Try
            loRtn = loCls.R_Save(poEntity, poCRUDMode)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getCmbCompany() As System.Collections.Generic.List(Of SAM01200Back.cmbDTO) Implements ISAM01200Service.getCmbCompany
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As List(Of cmbDTO)

        Try
            loRtn = loCls.getCmbCompany()
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getCmbMenu(pcCompId As String) As System.Collections.Generic.List(Of SAM01200Back.cmbDTO) Implements ISAM01200Service.getCmbMenu
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As List(Of cmbDTO)

        Try
            loRtn = loCls.getCmbMenu(pcCompId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getCmbCompanyCopy(pcUserId As String) As System.Collections.Generic.List(Of SAM01200Back.cmbDTO) Implements ISAM01200Service.getCmbCompanyCopy
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As List(Of cmbDTO)

        Try
            loRtn = loCls.getCmbCompanyCopy(pcUserId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getCmbMenuCopy(pcCompId As String, pcUserId As String) As System.Collections.Generic.List(Of SAM01200Back.cmbDTO) Implements ISAM01200Service.getCmbMenuCopy
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As List(Of cmbDTO)

        Try
            loRtn = loCls.getCmbMenuCopy(pcCompId, pcUserId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function resetPass(poParam As SAM01200Back.SAM01200UserDTO) As String Implements ISAM01200Service.resetPass
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As String

        Try
            loRtn = loCls.resetPass(poParam)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Sub SaveImage(pcUpdateBy As String, poSlice As SAM01200Back.SliceDTO) Implements ISAM01200Service.SaveImage
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls

        Try
            loCls.SaveImage(pcUpdateBy, poSlice)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub SliceFiles(pcUpdateBy As String, poSlice As SAM01200Back.SliceDTO) Implements ISAM01200Service.SliceFiles
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls

        Try
            loCls.SliceFiles(pcUpdateBy, poSlice)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function GetImage(pcUserId As String) As System.Collections.Generic.List(Of SAM01200Back.SliceDTO) Implements ISAM01200Service.GetImage
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As List(Of SliceDTO)

        Try
            loRtn = loCls.GetImage(pcUserId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getSMTP(pcCompId As String) As String Implements ISAM01200Service.getSMTP
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtn As String

        Try
            loRtn = loCls.getSMTP(pcCompId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function CheckAssignCompany(pcCompId As String, pcUserId As String, piLicenseNumber As Integer) As Boolean Implements ISAM01200Service.CheckAssignCompany
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim llRtn As Boolean

        Try
            llRtn = loCls.CheckAssignCompany(pcCompId, pcUserId, piLicenseNumber)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return llRtn
    End Function

    Public Function GetUserCompanyCount(pcCompId As String) As Integer Implements ISAM01200Service.GetUserCompanyCount
        Dim loEx As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim liRtn As Integer

        Try
            liRtn = loCls.GetUserCompanyCount(pcCompId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return liRtn
    End Function

    Public Function GetEmailTemplateId(pcCompanyId As String, pcTemplateId As String) As String Implements ISAM01200Service.GetEmailTemplateId
        Dim loEx As New R_Exception
        Dim loCls As New UserCompanyCls
        Dim lcRtn As String = ""

        Try
            lcRtn = loCls.GetEmailTemplateId(pcCompanyId, pcTemplateId)
        Catch ex As Exception
            loEx.Add(ex)
        End Try

        loEx.ConvertAndThrowToServiceExceptionIfErrors()

        Return lcRtn
    End Function
End Class
