-- This creates the tables used by Firefly. Execute with the 'firefly' user.

CREATE TABLE users (
    id serial PRIMARY KEY,
    name varchar (255) NOT NULL,
    password varchar (255) NOT NULL
);

CREATE TABLE accounts (
    id serial PRIMARY KEY,
    name varchar (255) NOT NULL,
    balance real DEFAULT 0
);

CREATE TABLE transactions (
    id UUID,
    value real,
    account integer REFERENCES accounts
);
