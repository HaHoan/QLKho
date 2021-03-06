USE [QLKHO]
GO
/****** Object:  StoredProcedure [dbo].[Sum_Inventory]    Script Date: 05/20/2020 9:08:03 ******/
create proc [dbo].[Sum_Inventory](@from DateTime, @to DateTime)
as 
select BarCode, DisplayName, TotalInput, TotalOutput from
(select IdProduct, Product.DisplayName,BarCode, sum(InputInfo.Count) as TotalInput from InputInfo 
inner join Product on IdProduct = Product.Id 
inner join Input on IdInput = Input.Id 
where DateInput >= @from and DateInput <= @to
group by IdProduct,Product.DisplayName, BarCode ) as TotalInputs
left join 
(select IdProduct, sum(OutputInfo.Count) as TotalOutput from 
(OutputInfo inner join Outputs on IdOutput = Outputs.Id)
where DateOutput >= @from and DateOutput <= @to
group by IdProduct) as TotalOutputs
on TotalInputs.IdProduct = TotalOutputs.IdProduct
go