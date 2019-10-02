USE [SportsMGMT-capstone]
GO

/****** Object: SqlProcedure [dbo].[sp_AddException] Script Date: 6/24/2019 12:28:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [sp_AddException] @message nvarchar(max),@date datetime
as
insert into ExceptionLogging(message_recieved,date_logged) values(@message,@date)
