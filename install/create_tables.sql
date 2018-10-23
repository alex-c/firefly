-- This creates the tables used by Firefly. Execute with the 'firefly' user.

CREATE TABLE users (
    id serial PRIMARY KEY,
    name varchar (255) NOT NULL UNIQUE,
    password varchar (255) NOT NULL,
    is_admin boolean DEFAULT false
);

CREATE TABLE accounts (
    id serial PRIMARY KEY,
    name varchar (255) NOT NULL UNIQUE,
    balance real DEFAULT 0
);

CREATE TABLE account_access (
    user_id integer REFERENCES users,
    account_id integer REFERENCES accounts,
    can_see boolean DEFAULT false,
    can_book_transaction boolean DEFAULT false
);

CREATE TABLE transaction_categories (
    id serial PRIMARY KEY,
    account_id integer REFERENCES accounts,
    name varchar (255) NOT NULL
);

CREATE TABLE transactions (
    id UUID,
    value real,
    account_id integer REFERENCES accounts,
    category_id integer REFERENCES transaction_categories,
    created_at timestamptz
);
