CREATE TABLE [dbo].[Table_1] (
    [ID]     INT           IDENTITY (1, 1) NOT NULL,
    [Ur_d_1] INT           NOT NULL,
    [Ur_d_2] INT           NOT NULL,
    [Ur_d_3] INT           NOT NULL,
    [Ur_d_4] INT           NOT NULL,
    [Ur_d_5] INT           NOT NULL,
    [Ur_d_6] INT           NOT NULL,
    [Ur_d_7] INT           NOT NULL,
    [Login]  VARCHAR (MAX) NOT NULL,
    [Pass]   NVARCHAR (50) NOT NULL,
    [Money]  FLOAT        NOT NULL,
    CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED ([ID] ASC)
);

