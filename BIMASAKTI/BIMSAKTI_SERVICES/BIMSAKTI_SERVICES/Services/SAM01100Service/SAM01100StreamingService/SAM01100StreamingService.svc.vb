Imports R_Common
Imports SAM01100Back
Imports System.ServiceModel.Channels
Imports SAM01100Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "SAM01100StreamingService" in code, svc and config file together.
Public Class SAM01100StreamingService
    Implements ISAM01100StreamingService

    Public Function getMenuList() As System.ServiceModel.Channels.Message Implements ISAM01100StreamingService.getMenuList
        Dim loException As New R_Exception
        Dim loCls As New SAM01100Cls
        Dim loRtnTemp As List(Of SAM01100GridDTO)
        Dim loRtn As Message = Nothing
        Dim lcCompId As String
        Dim loList As New List(Of Byte())

        Try
            lcCompId = R_Utility.R_GetStreamingContext("cCompId")

            loRtnTemp = loCls.getMenuList(lcCompId)

            loList = R_Utility.R_GetChunkData(Of SAM01100GridDTO)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getMenuList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getMenuProgramList() As System.ServiceModel.Channels.Message Implements ISAM01100StreamingService.getMenuProgramList
        Dim loException As New R_Exception
        Dim loCls As New SAM01100Cls
        Dim loRtnTemp As List(Of SAM01100MenuProgramDTOnon)
        Dim loRtn As Message = Nothing
        Dim lcCompId As String
        Dim lcMenuId As String
        Dim loList As New List(Of Byte())

        Try
            lcCompId = R_Utility.R_GetStreamingContext("cCompId")
            lcMenuId = R_Utility.R_GetStreamingContext("cMenuId")

            loRtnTemp = loCls.getMenuProgramList(lcCompId, lcMenuId)

            loList = R_Utility.R_GetChunkData(Of SAM01100MenuProgramDTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getMenuProgramList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getProgramList() As System.ServiceModel.Channels.Message Implements ISAM01100StreamingService.getProgramList
        Dim loException As New R_Exception
        Dim loCls As New SAM01100Cls
        Dim loRtnTemp As List(Of ProgramDTO)
        Dim loRtn As Message = Nothing
        Dim loList As New List(Of Byte())

        Try
            loRtnTemp = loCls.getProgramList()

            loList = R_Utility.R_GetChunkData(Of ProgramDTO)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getProgramList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getProgramButton() As System.ServiceModel.Channels.Message Implements ISAM01100StreamingService.getProgramButton
        Dim loException As New R_Exception
        Dim loCls As New SAM01100Cls
        Dim loRtnTemp As List(Of ButtonDTO)
        Dim loRtn As Message = Nothing
        Dim lcProgId As String
        Dim lcCompId As String
        Dim lcMenuId As String
        Dim loList As New List(Of Byte())

        Try
            lcProgId = R_Utility.R_GetStreamingContext("ProgID")
            lcCompId = R_Utility.R_GetStreamingContext("CompID")
            lcMenuId = R_Utility.R_GetStreamingContext("MenuID")

            loRtnTemp = loCls.getProgramButton(lcProgId, lcCompId, lcMenuId)

            loList = R_Utility.R_GetChunkData(Of ButtonDTO)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getProgramButton")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function
End Class
