CREATE TABLE Operation --Operacion
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     OperationPlotId INT NOT NULL  ,
     UserId INT NOT NULL  ,
     UserEmail VARCHAR(155) NOT NULL  ,
     RouteFrom VARCHAR(15) NOT NULL  ,
     RouteTo VARCHAR(15) NOT NULL  ,
     Pax INT NOT NULL  ,
     OAT INT NULL  ,
     Pbarometrica VARCHAR(255) NULL  ,
     Personal INT NULL  ,
     Fuel VARCHAR(255) NULL  ,
     TOW VARCHAR(255) NULL  ,
     CreationDate DATETIME NOT NULL  DEFAULT (GETDATE()) ,
     CONSTRAINT PK_Operation PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY]
)
GO

CREATE TABLE EngineEvent --Motores eventos
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     ForEvent INT NOT NULL  ,-- 1 encendido, 2 despegue, 3 climb, 4 crucero, 5 descenso
     EventName VARCHAR(25) NOT NULL  , -- encendido, despegue, climb, crucero, descenso
     CONSTRAINT PK_Engine PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY]
)
GO

CREATE TABLE EngineDetail --Motores eventos detalles
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     OperationId INT NOT NULL  ,
     EngineEventId INT NULL  ,
     EngineNro INT NULL  ,
     N1 DECIMAL NULL  ,
     EGT DECIMAL NULL  ,
     N2 DECIMAL NULL  ,
     FF DECIMAL NULL  ,
     Vib VARCHAR(255) NULL  ,
     EngineBleed BIT NULL  ,
     InletAI BIT NULL  ,
     OilPressure DECIMAL NULL  ,
     OilTemp DECIMAL NULL  ,
     CONSTRAINT PK_EngineDetail PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY],
     FOREIGN KEY (OperationId) REFERENCES [dbo].[Operation](Id),
     FOREIGN KEY (EngineEventId) REFERENCES [dbo].[EngineEvent](Id)
)
GO

CREATE TABLE DetailsInfo --despegue, climb, crucero , descenso
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     TAT DECIMAL NULL  ,-- 1, 2
     Mach DECIMAL NULL  ,
     PAltitude DECIMAL NULL  ,
     PackValve1 VARCHAR(255) NULL  ,
     PackValve2 VARCHAR(255) NULL  ,
     ConfATon BIT NULL  ,
     ConfAToff BIT NULL  ,
     IsolationValve VARCHAR(255) NULL  ,
     WingAI BIT NULL  ,
     ReducedPower BIT NULL  ,
     RegularPower BIT NULL  ,
     CONSTRAINT PK_DetailsInfo PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY]
)
GO

CREATE TABLE SwitchedOn --Encendido
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     OperationId INT NOT NULL  ,
     EngineEventId INT NOT NULL  ,
     CONSTRAINT PK_SwitchedOn PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY],
     FOREIGN KEY (OperationId) REFERENCES [dbo].[Operation](Id),
     FOREIGN KEY (EngineEventId) REFERENCES [dbo].[EngineEvent](Id)
)
GO

CREATE TABLE Takeoff --Despegue
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     OperationId INT NOT NULL  ,
     EngineEventId INT NOT NULL  ,
     DetailsInfoId INT NOT NULL  ,
     CONSTRAINT PK_Takeoff PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY],
     FOREIGN KEY (OperationId) REFERENCES [dbo].[Operation](Id),
     FOREIGN KEY (EngineEventId) REFERENCES [dbo].[EngineEvent](Id),
     FOREIGN KEY (DetailsInfoId) REFERENCES [dbo].[DetailsInfo](Id)
)
GO

CREATE TABLE Climb --Climb
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     OperationId INT NOT NULL  ,
     EngineEventId INT NOT NULL  ,
     DetailsInfoId INT NOT NULL  ,
     CONSTRAINT PK_Climb PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY],
     FOREIGN KEY (OperationId) REFERENCES [dbo].[Operation](Id),
     FOREIGN KEY (EngineEventId) REFERENCES [dbo].[EngineEvent](Id),
     FOREIGN KEY (DetailsInfoId) REFERENCES [dbo].[DetailsInfo](Id)
)
GO

CREATE TABLE Cruise --Crucero
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     OperationId INT NOT NULL  ,
     EngineEventId INT NOT NULL  ,
     DetailsInfoId INT NOT NULL  ,
     CONSTRAINT PK_Cruise PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY],
     FOREIGN KEY (OperationId) REFERENCES [dbo].[Operation](Id),
     FOREIGN KEY (EngineEventId) REFERENCES [dbo].[EngineEvent](Id),
     FOREIGN KEY (DetailsInfoId) REFERENCES [dbo].[DetailsInfo](Id)
)
GO

CREATE TABLE Decline --Descenso
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     OperationId INT NOT NULL  ,
     EngineEventId INT NOT NULL  ,
     DetailsInfoId INT NOT NULL  ,
     CONSTRAINT PK_Decline PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY],
     FOREIGN KEY (OperationId) REFERENCES [dbo].[Operation](Id),
     FOREIGN KEY (EngineEventId) REFERENCES [dbo].[EngineEvent](Id),
     FOREIGN KEY (DetailsInfoId) REFERENCES [dbo].[DetailsInfo](Id)
)
GO

CREATE TABLE Crew --Tripulacion
(
     Id INT IDENTITY(1,1) NOT NULL  ,
     OperationId INT NOT NULL  ,
     Captain VARCHAR(255) NOT NULL  ,
     FO VARCHAR(255) NOT NULL  ,
     Technical VARCHAR(255) NULL  ,
     CONSTRAINT PK_Crew PRIMARY KEY CLUSTERED (Id ASC) ON [PRIMARY],
     FOREIGN KEY (OperationId) REFERENCES [dbo].[Operation](Id)
)
GO

--INSERTS
INSERT INTO EngineEvent(ForEvent, EventName)
    VALUES
        (1,'SwitchedOn'),
        (2,'TakeOff'),
        (3,'Climb'),
        (4,'Cruise'),
        (5,'Decline')