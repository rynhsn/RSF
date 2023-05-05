Imports R_Common
Imports TelerikMenuBackCls
Imports System.ServiceModel.Channels
' NOTE: You can use the "Rename" command on the context menu to change the class name "MenuStreamingService" in code, svc and config file together.
Public Class MenuStreamingService
    Implements IMenuStreamingService

    Public Sub Dummy(poPar As System.Collections.Generic.List(Of TelerikMenuBackCls.MenuDTOnon)) Implements IMenuStreamingService.Dummy

    End Sub

    Public Function getMenu() As System.ServiceModel.Channels.Message Implements IMenuStreamingService.getMenu
        Dim loException As New R_Exception
        Dim loCls As New MenuCls
        Dim loRtnTemp As List(Of MenuDTOnon)
        Dim loRtn As Message = Nothing
        Dim lcCompId As String
        Dim lcUserId As String
        Dim lcMenuId As String
        Dim lcSubMenuId As String
        Dim lnLevel As Integer
        Dim lcModul As String
        Dim lcLangId As String

        Try
            lcCompId = R_Utility.R_GetStreamingContext("cCompId")
            lcUserId = R_Utility.R_GetStreamingContext("cUserId")
            lcMenuId = R_Utility.R_GetStreamingContext("cMenuId")
            lcSubMenuId = R_Utility.R_GetStreamingContext("cSubMenuId")
            lnLevel = R_Utility.R_GetStreamingContext("nLevel")
            lcModul = R_Utility.R_GetStreamingContext("cModul")
            lcLangId = R_Utility.R_GetStreamingContext("cLangId")
            'lcLangId = "en"

            loRtnTemp = loCls.getMenu(lcCompId, lcUserId, lcMenuId, lcSubMenuId, lnLevel, lcModul, lcLangId)

            loRtn = R_StreamUtility(Of MenuDTOnon).WriteToMessage(loRtnTemp.AsEnumerable, "getMenu")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getMenuAccess() As System.ServiceModel.Channels.Message Implements IMenuStreamingService.getMenuAccess
        Dim loException As New R_Exception
        Dim loCls As New MenuCls
        Dim loRtnTemp As List(Of MenuProgramAccessDTO)
        Dim loRtn As Message = Nothing
        Dim lcCompId As String
        Dim lcUserId As String
        Dim lcLang As String

        Try
            lcCompId = R_Utility.R_GetStreamingContext("cCompId")
            lcUserId = R_Utility.R_GetStreamingContext("cUserId")
            lcLang = R_Utility.R_GetStreamingContext("cLang")

            loRtnTemp = loCls.getMenuAccess(lcCompId, lcUserId, lcLang)

            loRtn = R_StreamUtility(Of MenuProgramAccessDTO).WriteToMessage(loRtnTemp.AsEnumerable, "getMenuAccess")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function
End Class
