select OutputInfo.*,
InputInfo.IdInput,
InputInfo.Count as InputCount,
InputInfo.InputPrice,
InputInfo.OutputPrice as DefaultOutputPrice,
InputInfo.States as InputState,
Input.DateInput,
Product.Id as IdProduct,
Product.DisplayName as ProductName,
                    Product.BarCode,
                    Product.IdUnit,
                    Product.IdSuplier,
                    Unit.DisplayName as UnitName,
                    Suplier.DisplayName as SuplierName,
                    Suplier.Address,
                    Suplier.Phone,
                    Suplier.Email,
                    Suplier.MoreInfo,
                    Suplier.ContractDate,
                    Product.States ProductStates,
					Outputs.DateOutput,
Customer.DisplayName as CustomerName,
                    Customer.Address as CustomerAddress,
                    Customer.Phone as CustomerPhone,
                    Customer.Email as CustomerEmail,
                    Customer.MoreInfo as CustomerMoreInfo
                   
from OutputInfo
inner join InputInfo on InputInfo.Id = OutputInfo.IdInputInfo
inner join Product on Product.Id = InputInfo.IdProduct
inner join Unit on Unit.Id = Product.IdUnit
inner join Suplier on Suplier.Id = Product.IdSuplier
inner join Outputs on Outputs.Id = OutputInfo.IdOutput
inner join Customer on Customer.Id = OutputInfo.IdCustomer
inner join Input on Input.Id = InputInfo.IdInput