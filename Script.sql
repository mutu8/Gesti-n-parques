DROP DATABASE BD_GestionAmbiental
CREATE DATABASE BD_GestionAmbiental
USE BD_GestionAmbiental

ALTER AUTHORIZATION ON DATABASE::BD_GestionAmbiental TO sa
GO

CREATE TABLE Empleados (
    ID_Empleado INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    esApoyo BIT NOT NULL, -- 1 para Administrador, 0 para Empleado regular
);

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
    ID_Empleado INT NULL, -- Nuevo campo para la relación
    FOREIGN KEY (ID_Empleado) REFERENCES Empleados(ID_Empleado) 
); 

-- Crear tabla Localidades
CREATE TABLE Localidades (
    ID_Localidad INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Localidad NVARCHAR(100),
    ID_Detalle_Localidad INT,
    FOREIGN KEY (ID_Detalle_Localidad) REFERENCES Detalles_Localidades(ID_Detalle_Localidad)
);

CREATE TABLE Visitas (
    ID_Visita INT IDENTITY(1,1) PRIMARY KEY,
    Fecha_Visita DATE,
    Estado BIT, 
    ID_Localidad INT,
    ID_Empleado INT, -- Nueva columna para el empleado encargado
    FOREIGN KEY (ID_Localidad) REFERENCES Detalles_Localidades(ID_Detalle_Localidad),
    FOREIGN KEY (ID_Empleado) REFERENCES Empleados(ID_Empleado) -- Nueva clave foránea
);


SELECT Localidades.ID_Localidad, Detalles_Localidades.*
FROM Localidades
INNER JOIN Detalles_Localidades ON Localidades.ID_Detalle_Localidad = Detalles_Localidades.ID_Detalle_Localidad;

SELECT * FROM Detalles_Localidades
SELECT * FROM Localidades
SELECT * FROM Urbanizaciones
SELECT * FROM Sectores
SELECT * FROM Empleados
SELECT * FROM Visitas

drop table Visitas

INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Alcalde', 'Carrillo Humberto', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Alcantara', 'Leiva Segundo', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Ana', 'Peña Cortez', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Ana María', 'Santos', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Alberto', 'Mercado Chamorro', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Aracely', 'Valderrama Rodriguez', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Bertha', 'Nieto Polo', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Betty', 'Cabrera Rivera', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Carolina', 'Dioses', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Carolina', 'Dioses Lopez', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Carranza', 'Narro Hanako', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Carlos', 'Peralta Alvarado', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Carmen', 'Zavaleta Cosavalente', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Celedonio', 'Castillo Cisneros', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Chavil', 'Vasquez Eduardo', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Constantino', 'Sequeiros Jimenéz', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Crispin', 'Mariños Juan', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Elena', 'Polo Ruiz', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Erika', 'Guerrero', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Erika', 'Rivera Guerra', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Eusebio', 'Rodríguez Zavaleta', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Fortunato', 'Valderrama Vega', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Garcia', 'Ruiz Luis', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Iraida', 'Cespedes Cedanillas', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Javier', 'Bazan Alva', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Jhonny', 'Reyes', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('José', 'Núñez Garcia', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('José', 'Peralta Flores', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('José', 'Vallejos Praxedes', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Julia', 'Lobato Lopez', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Julcamoro', 'Morales Hilario', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Layza', 'Perfecto Antonio', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Llave', 'Sanchez Juan', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Luna', 'Victoria Romulo', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('María Anita', 'Cruz Marquina', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Mengole', 'Rodriguez Dolores', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Nelly', 'Morales Solano', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Nicasio', 'Cruz', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Pandura', 'Alegría Ofelia', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Rosalia', 'Dioses Lopez', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Santiago', 'Medina Esteves', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Segunda', 'Garcia Ruiz', 1);
INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES ('Trujillo', 'Torres Luis', 1);

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

CREATE TRIGGER TRG_UniqueEmpleado
ON Empleados
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Empleados e
        JOIN inserted i ON e.Nombres = i.Nombres AND e.Apellidos = i.Apellidos
    )
    BEGIN
        RAISERROR ('No se pueden insertar valores duplicados en la tabla Empleados.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    INSERT INTO Empleados (Nombres, Apellidos, esApoyo)
    SELECT Nombres, Apellidos, esApoyo
    FROM inserted;
END;


CREATE OR ALTER TRIGGER TRG_UniqueVisitasPorDia
ON Visitas
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN Visitas v ON i.ID_Localidad = v.ID_Localidad AND i.Fecha_Visita = v.Fecha_Visita
    )
    BEGIN
        RAISERROR ('No se pueden insertar visitas duplicadas en una misma localidad en el mismo día.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    INSERT INTO Visitas (Fecha_Visita, Estado, ID_Localidad, ID_Empleado)
    SELECT Fecha_Visita, Estado, ID_Localidad, ID_Empleado
    FROM inserted;
END;


CREATE TRIGGER TRG_UniqueLocalidad
ON Localidades
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Localidades l
        JOIN inserted i ON l.Nombre_Localidad = i.Nombre_Localidad
    )
    BEGIN
        RAISERROR ('No se pueden insertar valores duplicados en la tabla Localidades.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    INSERT INTO Localidades (Nombre_Localidad, ID_Detalle_Localidad)
    SELECT Nombre_Localidad, ID_Detalle_Localidad
    FROM inserted;	
END;

/*
CREATE TRIGGER trgEvitarAsignacionDoble
ON Detalles_Localidades
FOR INSERT, UPDATE
AS
BEGIN
    -- Verificar si se intenta insertar o actualizar un registro
    -- que asigna el mismo empleado a una localidad diferente
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN Detalles_Localidades dl ON i.ID_Detalle_Localidad = dl.ID_Detalle_Localidad
        WHERE EXISTS (
            -- Buscar en Detalles_Localidades si ya existe una asignación para el mismo empleado
            SELECT 1
            FROM Detalles_Localidades
            WHERE ID_Empleado = i.ID_Empleado
            AND ID_Detalle_Localidad <> i.ID_Detalle_Localidad -- Excluir el registro actual en caso de UPDATE
        )
    )
    BEGIN
        -- Si ya existe una asignación, se cancela la operación
        RAISERROR ('No se puede asignar el mismo empleado a más de una localidad.', 16, 1);
        ROLLBACK TRANSACTION; -- Deshacer la transacción
        RETURN;
    END
END;
*/


