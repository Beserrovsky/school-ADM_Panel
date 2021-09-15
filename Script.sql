DROP DATABASE IF EXISTS ADM_PANEL;
CREATE DATABASE IF NOT EXISTS ADM_PANEL;
USE ADM_PANEL;

/* DDL */ 
CREATE TABLE Estado(
	UF varchar(2) primary key,
    Nome varchar(25) unique
);

CREATE TABLE CIdade(
	Nome varchar(50) primary key
);

CREATE TABLE Endereco(
	Id int auto_increment primary key,
    CEP varchar(8),
    Logradouro varchar(100),
    CIdade varchar(50),
    Estado_UF varchar(2),
    Numero int,
    foreign key (Estado_UF) references Estado(UF),
	foreign key (CIdade) references CIdade
);

CREATE TABLE Agente(
	CPF varchar(11) primary key,
    Nome varchar(50),
    Telefone varchar(11), /* 00000000000 ou 0000000000 -> (00) 9 0000-0000 ou (00) 0000-0000*/ 
    Endereco_Id int,
    foreign key (Endereco_Id) references Endereco(Id)
);

CREATE TABLE Cliente(
	Agente_CPF varchar(11) primary key,
    foreign key (Agente_CPF) references Agente(CPF)
);

CREATE TABLE Funcionario(
	Agente_CPF varchar(11) primary key,
    foreign key (Agente_CPF) references Agente(CPF)
);

CREATE TABLE Produto(
	Id int primary key auto_increment,
    Nome varchar(50),
    Valor decimal(10,2),
    QuantIdade int
);

CREATE VIEW agentes_types_view AS
SELECT A.CPF, A.Nome, A.Telefone, E.Logradouro, E.Numero, E.CIdade, E.Estado_UF,
	(SELECT COUNT(*) FROM Cliente WHERE Cliente.Agente_CPf = A.CPF) AS Cliente,
	(SELECT COUNT(*) FROM Funcionario WHERE Funcionario.Agente_CPf = A.CPF) AS Funcionario
FROM Agente AS A
LEFT JOIN Endereco AS E
	ON E.Id = A.Endereco_Id;

CREATE VIEW clientes_view AS
SELECT A.CPF, A.Nome, A.Telefone, E.Logradouro, E.Numero, E.CIdade, E.Estado_UF
FROM Agente AS A
RIGHT JOIN Cliente AS C
	ON A.CPF = C.Agente_CPF
LEFT JOIN Endereco AS E
	ON E.Id = A.Endereco_Id;

CREATE VIEW produtos_view AS
SELECT P.Nome, P.Valor, P.QuantIdade
FROM Produto as P;

CREATE VIEW funcionarios_view AS
SELECT A.CPF, A.Nome, A.Telefone, E.Logradouro, E.Numero, E.CIdade, E.Estado_UF
FROM Agente AS A
RIGHT JOIN Funcionario AS F
	ON A.CPF = F.Agente_CPF
LEFT JOIN Endereco AS E
	ON E.Id = A.Endereco_Id;

/* Initial DML */

INSERT INTO Estado(UF, Nome) 
VAlUES 
	('RO', 'Rondônia'),
    ('AC', 'Acre'),
    ('AM', 'Amazonas'),
    ('RR', 'Roraima'),
    ('PA', 'Pará'),
    ('AP', 'Amapá'),
    ('TO', 'Tocantins'),
    ('MA', 'Maranhão'),
    ('PI', 'Piauí'),
    ('CE', 'Ceará'),
    ('RN', 'Rio Grande do Norte'),
    ('PB', 'Paraíba'),
    ('PE', 'Pernambuco'),
    ('AL', 'Alagoas'),
    ('SE', 'Sergipe'),
    ('BA', 'Bahia'),
    ('MG', 'Minas Gerais'),
    ('ES', 'Espírito Santo'),
    ('RJ', 'Rio de Janeiro'),
    ('SP', 'São Paulo'),
    ('PR', 'Paraná'),
    ('SC', 'Santa Catarina'),
    ('RS', 'Rio Grande do Sul'),
    ('MS', 'Mato Grosso do Sul'),
    ('MT', 'Mato Grosso'),
    ('GO', 'Goiás'),
    ('DF', 'Distrito Federal');

INSERT INTO Produto(Nome, Valor, QuantIdade)
VALUES
	('Produto genérico 1', 12.65, 1),
    ('Produto genérico 2', 5.25, 100),
    ('Produto genérico 3', 12.65, 10);

INSERT INTO Endereco(Logradouro, CIdade, Estado_UF, Numero)
VALUES
	('R. Guaipá', 'São Paulo', 'SP', 678),
    ('R. Pref. Olímpio de Melo', 'Rio de Janeiro', 'RJ', 1485),
    ('Av. Flor de Seda', 'Belo Horizonte', 'MG', 154);

INSERT INTO Agente(CPF, Nome, Telefone, Endereco_Id)
VALUES
	('92208673468', 'São Tomás de Aquino', '1189895656', 1),
    ('28662918085', 'Marthin Luther King', '1512795256', 2),
    ('39782459062', 'Nelson Mandela', '1198295556', 3);

INSERT INTO Cliente(Agente_CPF)
VALUES
	('92208673468'),
    ('39782459062');

INSERT INTO Funcionario(Agente_CPF)
VALUES
	('28662918085');
    
/* DQL */

SELECT * FROM clientes_view;
SELECT * FROM funcionarios_view;
SELECT * FROM produtos_view;
SELECT * FROM agentes_types_view;
SELECT * FROM Agente;