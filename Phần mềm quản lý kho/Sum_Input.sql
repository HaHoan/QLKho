use QLKHO
go
create proc Sum_Output(@from DateTime, @to DateTime)
as 
select sum(OutputInfo.Count) as Total from OutputInfo where IdOutput in (select Id from Outputs where DateOutput >= @from and DateOutput <= @to)
go