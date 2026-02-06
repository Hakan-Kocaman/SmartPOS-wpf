use master
GO

create database OTOMATIONDB
go

use OTOMATIONDB
go


create table Employee(
Employee_id int identity primary key,
Employee_name varchar(20) unique,

Company_id int,
);

create table Company(
Company_id int identity primary key,
Company_title varchar(20) unique,
);


create table Item(
Item_id int identity primary key,
Item_title varchar(20) unique,
Item_price decimal(10,2),

Category_id int,
);

create table Category(
Category_id int identity primary key,
Category_title varchar(20) unique,

Company_id int,
);


create table Orders(
Orders_id int identity primary key,
Create_date date, 
Employee_id int,
Total_price decimal(10,2), 
);

create table OrderItem(
OrderItem_id int identity primary key,
OrderItem_price decimal(10,2),
OrderItem_quantity int,
Item_id int,
Orders_id int,
);

alter table Employee
 add foreign key (Company_id) references Company(Company_id);

alter table Item
 add foreign key (Category_id) references Category(Category_id);

alter table Category
 add foreign key (Company_id) references Company(Company_id);

alter table Orders
 add foreign key (Employee_id) references Employee(Employee_id);

alter table OrderItem
 add foreign key (Item_id) references Item(Item_id);

alter table OrderItem
 add foreign key (Orders_id) references Orders(Orders_id);

