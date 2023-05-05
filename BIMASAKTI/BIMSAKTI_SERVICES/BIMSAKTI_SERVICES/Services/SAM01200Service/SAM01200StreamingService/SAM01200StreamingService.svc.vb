Imports R_Common
Imports SAM01200Back
Imports System.ServiceModel.Channels
Imports SAM01200Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "SAM01200StreamingService" in code, svc and config file together.
Public Class SAM01200StreamingService
    Implements ISAM01200StreamingService

    Public Function getUserCompanyList() As System.ServiceModel.Channels.Message Implements ISAM01200StreamingService.getUserCompanyList
        Dim loException As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtnTemp As List(Of UserCompanyDTOnon)
        Dim loRtn As Message
        Dim lcUserId As String
        Dim loList As New List(Of Byte())

        Try
            lcUserId = R_Utility.R_GetStreamingContext("cUserId")

            loRtnTemp = loCls.getUserCompanyList(lcUserId)

            loList = R_Utility.R_GetChunkData(Of UserCompanyDTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getUserCompanyList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getUserList() As System.ServiceModel.Channels.Message Implements ISAM01200StreamingService.getUserList
        Dim loException As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtnTemp As List(Of SAM01200UserDTOnon)
        Dim loRtn As Message
        Dim loList As New List(Of Byte())

        Try
            loRtnTemp = loCls.getUserList()

            loList = R_Utility.R_GetChunkData(Of SAM01200UserDTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getUserList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getUserMenuList() As System.ServiceModel.Channels.Message Implements ISAM01200StreamingService.getUserMenuList
        Dim loException As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtnTemp As List(Of UserMenuDTOnon)
        Dim loRtn As Message
        Dim lcUserId As String
        Dim lcCompId As String
        Dim loList As New List(Of Byte())

        Try
            lcCompId = R_Utility.R_GetStreamingContext("cCompanyId")
            lcUserId = R_Utility.R_GetStreamingContext("cUserId")

            loRtnTemp = loCls.getUserMenuList(lcUserId, lcCompId)

            loList = R_Utility.R_GetChunkData(Of UserMenuDTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getUserMenuList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getCompanyMultiple() As System.ServiceModel.Channels.Message Implements ISAM01200StreamingService.getCompanyMultiple
        Dim loException As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtnTemp As List(Of UserCompanyDTOnon)
        Dim loRtn As Message
        Dim loList As New List(Of Byte())

        Try
            loRtnTemp = loCls.getCompanyMultiple()

            loList = R_Utility.R_GetChunkData(Of UserCompanyDTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getCompanyMultiple")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getMenuMultiple() As System.ServiceModel.Channels.Message Implements ISAM01200StreamingService.getMenuMultiple
        Dim loException As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtnTemp As List(Of UserMenuDTOnon)
        Dim loRtn As Message
        Dim lcCompId As String
        Dim loList As New List(Of Byte())

        Try
            lcCompId = R_Utility.R_GetStreamingContext("cCompanyId")

            loRtnTemp = loCls.getMenuMultiple(lcCompId)

            loList = R_Utility.R_GetChunkData(Of UserMenuDTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getMenuMultiple")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getCompanyCopyList() As System.ServiceModel.Channels.Message Implements ISAM01200StreamingService.getCompanyCopyList
        Dim loException As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtnTemp As List(Of CompanyCopyDTO)
        Dim loRtn As Message
        Dim lcUserId As String
        Dim loList As New List(Of Byte())

        Try
            lcUserId = R_Utility.R_GetStreamingContext("cUserId")

            loRtnTemp = loCls.getCompanyCopyList(lcUserId)

            loList = R_Utility.R_GetChunkData(Of CompanyCopyDTO)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getCompanyCopyList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getMenuCopyList() As System.ServiceModel.Channels.Message Implements ISAM01200StreamingService.getMenuCopyList
        Dim loException As New R_Exception
        Dim loCls As New SAM01200Cls
        Dim loRtnTemp As List(Of UserMenuDTOnon)
        Dim loRtn As Message
        Dim lcCompId As String
        Dim loList As New List(Of Byte())

        Try
            lcCompId = R_Utility.R_GetStreamingContext("cCompanyId")

            loRtnTemp = loCls.getMenuCopyList(lcCompId)

            loList = R_Utility.R_GetChunkData(Of UserMenuDTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getMenuCopyList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function GetAssignmentUserList() As System.ServiceModel.Channels.Message Implements ISAM01200StreamingService.GetAssignmentUserList
        Dim loException As New R_Exception
        Dim loCls As New RapidAssignmentCls
        Dim loRtnTemp As List(Of SAM01200UserDTOnon)
        Dim loRtn As Message
        Dim loList As New List(Of Byte())

        Try
            Dim lcCompId As String = R_Utility.R_GetStreamingContext("CCOMPANY_ID")

            loRtnTemp = loCls.GetAssignmentUserList(lcCompId)

            loList = R_Utility.R_GetChunkData(Of SAM01200UserDTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "GetAssignmentUserList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function
End Class
