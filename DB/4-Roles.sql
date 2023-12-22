create table if not exists AppRole (
	AppRoleId serial primary key,
	Abbreviation varchar(10),
	RoleName varchar(50)
);

create table if not exists AppUserAppRole (
	AppUserId int, 
	AppRoleId int,
	PRIMARY KEY(AppUserId, AppRoleId)
);

insert into AppRole (Abbreviation, RoleName) 
select 'Admin', 'Administrator'
where not exists (select from AppRole where Abbreviation ='Admin');


insert into AppUser (Email, Password, Salt, Status)
select 'admin@willow.com', 'G4rKaDgp72RZkE1VWnOfwC/68UPRrX43XcbXeEvTrSoQdkZyAtUh229xLOKX7K3onp9zHTuHU63DHc1EXIxV6w==', 'd5c1092c-c9a6-41f6-a70f-7e2965d810a3', 1
where 
	not exists (select * from AppUser where Email = 'admin@willow.com');
	
insert into AppUserAppRole
select (select UserId from AppUser where Email = 'admin@willow.com'),
	(select AppRoleId from AppRole where Abbreviation ='Admin')
where not exists (select from AppUserAppRole where AppUserID = (select UserId from AppUser where Email = 'admin@willow.com'));
				  