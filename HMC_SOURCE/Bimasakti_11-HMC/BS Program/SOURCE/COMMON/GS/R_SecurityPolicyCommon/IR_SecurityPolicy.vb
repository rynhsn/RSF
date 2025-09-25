Public Interface IR_SecurityPolicy
    Function R_GetSecurityPolicyParameter() As SecurityPolicyGenericResultDTO(Of R_SecurityPolicyParameterDTO)

    Function R_SecurityPolicyLogon(ByVal poParameter As R_SecurityPolicyDTO) As SecurityPolicyGenericResultDTO(Of R_SecurityPolicyTokenDTO)
End Interface
