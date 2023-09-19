drop table if exists Containers;
drop table if exists Spots;

create table Containers (
	Id int not null primary key identity,
	Code nvarchar(255) not null,
	Length int not null,
	Type nvarchar(255) not null,
);

create table Spots (
	Id int not null primary Key identity,
	X int not null,
	Y int not null,
	ContainerId int null foreign key references Containers(Id)
);