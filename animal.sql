CREATE DATABASE animal_shelter;
USE animal_shelter;


CREATE TABLE animal (name VARCHAR (255), sex VARCHAR (10), breed VARCHAR (255), dateofadmittance DATETIME);
ALTER TABLE animal ADD id serial PRIMARY KEY;
INSERT INTO animal (name, sex, breed, dateofadmittance) VALUES ('Test', 'Male', 'Dog', '2019-02-05');
