﻿CREATE PROC FI_SP_IncBeneficiarioV2
    @NOME          VARCHAR (50),
    @CPF           VARCHAR (11),
	@IDCLIENTE	   BIGINT
AS
BEGIN
	INSERT INTO BENEFICIARIOS (NOME,  CEP, IDCLIENTE) 
	VALUES (@NOME, CPF, @IDCLIENTE)

	SELECT SCOPE_IDENTITY()
END