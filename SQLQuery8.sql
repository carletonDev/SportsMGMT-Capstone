﻿USE [SportsMGMT-capstone]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[sp_checkroleaccessbyname]
		@name = N'Carleton Cabarrus'

SELECT	@return_value as 'Return Value'

GO
