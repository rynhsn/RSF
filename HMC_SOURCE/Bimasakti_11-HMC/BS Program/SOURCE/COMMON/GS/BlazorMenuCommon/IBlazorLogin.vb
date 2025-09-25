Public Interface IBlazorLogin
    'Function UserLocking(ByVal poParameter As LoginDTO) As BlazorMenuResultDTO

    'Function UserLockingCompany(ByVal pcCurrentCompanyId As String, ByVal pcNewCompanyId As String, ByVal pcUserId As String) As BlazorMenuResultDTO

    Function UserLockingFlush(ByVal poParameter As LoginDTO) As BlazorMenuResultDTO

    Function Logon(ByVal poParameter As LoginDTO) As BlazorMenuResultDTO(Of LoginDTO)

    'Function SetKey(ByVal pcKey As String) As BlazorMenuResultDTO

    'Function GetUserCompanyBroadcast(ByVal poParameter As UserCompanyDTO) As BlazorMenuResultDTO(Of UserCompanyDTO)

    'Function GetCompanyAndUserName(ByVal poParameter As LoginDTO) As BlazorMenuResultDTO(Of LoginDTO)

    'Function GetLastUpdate(ByVal poParameter As LoginDTO) As BlazorMenuResultDTO(Of Nullable(Of Date))

    'Function ClearUserData(ByVal pcUserId As String, ByVal pcCompanyId As String) As BlazorMenuResultDTO

    'Function GetCompanyIdByUser() As BlazorMenuResultDTO(Of String)
End Interface
