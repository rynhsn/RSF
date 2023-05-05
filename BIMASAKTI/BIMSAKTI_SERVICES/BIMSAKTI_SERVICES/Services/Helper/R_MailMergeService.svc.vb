Imports R_Common
' NOTE: You can use the "Rename" command on the context menu to change the class name "R_MailMergeService" in code, svc and config file together.
Public Class R_MailMergeService
    Implements IR_MailMergeService

    Public Function Svc_R_LoadMailMergeTemplateList(poMailMergePar As R_Common.R_MailMergePar) As System.Collections.Generic.List(Of R_Common.R_MailMergePar) Implements R_Common.IR_MailMergeService.Svc_R_LoadMailMergeTemplateList
        Dim loException As New R_Exception
        Dim loResult As List(Of R_MailMergePar) = Nothing

        Try
            Dim loClass As New R_BackEnd.R_MailMergeBack

            loResult = loClass.R_LoadMailMergeTemplateList(poMailMergePar)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loResult
    End Function

    Public Function R_LoadMailMergeDoc(poMailMergePar As R_Common.R_MailMergePar) As Byte() Implements R_Common.IR_MailMergeService.R_LoadMailMergeDoc
        Dim loException As New R_Exception
        Dim loResult As Byte() = Nothing

        Try
            Dim loClass As New R_BackEnd.R_MailMergeBack

            loResult = loClass.R_LoadMailMergeDoc(poMailMergePar)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loResult
    End Function
End Class
