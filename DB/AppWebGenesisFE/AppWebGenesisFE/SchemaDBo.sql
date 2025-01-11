CREATE DATABASE dbGenesisFEWebApp;
GO

USE dbGenesisFEWebApp;
GO


-- Tabla para las empresas/tenants
CREATE TABLE [dbo].[Tenants] (
    ID BIGINT IDENTITY PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Identification NVARCHAR(50) NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME
);

INSERT INTO Tenants ( Name, Identification, IsActive) VALUES
('Empresa ABC', '3101123456', 1),
('Comercial XYZ', '3101789012', 1),
('Tech Solutions SA', '3101345678', 1);

-- Tabla para usuarios
CREATE TABLE [dbo].[Users] (
    ID BIGINT IDENTITY PRIMARY KEY,
    TenantId BIGINT NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
	Rol NVARCHAR(100) NOT NULL,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    FOREIGN KEY (TenantId) REFERENCES Tenants(ID)
);

-- Datos de prueba para Users
INSERT INTO Users (TenantId, Email, PasswordHash, Rol, FirstName, LastName, IsActive) VALUES
( 1, 'admin@abc.com', 'AQAAAAIAAYagAAAAELpUWqz5rzxPDhVol3C4pz4tVEI1cxrNHXcYyHxD7GIX8zfDYc48f7I9mCzH5LvS8Q==', 'Admin', 'Juan', 'Pérez', 1),
( 1, 'user@abc.com', 'AQAAAAIAAYagAAAAELpUWqz5rzxPDhVol3C4pz4tVEI1cxrNHXcYyHxD7GIX8zfDYc48f7I9mCzH5LvS8Q==', 'User', 'María', 'López', 1),
(2, 'admin@xyz.com', 'AQAAAAIAAYagAAAAELpUWqz5rzxPDhVol3C4pz4tVEI1cxrNHXcYyHxD7GIX8zfDYc48f7I9mCzH5LvS8Q==', 'Admin', 'Pedro', 'González', 1),
(3, 'admin@tech.com', 'AQAAAAIAAYagAAAAELpUWqz5rzxPDhVol3C4pz4tVEI1cxrNHXcYyHxD7GIX8zfDYc48f7I9mCzH5LvS8Q==', 'Admin', 'Ana', 'Martínez', 1);

--Password123!
select * from Users

--drop table users



-- Tabla para los cliente a facturar.
CREATE TABLE Customers(
    ID BIGINT IDENTITY PRIMARY KEY, -- Llave primaria con autoincremento
    CustomerName NVARCHAR(255) NOT NULL, -- Nombre del cliente
    CommercialName NVARCHAR(255), -- Nombre comercial
    Identification NVARCHAR(255) NOT NULL, -- Documento
    IdentificationTypeId NVARCHAR(3) NOT NULL, -- Tipo de documento
    Address NVARCHAR(255), -- Dirección	
    Email NVARCHAR(255), -- Correo electrónico
    PhoneCode NVARCHAR(10), -- Código de teléfono
    Phone NVARCHAR(20), -- Teléfono
    TenantId BIGINT NOT NULL,
	Neighborhood VARCHAR(250),
	DistrictID int,
	CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME,
    Foreign Key (IdentificationTypeId) REFERENCES Catalog.IdentificationTypes(ID),
	Foreign Key (DistrictID) REFERENCES Catalog.Districts(DistrictID),
	FOREIGN KEY (TenantId) REFERENCES Tenants(ID)
	--ProvinceID INT,
	--CantonID INT,
	--Foreign Key (ProvinceID) REFERENCES Catalog.Provinces(ProvinceID),
	--Foreign Key (CantonID) REFERENCES Catalog.Cantons(CantonID),
);

-- Clientes para Empresa ABC
INSERT INTO Customers (
    CustomerName, CommercialName, Identification, IdentificationTypeId,
    Address, Email, PhoneCode, Phone, TenantId, Neighborhood, DistrictID
) VALUES 
(
    'Juan Mora Porras', 'Tienda Juan', '101230456', '01',
    'Frente al parque', 'juan.mora@email.com', '+506', '88776655', 1,
    'Los Colegios', 11901
),
(
    'María Castro Madriz', 'Boutique María', '401230456', '01',
    'Costado norte de la iglesia', 'maria.castro@email.com', '+506', '87654321', 1,
    'El Centro', 11902
);

-- Clientes para Comercial XYZ
INSERT INTO Customers (
    CustomerName, CommercialName, Identification, IdentificationTypeId,
    Address, Email, PhoneCode, Phone, TenantId, Neighborhood, DistrictID
) VALUES 
(
    'Corporación ABC', 'Corp ABC', '3101123456', '02',
    'Zona Industrial', 'contacto@corpabc.com', '+506', '22334455', 2,
    'Zona Industrial', 11903
),
(
    'Inversiones XYZ', 'XYZ', '3101789012', '02',
    'Centro Comercial Plaza', 'info@xyz.co.cr', '+506', '22998877', 2,
    'Centro', 11904
);

-- Clientes para Tech Solutions SA
INSERT INTO Customers (
    CustomerName, CommercialName, Identification, IdentificationTypeId,
    Address, Email, PhoneCode, Phone, TenantId, Neighborhood, DistrictID
) VALUES 
(
    'Software Solutions CR', 'SoftCR', '3101567890', '02',
    'Torre Empresarial', 'contacto@softcr.com', '+506', '22113344', 3,
    'San Pedro', 11905
),
(
    'Carlos Tecnología', 'TechCarlos', '108890123', '01',
    'Residencial Los Pinos', 'carlos@techcarlos.com', '+506', '88990011', 3,
    'Los Pinos', 11906
);


select * from Catalog.IdentificationTypes

select COUNT(*) from Catalog.Provinces
select COUNT(*) from Catalog.Cantons
select COUNT(*) from Catalog.Districts


select * from Customers


