
--Category Form
Create proc usp_SSelectCategories
as
Select [CategoryID], [CategoryName]
from
[dbo].[Categories]

Create proc usp_SSelectCategoryByID 
@CategoryID int
As
Select [CategoryName], [Description]
from [dbo].[Categories]
Where [CategoryID] = @CategoryID

Create proc usp_SInsertCategory --'SaneleTest', 'bnbnbnbnbnbnbnbnb'
@CategoryName nvarchar(15),
@Description ntext
As
Insert into [dbo].[Categories]
([CategoryName], [Description])
Values(@CategoryName, @Description)

Create proc usp_SUpdateCategory  --'Hot Beverages','aksjdhsakjsdhaksjdshakjd'
@CategoryID int,
@CategoryName nvarchar(15),
@Description ntext
As
Update [dbo].[Categories]
Set [CategoryName] = @CategoryName,
[Description] = @Description
Where [CategoryID] = @CategoryID
--------------------------------------------------------------------------------

--Login Form
Create proc [dbo].[usp_SLogin] 
@UserName varchar(15),
@Password varchar(10)
AS 
SELECT count([Pk_UserId])
FROM [dbo].[tblUsers]
Where [UserName]= @UserName AND [UserPassword]= @Password
---------------------------------------------------------------------------------

--Products Form
-- load cboProducts
Create proc usp_SSelectProducts
As
Select ProductID, ProductName
From Products
Order by ProductName

--load cboCategories
Create proc usp_SSelectCategoriesFK
As
Select CategoryID, CategoryName
From Categories
Order by CategoryName

--load cboSuppliers
Create proc usp_SSelectSuppliersFK
As
Select SupplierID, CompanyName
From tblSuppliers
Order by CompanyName



--Load details from selection of cbo
Create proc [dbo].[usp_SSelectProductById]
@ProductId int
As
Select [ProductName], [CompanyName], [CategoryName], [QuantityPerUnit],
	[UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued]
from [dbo].[tblSuppliers] inner join [dbo].[Products]
on tblSuppliers.[SupplierID] = Products.SupplierID
inner join Categories
on Products.CategoryID = Categories.CategoryID
Where ProductID = @ProductId

--Insert Product
Create proc usp_SInsertProduct
@ProductName nvarchar(40),
@SupplierID int,
@CategoryID int,
@QuantityPerUnit nvarchar(20),
@UnitPrice money,
@UnitsInStock smallint,
@UnitsOnOrder smallint,
@ReOrderLevel smallint,
@Discontinued bit
As
Insert into Products
(ProductName,
SupplierID,
CategoryID,
QuantityPerUnit,
UnitPrice,
UnitsInStock,
UnitsOnOrder,
ReorderLevel,
Discontinued)
Values
(@ProductName,
@SupplierID,
@CategoryID,
@QuantityPerUnit,
@UnitPrice,
@UnitsInStock,
@UnitsOnOrder,
@ReorderLevel,
@Discontinued)

--Update Product
Create proc usp_SUpdateProduct
@ProductID int,
@ProductName nvarchar(40),
@SupplierID int,
@CategoryID int,
@QuantityPerUnit nvarchar(20),
@UnitPrice money,
@UnitsInStock smallint,
@UnitsOnOrder smallint,
@ReOrderLevel smallint,
@Discontinued bit
As
Update Products
Set
ProductName = @ProductName,
SupplierID = @SupplierID,
CategoryID = @CategoryID,
QuantityPerUnit = @QuantityPerUnit,
UnitPrice = @UnitPrice,
UnitsInStock = @UnitsInStock,
UnitsOnOrder = @UnitsOnOrder,
ReorderLevel = @ReorderLevel,
Discontinued = @Discontinued
WHere ProductId = @ProductID
---------------------------------------------------------------------
--Customer Form
--fill combo box
Create proc usp_SSelectCustomers
as
Select [CustomerID], [CompanyName]
from
[dbo].[Customers]
--Show by CustomerID
create proc [dbo].[usp_SSelectCustomerDetails] --9
@CustomerID char(5)
As
Select [CompanyName], ContactName, ContactTitle, [Address],
[City], [Region], [PostalCode], [Country], [Phone], [Fax]
from [dbo].[Customers]
Where [CustomerID] = @CustomerID


--Update Customer
create PROCEDURE [dbo].[usp_SUpdateCustomer]
	@CustID				Char(5),
	@CompanyName	VARCHAR(40),
	@ContactName	VARCHAR(40),
	@ContactTitle   VARCHAR(30),
	@Address		VARCHAR(60),
	@City			VARCHAR(15),
	@Region			VARCHAR(25),
	@PostalCode		VARCHAR(10),
	@Country		VARCHAR(15),
	@Phone			VARCHAR(25),
	@Fax			VARCHAR(25)
AS
	UPDATE dbo.Customers
	SET 
		CompanyName		= @CompanyName,
		ContactName		= @ContactName,
		ContactTitle	= @ContactTitle,
		[Address]		= @Address,
		City			= @City,
		Region			= @Region,
		PostalCode		= @PostalCode,
		Country			= @Country,
		Phone			= @Phone,
		Fax				= @Fax
   WHERE 
		CustomerID	= @CustID 
--Insert into Customers
Create proc usp_SInsertCustomer
@CustomerID nchar(5),
@CompanyName nvarchar(40),
@ContactName nvarchar(30),
@ContactTitle nvarchar(30),
@Address nvarchar(60),
@City nvarchar(15),
@Region nvarchar (15),
@PostalCode nvarchar(10),
@Country nvarchar(15),
@Phone nvarchar(24),
@Fax nvarchar(24)
As
Insert into Customers
(CustomerID,
CompanyName,
ContactName,
ContactTitle,
[Address],
City,
Region,
PostalCode,
Country,
Phone,
Fax)
Values
(@CustomerID,
@CompanyName,
@ContactName,
@ContactTitle,
@Address,
@City,
@Region,
@PostalCode,
@Country,
@Phone,
@Fax)
---------------------------------------------------------------------------
--Suppliers Form
--Fill Combo
Create proc usp_SSelectSuppliers
AS
	SELECT 
		SupplierID, 
		CompanyName
	FROM 
		dbo.tblSuppliers
	ORDER BY
		CompanyName
---Fill Form by ID
create proc [dbo].[usp_SSelectSuppliersById]
	@SupplierID int
AS
	SELECT  
		SupplierID,
		CompanyName,
		ContactName,
		ContactTitle,
		[Address],
		City,
		Region,
		PostalCode,
		Country,
		Phone,
		Fax,
		HomePage
	FROM 
		dbo.tblSuppliers
	WHERE
		SupplierID = @SupplierID
---Update Supplier Info 
create proc usp_SUpdateSuppliers
	@SupplierID	INT,
	@CompanyName VARCHAR(40),
	@ContactName VARCHAR(30),
	@ContactItile	VARCHAR(30),
	@Address		VARCHAR(60),
	@City			VARCHAR(15),
	@Region			VARCHAR(15),
	@PostalCode		VARCHAR(15),
	@Country		VARCHAR(15),
	@Phone			VARCHAR(25),
	@Fax			VARCHAR(25),
	@HomePage		VARCHAR(MAX)
AS
	UPDATE dbo.tblSuppliers
	SET 
		CompanyName		= @CompanyName,
		ContactName		= @ContactName,
		ContactTitle	= @ContactItile,
		[Address]		= @Address,
       City				= @City,
	   Region			= @Region,
	   PostalCode		= @PostalCode,
	   Country			= @Country,
	   Phone			= @Phone,
	   Fax				= @Fax,
	   HomePage			= @HomePage
	WHERE 
		SupplierID		= @SupplierID
--Insert Supplier
create proc usp_SInsertSuppliers
	@CompanyName VARCHAR(40),
	@ContactName	VARCHAR(30),
	@ContactTitle	VARCHAR(30),
	@Address		VARCHAR(60),
	@City			VARCHAR(15),
	@Region			VARCHAR(15),
	@PostalCode		VARCHAR(15),
	@Country		VARCHAR(15),
	@Phone			VARCHAR(25),
	@Fax			VARCHAR(25),
	@HomePage		VARCHAR(MAX)
AS
	INSERT INTO tblSuppliers
	VALUES
	(
		@CompanyName,
		@ContactName,
		@ContactTitle,
		@Address,
		@City,
		@Region,
		@PostalCode,
		@Country,
		@Phone,
		@Fax,
		@HomePage
	)
-----------------------------------------------------------------
--Shippers Form
-- Fill cbo
create proc usp_SSelectShippers
AS
	SELECT 
		ShipperID, 
		CompanyName
	FROM 
		dbo.Shippers
	ORDER BY
		CompanyName
--Select Shippers
create proc usp_SSelectShippersById
	@ShipperID int
AS
	SELECT  
		ShipperID, 
		CompanyName,
		Phone
	FROM 
		dbo.Shippers
	WHERE
		ShipperID = @ShipperID
--Update details shipper
create proc SUpdateShippers
	@ShipperID		INT,
	@CompanyName	VARCHAR(50),
	@Phone			NVARCHAR(25)
AS
	UPDATE dbo.Shippers
	SET 
		CompanyName		= @CompanyName,
		Phone			= @Phone
	WHERE 
		ShipperID		= @ShipperID
---Insert Details shipper
create proc SInsertShipper
	@ShipperID		INT,
	@CompanyName	VARCHAR(50),
	@Phone			NVARCHAR(25)
AS
	INSERT INTO Shippers (ShipperID, CompanyName, Phone)
	VALUES
	(
	@ShipperID,
	@CompanyName,
	@Phone
	)
------------------------------------------------------------------------------
---Fill Combo User
create proc SSelectUsers
AS
SELECT 
		Username, 
		PK_UserID
FROM 
		dbo.tblUsers
ORDER BY
		Username
--Fill Combo UserRole
create proc SSelectUserRoles
AS
SELECT 
	UserRole, 
	Pk_UserRoleId
FROM 
	dbo.tblUserRoles
-- Fill in Form User
create proc SSelectUsersById
	@UserID int
AS
	SELECT  
		UserFirstName,
	    UserLastName,
		UserPassword,
		UserRole
	FROM 
			dbo.tblUsers AS U 
		INNER JOIN tblUserRoles AS UR ON U.Fk_UserRoleId = UR.Pk_UserRoleId
	WHERE 
		Pk_UserId = @UserID
--Update User Details
create proc SUpdateUsers
	@PK_UserID	INT,
	@FirstName	VARCHAR(50),
	@LastName	VARCHAR(50),
	@Name		VARCHAR(10),
	@Pass		VARCHAR(8),
	@RoleID		INT
AS
	UPDATE dbo.tblUsers
	SET 
		UserFirstName		= @FirstName,
		UserLastName		= @LastName,
		UserName			= @Name,
		UserPassword		= @Pass,
		Fk_UserRoleId		= @RoleID
   WHERE 
		Pk_UserId			= @PK_UserID