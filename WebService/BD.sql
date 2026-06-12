Create Database Gestion_Eventos;

use Gestion_Eventos; 


Create Table Categories(
	id int primary key identity(1,1),
	nameCategory varchar(30)not null,
); 


Create Table Activities(
	id int primary key identity(1,1),
	nameActivitie varchar(50)not null,
	starDate date not null,
	endDate date not null,
	categorieId int not null,
	notes text not null,
	constraint fk_categorie foreign key (categorieId)references Categories(id)
);



