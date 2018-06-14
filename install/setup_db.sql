DROP DATABASE IF EXISTS firefly;
DROP ROLE IF EXISTS firefly;
CREATE ROLE firefly LOGIN PASSWORD 'password'; --change 'password' to desired password
CREATE DATABASE firefly OWNER firefly;
