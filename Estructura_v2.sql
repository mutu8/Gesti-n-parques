CREATE TABLE SectoresTurnos (
    ID INT IDENTITY(1,1) PRIMARY KEY,  -- ID autoincremental
    Numero INT,                        -- Número del sector
    Sector NVARCHAR(255),              -- Descripción del sector
    Turno NVARCHAR(50)                 -- Turno asignado
);

INSERT INTO SectoresTurnos (Numero, Sector, Turno)
VALUES 
(1, 'Las praderas jardines del golf, Av. César Vallejo 01 carril Av. Fátima (desde claretiano hasta plaza vea)', '6:00 am – 2:00 pm'),
(2, 'Las Hortensias Av. Los Ángeles y Av. El Golf Entre los ángeles y Av. César Vallejo', '6:00 am – 2:00 pm'),
(3, 'Av. Huamán / Prolongación Huamán', '6:00 am – 2:00 pm'),
(4, 'Las Flores del Golf Jardines del golf II Pról. Av. Huamán y Av. Vallejo Carril 2 hasta plaza vea.', '6:00 am – 2:00 pm'),
(5, 'Av. El Golf (desde Av. C. Vallejo hasta ovalo la Marinera/pról. César Vallejo a la taberna las Palmeras I II III.', '6:00 am – 2:00 pm'),
(6, 'Portales y Palmeras el Golf', '6:00 am – 2:00 pm'),
(7, 'Palmas IIIIII', '6:00 am – 2:00 pm'),
(8, 'Palmeras del Golf y Urb. El Golf', '6:00 am – 2:00 pm'),
(9, 'Fundo del Golf', '6:00 am – 2:00 pm'),
(10, 'San José de California I II', '6:00 am – 2:00 pm'),
(11, 'Desde Larco con Huamán Hasta Av. Fátima (Inka Farma)', '6:00 am – 2:00 pm'),
(12, 'Santa Edelmira', '6:00 am – 2:00 pm'),
(13, 'Los Granados Chiras Ovalito de Fátima todas las calles de Fátima hasta espalda de la Iglesia.', '6:00 am – 2:00 pm'),
(14, 'Desde Av. Larco con Av. Huamán – Av. Larco hasta Av. Fátima y desde Av. Fátima hasta Inka Farma.', '6:00 am – 2:00 pm'),
(15, 'Túpac Amaru', '6:00 am – 2:00 pm'),
(16, 'San Pedro', '6:00 am – 2:00 pm'),
(17, 'Los Mangos las Vegas', '6:00 am – 2:00 pm'),
(18, 'Huamán – Mangos 1 Sector', '6:00 am – 2:00 pm'),
(19, 'Vista Alegre 1', '6:00 am – 2:00 pm'),
(20, 'Vista Alegre 2', '6:00 am – 2:00 pm'),
(21, 'Vista Alegre 3', '6:00 am – 2:00 pm'),
(22, 'Vista Alegre 4', '6:00 am – 2:00 pm'),
(23, 'Av. Seoane desde sedalib – Av 2 de Mayo', '6:00 am – 2:00 pm'),
(24, 'Av. 2 de Mayo Desde Antenor Orrego hasta Tecsup', '6:00 am – 2:00 pm'),
(25, 'Sauces I II Etapa intersección Pról. Bolivia /Desamparados.', '6:00 am – 2:00 pm'),
(26, 'Bnos Aires Centro desde la sirena hasta la Av. 2 de Mayo y Av. Bolivia Lado Derecho', '6:00 am – 2:00 pm'),
(27, 'Bnos Aires Centro lado Izquierdo (Torrejón) y Av. Bolivia desde Av. Larco a Manuel Seoane', '6:00 am – 2:00 pm'),
(28, 'Av. Seoane/Av. 2 de mayo - hasta Comisaria. Calles: Antenor Orrego y España.', '6:00 am – 2:00 pm'),
(29, 'Bnos Aires Sur – Av. Seoane Calles: Mathey Hnos Pinzon CallaoLimaSta Rosa.', '6:00 am – 2:00 pm'),
(30, 'Bnos Aires Norte Plaza de Armas', '6:00 am – 2:00 pm'),
(31, 'Bnos Aires Norte – Av. Libertad – Evitamiento hasta Terminal Pesquero.', '6:00 am – 2:00 pm'),
(32, 'Armando Villanueva Carlos Manuel Cox La Victoria Montevideo.', '6:00 am – 2:00 pm'),
(33, 'Av. 2 de Mayo/Av. Seoane hasta calle Montevideo/Calle Colón.', '6:00 am – 2:00 pm'),
(34, 'Av. Larco Desde Malecón Colon hasta la comisaria', '6:00 am – 2:00 pm'),
(35, 'Av. Larco Desde La comisaria hasta Av. Huamán', '6:00 am – 2:00 pm'),
(36, 'Av. Bolivia desde la I.E Encinas hasta Monte video las 2 vías', '6:00 am – 2:00 pm'),
(37, 'Las Flores', '6:00 am – 2:00 pm'),
(38, 'Av. Paujiles – Pról. Juan Pablo II', '6:00 am – 2:00 pm'),
(39, 'San Andrés V etapa Lado Derecho', '6:00 am – 2:00 pm'),
(40, 'San Andrés V etapa Lado Izquierdo', '6:00 am – 2:00 pm'),
(41, 'San Andrés V (Hogar de la Niña)', '6:00 am – 2:00 pm'),
(42, 'Liberación Social', '6:00 am – 2:00 pm'),
(43, 'Tercera Vía Huamán y todo Paseo de Aguas', '6:00 am – 2:00 pm'),
(44, 'Auxiliar de la Av. Larco (Tercera Vía) – Coliseo Municipal y zona Rústica', '6:00 am – 2:00 pm'),
(45, 'Desde Auxiliar Av. Larco con Huamán – Prolongación Santa Edelmira (Av. Chota)', '6:00 am – 2:00 pm'),
(46, 'San Andrés V Etapa II Sector', '6:00 am – 2:00 pm'),
(47, 'Rosales de San Luis', '6:00 am – 2:00 pm');


ALTER TABLE Asistencias
ADD ID_SectorTurno INT;

ALTER TABLE Asistencias
ADD CONSTRAINT FK_Asistencias_SectoresTurnos
FOREIGN KEY (ID_SectorTurno) REFERENCES SectoresTurnos(ID);


DELETE FROM Asistencias
SELECT * FROM Empleados
SELECT * FROM Asistencias
SELECT * FROM Opcion
SELECT * FROM SectoresTurnos

EXEC sp_help 'Empleados';


CREATE TRIGGER trg_PreventDuplicateAsistencias
ON Asistencias
INSTEAD OF INSERT
AS
BEGIN
    -- Verificar si ya existe una asistencia para el mismo empleado y la misma fecha
    IF EXISTS (
        SELECT 1
        FROM Asistencias A
        JOIN inserted I ON A.ID_Empleado = I.ID_Empleado 
                       AND CONVERT(DATE, A.Fecha_Asistencia) = CONVERT(DATE, I.Fecha_Asistencia)
    )
    BEGIN
        -- Si ya existe, devolver un error y no permitir la inserción
        RAISERROR('Ya existe una asistencia registrada para este empleado en la fecha indicada.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- Si no existe, proceder con la inserción
        INSERT INTO Asistencias (ID_Empleado, Fecha_Asistencia, ID_Opcion, ID_SectorTurno)
        SELECT ID_Empleado, Fecha_Asistencia, ID_Opcion, ID_SectorTurno
        FROM inserted;
    END
END;
