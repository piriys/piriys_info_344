
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/14/2012 23:29:56
-- Generated from EDMX file: C:\dev\tfs\thundercats\ViewSpark\ViewSparkProject\ViewSpark.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ViewSpark];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Chat_Presentation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK_Chat_Presentation];
GO
IF OBJECT_ID(N'[dbo].[FK_Chat_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chat] DROP CONSTRAINT [FK_Chat_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupMembers_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupMembers] DROP CONSTRAINT [FK_GroupMembers_0];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupMembers_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupMembers] DROP CONSTRAINT [FK_GroupMembers_1];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupPresentationShares_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupPresentationShares] DROP CONSTRAINT [FK_GroupPresentationShares_0];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupPresentationShares_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupPresentationShares] DROP CONSTRAINT [FK_GroupPresentationShares_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Groups_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Groups] DROP CONSTRAINT [FK_Groups_0];
GO
IF OBJECT_ID(N'[dbo].[FK_Images_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Images] DROP CONSTRAINT [FK_Images_0];
GO
IF OBJECT_ID(N'[dbo].[FK_Images_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Images] DROP CONSTRAINT [FK_Images_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Presentations_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Presentations] DROP CONSTRAINT [FK_Presentations_0];
GO
IF OBJECT_ID(N'[dbo].[FK_Presentations_2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Presentations] DROP CONSTRAINT [FK_Presentations_2];
GO
IF OBJECT_ID(N'[dbo].[FK_PresentationSlides_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PresentationSlides] DROP CONSTRAINT [FK_PresentationSlides_0];
GO
IF OBJECT_ID(N'[dbo].[FK_PresentationSlides_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PresentationSlides] DROP CONSTRAINT [FK_PresentationSlides_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Slides_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Slides] DROP CONSTRAINT [FK_Slides_0];
GO
IF OBJECT_ID(N'[dbo].[FK_Slides_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Slides] DROP CONSTRAINT [FK_Slides_1];
GO
IF OBJECT_ID(N'[dbo].[FK_Slides_Layout]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Slides] DROP CONSTRAINT [FK_Slides_Layout];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPresentationShares_0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserPresentationShares] DROP CONSTRAINT [FK_UserPresentationShares_0];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPresentationShares_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserPresentationShares] DROP CONSTRAINT [FK_UserPresentationShares_1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Chat]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chat];
GO
IF OBJECT_ID(N'[dbo].[GroupMembers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupMembers];
GO
IF OBJECT_ID(N'[dbo].[GroupPresentationShares]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupPresentationShares];
GO
IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[Images]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Images];
GO
IF OBJECT_ID(N'[dbo].[Presentations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Presentations];
GO
IF OBJECT_ID(N'[dbo].[PresentationSlides]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PresentationSlides];
GO
IF OBJECT_ID(N'[dbo].[SlideLayouts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SlideLayouts];
GO
IF OBJECT_ID(N'[dbo].[Slides]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Slides];
GO
IF OBJECT_ID(N'[dbo].[UserPresentationShares]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserPresentationShares];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'GroupMembers'
CREATE TABLE [dbo].[GroupMembers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [GroupID] int  NOT NULL
);
GO

-- Creating table 'GroupPresentationShares'
CREATE TABLE [dbo].[GroupPresentationShares] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GroupID] int  NOT NULL,
    [PresentationID] int  NOT NULL,
    [Permissions] tinyint  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Description] nvarchar(255)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Url] nvarchar(255)  NOT NULL,
    [Data] varbinary(max)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [ModificationDate] datetime  NULL,
    [ModifiedBy] int  NULL,
    [MimeType] nvarchar(64)  NOT NULL
);
GO

-- Creating table 'Presentations'
CREATE TABLE [dbo].[Presentations] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NULL,
    [CreationDate] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [ModificationDate] datetime  NOT NULL,
    [ModifiedBy] int  NULL,
    [PresentationDate] datetime  NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'PresentationSlides'
CREATE TABLE [dbo].[PresentationSlides] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PresentationID] int  NOT NULL,
    [SlideID] int  NOT NULL,
    [OrderNum] int  NOT NULL,
    [PresentationDate] datetime  NULL
);
GO

-- Creating table 'Slides'
CREATE TABLE [dbo].[Slides] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Content] nvarchar(4000)  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [ModificationDate] datetime  NOT NULL,
    [ModifiedBy] int  NULL,
    [PresentationDate] datetime  NULL,
    [Layout] int  NULL
);
GO

-- Creating table 'UserPresentationShares'
CREATE TABLE [dbo].[UserPresentationShares] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [PresentationID] int  NOT NULL,
    [Permissions] tinyint  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(32)  NOT NULL,
    [Authorizer] nvarchar(255)  NOT NULL,
    [AuthorizationID] nvarchar(255)  NOT NULL,
    [RegistrationDate] datetime  NOT NULL,
    [LastLoginDate] datetime  NULL,
    [Email] nvarchar(127)  NOT NULL
);
GO

-- Creating table 'SlideLayouts'
CREATE TABLE [dbo].[SlideLayouts] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Data] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Chats'
CREATE TABLE [dbo].[Chats] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PresentationID] int  NOT NULL,
    [UserID] int  NOT NULL,
    [Text] nvarchar(2048)  NOT NULL,
    [Timestamp] int  NOT NULL
);
GO

-- Creating table 'Polls'
CREATE TABLE [dbo].[Polls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Choice1] nvarchar(max)  NOT NULL,
    [Choice2] nvarchar(max)  NOT NULL,
    [Choice3] nvarchar(max)  NOT NULL,
    [Choice4] nvarchar(max)  NOT NULL,
    [Choice5] nvarchar(max)  NOT NULL,
    [CreatedBy] int  NOT NULL,
    [User_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'GroupMembers'
ALTER TABLE [dbo].[GroupMembers]
ADD CONSTRAINT [PK_GroupMembers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GroupPresentationShares'
ALTER TABLE [dbo].[GroupPresentationShares]
ADD CONSTRAINT [PK_GroupPresentationShares]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Presentations'
ALTER TABLE [dbo].[Presentations]
ADD CONSTRAINT [PK_Presentations]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PresentationSlides'
ALTER TABLE [dbo].[PresentationSlides]
ADD CONSTRAINT [PK_PresentationSlides]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Slides'
ALTER TABLE [dbo].[Slides]
ADD CONSTRAINT [PK_Slides]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserPresentationShares'
ALTER TABLE [dbo].[UserPresentationShares]
ADD CONSTRAINT [PK_UserPresentationShares]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SlideLayouts'
ALTER TABLE [dbo].[SlideLayouts]
ADD CONSTRAINT [PK_SlideLayouts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [PK_Chats]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'Polls'
ALTER TABLE [dbo].[Polls]
ADD CONSTRAINT [PK_Polls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserID] in table 'GroupMembers'
ALTER TABLE [dbo].[GroupMembers]
ADD CONSTRAINT [FK_GroupMembers_0]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupMembers_0'
CREATE INDEX [IX_FK_GroupMembers_0]
ON [dbo].[GroupMembers]
    ([UserID]);
GO

-- Creating foreign key on [GroupID] in table 'GroupMembers'
ALTER TABLE [dbo].[GroupMembers]
ADD CONSTRAINT [FK_GroupMembers_1]
    FOREIGN KEY ([GroupID])
    REFERENCES [dbo].[Groups]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupMembers_1'
CREATE INDEX [IX_FK_GroupMembers_1]
ON [dbo].[GroupMembers]
    ([GroupID]);
GO

-- Creating foreign key on [GroupID] in table 'GroupPresentationShares'
ALTER TABLE [dbo].[GroupPresentationShares]
ADD CONSTRAINT [FK_GroupPresentationShares_0]
    FOREIGN KEY ([GroupID])
    REFERENCES [dbo].[Groups]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupPresentationShares_0'
CREATE INDEX [IX_FK_GroupPresentationShares_0]
ON [dbo].[GroupPresentationShares]
    ([GroupID]);
GO

-- Creating foreign key on [PresentationID] in table 'GroupPresentationShares'
ALTER TABLE [dbo].[GroupPresentationShares]
ADD CONSTRAINT [FK_GroupPresentationShares_1]
    FOREIGN KEY ([PresentationID])
    REFERENCES [dbo].[Presentations]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupPresentationShares_1'
CREATE INDEX [IX_FK_GroupPresentationShares_1]
ON [dbo].[GroupPresentationShares]
    ([PresentationID]);
GO

-- Creating foreign key on [CreatedBy] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [FK_Groups_0]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Groups_0'
CREATE INDEX [IX_FK_Groups_0]
ON [dbo].[Groups]
    ([CreatedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_Images_0]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Images_0'
CREATE INDEX [IX_FK_Images_0]
ON [dbo].[Images]
    ([CreatedBy]);
GO

-- Creating foreign key on [ModifiedBy] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_Images_1]
    FOREIGN KEY ([ModifiedBy])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Images_1'
CREATE INDEX [IX_FK_Images_1]
ON [dbo].[Images]
    ([ModifiedBy]);
GO

-- Creating foreign key on [CreatedBy] in table 'Presentations'
ALTER TABLE [dbo].[Presentations]
ADD CONSTRAINT [FK_Presentations_0]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Presentations_0'
CREATE INDEX [IX_FK_Presentations_0]
ON [dbo].[Presentations]
    ([CreatedBy]);
GO

-- Creating foreign key on [ModifiedBy] in table 'Presentations'
ALTER TABLE [dbo].[Presentations]
ADD CONSTRAINT [FK_Presentations_2]
    FOREIGN KEY ([ModifiedBy])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Presentations_2'
CREATE INDEX [IX_FK_Presentations_2]
ON [dbo].[Presentations]
    ([ModifiedBy]);
GO

-- Creating foreign key on [PresentationID] in table 'PresentationSlides'
ALTER TABLE [dbo].[PresentationSlides]
ADD CONSTRAINT [FK_PresentationSlides_0]
    FOREIGN KEY ([PresentationID])
    REFERENCES [dbo].[Presentations]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PresentationSlides_0'
CREATE INDEX [IX_FK_PresentationSlides_0]
ON [dbo].[PresentationSlides]
    ([PresentationID]);
GO

-- Creating foreign key on [PresentationID] in table 'UserPresentationShares'
ALTER TABLE [dbo].[UserPresentationShares]
ADD CONSTRAINT [FK_UserPresentationShares_1]
    FOREIGN KEY ([PresentationID])
    REFERENCES [dbo].[Presentations]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPresentationShares_1'
CREATE INDEX [IX_FK_UserPresentationShares_1]
ON [dbo].[UserPresentationShares]
    ([PresentationID]);
GO

-- Creating foreign key on [SlideID] in table 'PresentationSlides'
ALTER TABLE [dbo].[PresentationSlides]
ADD CONSTRAINT [FK_PresentationSlides_1]
    FOREIGN KEY ([SlideID])
    REFERENCES [dbo].[Slides]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PresentationSlides_1'
CREATE INDEX [IX_FK_PresentationSlides_1]
ON [dbo].[PresentationSlides]
    ([SlideID]);
GO

-- Creating foreign key on [CreatedBy] in table 'Slides'
ALTER TABLE [dbo].[Slides]
ADD CONSTRAINT [FK_Slides_0]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Slides_0'
CREATE INDEX [IX_FK_Slides_0]
ON [dbo].[Slides]
    ([CreatedBy]);
GO

-- Creating foreign key on [ModifiedBy] in table 'Slides'
ALTER TABLE [dbo].[Slides]
ADD CONSTRAINT [FK_Slides_1]
    FOREIGN KEY ([ModifiedBy])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Slides_1'
CREATE INDEX [IX_FK_Slides_1]
ON [dbo].[Slides]
    ([ModifiedBy]);
GO

-- Creating foreign key on [UserID] in table 'UserPresentationShares'
ALTER TABLE [dbo].[UserPresentationShares]
ADD CONSTRAINT [FK_UserPresentationShares_0]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPresentationShares_0'
CREATE INDEX [IX_FK_UserPresentationShares_0]
ON [dbo].[UserPresentationShares]
    ([UserID]);
GO

-- Creating foreign key on [Layout] in table 'Slides'
ALTER TABLE [dbo].[Slides]
ADD CONSTRAINT [FK_Slides_Layout]
    FOREIGN KEY ([Layout])
    REFERENCES [dbo].[SlideLayouts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Slides_Layout'
CREATE INDEX [IX_FK_Slides_Layout]
ON [dbo].[Slides]
    ([Layout]);
GO

-- Creating foreign key on [PresentationID] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK_Chat_Presentation]
    FOREIGN KEY ([PresentationID])
    REFERENCES [dbo].[Presentations]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Chat_Presentation'
CREATE INDEX [IX_FK_Chat_Presentation]
ON [dbo].[Chats]
    ([PresentationID]);
GO

-- Creating foreign key on [UserID] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK_Chat_Users]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Chat_Users'
CREATE INDEX [IX_FK_Chat_Users]
ON [dbo].[Chats]
    ([UserID]);
GO

-- Creating foreign key on [User_ID] in table 'Polls'
ALTER TABLE [dbo].[Polls]
ADD CONSTRAINT [FK_UserPoll]
    FOREIGN KEY ([User_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPoll'
CREATE INDEX [IX_FK_UserPoll]
ON [dbo].[Polls]
    ([User_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------