EXEC master.dbo.sp_configure 'show advanced options', 1;							--allow= instance tuning 
RECONFIGURE;
EXEC master.dbo.sp_configure 'xp_cmdshell', 1;										--start xp_cmdshell for bcp operates
RECONFIGURE;

----------------------------------------Write Books+Authors to XML-----------------------

alter PROCEDURE [dbo].[export_book_from_order_ToXML] 
	@path nvarchar(256)												
AS 
begin	
		declare @sql varchar(1000);
			select @sql = 'bcp "SELECT [id_book], [id_order], [count] FROM  course_proj.dbo.[book-order] FOR XML PATH(''Order''), ROOT(''Book'')" queryout "'+@path+'"  -r  -C1251 -w -T -S DESKTOP-KHIRI9G\SQLEXPRESS -d Course_proj';    
		EXEC xp_cmdshell @sql;	
end;
GO

exec [dbo].[export_book_from_order_ToXML] @path = 'D:\xml\bokk_order.xml'

select * from course_proj.dbo.[book-order] 