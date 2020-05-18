create database QLKHO
go
use QLKHO
go
create table Unit
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max)

)
go

create table Suplier
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	Address nvarchar(max),
	Phone nvarchar(20),
	Email nvarchar(200),
	MoreInfo nvarchar(max),
	ContractDate DateTime
)
go

create table Customer
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	Address nvarchar(max),
	Phone nvarchar(20),
	Email nvarchar(200),
	MoreInfo nvarchar(max)
)
go

create table Product
(
	Id int identity(1,1) primary key,
	DisplayName nvarchar(max),
	BarCode nvarchar(max),
	IdUnit int,
	IdSuplier int,
	States nvarchar(max),
	foreign key (IdUnit) references Unit(Id),
	foreign key(IdSuplier) references Suplier(Id)
)
go

create table Input
(
	Id int identity(1,1) primary key,
	DateInput DateTime
)
go

create table InputInfo
(
	Id int identity(1,1) primary key,
	IdProduct int not null,
	IdInput int not null,
	Count int,
	InputPrice float default 0,
	OutputPrice float default 0,
	States nvarchar(max),

	foreign key(IdInput) references Input(Id),
	foreign key(IdProduct) references Product(Id)
)
go

create table Outputs
(
	Id int identity(1,1) primary key,
	DateOutput DateTime
)
go

create table OutputInfo
(
	Id int identity(1,1) primary key,
	IdProduct int not null,
	IdInputInfo int not null,
	IdOutput int not null,
	OutputPrice float default 0,
	Count int,
	IdCustomer int,
	States nvarchar(max),
	
	foreign key (IdProduct) references Product(Id),
	foreign key(IdInputInfo) references InputInfo(Id),
	foreign key(IdOutput) references Outputs(Id),
	foreign key(IdCustomer) references Customer(Id)
)
go