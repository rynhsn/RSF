Imports R_Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "MenuService" in code, svc and config file together.
Public Class MenuService
    Implements IMenuService

    Public Sub deleteMenuFromDB(poMenuDTO As TelerikMenuBackCls.MenuDTO) Implements IMenuService.deleteMenuFromDB
        Dim loException As New R_Exception

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loClass.deleteMenuFromDB(poMenuDTO)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function getLastId(pcCompanyId As String, pcUserId As String, pcMENU_ID As String, pcSUB_MENU_TYPE As String) As TelerikMenuBackCls.MenuDTO Implements IMenuService.getLastId
        Dim loException As New R_Exception
        Dim loLastId As TelerikMenuBackCls.MenuDTO = Nothing

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loLastId = loClass.getLastId(pcCompanyId, pcUserId, pcMENU_ID, pcSUB_MENU_TYPE)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return loLastId
    End Function

    Public Sub insertMenuToDB(poMenuDTO As TelerikMenuBackCls.MenuDTO) Implements IMenuService.insertMenuToDB
        Dim loException As New R_Exception

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loClass.insertMenuToDB(poMenuDTO)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub updateGroupIndexToDB(poMenuDTO As TelerikMenuBackCls.MenuDTO, pcOperation As String) Implements IMenuService.updateGroupIndexToDB
        Dim loException As New R_Exception

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loClass.updateGroupIndexToDB(poMenuDTO, pcOperation)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub updateMenuNameToDB(poMenuDTO As TelerikMenuBackCls.MenuDTO) Implements IMenuService.updateMenuNameToDB
        Dim loException As New R_Exception

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loClass.updateMenuNameToDB(poMenuDTO)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub updateMenuPositionToDB(poMenuDTO As TelerikMenuBackCls.MenuDTO) Implements IMenuService.updateMenuPositionToDB
        Dim loException As New R_Exception

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loClass.updateMenuPositionToDB(poMenuDTO)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function _getProgramImage(poMenuDTO As TelerikMenuBackCls.MenuDTO) As String Implements IMenuService._getProgramImage
        Dim loException As New R_Exception
        Dim lcProgramImageName As String = ""

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            lcProgramImageName = loClass._getProgramImage(poMenuDTO)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return lcProgramImageName
    End Function

    Public Sub saveMenuFavourite(poMenuDTO As TelerikMenuBackCls.MenuDTO) Implements IMenuService.saveMenuFavourite
        Dim loException As New R_Exception

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loClass.saveMenuFavourite(poMenuDTO)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Sub deleteMenuFavourite(poMenuDTO As TelerikMenuBackCls.MenuDTO) Implements IMenuService.deleteMenuFavourite
        Dim loException As New R_Exception

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loClass.deleteMenuFavourite(poMenuDTO)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function svc_getCompanyList(pcUserId As String, pcCompanyId As String) As System.Collections.Generic.List(Of TelerikMenuBackCls.SAM_USER_COMPANYDTO) Implements IMenuService.svc_getCompanyList
        Dim loException As New R_Exception
        Dim loRtn As New List(Of TelerikMenuBackCls.SAM_USER_COMPANYDTO)

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loRtn = loClass.getCompanyList(pcUserId, pcCompanyId)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return loRtn
    End Function

    Public Function checkMenuFavourite(poMenuDTO As TelerikMenuBackCls.MenuDTO) As Boolean Implements IMenuService.checkMenuFavourite
        Dim loException As New R_Exception
        Dim llExist As Boolean = False

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            llExist = loClass.checkMenuFavourite(poMenuDTO)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return llExist
    End Function

    Public Function getSecurity() As Boolean Implements IMenuService.getSecurity
        Dim loException As New R_Exception
        Dim llSecurity As Boolean = False

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            llSecurity = loClass.getSecurity()
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return llSecurity
    End Function

    Public Function getAccessButton(pcCompId As String, pcProgId As String) As System.Collections.Generic.List(Of TelerikMenuBackCls.MenuProgramAccessDTO) Implements IMenuService.getAccessButton
        Dim loException As New R_Exception
        Dim loRtn As New List(Of TelerikMenuBackCls.MenuProgramAccessDTO)

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loRtn = loClass.getAccessButton(pcCompId, pcProgId)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return loRtn
    End Function

    Public Sub SaveHistory(pcCompId As String, pcUserId As String, pcProgId As String, pcAction As String) Implements IMenuService.SaveHistory
        Dim loException As New R_Exception

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loClass.SaveHistory(pcCompId, pcUserId, pcProgId, pcAction)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
    End Sub

    Public Function GetInfo(pcAppId As String) As List(Of TelerikMenuBackCls.AboutDTO) Implements IMenuService.GetInfo
        Dim loException As New R_Exception
        Dim loRtn As New List(Of TelerikMenuBackCls.AboutDTO)

        Try
            Dim loClass As New TelerikMenuBackCls.MenuCls

            loRtn = loClass.GetInfo(pcAppId)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()
        Return loRtn
    End Function
End Class
