﻿USE Covalco
GO
--N CONVIERTE EL TEXTO EN NVARCHAR
--U USER DEFINED TABLE
IF OBJECT_ID(N'Covalco.dbo.Alumno', N'U') IS NULL
BEGIN
	-- Creamos la Tabla
CREATE TABLE dbo.Alumno
(
	Id INT IDENTITY NOT NULL PRIMARY KEY,
	Nombre [NVARCHAR](50) NOT NULL,
	Apellidos [NVARCHAR](50) NOT NULL,
	Dni [NVARCHAR](14) NOT NULL
);
END