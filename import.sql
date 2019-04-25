use Shop

delete from  [book-order];

DBCC CHECKIDENT ('[Shop].[Products]', RESEED, 0);


CREATE PROCEDURE [dbo].[bopk_ord_fromXml] 
	@path nvarchar(256)
AS 
begin	
	BEGIN TRAN
		declare @results table (x xml)			
		declare @sql nvarchar(300)=
				   'SELECT 
			CAST(REPLACE(CAST(x AS VARCHAR(MAX)), ''encoding="utf-16"'', ''encoding="utf-8"'') AS XML)
			FROM OPENROWSET(BULK '''+@path+''', SINGLE_BLOB) AS T(x)'; 
		INSERT INTO @results EXEC (@sql) 
		declare @xml XML = (SELECT  TOP 1 x from  @results);
		INSERT INTO dbo.[book-order](id_book, id_order, count) 
			SELECT 
			C3.value('id_book[1]', 'int') AS id_book,
			C3.value('id_order[1]', 'int') AS id_order,
			C3.value('count[1]', 'int') AS count
			FROM @xml.nodes('Book/Order') AS T3(C3) 
	COMMIT;
end;
GO

exec dbo.bopk_ord_fromXml @path = 'D:\\xml\bokk_order.xml'


select * from [book-order]