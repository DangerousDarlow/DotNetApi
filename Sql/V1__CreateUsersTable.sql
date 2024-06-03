CREATE TABLE users (
    id uuid NOT NULL PRIMARY KEY,
    name character varying(255) NOT NULL
);

INSERT INTO users (id, name) VALUES ('cf3d6664-6b7a-41eb-a223-9dd2121d6ca9', 'Anna');
INSERT INTO users (id, name) VALUES ('a43b04dc-1607-4825-8b45-70b46f5378db', 'Bill');
INSERT INTO users (id, name) VALUES ('9ba58670-47c6-4943-ae8d-785cb4d4c1f7', 'Caia');
INSERT INTO users (id, name) VALUES ('d995b30a-4263-420c-a698-8a3b1f4af4f3', 'Dave');