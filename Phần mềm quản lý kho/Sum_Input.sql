use QLKHO
go
create proc Sum_Inventory
as 
select Product.BarCode,DisplayName,sum(OutputInfo.Count) as TotalOutput, sum(InputInfo.Count) as TotalInput from OutputInfo
left join ( InputInfo left join Product on IdProduct = Product.Id) on IdInputInfo = InputInfo.Id
group by DisplayName,Product.BarCode

go