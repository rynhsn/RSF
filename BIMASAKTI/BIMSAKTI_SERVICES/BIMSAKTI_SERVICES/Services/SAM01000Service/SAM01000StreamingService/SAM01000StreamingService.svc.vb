Imports R_Common
Imports SAM01000Back
Imports System.ServiceModel.Channels

' NOTE: You can use the "Rename" command on the context menu to change the class name "SAM01000StreamingService" in code, svc and config file together.
Public Class SAM01000StreamingService
    Implements ISAM01000StreamingService

    Public Sub Dummy(poPar As List(Of SAM01000GridDTO), poPar1 As List(Of SMTPDTO), poPar2 As List(Of PlatformDTOnon)) Implements ISAM01000StreamingService.Dummy

    End Sub

    Public Function getCompList() As System.ServiceModel.Channels.Message Implements ISAM01000StreamingService.getCompList
        Dim loException As New R_Exception
        Dim loCls As New SAM01000Cls
        Dim loRtnTemp As List(Of SAM01000GridDTO)
        Dim loRtn As Message = Nothing

        Try
            loRtnTemp = loCls.getCompList()

            loRtn = R_StreamUtility(Of SAM01000GridDTO).WriteToMessage(loRtnTemp.AsEnumerable, "getCompList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function getSMTPList() As System.ServiceModel.Channels.Message Implements ISAM01000StreamingService.getSMTPList
        Dim loException As New R_Exception
        Dim loCls As New SAM01000Cls
        Dim loRtnTemp As List(Of SMTPDTO)
        Dim loRtn As Message = Nothing

        Try
            loRtnTemp = loCls.getSMTPList()

            loRtn = R_StreamUtility(Of SMTPDTO).WriteToMessage(loRtnTemp.AsEnumerable, "getSMTPList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function

    Public Function GetPlatformList() As Message Implements ISAM01000StreamingService.GetPlatformList
        Dim loException As New R_Exception
        Dim loCls As New SAM01000PlatformCls
        Dim loRtnTemp As List(Of PlatformDTOnon)
        Dim loRtn As Message = Nothing
        Dim loList As New List(Of Byte())
        Dim lcCompId As String = ""

        Try
            lcCompId = R_Utility.R_GetStreamingContext("CCOMPANY_ID")

            loRtnTemp = loCls.GetPlatformList(lcCompId)

            loRtn = R_StreamUtility(Of PlatformDTOnon).WriteToMessage(loRtnTemp.AsEnumerable, "GetPlatformList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function
End Class
