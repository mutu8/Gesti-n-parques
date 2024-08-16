DROP DATABASE BD_GestionAmbiental
CREATE DATABASE BD_GestionAmbiental
USE BD_GestionAmbiental

ALTER AUTHORIZATION ON DATABASE::BD_GestionAmbiental TO sa
GO

CREATE TABLE Empleados (
    ID_Empleado INT IDENTITY(1,1) PRIMARY KEY,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    esApoyo BIT NOT NULL, 
);

ALTER TABLE Empleados
ADD esPersonalLimpieza BIT NULL;

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
    Nota NVARCHAR(255) NULL, -- Nueva columna para la nota
    FOREIGN KEY (ID_Localidad) REFERENCES Detalles_Localidades(ID_Detalle_Localidad),
    FOREIGN KEY (ID_Empleado) REFERENCES Empleados(ID_Empleado) -- Nueva clave foránea
);

CREATE TABLE Asistencias (
    ID_Asistencia INT IDENTITY(1,1) PRIMARY KEY,
    Fecha_Asistencia DATE NOT NULL,
    ID_Empleado INT NOT NULL FOREIGN KEY REFERENCES Empleados(ID_Empleado),
    Asistio BIT NULL
);
	

SELECT Localidades.ID_Localidad, Detalles_Localidades.*
FROM Localidades
INNER JOIN Detalles_Localidades ON Localidades.ID_Detalle_Localidad = Detalles_Localidades.ID_Detalle_Localidad;

SELECT * FROM Detalles_Localidades
SELECT * FROM Localidades
SELECT * FROM Empleados
SELECT * FROM Visitas
SELECT * FROM Asistencias
drop table Asistencias

drop table Visitas
ALTER TABLE Visitas ALTER COLUMN Nota NVARCHAR(255);
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Visitas'

INSERT INTO Empleados (Nombres, Apellidos, esApoyo) VALUES
('Julia', 'Lobato Lopez', 1),
('Carranza', 'Narro Hanako', 1),
('Erika', 'Guerrero', 0),
('Iraida', 'Cespedes Cadenillas', 1),
('Jose', 'Peralta Flores', 1),
('Elena', 'Polo Ruiz', 1),
('Pandura', 'Alegria Ofelia', 0),
('Alberto', 'Mercado Chamorro', 1),
('Santos', 'Rodriguez León', 1),
('Jhonny', 'Reyes', 1),
('Javier', 'Bazan Alva', 1),
('Nicasio', 'Cruz', 1),
('Aracely', 'Valderrama Rodriguez', 0),
('Jose', 'Silva Guevaron', 1),
('Benises', 'Vereau Luis', 1),
('Rumay', 'Reyes José', 1),
('Trujillo', 'Torres Luis', 1),
('Quiroz', 'Cholan Miuler', 1),
('Rubio', 'Moya Adolfo', 1),
('Segunda', 'Garcia Ruiz', 1),
('Salcedo', 'Merino Cesar', 1),
('Betty', 'Cabrera Rivera', 1),
('Rosa', 'Villa Cachi', 1),
('Santiago', 'Medina Estrevés', 1),
('Chavil', 'Vasquez Eduardo', 1),
('Layza', 'Perfecto Antonio', 1),
('Garro', 'Marin Ernesto', 1),
('Pedro', 'Cruz Sanchez', 1),
('Carolina', 'Dioses Lopez', 1),
('Onstantino', 'Sequeiros Jimenez', 1),
('Luna', 'Virtoria Romulo', 1),
('José', 'Vallejos Praxederes', 1),
('Tombia', 'Jicaro', 0),
('Maria Anita', 'Cruz Marquina', 1),
('Zenen', 'Avila Velasquez', 1),
('Llave', 'Sanchez Juan', 1),
('Yenny', 'Villegas Chavez', 1),
('Garcia', 'Ruiz Luis', 1),
('Betty', 'Cabrera Rivera', 1),
('Erika', 'Rivera Guerrera', 1),
('Carlos', 'Peralta Alvarado', 1),
('Alcantara', 'Leiva Segundo', 1),
('Lopez', 'Campos Teodora', 1),
('Ana Maria', 'Santos', 1),
('Fortunato', 'Valderrama Vega', 1),
('Juan', 'Crespin Mariños', 1),
('Rosalia', 'Dioses Lopez', 1),
('Mengole', 'Rodriguez Dolores', 1),
('Carmen', 'Zavalera Cosavalente', 1),
('Nelly', 'Morales Solano', 1),
('Julcamoro', 'Morales Hilario', 1),
('Bertha', 'Nieto Polo', 1),
('Celedonio', 'Castillo Cisneros', 1),
('Victor Hugo', 'Contreras Moran', 0),
('Romulo', 'Luna Victoria', 1),
('Praxederes', 'José Vallejos', 1),
('Jicaro', 'Tombia', 0),
('Maria Anita', 'Cruz Marquina', 1),
('Zenen', 'Avila Velasquez', 1),
('Llave', 'Sanchez Juan', 1),
('Yenny', 'Villegas Chavez', 1),
('Luis', 'Garcia Ruiz', 1),
('Betty', 'Cabrera Rivera', 1);


INSERT INTO Empleados (Nombres, Apellidos, esApoyo, esPersonalLimpieza) VALUES 
('AGUSTÍN', 'GUEVARA MÉNDEZ', 1, 1),
('ALBERTO', 'CORREA CERNA', 1, 1),
('ALDO', 'CHILCON CARBONEL', 1, 1),
('ANTONIO', 'LAUREANO POMA', 1, 1),
('BERTHA', 'PINCHI USHIÑAGUA', 1, 1),
('BETTY', 'CABRERA RIVERA', 1, 1),
('CARLOS ALBERTO', 'DEZA MELÉNDEZ', 1, 1),
('CARLOS', 'LUGONES GUZMAN', 1, 1),
('CARLOS', 'RUIZ BRICEÑO', 1, 1),
('CARLOS', 'RUIZ SARE', 1, 1),
('GUILLERMO', 'CHAMAYA LUMBA', 1, 1),
('CARMEN', 'NAVARRETE', 1, 1),
('CARMEN', 'DIOSES', 1, 1),
('CELIA', 'LOPEZ ANISMENDIZ', 1, 1),
('CESAR', 'ASCÓN SERRANO', 1, 1),
('CÉSAR', 'CHÁVEZ CORONEL', 1, 1),
('CÉSAR IVAN', 'ANTICONA VILLANUEVA', 1, 1),
('CHARLES', 'BRIONES ROJAS', 1, 1),
('CONSUELO', 'TORRES DOMINGUEZ', 1, 1),
('CRISTHIAN', 'JERÓNIMO CHANG', 1, 1),
('DAVID', 'MERINO QUIÑONES', 1, 1),
('DEYSI PATRICIA', 'GARCIA CHICLAYO', 1, 1),
('DEYSI', 'RODRÍGUEZ DIESTRA', 1, 1),
('DOLLY', 'API PERALTA', 1, 1),
('DORIS', 'SALDAÑA RAMÍREZ', 1, 1),
('EDGAR JHONATAN', 'ESCOBEDO MARTINEZ', 1, 1),
('EDITA', 'DEL CASTILLO ABANTO', 1, 1),
('EDITH', 'ESPIRITU GUERRA', 1, 1),
('EDUARDO', 'FERNÁNDEZ ALCÁNTARA', 1, 1),
('ELIAS', 'LAYZA HORNA', 1, 1),
('ESTEBAN', 'MAURICIO VALVERDE', 1, 1),
('EVELYN', 'CASTAÑEDA', 1, 1),
('ESTHER', 'TRUJILLO MARCHENA', 1, 1),
('EUDOCIA', 'PAREDES CRUZ', 1, 1),
('EUFEMIO', 'TORRES ZAVALETA', 1, 1),
('FELICITA', 'VENAUTE MEZA', 1, 1),
('FRANCIS', 'CUEVA CHININI', 1, 1),
('GEOVANA', 'BOBADILLA CONICILLA', 1, 1),
('GONZALO', 'SÁNCHEZ ÁLVAREZ', 1, 1),
('GUADALUPE', 'SANTOS CRUZ', 1, 1),
('GUILLERMO', 'CHAMAYA LUMBA', 1, 1),
('HARRY', 'ARANDA GALLOSO', 1, 1),
('ISABEL', 'BRICEÑO APONTE', 1, 1),
('IRVIN', 'MERA', 1, 1),
('JHON', 'MATTUS CÁRDENAS', 1, 1),
('JHONATAN', 'SANCHEZ AZABACHE', 1, 1),
('JONATHAN', 'SALINAS EURIBE', 1, 1),
('JORGE', 'BUENO', 1, 1),
('JORGE LUIS', 'GARCÍA COSTILLA', 1, 1),
('JORGE LUIS', 'LOZANO DOMINGUEZ', 1, 1),
('JORGE', 'POLO SOTELO', 1, 1),
('JOSE', 'LORENZO ABANTO', 1, 1),
('JOSÉ ZACARÍAS', 'ZAVALETA SANTOS', 1, 1),
('JUAN', 'CASTILLO RAMOS', 1, 1),
('JUAN', 'GUERRA LOPEZ', 1, 1),
('JUAN', 'VARGAS TELLO', 1, 1),
('JULIO CÉSAR', 'EDQUEN VERÁSTEGUI', 1, 1),
('LIDER', 'VILLANUEVA DIESTRA', 1, 1),
('LUIS', 'ANTICONA QUIROZ', 1, 1),
('LUIS', 'CULQUI PEÑA', 1, 1),
('LUIS NICASIO', 'MORALES', 1, 1),
('LUIS JOEL', 'TERRONES RONCAL', 1, 1),
('MANUEL JESUS', 'CALDERON FLORES', 1, 1),
('MANUEL', 'OLIVA AVALOS', 1, 1),
('MANUEL', 'PORTOCARRERO HURTADO', 1, 1),
('MARCELO', 'SÁNCHEZ GIL', 1, 1),
('MARIA', 'GARCIA DE RISCO', 1, 1),
('MARÍA ISABEL', 'LAIZA CASTILLO', 1, 1),
('MÁRTIRES', 'ROSAS CRUZ', 1, 1),
('MARUJA ISABEL', 'SALAS PANOCA', 1, 1),
('MATEO', 'SILVA ROMERO', 1, 1),
('MIGUEL', 'SALINAS CHAVEZ', 1, 1),
('MILTON', 'LÓPEZ MOSTACERO', 1, 1),
('NORMA', 'SEVILLANO CARBAJAL', 1, 1),
('OSCAR', 'VIZCARRA RIVAS', 1, 1),
('PETER', 'CLAUDIO ZARE BRICEÑO', 1, 1),
('RICHARD', 'SANCHEZ AZABACHE', 1, 1),
('ROBERTH', 'PELAEZ MEREGILDO', 1, 1),
('ROBERTO', 'VALERIANO DIESTRA', 1, 1),
('ROCÍO', 'RUBIO ASUNCIÓN', 1, 1),
('RONALD', 'REINA CANO', 1, 1),
('ROSA ELENA', 'MENDOZA GUERRA', 1, 1),
('ROSA ELVIRA', 'VARGAS ÁVILA', 1, 1),
('SANTIAGO', 'RODRIGUEZ ASUNCION', 1, 1),
('SANTOS', 'CARBAJAL JIMÉNEZ', 1, 1),
('SANTOS', 'MUÑOZ REYES', 1, 1),
('SANTOS', 'SANDOVAL MAURICIO', 1, 1),
('SANTOS', 'VILCA CRUZ', 1, 1),
('SEGUNDO', 'CASANOVA ALCANTARA', 1, 1),
('SERAPIO', 'ENCOMENDEROS SOTO', 1, 1),
('VICTOR', 'ARCE CASTILLO', 1, 1),
('VICTOR', 'CASTILLO CARRANZA', 1, 1),
('VICTOR', 'GARCIA ANDRADE', 1, 1),
('VICTOR', 'SANDOVAL CASTILLO', 1, 1),
('WILDER', 'CORREA ALVAREZ', 1, 1),
('YENNY', 'VILLEGAS CHAVEZ', 1, 1),
('YENY', 'AVALOS IBAÑEZ', 1, 1),
('YIMI', 'AREVALO PINCHI', 1, 1),
('YOSHI', 'CAVERO CHAVEZ', 1, 1),
('ALEXANDER SAUL', 'CHERO COBA', 1, 1);


CREATE OR ALTER TRIGGER TRG_UniqueEmpleado
ON Empleados
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si hay algún DNI duplicado en los registros insertados
    IF EXISTS (
        SELECT 1
        FROM Empleados e
        JOIN inserted i ON e.DNI = i.DNI
    )
    BEGIN
        RAISERROR ('No se pueden insertar valores con DNI duplicado en la tabla Empleados.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Insertar los registros nuevos si no hay DNI duplicados
    INSERT INTO Empleados (Nombres, Apellidos, esApoyo, DireccionCorreo, urlFoto, DNI)
    SELECT Nombres, Apellidos, esApoyo, DireccionCorreo, urlFoto, DNI
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


CREATE OR ALTER TRIGGER TRG_UniqueLocalidad
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


CREATE OR ALTER TRIGGER TRG_UpdateEstadoActivo
ON Empleados
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar el estado EstadoActivo basado en la fecha de nacimiento
    UPDATE e
    SET e.EstadoActivo = CASE 
                            WHEN DATEPART(DAY, i.FechaNacimiento) = DATEPART(DAY, GETDATE()) 
                             AND DATEPART(MONTH, i.FechaNacimiento) = DATEPART(MONTH, GETDATE()) THEN 0 
                            ELSE 1 
                         END
    FROM Empleados e
    INNER JOIN inserted i ON e.ID_Empleado = i.ID_Empleado;
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


