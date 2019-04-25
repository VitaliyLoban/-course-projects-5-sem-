CREATE PROCEDURE [dbo].[insert_customer]
    @name nvarchar(50),
     @f_name nvarchar(50),
	 @login nvarchar(50),
	 @phone_num nvarchar(50),
	 @password nvarchar(50)
AS
    INSERT INTO Customer(Name,F_name,login,phone_num,password)
    VALUES (@name, @f_name,@login,@phone_num,@password);
  
    SELECT SCOPE_IDENTITY()
GO
-----
create function IsExists (@name nvarchar(20))
returns int
as begin 
Declare @a int =0;
if  exists (select Customer.name from Customer where name=@name) set @a=1;  
	else set @a=0;
	return @a;
end;