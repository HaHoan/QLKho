USE [QLKHO]
GO
/****** Object:  StoredProcedure [dbo].[Sum_Output]    Script Date: 05/20/2020 9:08:16 ******/
create proc [dbo].[Sum_Output](@from DateTime, @to DateTime)
as 
select sum(OutputInfo.Count) as Total from OutputInfo where IdOutput in (select Id from Outputs where DateOutput >= @from and DateOutput <= @to)
