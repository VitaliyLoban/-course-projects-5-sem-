  --full text search
SELECT * FROM Author 
   WHERE contains (Name,'���������') and contains (F_name,'������')