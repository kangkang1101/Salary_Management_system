/*
 Navicat Premium Data Transfer

 Source Server         : WZ_SQL
 Source Server Type    : SQL Server
 Source Server Version : 15002000
 Source Catalog        : gongzi
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000
 File Encoding         : 65001

 Date: 24/06/2021 14:57:07
*/


-- ----------------------------
-- Table structure for mingxi
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[mingxi]') AND type IN ('U'))
	DROP TABLE [dbo].[mingxi]
GO

CREATE TABLE [dbo].[mingxi] (
  [sid] int  NOT NULL,
  [oday] int  NOT NULL,
  [aday] int  NOT NULL,
  [gold] money  NOT NULL,
  [tax] float(53)  NOT NULL,
  [min] money  NOT NULL,
  [jiang] float(53)  NOT NULL,
  [fa] float(53)  NOT NULL,
  [shouldsalary] money  NOT NULL,
  [realsalary] money  NOT NULL,
  [time] date  NOT NULL,
  [mid] int  IDENTITY(1,1) NOT NULL
)
GO

ALTER TABLE [dbo].[mingxi] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for mingxi
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[mingxi]', RESEED, 2)
GO


-- ----------------------------
-- Primary Key structure for table mingxi
-- ----------------------------
ALTER TABLE [dbo].[mingxi] ADD CONSTRAINT [PK__mingxi__DF5032ECCC4A9AFA] PRIMARY KEY CLUSTERED ([mid])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

