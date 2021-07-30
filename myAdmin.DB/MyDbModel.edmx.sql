
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/21/2021 12:43:23
-- Generated from EDMX file: F:\AdminPanel\AdminPanelSln\AdminPanelLte\myAdmin.DB\MyDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SchoolDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tblGallery]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblGallery];
GO
IF OBJECT_ID(N'[dbo].[Teacher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Teacher];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tblGalleries'
CREATE TABLE [dbo].[tblGalleries] (
    [imgID] int IDENTITY(1,1) NOT NULL,
    [ImageTitle] nvarchar(max)  NULL,
    [ImageDesc] nvarchar(max)  NULL
);
GO

-- Creating table 'Teacher'
CREATE TABLE [dbo].[Teacher] (
    [id] int  NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NULL,
    [Department] nvarchar(max)  NULL,
    [role] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [imgID] in table 'tblGalleries'
ALTER TABLE [dbo].[tblGalleries]
ADD CONSTRAINT [PK_tblGalleries]
    PRIMARY KEY CLUSTERED ([imgID] ASC);
GO

-- Creating primary key on [id] in table 'Teacher'
ALTER TABLE [dbo].[Teacher]
ADD CONSTRAINT [PK_Teacher]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------