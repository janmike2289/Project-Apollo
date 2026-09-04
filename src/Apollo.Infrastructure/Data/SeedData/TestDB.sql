USE [TestDB]
GO
/****** Object:  Table [dbo].[CMItem]    Script Date: 9/4/2026 10:27:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMItem](
	[itemid] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[ItemType] [varchar](255) NULL,
	[Status] [varchar](20) NULL,
	[Priority] [varchar](20) NULL,
	[Publish] [varchar](10) NULL,
	[RequestedBy] [varchar](255) NULL,
	[Module] [varchar](50) NULL,
	[TargetDate] [datetime] NULL,
	[Description] [varchar](max) NULL,
	[PublishStatement] [varchar](max) NULL,
	[Objects] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](255) NULL,
	[CreatedOn] [datetime] NULL,
	[ChangedBy] [nvarchar](255) NULL,
	[ChangedOn] [datetime] NULL,
	[AssignedTo] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[itemid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CMItem] ON 
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (1, N'Apollo login redesign', N'Requirement', N'New', N'High', N'No', N'alice.morgan', N'Authentication', CAST(N'2026-09-15T00:00:00.000' AS DateTime), N'Refresh the login experience and improve validation feedback.', N'Pending review', N'Login page, validation rules', N'alice.morgan', CAST(N'2026-09-04T09:00:00.000' AS DateTime), N'alice.morgan', CAST(N'2026-09-04T09:00:00.000' AS DateTime), N'dev-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (2, N'Export requirements to CSV', N'Feature', N'In Progress', N'Medium', N'No', N'ben.carter', N'Requirements', CAST(N'2026-09-22T00:00:00.000' AS DateTime), N'Allow users to export the filtered requirements list as CSV.', N'Draft', N'CSV export, filters', N'ben.carter', CAST(N'2026-09-04T09:05:00.000' AS DateTime), N'ben.carter', CAST(N'2026-09-04T09:05:00.000' AS DateTime), N'platform-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (3, N'Dashboard response time', N'Improvement', N'Approved', N'High', N'Yes', N'chloe.nguyen', N'Dashboard', CAST(N'2026-09-12T00:00:00.000' AS DateTime), N'Reduce dashboard initial load time below two seconds.', N'Approved for publication', N'Dashboard queries, caching', N'chloe.nguyen', CAST(N'2026-09-04T09:10:00.000' AS DateTime), N'chloe.nguyen', CAST(N'2026-09-04T09:10:00.000' AS DateTime), N'performance-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (4, N'Add requirement ownership', N'Feature', N'New', N'Medium', N'No', N'daniel.ross', N'Requirements', CAST(N'2026-09-29T00:00:00.000' AS DateTime), N'Add an explicit owner field to each requirement.', N'Pending triage', N'Owner field, assignment workflow', N'daniel.ross', CAST(N'2026-09-04T09:15:00.000' AS DateTime), N'daniel.ross', CAST(N'2026-09-04T09:15:00.000' AS DateTime), N'dev-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (5, N'Audit change history', N'Requirement', N'In Review', N'High', N'No', N'emma.wright', N'Change Management', CAST(N'2026-10-03T00:00:00.000' AS DateTime), N'Capture and display a readable history of requirement changes.', N'Awaiting approval', N'Audit log, history panel', N'emma.wright', CAST(N'2026-09-04T09:20:00.000' AS DateTime), N'emma.wright', CAST(N'2026-09-04T09:20:00.000' AS DateTime), N'compliance-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (6, N'Mobile sidebar layout', N'Bug', N'In Progress', N'Low', N'No', N'farid.hassan', N'Client', CAST(N'2026-09-18T00:00:00.000' AS DateTime), N'Correct sidebar overflow on narrow viewport widths.', N'Fix scheduled', N'Responsive layout, navigation', N'farid.hassan', CAST(N'2026-09-04T09:25:00.000' AS DateTime), N'farid.hassan', CAST(N'2026-09-04T09:25:00.000' AS DateTime), N'client-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (7, N'Requirement search filters', N'Feature', N'New', N'Medium', N'No', N'grace.lee', N'Search', CAST(N'2026-10-10T00:00:00.000' AS DateTime), N'Provide filters for status, priority, module, and assignee.', N'Pending triage', N'Search filters, query parameters', N'grace.lee', CAST(N'2026-09-04T09:30:00.000' AS DateTime), N'grace.lee', CAST(N'2026-09-04T09:30:00.000' AS DateTime), N'platform-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (8, N'Notification preferences', N'Improvement', N'Approved', N'Low', N'Yes', N'henry.kim', N'Notifications', CAST(N'2026-10-17T00:00:00.000' AS DateTime), N'Let users choose which requirement events generate notifications.', N'Ready to publish', N'Email, in-app notifications', N'henry.kim', CAST(N'2026-09-04T09:35:00.000' AS DateTime), N'henry.kim', CAST(N'2026-09-04T09:35:00.000' AS DateTime), N'dev-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (9, N'Bulk status update', N'Feature', N'New', N'High', N'No', N'isla.patel', N'Requirements', CAST(N'2026-10-24T00:00:00.000' AS DateTime), N'Support updating the status of multiple requirements in one action.', N'Draft', N'Bulk actions, status workflow', N'isla.patel', CAST(N'2026-09-04T09:40:00.000' AS DateTime), N'isla.patel', CAST(N'2026-09-04T09:40:00.000' AS DateTime), N'dev-team')
GO
INSERT [dbo].[CMItem] ([itemid], [Name], [ItemType], [Status], [Priority], [Publish], [RequestedBy], [Module], [TargetDate], [Description], [PublishStatement], [Objects], [CreatedBy], [CreatedOn], [ChangedBy], [ChangedOn], [AssignedTo]) VALUES (10, N'Archive completed items', N'Requirement', N'In Review', N'Low', N'No', N'jon.bell', N'Change Management', CAST(N'2026-10-31T00:00:00.000' AS DateTime), N'Define archive rules for completed and obsolete change items.', N'Awaiting approval', N'Archive rules, retention', N'jon.bell', CAST(N'2026-09-04T09:45:00.000' AS DateTime), N'jon.bell', CAST(N'2026-09-04T09:45:00.000' AS DateTime), N'compliance-team')
GO
SET IDENTITY_INSERT [dbo].[CMItem] OFF
GO
