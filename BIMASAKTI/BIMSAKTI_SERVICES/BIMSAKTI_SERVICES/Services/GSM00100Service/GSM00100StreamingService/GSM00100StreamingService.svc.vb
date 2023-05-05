Imports R_Common
Imports GSM00100Back
Imports System.ServiceModel.Channels
Imports HelperStreamExtensionLibrary
Imports GSM00100Common

' NOTE: You can use the "Rename" command on the context menu to change the class name "GSM00100StreamingService" in code, svc and config file together.
Public Class GSM00100StreamingService
    Implements IGSM00100StreamingService

    Public Function getSMTPList() As System.ServiceModel.Channels.Message Implements IGSM00100StreamingService.getSMTPList
        Dim loException As New R_Exception
        Dim loCls As New GSM00100Cls
        Dim loRtnTemp As List(Of GSM00100DTOnon)
        Dim loRtn As Message = Nothing
        Dim loList As New List(Of Byte())

        Try
            loRtnTemp = loCls.getSMTPList()

            loList = R_Utility.R_GetChunkData(Of GSM00100DTOnon)(loRtnTemp, R_BackEnd.R_BackGlobalVar.CHUNK_SIZE)

            loRtn = R_StreamUtility(Of Byte()).WriteToMessage(loList.AsEnumerable, "getSMTPList")
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loRtn
    End Function
End Class
