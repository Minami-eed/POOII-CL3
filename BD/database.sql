use master;
GO

CREATE DATABASE POOCL3;
GO

USE POOCL3
GO

-- Tabla TipoProducto
CREATE TABLE TipoProducto (
    id INT PRIMARY KEY,
    tipo VARCHAR(50)
);
GO

-- Tabla Producto
CREATE TABLE Producto (
    id INT IDENTITY(1, 1) PRIMARY KEY,
    nombre VARCHAR(100),
    idtipo INT,
    precio DECIMAL(10,2),
    fecha DATE,
    FOREIGN KEY (idtipo) REFERENCES TipoProducto(id)
);
GO

INSERT INTO TipoProducto (id, tipo)
VALUES
	(1,'Accesorio'),
	(2, 'Parte'),
	(3, 'Otro');
GO

INSERT INTO Producto (nombre, idtipo, precio, fecha)
VALUES
   ('teclado', 1, 40.00, '2023-01-01'),
   ('mouse', 1, 35.00, '2022-07-28'),
   ('monitor', 1, 200.00, '2023-12-24'),
   ('memoria', 2, 120.00, '2023-10-08'),
   ('impresora', 1, 300.00, '2022-05-01'),
   ('procesador', 2, 800.00, '2023.07.29'),
   ('parlantes', 1, 180.00, '2023-06-07'),
   ('tarjeta de red', 2, 150.00, '2022-12-31'),
   ('microfono', 1, 60.00, '2022-06-24'),
   ('disco duro', 2, 350.00, '2022-08-30');
Go

CREATE PROCEDURE sp_GetProductoByID
@id INT
AS
BEGIN
    SELECT p.id, p.nombre, tp.tipo, p.precio, p.fecha
    FROM Producto p
    INNER JOIN TipoProducto tp ON p.idtipo = tp.id
    WHERE p.id = @id;
END
GO

CREATE PROCEDURE sp_AddProducto
@nombre VARCHAR(100),
@idtipo INT,
@precio DECIMAL(10,2),
@fecha DATE
AS
BEGIN
    INSERT INTO Producto (nombre, idtipo, precio, fecha)
    VALUES (@nombre, @idtipo, @precio, @fecha);
END
GO

CREATE PROCEDURE sp_UpdateProducto
@id INT,
@nombre VARCHAR(100),
@idtipo INT,
@precio DECIMAL(10,2),
@fecha DATE
AS
BEGIN
    UPDATE Producto
    SET nombre = @nombre, idtipo = @idtipo, precio = @precio, fecha = @fecha
    WHERE id = @id;
END
GO

CREATE PROCEDURE sp_DeleteProducto
@id INT
AS
BEGIN
    DELETE FROM Producto WHERE id = @id;
END
GO

SELECT * FROM Producto;
GO

SELECT * FROM TipoProducto;
GO
