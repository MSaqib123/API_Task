create database Evolve
use Evolve

--_______________________ User ____________________________
create table UserInf(
	UserId int primary key identity,
	UserName varchar(35),
	UserPassword varchar(50),
	email varchar(50),
	gender bit,
	status bit default 0,
	CreateBy int,
	CreateOn date,
	ModifiedBy int,
	ModifiedOn date,
)



--_______________________ Category ____________________________
create table Category(
	Id int primary key identity , 
	Name varchar(25),
	Description varchar(500),
	status bit default 0,
	CreateBy int,
	CreateOn date,
	ModifiedBy int,
	ModifiedOn date,
)

create table Products(
	Id int primary key identity,
	Name varchar(25),
	Description varchar(500),
	status bit default 0,
	Price money,
	CatId int foreign key references Category(Id),
	CreateBy int,
	CreateOn date,
	ModifiedBy int,	
	ModifiedOn date,
)




alter PROCEDURE spInsertProduct
    @Name NVARCHAR(100),
    @Price DECIMAL(18, 2),
	@Description varchar(500),
    @CategoryId INT,
	@CreateBy int,
	@CreateOn date
AS
BEGIN
    INSERT INTO Products(Name, Price, Description, CatId, CreateBy,CreateOn)
    VALUES(@Name, @Price , @Description, @CategoryId , @CreateBy,@CreateOn)
END									   


alter PROCEDURE spUpdateProduct
    @Id INT,
    @Name NVARCHAR(100),
    @Price DECIMAL(18, 2),
	@Description varchar(500),
    @CategoryId INT,
	@ModifiedBy int ,
	@ModifiedOn date
AS
BEGIN
    UPDATE Products
    SET Name = @Name,
        Price = @Price,
		Description = @Description,
        CatId= @CategoryId,
		ModifiedBy = @ModifiedBy,
		ModifiedOn = @ModifiedOn
    WHERE Id = @Id
END

alter PROCEDURE spGetProductById
    @Id INT
AS
BEGIN
    SELECT *
    FROM Products
    WHERE Id = @Id and status = 0
END

select * from Products

alter PROCEDURE spGetAllProducts
AS
BEGIN
    SELECT *
    FROM Products where status = 0
END

alter PROCEDURE spDeleteProducts
@Id int,
@ModifiedBy int,
@ModifiedOn date
AS
BEGIN
    Update Products
	set
	Status = 1,
	ModifiedBy = @ModifiedBy,
	ModifiedOn = @ModifiedOn
	where Id = @Id
END



--______________________________ category ________________________________
alter PROCEDURE spInsertCategory
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
	@CreateBy int,
	@CreateOn date
AS
BEGIN
    INSERT INTO Category (Name, Description,CreateBy,CreateOn)
    VALUES(@Name, @Description,@CreateBy,@CreateOn)
END


alter PROCEDURE spUpdateCategory
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
	@ModifiedBy int,
	@ModifiedOn date
AS
BEGIN
    UPDATE Category
    SET Name = @Name,
    Description = @Description,
	ModifiedBy = @ModifiedBy,
	ModifiedOn = @ModifiedOn
    WHERE Id = @Id and status = 0
END


alter PROCEDURE spGetCategoryById
    @Id INT
AS
BEGIN
    SELECT *
    FROM Category
    WHERE Id = @Id and status = 0
END


alter PROCEDURE spGetAllCategories
AS
BEGIN
    SELECT *
    FROM Category where status = 0
END


create PROCEDURE spDeleteCategory
@Id int,
@ModifiedBy int,
@ModifiedOn date
AS
BEGIN
    Update Category
	set
	Status = 1,
	ModifiedBy = @ModifiedBy,
	ModifiedOn = @ModifiedOn
	where Id = @Id
END
