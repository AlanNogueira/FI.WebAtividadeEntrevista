﻿CREATE PROC FI_SP_BeneficiariosPorCliente
	@IDCLIENTE BIGINT
AS
BEGIN
	SELECT NOME, CPF, IDCLIENTE, ID FROM BENEFICIARIOS WITH(NOLOCK) WHERE IDCLIENTE = @IDCLIENTE
END