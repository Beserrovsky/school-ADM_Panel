DROP DATABASE IF EXISTS ADM_PANEL;
CREATE DATABASE IF NOT EXISTS ADM_PANEL;
USE ADM_PANEL;

/* DDL */ 
CREATE TABLE Estado(
	UF varchar(2) primary key,
    Nome varchar(25) unique
);

CREATE TABLE Cidade(
	Nome varchar(50) primary key
);

CREATE TABLE Endereco(
	CEP varchar(9) primary key, /* XXXXX-XXX -> 8 / 9 */
    Estado_UF varchar(2),
    Cidade_Nome varchar(50),
    Logradouro varchar(100),
    foreign key (Estado_UF) references Estado(UF),
    foreign key (Cidade_Nome) references Cidade(Nome)
);

CREATE TABLE Agente(
	CPF varchar(14) primary key, /* XXX.XXX.XXX-XX -> 11 / 14 */
    Nome varchar(50),
    Telefone varchar(15), /* (XX) 9XXXX-XXXX -> 10 ou 11 / 15 */
    Endereco_CEP varchar(9),
    Numero int,
    foreign key (Endereco_CEP) references Endereco(CEP)
);

CREATE TABLE Cliente(
	Agente_CPF varchar(14) primary key,
    foreign key (Agente_CPF) references Agente(CPF)
);

CREATE TABLE Funcionario(
	Agente_CPF varchar(14) primary key,
    foreign key (Agente_CPF) references Agente(CPF)
);

CREATE TABLE Produto(
	ID int primary key auto_increment,
    Nome varchar(50),
    Valor decimal(10,2),
    Quantidade int
);

CREATE VIEW agente_view AS
SELECT A.CPF, A.Nome, A.Telefone, E.CEP, E.Estado_UF, C.Nome as Cidade, E.Logradouro, A.Numero,
	(SELECT COUNT(*) FROM Cliente WHERE Cliente.Agente_CPf = A.CPF) AS Cliente,
	(SELECT COUNT(*) FROM Funcionario WHERE Funcionario.Agente_CPf = A.CPF) AS Funcionario
FROM Agente AS A
LEFT JOIN Endereco AS E
	ON E.CEP = A.Endereco_CEP
LEFT JOIN Cidade AS C
	ON E.Cidade_Nome = C.Nome;

CREATE VIEW clientes_view AS
SELECT A.CPF, A.Nome, A.Telefone, E.CEP, E.Estado_UF, CI.Nome as Cidade, E.Logradouro, A.Numero
FROM Agente AS A
RIGHT JOIN Cliente AS C
	ON A.CPF = C.Agente_CPF
LEFT JOIN Endereco AS E
	ON E.CEP = A.Endereco_CEP
LEFT JOIN Cidade AS CI
	ON E.Cidade_Nome = CI.Nome;

CREATE VIEW funcionarios_view AS
SELECT A.CPF, A.Nome, A.Telefone, E.CEP, E.Estado_UF, C.Nome as Cidade, E.Logradouro, A.Numero
FROM Agente AS A
RIGHT JOIN Funcionario AS F
	ON A.CPF = F.Agente_CPF
LEFT JOIN Endereco AS E
	ON E.CEP = A.Endereco_CEP
LEFT JOIN Cidade AS C
	ON E.Cidade_Nome = C.Nome;

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

INSERT INTO Cidade(Nome)
VALUES
	("São Paulo"),
    ("Rio de Janeiro"),
    ("Belo Horizonte");

INSERT INTO Endereco(CEP, Logradouro, Cidade_Nome, Estado_UF)
VALUES
	('05089-000', 'R. Guaipá', "São Paulo", 'SP'),
    ('20930-004', 'R. Pref. Olímpio de Melo', "Rio de Janeiro", 'RJ'),
    ('30690-580', 'Av. Flor de Seda', "Belo Horizonte", 'MG');

INSERT INTO Agente(CPF, Nome, Telefone, Endereco_CEP, Numero)
VALUES
	('922.086.734-68', 'São Tomás de Aquino', '(11) 8989-5656', '05089-000', 678),
    ('286.629.180-85', 'Marthin Luther King', '(15) 1279-5256', '20930-004', 1485),
    ('397.824.590-62', 'Nelson Mandela', '(11) 99829-5556', '30690-580', 154);

INSERT INTO Cliente(Agente_CPF)
VALUES
	('922.086.734-68'),
    ('397.824.590-62');

INSERT INTO Funcionario(Agente_CPF)
VALUES
	('286.629.180-85');
    
/* DQL */

SELECT * FROM clientes_view;
SELECT * FROM funcionarios_view;
SELECT * FROM agente_view;
SELECT * FROM Agente;