create table tblImages
(
	iamgeName nvarchar(255) CONSTRAINT pk_tblImages primary key NOT NULL,
	postedTime DATE NOT NULL,
	ImageData varbinary NOT NULL
);


create proc procUploadImage @iamgeName nvarchar(255), @size int, @ImageData varbinary, @newID int output
as
Begin
	Insert into tblImages 
	values(@iamgeName, @size, @ImageData)
	
	select @newID = SCOPE_IDEMTITY()
End
