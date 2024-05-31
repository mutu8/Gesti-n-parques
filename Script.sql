DROP DATABASE BD_GestionAmbiental
CREATE DATABASE BD_GestionAmbiental
USE BD_GestionAmbiental

ALTER AUTHORIZATION ON DATABASE::BD_GestionAmbiental TO sa
GO

CREATE TABLE Detalles_Localidades (
    ID_Detalle_Localidad INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(MAX) NULL,
    Referencias NVARCHAR(MAX) NULL,
    Urbanizacion NVARCHAR(100) NULL,
    Sector NVARCHAR(50) NULL,
    Direccion NVARCHAR(255) NULL,
    Latitud DECIMAL(10, 8) NULL,
    Longitud DECIMAL(11, 8) NULL,
    url_Localidad NVARCHAR(MAX) NULL,
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

CREATE TABLE Empleados (
    ID_Empleado INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    esApoyo BIT NOT NULL -- 1 para Administrador, 0 para Empleado regular
);

CREATE TABLE Urbanizaciones (
    ID_Urbanizacion INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Urbanizacion NVARCHAR(100) NOT NULL
);

CREATE TABLE Sectores (
    ID_Sector INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Sector NVARCHAR(100) NOT NULL
);

INSERT INTO Urbanizaciones (Nombre_Urbanizacion) VALUES
('Centro Histórico'),
('San Andrés'),
('El Recreo'),
('Mansiche'),
('Las Capullanas'),
('Covicorti'),
('Primavera'),
('Huerta Grande'),
('Los Cedros'),
('La Intendencia'),
('Santa María'),
('Las Casuarinas'),
('La Arboleda'),
('Pay Pay'),
('Los Granados'),
('Los Portales'),
('Andrés Rázuri'),
('Los Rosales de San Andrés'),
('Galeno'),
('La Esmeralda'),
('Santo Dominguito'),
('Torres Araujo'),
('Santa Isabel'),
('Monserrate'),
('San Salvador'),
('Trupal'),
('Santa Inés'),
('Las Quintanas'),
('Miraflores'),
('Mochica'),
('Aranjuez'),
('Chicago'),
('Los Pinos'),
('San Eloy'),
('Santa Teresa de Ávila'),
('Chimú'),
('Huerta Bella'),
('Vista Bella'),
('La Noria'),
('UPAO'),
('San Isidro'),
('Libertad'),
('La Merced'),
('La Perla'),
('El Alambre'),
('20 de abril'),
('San Fernando'),
('Los Naranjos'),
('Los Jardines'),
('El Molino'),
('Palermo'),
('El Sol'),
('Vista Hermosa'),
('Ingeniería'),
('Daniel Hoyle'),
('La Rinconada'),
('Jorge Chávez'),
('El Bosque'),
('Independencia'),
('San Luis'),
('San Vicente');

INSERT INTO Sectores (Nombre_Sector) VALUES
('Liberación Social'),
('San Andrés V Etapa'),
('Las Flores'),
('San Andrés - Costado de Paseo de Aguas'),
('California'),
('Huaman'),
('Las Hortenzias'),
('Las Flores - El Golf'),
('Palmeras y Palmas del Golf'),
('San Vicente'),
('Vista Alegre'),
('Golf - Primera Etapa');

SELECT Localidades.ID_Localidad, Detalles_Localidades.*
FROM Localidades
INNER JOIN Detalles_Localidades ON Localidades.ID_Detalle_Localidad = Detalles_Localidades.ID_Detalle_Localidad;

SELECT * FROM Detalles_Localidades
SELECT * FROM Localidades
SELECT * FROM Urbanizaciones
SELECT * FROM Sectores

INSERT INTO Detalles_Localidades (Descripcion, Urbanizacion, Sector, Direccion) VALUES
('Parque', 'Centro Histórico', 'Liberación Social', 'Liberación Social'),
('Parque', 'San Andrés', 'Liberación Social', 'Liberación Social'),
('Parque', 'El Recreo', 'Liberación Social', 'Espalda de la Posta Médica L.S'),
('Parque', 'Mansiche', 'Liberación Social', 'Av. Chota'),
('Parque', 'Las Capullanas', 'Liberación Social', 'Av. Chota'),
('Parque', 'Covicorti', 'Liberación Social', 'Paralela Av. Juan Pablo II y Av. Chota'),
('Parque', 'Primavera', 'Liberación Social', 'Av. Larco Frente del Colegio Alfred Nobel'),
('Parque', 'Huerta Grande', 'Liberación Social', 'Calle Chota'),
('Parque', 'Los Cedros', 'Liberación Social', 'Espalda de Coco Torete Av. Larco'),
('Parque', 'La Intendencia', 'Liberación Social', 'Av. Larco Frente del Colegio Alfred Nobel'),
('Parque', 'Santa María', 'Liberación Social', 'Av. Larco Frente del Colegio Alfred Nobel'),
('Parque', 'Las Casuarinas', 'Liberación Social', 'Espalda de Centro Hogar de la Niña'),
('Parque', 'La Arboleda', 'San Andres V Etapa', 'San Andres V Etapa'),
('Parque', 'Pay Pay', 'San Andres V Etapa', 'Calle las Poncianas - San Andres V Etapa'),
('Parque', 'Los Granados', 'San Andres V Etapa', 'San Andres V Etapa, Paralela Av. Paujiles'),
('Parque', 'Los Portales', 'San Andres V Etapa', 'Av. Huaman'),
('Parque', 'Andrés Rázuri', 'San Andres, Costado de Paseo de Aguas', 'Av. Larco'),
('Parque', 'Los Rosales de San Andrés', 'San Andres, Costado de Paseo de Aguas', 'Av. Huaman'),
('Parque', 'Galeno', 'San Andres, Costado de Paseo de Aguas', 'Calle las Poncianas'),
('Parque', 'La Esmeralda', 'San Andres, Costado de Paseo de Aguas', 'Entre Seoane y Huaman'),
('Parque', 'Santo Dominguito', 'San Andres, Costado de Paseo de Aguas', 'Entre Seoane y Huaman'),
('Parque', 'Torres Araujo', 'San Andres, Costado de Paseo de Aguas', 'Calle Lss Casuarinas, Santa Edelmira'),
('Parque', 'Santa Isabel', 'California', 'Pasaje los Jazmines'),
('Parque', 'Monserrate', 'California', 'Pasaje los Jazmines'),
('Parque', 'San Salvador', 'California', 'Calle Los Tulipanes y Huaman'),
('Parque', 'Trupal', 'California', 'Calle Los Tulipanes y Huaman'),
('Parque', 'Santa Inés', 'Huaman', 'Entre Calle Talco y Frente a la Iglesia'),
('Parque', 'Las Quintanas', 'Huaman', 'Entre Manuel Seoane y Calle Ruben Dario'),
('Parque', 'Miraflores', 'California', 'Calle los Geranios'),
('Parque', 'Mochica', 'California', 'Espalda de Iglesia Fatima'),
('Parque', 'Aranjuez', 'Huaman', 'Calle los Alamos'),
('Parque', 'Chicago', 'Las Hortensias', 'Av. Los Angeles'),
('Parque', 'Los Pinos', 'Las Hortensias', 'Av. Los Angeles'),
('Parque', 'San Eloy', 'Las Hortensias', 'Av. Los Angeles'),
('Parque', 'Santa Teresa de Ávila', 'Praderas del Golf', 'Paralela entre Av. Fátima y Prolongación Vallejo'),
('Parque', 'Chimú', 'Las Flores del Golf', 'Entre Av. Huaman y Prolongación Vallejo'),
('Parque', 'Huerta Bella', 'Las Flores del Golf', 'Entre Av. Huaman y Prolongación Vallejo'),
('Parque', 'Vista Bella', 'Las Flores del Golf', 'Entre Calles Las Begonias y Las Dalias'),
('Parque', 'La Noria', 'Las Flores del Golf', 'Costado de Parque los Cedros'),
('Parque', 'UPAO', 'Las Flores del Golf', 'Espalda de Fatima y Prolongación Vallejo'),
('Parque', 'San Isidro', 'Los Sauces', 'Costado de los Sauces'),
('Parque', 'Libertad', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'La Merced', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'La Perla', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'El Alambre', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', '20 de abril', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'San Fernando', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'Los Naranjos', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'Los Jardines', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'El Molino', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'Palermo', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'El Sol', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'Vista Hermosa', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'Ingeniería', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo'),
('Parque', 'Daniel Hoyle', 'Los Sauces', 'Esquina de Fátima y Prolongación Vallejo');


-- Asegúrate de reemplazar los IDs correctos generados en los inserts anteriores.
INSERT INTO Localidades (Nombre_Localidad, ID_Detalle_Localidad) VALUES
('Parque El Tingo', 1),
('Parque Indoamerica', 2),
('Parque La Amistad', 3),
('Parque 21 de Abril', 4),
('Parque B. Vecindad', 5),
('Parque Lorgio Caceda', 6),
('Parque Enrejado', 7),
('Parque El Chotano', 8),
('Parque Hogar de la Niña', 9),
('Parque Paseo Aguas 1', 10),
('Parque Paseo Aguas 2', 11),
('Parque Las Artes', 12),
('Parque El Delfín', 13),
('Parque Celendino', 14),
('Parque Las Flores', 15),
('Parque Robinson Soto', 16),
('Parque Pasaje Italia', 17),
('Parque Com.D. Sta. Edelmira', 18),
('Parque Segundo Estrada', 19),
('Parque Sedalib', 20),
('Parque Reservorio', 21),
('Parque La Paz', 22),
('Parque Cesar Vall. Calif. 1', 23),
('Parque Cesar Vall. Calif. 2', 24),
('Parque San Jose California I', 25),
('Parque San Jose California II', 26),
('Parque Plaza Huaman', 27),
('Parque Alameda Jhon Kennedy', 28),
('Parque J. Julio Ganoza', 29),
('Parque Cecilio Cox', 30),
('Parque Ovalo el Trebol', 31),
('Parque Hortensias California I', 32),
('Parque Hortensias California II', 33),
('Parque Hortensias California III', 34),
('Parque Praderas del Golf', 35),
('Parque El Flor del', 36),
('Parque Jardines del Golf I', 37),
('Parque Jardines del Golf II', 38),
('Parque Cedros del Golf', 39),
('Parque Condominios Las Flores', 40),
('Parque Los Sauces', 41),
('Parque Hortensias del Golf', 42);

