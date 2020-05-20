USE [QLKHO]
GO
/****** Object:  StoredProcedure [dbo].[Sum_Input]    Script Date: 05/20/2020 9:07:33 ******/
create proc Sum_Input(@from DateTime, @to DateTime)
as 
select sum(InputInfo.Count) as Total from InputInfo where IdInput in (select Id from Input where DateInput >= @from and DateInput <= @to)
