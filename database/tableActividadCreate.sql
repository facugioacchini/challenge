USE Challenge;  
GO  
CREATE TABLE Actividades (
    id_atividad int IDENTITY(1,1) PRIMARY KEY,
    fecha_alta date NOT NULL,
    id_usuario int NOT NULL FOREIGN KEY REFERENCES Usuarios(id_usuario),
    actividad varchar(100) NOT NULL,
);