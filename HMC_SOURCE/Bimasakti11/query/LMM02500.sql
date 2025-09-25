RSP_LM_GET_TENANT_GROUP_LIST 'rcd', 'jbmpc', 'hmc'

SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', 'rcd', '_BS_TAX_TYPE', '', 'en')

SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', 'rcd', '_BS_ID_TYPE', '', 'en')
SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', 'rcd', '_TAX_CODE', '', 'en')

RSP_GS_GET_PROPERTY_LIST  'rcd', 'hmc'



--DETAIL
RSP_LM_GET_TENANT_GROUP_DETAIL 'rcd', 'jbmpc', 'TGFnB', 'hmc'

--TAB 1, LIST ....
RSP_LM_GET_TENANT_GROUP_LIST 'rcd', 'jbmpc', 'hmc'
RSP_LM_GET_TENANT_LIST_GROUP 'rcd', 'jbmpc', 'TGFnB', 'hmc'

--TAB 2, TAX INFO
RSP_LM_GET_TENANT_GROUP_TAX_INFO 'rcd', 'jbmpc', 'TGFnB','hmc'
--TAB 2, PROFILE
RSP_LM_GET_TENANT_GROUP_DETAIL 'rcd', 'jbmpc',  'TGFnB','hmc'
               
              
             