Create Table dbo.CMItem (
	itemid int Identity(1,1) Primary Key,
	[Name] varchar(255),
	ItemType varchar(255),
	[Status] varchar(20),
	[Priority] varchar(20),
	Publish varchar(10),
	RequestedBy varchar(255),
	Module varchar(50),
	TargetDate DateTime,
	[Description] varchar(max),
	PublishStatement varchar(max),
	[Objects] nvarchar(max),
	CreatedBy nvarchar(255),
	CreatedOn datetime,
	ChangedBy nvarchar(255),
	ChangedOn datetime,
	AssignedTo varchar(255)
)