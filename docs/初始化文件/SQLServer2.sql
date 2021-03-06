USE [AntDesign]
GO
/****** Object:  Table [dbo].[F_Evaluation]    Script Date: 2020/5/11 10:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [F_Evaluation]
CREATE TABLE [dbo].[F_Evaluation](
	[Id] [nvarchar](50) NOT NULL,
	[UserInfoId] [nvarchar](50) NULL,
	[FoodInfoId] [nvarchar](50) NULL,
	[Score] [int] NULL,
	[FoodContent] [nvarchar](500) NULL,
	[Remark] [nvarchar](500) NULL,
	[CreatorId] [nvarchar](50) NOT NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[UpdateName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_F_Evaluation_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[F_FoodInfo]    Script Date: 2020/5/11 10:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [F_FoodInfo]
CREATE TABLE [dbo].[F_FoodInfo](
	[Id] [nvarchar](50) NOT NULL,
	[ShopInfoId] [nvarchar](50) NULL,
	[SupplierName] [nvarchar](50) NULL,
	[FoodName] [nvarchar](50) NULL,
	[FoodDesc] [nvarchar](500) NULL,
	[Score] [int] NULL,
	[EvaluatorsNumber] [int] NULL,
	[Limit] [int] NULL,
	[Price] [numeric](18, 2) NULL,
	[ImgUrl] [nvarchar](500) NULL,
	[CreatorId] [nvarchar](50) NOT NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[UpdateName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_F_FoodInfo_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[F_Order]    Script Date: 2020/5/11 10:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [F_Order]
CREATE TABLE [dbo].[F_Order](
	[Id] [nvarchar](50) NOT NULL,
	[OrderCode] [nvarchar](50) NULL,
	[UserInfoId] [nvarchar](50) NULL,
	[OrderCount] [int] NULL,
	[Status] [int] NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[CreatorId] [nvarchar](50) NOT NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[UpdateName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_F_Order_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[F_OrderInfo]    Script Date: 2020/5/11 10:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [F_OrderInfo]
CREATE TABLE [dbo].[F_OrderInfo](
	[Id] [nvarchar](50) NOT NULL,
	[OrderCode] [nvarchar](50) NULL,
	[PublishFoodId] [nvarchar](50) NULL,
	[OrderInfoQty] [int] NULL,
	[CreatorId] [nvarchar](50) NOT NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[UpdateName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_F_OrderInfo_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[F_PublishFood]    Script Date: 2020/5/11 10:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [F_PublishFood]
CREATE TABLE [dbo].[F_PublishFood](
	[Id] [nvarchar](50) NOT NULL,
	[ShopInfoId] [nvarchar](50) NULL,
	[FoodInfoId] [nvarchar](50) NULL,
	[Price] [decimal](18, 2) NULL,
	[SupplierName] [nvarchar](50) NULL,
	[FoodName] [nvarchar](50) NULL,
	[FoodDesc] [nvarchar](500) NULL,
	[FoodQty] [int] NULL,
	[Limit] [int] NULL,
	[ImgUrl] [nvarchar](500) NULL,
	[PublishDate] [datetime] NULL,
	[CreatorId] [nvarchar](50) NOT NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[UpdateName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_F_PublishFood_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[F_ShopInfo]    Script Date: 2020/5/11 10:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [F_ShopInfo]
CREATE TABLE [dbo].[F_ShopInfo](
	[Id] [nvarchar](50) NOT NULL,
	[ShopName] [nvarchar](50) NOT NULL,
	[ShopDesc] [nvarchar](500) NULL,
	[CreatorId] [nvarchar](50) NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[UpdateName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_F_ShopInfo_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[F_ShopInfoSet]    Script Date: 2020/5/11 10:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [F_ShopInfoSet]
CREATE TABLE [dbo].[F_ShopInfoSet](
	[Id] [nvarchar](50) NOT NULL,
	[ShopInfoId] [nvarchar](50) NULL,
	[UserOrderNum] [int] NULL,
	[OrderFoodTypeNum] [int] NULL,
	[OrderBeginDate] [datetime] NULL,
	[OrderBeginEnd] [datetime] NULL,
	[OrderBeginRemind] [nvarchar](500) NULL,
	[OrderEndRemind] [nvarchar](500) NULL,
	[CreatorId] [nvarchar](50) NOT NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[UpdateName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_F_ShopInfoSet_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[F_UserInfo]    Script Date: 2020/5/11 10:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [F_UserInfo]
CREATE TABLE [dbo].[F_UserInfo](
	[Id] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[IsAdmin] [bit] NULL,
	[ShopInfoId] [nvarchar](50) NULL,
	[Department] [nvarchar](50) NULL,
	[Remark] [nvarchar](200) NULL,
	[WeCharUserId] [nvarchar](50) NULL,
	[UserImgUrl] [nvarchar](500) NULL,
	[CreatorId] [nvarchar](50) NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateId] [nvarchar](50) NULL,
	[UpdateName] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_F_UserInfo_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[F_Evaluation] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[F_FoodInfo] ADD  CONSTRAINT [DF__F_FoodInf__Creat__619B8048]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[F_Order] ADD  CONSTRAINT [DF_F_Order_Status]  DEFAULT ((10)) FOR [Status]
GO
ALTER TABLE [dbo].[F_Order] ADD  CONSTRAINT [DF__F_Order__CreateD__6A30C649]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[F_OrderInfo] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[F_PublishFood] ADD  CONSTRAINT [DF__F_Publish__Creat__6477ECF3]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[F_ShopInfoSet] ADD  CONSTRAINT [DF__F_ShopInf__Creat__5EBF139D]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[F_UserInfo] ADD  CONSTRAINT [DF__F_UserInf__Creat__6754599E]  DEFAULT (getdate()) FOR [CreateTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'UserInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'FoodInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'Score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'FoodContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人编号 当前用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期 默认为当前时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'UpdateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'UpdateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品评价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Evaluation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'ShopInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'SupplierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'FoodName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品描述信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'FoodDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品综合评分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'Score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评价人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'EvaluatorsNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限购数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'Limit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'ImgUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期 默认为当前时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'UpdateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'UpdateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单信息库' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_FoodInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'OrderCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'UserInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'OrderCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人编号 当前用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期 默认为当前时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'UpdateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'UpdateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_Order'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'OrderCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布菜品ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'PublishFoodId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'OrderInfoQty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人编号 当前用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期 默认为当前时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'UpdateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'UpdateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_OrderInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'ShopInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'FoodInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商家名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'SupplierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'FoodName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品描述信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'FoodDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'FoodQty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限购数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'Limit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'ImgUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'PublishDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人编号 当前用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期 默认为当前时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'UpdateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'UpdateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发布菜品' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_PublishFood'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'ShopName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'ShopDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人编号 当前用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'UpdateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'UpdateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'ShopInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单用户当天订单数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'UserOrderNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单订单商品种类数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'OrderFoodTypeNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点餐开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'OrderBeginDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点餐结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'OrderBeginEnd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始点餐提醒信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'OrderBeginRemind'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束点餐提醒信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'OrderEndRemind'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期 默认为当前时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'UpdateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'UpdateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店设置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_ShopInfoSet'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'IsAdmin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'ShopInfoId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'Department'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人编号 当前用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'CreatorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期 默认为当前时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'UpdateId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'UpdateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户信息管理' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'F_UserInfo'
GO
