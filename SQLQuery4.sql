
create procedure sp_CreateContract @type nvarchar(100), @salary decimal(18,2)
as
Insert into Contracts(contract_type,salary) values (@type,@salary)
go

select *  from Contracts