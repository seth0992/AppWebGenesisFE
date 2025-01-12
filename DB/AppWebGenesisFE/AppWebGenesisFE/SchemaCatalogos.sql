CREATE DATABASE dbGenesisFEWebApp;
GO

USE dbGenesisFEWebApp;
GO

-- Catalogos de datos

--Crear un schema para los datos de tipo catalogo
CREATE SCHEMA Catalog;
GO


CREATE TABLE Catalog.IdentificationTypes(
    ID NVARCHAR(3) PRIMARY KEY, 
    Description NVARCHAR(255) NOT NULL -- Nombre del tipo de identificación
);

CREATE TABLE Catalog.Region (
    RegionID INT IDENTITY(1,1) PRIMARY KEY,
    RegionName VARCHAR(150) NOT NULL,
    CONSTRAINT UQ_NombreRegion UNIQUE (RegionName)
);

CREATE TABLE Catalog.Provinces(
    ProvinceID INT PRIMARY KEY, -- Llave primaria con autoincremento
    ProvinceName NVARCHAR(255) NOT NULL UNIQUE -- Nombre de la provincia
);

CREATE TABLE Catalog.Cantons(
    CantonID INT PRIMARY KEY, -- Llave primaria con autoincremento
    CantonName NVARCHAR(255) NOT NULL, -- Nombre del cantón
    ProvinceId INT , -- Provincia
    FOREIGN KEY (ProvinceId) REFERENCES Catalog.Provinces(ProvinceID),
	CONSTRAINT UQ_Canton_Provincia UNIQUE (ProvinceId, CantonName)
);

CREATE TABLE Catalog.Districts(
    DistrictID INT PRIMARY KEY, -- Llave primaria con autoincremento
    DistrictName NVARCHAR(255) NOT NULL, -- Nombre del distrito
    CantonId INT , -- Cantón
	RegionID INT 
    FOREIGN KEY (CantonId) REFERENCES Catalog.Cantons(CantonID),
	FOREIGN KEY (RegionID) REFERENCES  Catalog.Region(RegionID)
);
