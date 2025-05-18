CREATE TABLE Artist (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(200) NOT NULL
) ENGINE=InnoDB;

CREATE TABLE Album (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(200) NOT NULL,
    ArtistId INT NOT NULL,
    ReleaseDate DATE,
    FOREIGN KEY (ArtistId) REFERENCES Artist(Id)
        ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE Song (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(200) NOT NULL,
    ArtistId INT NOT NULL,
    Duration TIME NOT NULL,
    FilePath VARCHAR(500),
    AlbumCoverPath VARCHAR(500),
    AlbumId INT NOT NULL,
    FOREIGN KEY (AlbumId) REFERENCES Album(Id)
        ON DELETE CASCADE,
    FOREIGN KEY (ArtistId) REFERENCES Artist(Id)
        ON DELETE CASCADE
) ENGINE=InnoDB;
   
INSERT INTO Artist (Name) VALUES ('Luis Alfonso');
INSERT INTO Artist (Name) VALUES ('Grupo Frontera');
INSERT INTO Artist (Name) VALUES ('Elvis Crespo');
INSERT INTO Artist (Name) VALUES ('Guns N''Roses');
INSERT INTO Artist (Name) VALUES ('Feid');

INSERT INTO Album (Name, ArtistId, ReleaseDate) VALUES ('La Terraza', (SELECT Id FROM Artist WHERE Name = 'Luis Alfonso'), '2024-01-01'); 
INSERT INTO Album (Name, ArtistId, ReleaseDate) VALUES ('El Comienzo', (SELECT Id FROM Artist WHERE Name = 'Grupo Frontera'), '2024-01-01');
INSERT INTO Album (Name, ArtistId, ReleaseDate) VALUES ('Suavemente', (SELECT Id FROM Artist WHERE Name = 'Elvis Crespo'), '1998-01-01'); 
INSERT INTO Album (Name, ArtistId, ReleaseDate) VALUES ('Use Your Illusion I', (SELECT Id FROM Artist WHERE Name = 'Guns N''Roses'), '1991-01-01'); 
INSERT INTO Album (Name, ArtistId, ReleaseDate) VALUES ('MANIFESTTING 20-05', (SELECT Id FROM Artist WHERE Name = 'Feid'), '2024-01-01');

INSERT INTO Song (Title, ArtistId, Duration, AlbumId, FilePath, AlbumCoverPath)
VALUES ('Cu�nto Vale', (SELECT Id FROM Artist WHERE Name = 'Luis Alfonso'), '00:03:37', (SELECT Id FROM Album WHERE Name = 'La Terraza'), '/views/Assets/Music/CuantoVale-LuisAlfonso/cuanto-vale.mp3', '/MusicApp;/views/Assets/Music/CuantoVale-LuisAlfonso/cuanto-vale.jpeg'); 
INSERT INTO Song (Title, ArtistId, Duration, AlbumId, FilePath, AlbumCoverPath)
VALUES ('Grupo Frontera x Ke Personajes - OJITOS ROJOS', (SELECT Id FROM Artist WHERE Name = 'Grupo Frontera'), '00:03:52', (SELECT Id FROM Album WHERE Name = 'El Comienzo'), '/ruta/ojitos_rojos.mp3', '/ruta/el_comienzo.jpg'); 
INSERT INTO Song (Title, ArtistId, Duration, AlbumId, FilePath, AlbumCoverPath)
VALUES ('Nuestra Cancion', (SELECT Id FROM Artist WHERE Name = 'Elvis Crespo'), '00:03:29', (SELECT Id FROM Album WHERE Name = 'Suavemente'), '/ruta/nuestra_cancion.mp3', '/ruta/suavemente.jpg'); 
INSERT INTO Song (Title, ArtistId, Duration, AlbumId, FilePath, AlbumCoverPath)
VALUES ('Don''t Cry', (SELECT Id FROM Artist WHERE Name = 'Guns N''Roses'), '00:04:44', (SELECT Id FROM Album WHERE Name = 'Use Your Illusion I'), '/ruta/dont_cry.mp3', '/ruta/use_your_illusion_i.jpg'); 
INSERT INTO Song (Title, ArtistId, Duration, AlbumId, FilePath, AlbumCoverPath)
VALUES ('No Digas Na', (SELECT Id FROM Artist WHERE Name = 'Feid'), '00:03:29', (SELECT Id FROM Album WHERE Name = 'MANIFESTTING 20-05'), '/ruta/no_digas_na.mp3', '/ruta/manifesting_2005.jpg');

