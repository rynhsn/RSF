Public Class R_SecurityPolicyDTO
    Public Property CCOMPANY_ID As String
    Public Property CUSER_ID As String
    Public Property CUSER_PASSWORD As String

    Public Property LMINIMUM_PASSWORD_LENGTH As Boolean
    Public Property IMINIMUM_PASSWORD_LENGTH As Integer
    Public Property LMINIMUM_PASSWORD_AGE As Boolean
    Public Property IMINIMUM_PASSWORD_AGE As Integer
    Public Property LMAXIMUM_PASSWORD_AGE As Boolean
    Public Property CMAXIMUM_PASSWORD_AGE_OPTION As String
    Public Property IMAXIMUM_PASSWORD_AGE_DAYS As Integer
    Public Property IMAXIMUM_PASSWORD_AGE_CERTAIN_DAY_OF_MONTH As Integer
    Public Property IMAXIMUM_PASSWORD_AGE_MONTH As Integer
    Public Property CMAXIMUM_PASSWORD_AGE_MONTH_EFFECTIVE_DATE As String
    Public Property LENFORCE_PASSWORD_HISTORY As Boolean
    Public Property IENFORCE_PASSWORD_HISTORY As Integer
    Public Property LCOMPLEXITY As Boolean
    Public Property LINCLUDE_LETTER As Boolean
    Public Property LINCLUDE_UPPERCASE As Boolean
    Public Property LINCLUDE_LOWERCASE As Boolean
    Public Property LINCLUDE_NUMBER As Boolean
    Public Property LINCLUDE_SPECIAL_CHARACTER As Boolean
    Public Property LCANNOT_CONTAIN_USER_ID As Boolean
    Public Property LACCOUNT_LOCKOUT_DURATION As Boolean
    Public Property IACCOUNT_LOCKOUT_DURATION As Integer
    Public Property LACCOUNT_LOCKOUT_THRESHOLD As Boolean
    Public Property IACCOUNT_LOCKOUT_THRESHOLD As Integer
    Public Property LRESET_ACCOUNT_LOCKOUT_THRESHOLD As Boolean
    Public Property IRESET_ACCOUNT_LOCKOUT_THRESHOLD As Integer

    Public Property CLAST_UPDATE_PASSWORD As String
    Public Property CCURRENT_DATE As String
    Public Property CACCOUNT_LOCKOUT_DATE As String
    Public Property CFAILED_ATTEMPT_DATE As String
    Public Property NFAILED_ATTEMPT As Integer
    Public Property CNEXT_UPDATE_PASSWORD As String

    'Public Property lChooseCompanyAtLoginPage As Boolean
    'Public Property cSecurityAndAccountPolicy As String
    'Public Property cLastUpdatePassword As String
    'Public Property cNextUpdatePassword As String
    'Public Property cCurrentDate As String
    'Public Property cAccountLockoutDate As String
    'Public Property nFailedAttempt As Integer
    'Public Property cFailedAttemptDate As String
End Class
