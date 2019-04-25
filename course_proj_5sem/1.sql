  --full text search
SELECT * FROM Author 
   WHERE contains (Name,'александр') and contains (F_name,'Пушкин')