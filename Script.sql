DROP DATABASE IF EXISTS ADM_PANEL;
CREATE DATABASE IF NOT EXISTS ADM_PANEL;
USE ADM_PANEL;

/* DDL */ 
CREATE TABLE Estado(
	UF varchar(2) primary key,
    Nome varchar(25) unique
);

CREATE TABLE Endereco(
	ID int auto_increment primary key,
    Logradouro varchar(100),
    Cidade varchar(50),
    Estado_UF varchar(2),
    foreign key (Estado_UF) references Estado(UF),
    Numero int
);

CREATE TABLE Agente(
	CPF varchar(11) primary key,
    Nome varchar(50),
    Telefone varchar(11), /* 00000000000 ou 0000000000 -> (00) 9 0000-0000 ou (00) 0000-0000*/ 
    Endereco_ID int,
    foreign key (Endereco_ID) references Endereco(ID)
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
	ID int primary key auto_increment,
    Nome varchar(50),
    Valor decimal,
    Quantidade int
);

CREATE VIEW clientes_view AS
SELECT A.CPF, A.Nome, A.Telefone, E.Logradouro, E.Numero, E.Cidade, E.Estado_UF
FROM Agente AS A
RIGHT JOIN Cliente AS C
	ON A.CPF = C.Agente_CPF
LEFT JOIN Endereco AS E
	ON E.ID = A.Endereco_ID;

CREATE VIEW produtos_view AS
SELECT P.Nome, P.Valor, P.Quantidade
FROM Produto as P;

CREATE VIEW funcionarios_view AS
SELECT A.CPF, A.Nome, A.Telefone, E.Logradouro, E.Numero, E.Cidade, E.Estado_UF
FROM Agente AS A
RIGHT JOIN Funcionario AS F
	ON A.CPF = F.Agente_CPF
LEFT JOIN Endereco AS E
	ON E.ID = A.Endereco_ID;

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

INSERT INTO Produto(Nome, Valor, Quantidade)
VALUES
	('Produto genérico 1', 12.65, 1),
    ('Produto genérico 2', 5.25, 100),
    ('Produto genérico 3', 12.65, 10);

INSERT INTO Endereco(Logradouro, Cidade, Estado_UF, Numero)
VALUES
	('R. Guaipá', 'São Paulo', 'SP', 678),
    ('R. Pref. Olímpio de Melo', 'Rio de Janeiro', 'RJ', 1485),
    ('Av. Flor de Seda', 'Belo Horizonte', 'MG', 154);

INSERT INTO Agente(CPF, Nome, Telefone, Endereco_ID)
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