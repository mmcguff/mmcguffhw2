--This script breaks down the user Table by Conversations Id.  
Declare @t table (id int identity, ConId nvarchar(max));

insert into @t
select distinct ConversationId
from UserLog;

Declare @i int = 1;
while @i <= (select count(*) from @t)
begin

	select *
	from UserLog
	where ConversationId = 
	(	select conId
		from @t
		where id = @i
		
	)

	order by Created;

	set @i += 1;
end 


