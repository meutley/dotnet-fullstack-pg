-- Role: DatabaseName_application
-- DROP ROLE DatabaseName_application;

CREATE ROLE SourceName_application WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  CREATEDB
  NOCREATEROLE
  NOREPLICATION;


-- Database: DatabaseName
-- DROP DATABASE DatabaseName;

CREATE DATABASE DatabaseName
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.UTF-8'
    LC_CTYPE = 'en_US.UTF-8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

GRANT ALL ON DATABASE DatabaseName TO postgres;

GRANT TEMPORARY ON DATABASE DatabaseName TO DatabaseName_application;

GRANT TEMPORARY, CONNECT ON DATABASE DatabaseName TO PUBLIC;


-- Table: application_user
-- DROP TABLE application_user;

CREATE TABLE application_user (
    id              SERIAL       PRIMARY KEY,
    first_name      VARCHAR(50)  NOT NULL,
    last_name       VARCHAR(50)  NOT NULL,
    username        VARCHAR(255) NOT NULL,
    password_hash   BYTEA        NOT NULL,
    password_salt   BYTEA        NOT NULL,
    is_active       BOOLEAN      NOT NULL DEFAULT TRUE
);

CREATE UNIQUE INDEX uq_ix_application_user_username
ON application_user(username);

-- Table: application_role
-- DROP TABLE application_role;

CREATE TABLE application_role (
    id              SERIAL       PRIMARY KEY,
    name            VARCHAR(50)  NOT NULL,
    description     VARCHAR(255) NOT NULL
);


-- Table: application_user_role
-- DROP TABLE application_user_role;

CREATE TABLE application_user_role (
    id                  SERIAL PRIMARY KEY,
    application_user_id INT    NOT NULL,
    application_role_id INT    NOT NULL,
    FOREIGN KEY (application_user_id) REFERENCES application_user(id),
    FOREIGN KEY (application_role_id) REFERENCES application_role(id)
);

CREATE UNIQUE INDEX uq_ix_application_user_role_user_id_role_id ON application_user_role(application_user_id, application_role_id);


GRANT ALL PRIVILEGES ON ALL TABLES    IN SCHEMA public TO DatabaseName_application;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO DatabaseName_application;
