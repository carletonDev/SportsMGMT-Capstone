USE [SportsMGMT-capstone]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[sp_addexceptions]
		@message = NULL,
		@inner = NULL,
		@link = NULL,
		@date = NULL

SELECT	@return_value as 'Return Value'

GO
