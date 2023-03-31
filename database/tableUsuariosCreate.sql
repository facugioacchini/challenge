USE Challenge;  
GO  
CREATE TABLE Usuarios (
    id_usuario int IDENTITY(1,1) PRIMARY KEY,
    nombre varchar(50) NOT NULL,
    apellido varchar(50) NOT NULL,
    email varchar(50) NOT NULL,
    fecha_nacimiento date NOT NULL,
    telefono varchar(15),
	pais varchar(50) NOT NULL,
	info bit NOT NULL
);