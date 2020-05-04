INSERT [dbo].[Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1253239499513663488', CAST(N'2020-04-23T16:29:15.000' AS DateTime), N'Admin', 0, NULL, 0, N'点餐管理', NULL, NULL, 0, N'appstore', 2)
GO
INSERT [dbo].[Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1253240822753660928', CAST(N'2020-04-23T16:34:30.890' AS DateTime), N'Admin', 0, N'1253239499513663488', 1, N'门店管理', N'/ServerFood/F_ShopInfo/List', NULL, 1, NULL, 0)
GO
INSERT [dbo].[Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1253243773765292032', CAST(N'2020-04-23T16:46:14.467' AS DateTime), N'Admin', 0, N'1253239499513663488', 1, N'门店设置', N'/ServerFood/F_ShopInfoSet/List', NULL, 1, NULL, 0)
GO
INSERT [dbo].[Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1253244080020787200', CAST(N'2020-04-23T16:47:27.483' AS DateTime), N'Admin', 0, N'1253239499513663488', 1, N'菜品管理', N'/ServerFood/F_FoodInfo/List', NULL, 1, NULL, 0)
GO
INSERT [dbo].[Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1253244222853615616', CAST(N'2020-04-23T16:48:01.000' AS DateTime), N'Admin', 0, N'1253239499513663488', 1, N'已发菜品', N'/ServerFood/F_PublishFood/List', NULL, 1, NULL, 0)
GO
INSERT [dbo].[Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1253244555944267776', CAST(N'2020-04-23T16:49:20.953' AS DateTime), N'Admin', 0, N'1253239499513663488', 1, N'订单管理', N'/ServerFood/F_Order/List', NULL, 1, NULL, 0)
GO
INSERT [dbo].[Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1253244913714204672', CAST(N'2020-04-23T16:50:46.250' AS DateTime), N'Admin', 0, N'1253239499513663488', 1, N'评价管理', N'/ServerFood/F_Evaluation/List', NULL, 1, NULL, 0)
GO
INSERT [dbo].[Base_Action] ([Id], [CreateTime], [CreatorId], [Deleted], [ParentId], [Type], [Name], [Url], [Value], [NeedAction], [Icon], [Sort]) VALUES (N'1253264232959447040', CAST(N'2020-04-23T18:07:32.320' AS DateTime), N'Admin', 0, N'1253239499513663488', 1, N'用户管理', N'/ServerFood/F_UserInfo/List', NULL, 1, NULL, 0)