Imports R_CommonFrontBackAPI

Public Interface IGSM00100
    Inherits R_IServiceCRUDBase(Of GSM00100DTO)

    Function GetSMTPList() As GSM00100GenericResultDTO(Of List(Of GSM00100DTOList))

    Function CheckDelete(poParam As GSM00100DTO) As GSM00100GenericResultDTO(Of Boolean)

    Function TestSendEmail(poParam As EmailDTO) As GSM00100GenericResultDTO

    Function CheckSupportedEmailProvider() As GSM00100GenericResultDTO
End Interface
