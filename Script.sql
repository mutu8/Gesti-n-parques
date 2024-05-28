DROP DATABASE BD_GestionAmbiental
CREATE DATABASE BD_GestionAmbiental
USE BD_GestionAmbiental

ALTER AUTHORIZATION ON DATABASE::BD_GestionAmbiental TO sa
GO

-- Crear una nueva tabla con la columna de identidad
CREATE TABLE Detalles_Localidades (
    ID_Detalle_Localidad INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(MAX) NULL,
    Referencias NVARCHAR(MAX) NULL,
    Urbanizacion NVARCHAR(100) NULL,
    Manzana NVARCHAR(50) NULL,
    Jiron NVARCHAR(100) NULL,
    Direccion NVARCHAR(255) NULL,
    Latitud DECIMAL(10, 8) NULL,
    Longitud DECIMAL(11, 8) NULL,
    url_Localidad NVARCHAR(MAX) NULL
);

-- Crear tabla Localidades
CREATE TABLE Localidades (
    ID_Localidad INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Localidad NVARCHAR(100),
    ID_Detalle_Localidad INT,
    FOREIGN KEY (ID_Detalle_Localidad) REFERENCES Detalles_Localidades(ID_Detalle_Localidad)
);

-- Crear tabla Visitas
CREATE TABLE Visitas (
    ID_Visita INT IDENTITY(1,1) PRIMARY KEY,
    Fecha_Visita DATE,
    Nota NVARCHAR(MAX) NULL,
    Estado BIT, -- Puede ser 0 (Pendiente) o 1 (Completado)
    ID_Localidad INT,
    FOREIGN KEY (ID_Localidad) REFERENCES Localidades(ID_Localidad)
);


SELECT Localidades.ID_Localidad, Detalles_Localidades.*
FROM Localidades
INNER JOIN Detalles_Localidades ON Localidades.ID_Detalle_Localidad = Detalles_Localidades.ID_Detalle_Localidad;

SELECT * FROM Detalles_Localidades
SELECT * FROM Localidades

