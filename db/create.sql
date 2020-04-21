USE EnvelhecaBem;

-- DROP TABLE EnderecosParceiros;
-- DROP TABLE Parceiros;
-- DROP TABLE EnderecosClientes;
-- DROP TABLE Clientes;

CREATE TABLE Parceiros (
	Id						UNIQUEIDENTIFIER NOT NULL,
	Cnpj					VARCHAR(20) NOT NULL,
	RazaoSocial				VARCHAR(120) NOT NULL,
	Contato					VARCHAR(64) NOT NULL,
	Telefone				VARCHAR(14) NOT NULL,
	DataRegistro			DATETIME NOT NULL
);

ALTER TABLE Parceiros ADD CONSTRAINT PK_Parceiros  PRIMARY KEY (Id);
CREATE INDEX UX_Parceiros_Cnpj ON Parceiros (Cnpj);

CREATE TABLE EnderecosParceiros (
	ParceiroId				UNIQUEIDENTIFIER NOT NULL,
	Logradouro				VARCHAR(128) NULL,
	Numero					VARCHAR(8) NULL,
	Complemento				VARCHAR(128) NULL,
	Cidade					VARCHAR(64) NULL,
	Uf						VARCHAR(2) NULL,
	Cep						VARCHAR(8) NULL
);
ALTER TABLE EnderecosParceiros ADD CONSTRAINT PK_EnderecosParceiros  PRIMARY KEY (ParceiroId);
ALTER TABLE EnderecosParceiros ADD CONSTRAINT FK_EnderecosParceiros_Parceiros FOREIGN KEY (ParceiroId) REFERENCES Parceiros(Id);


CREATE TABLE Clientes (
	Id						UNIQUEIDENTIFIER NOT NULL,
	Cpf						VARCHAR(16) NOT NULL,
	Nome					VARCHAR(120) NOT NULL,
	Email					VARCHAR(120) NOT NULL,
	Telefone				VARCHAR(14) NOT NULL,
	Sexo					INT NOT NULL,
	Plano					INT NOT NULL,
	DataNascimento			DATE NOT NULL,
	DataRegistro			DATETIME NOT NULL
);

ALTER TABLE Clientes ADD CONSTRAINT PK_Clientes  PRIMARY KEY (Id);
CREATE INDEX UX_Clientes_Cpf ON Clientes (Cpf); 

CREATE TABLE EnderecosClientes (
	ClienteId				UNIQUEIDENTIFIER NOT NULL,
	Logradouro				VARCHAR(128) NULL,
	Numero					VARCHAR(8) NULL,
	Complemento				VARCHAR(128) NULL,
	Cidade					VARCHAR(64) NULL,
	Uf						VARCHAR(2) NULL,
	Cep						VARCHAR(8) NULL
);

ALTER TABLE EnderecosClientes ADD CONSTRAINT PK_EnderecosClientes  PRIMARY KEY (ClienteId);
ALTER TABLE EnderecosClientes ADD CONSTRAINT FK_EnderecosClientes_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id);