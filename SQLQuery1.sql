USE [SportsMGMT-capstone]
GO

DECLARE	@return_value Int

EXEC	[dbo].[sp_ViewNullContracts]

SELECT	@return_value as 'Return Value'

GO
