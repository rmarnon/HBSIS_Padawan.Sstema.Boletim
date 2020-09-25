IF OBJECT_ID(N'[Boletim_Migrations_History]') IS NULL
BEGIN
    CREATE TABLE [Boletim_Migrations_History] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_Boletim_Migrations_History] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Cursos] (
    [Id] bigint NOT NULL IDENTITY,
    [Nome] Varchar(50) NOT NULL,
    [Situacao] nvarchar(max) NOT NULL DEFAULT N'Ativo',
    CONSTRAINT [PK_Cursos] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Materias] (
    [Id] bigint NOT NULL IDENTITY,
    [Nome] Varchar(50) NOT NULL,
    [Descricao] TEXT NOT NULL,
    [Cadastro] datetime2 NOT NULL DEFAULT (GETDATE()),
    [Status] nvarchar(max) NOT NULL DEFAULT N'Ativo',
    CONSTRAINT [PK_Materias] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Usuarios] (
    [Id] bigint NOT NULL IDENTITY,
    [Login] nvarchar(50) NOT NULL,
    [Senha] nvarchar(max) NOT NULL,
    [Tipo] int NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Alunos] (
    [Id] bigint NOT NULL IDENTITY,
    [CursoId] bigint NOT NULL,
    [Nome] nvarchar(20) NOT NULL,
    [Sobrenome] nvarchar(20) NOT NULL,
    [Cpf] bigint NOT NULL,
    [Nascimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Alunos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Alunos_Cursos_CursoId] FOREIGN KEY ([CursoId]) REFERENCES [Cursos] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CursoMateria] (
    [CursoId] bigint NOT NULL,
    [MateriaId] bigint NOT NULL,
    [Id] bigint NOT NULL,
    CONSTRAINT [PK_CursoMateria] PRIMARY KEY ([CursoId], [MateriaId]),
    CONSTRAINT [AK_CursoMateria_Id] UNIQUE ([Id]),
    CONSTRAINT [FK_CursoMateria_Cursos_CursoId] FOREIGN KEY ([CursoId]) REFERENCES [Cursos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CursoMateria_Materias_MateriaId] FOREIGN KEY ([MateriaId]) REFERENCES [Materias] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [CursoMateriaAluno] (
    [CursoMateriaId] bigint NOT NULL,
    [AlunoId] bigint NOT NULL,
    [Nota] float NOT NULL,
    CONSTRAINT [PK_CursoMateriaAluno] PRIMARY KEY ([AlunoId], [CursoMateriaId]),
    CONSTRAINT [FK_CursoMateriaAluno_Alunos_AlunoId] FOREIGN KEY ([AlunoId]) REFERENCES [Alunos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CursoMateriaAluno_CursoMateria_CursoMateriaId] FOREIGN KEY ([CursoMateriaId]) REFERENCES [CursoMateria] ([Id])
);

GO

CREATE INDEX [IX_Alunos_CursoId] ON [Alunos] ([CursoId]);

GO

CREATE INDEX [IX_CursoMateria_MateriaId] ON [CursoMateria] ([MateriaId]);

GO

CREATE INDEX [IX_CursoMateriaAluno_CursoMateriaId] ON [CursoMateriaAluno] ([CursoMateriaId]);

GO

INSERT INTO [Boletim_Migrations_History] ([MigrationId], [ProductVersion])
VALUES (N'20200925184517_FirstMigration', N'3.1.8');

GO

