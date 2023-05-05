Imports R_Common
Imports R_BackEnd
' NOTE: You can use the "Rename" command on the context menu to change the class name "R_LockingService" in code, svc and config file together.
Public Class R_LockingService
    Implements IR_LockingService

    Public Function Svc_R_GetLockBy(poUnlockPar As R_Common.R_UnlockPar) As R_Common.R_StructureLockResult Implements R_Common.IR_LockingService.Svc_R_GetLockBy
        Dim loLockResult As R_StructureLockResult = Nothing
        Dim loException As New R_Exception

        Try
            loLockResult = R_LockingBack.R_GetLockBy(poUnlockPar)
        Catch ex As Exception
            loException.Add(ex)
        End Try
        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loLockResult
    End Function

    Public Function Svc_R_Lock(poLockPar As R_Common.R_LockPar) As R_Common.R_StructureLockResult Implements R_Common.IR_LockingService.Svc_R_Lock
        Dim loLockResult As R_StructureLockResult = Nothing
        Dim loException As New R_Exception

        Try
            loLockResult = R_LockingBack.R_Lock(poLockPar)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loLockResult
    End Function

    Public Function Svc_R_Unlock(poUnlockPar As R_Common.R_UnlockPar) As R_Common.R_StructureLockResult Implements R_Common.IR_LockingService.Svc_R_Unlock
        Dim loLockResult As R_StructureLockResult = Nothing
        Dim loException As New R_Exception

        Try
            loLockResult = R_LockingBack.R_Unlock(poUnlockPar)
        Catch ex As Exception
            loException.Add(ex)
        End Try

        loException.ConvertAndThrowToServiceExceptionIfErrors()

        Return loLockResult
    End Function

End Class
