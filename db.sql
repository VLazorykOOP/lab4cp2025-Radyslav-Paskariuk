CREATE DATABASE IF NOT EXISTS SoftAlcoholProduction;
USE SoftAlcoholProduction;

CREATE TABLE Drinks (
    id INT AUTO_INCREMENT PRIMARY KEY,
    type VARCHAR(50),             
    brand VARCHAR(100),           
    manufacturer VARCHAR(100),     
    supplier VARCHAR(100),        
    expiration_date DATE,        
    price DECIMAL(10, 2)          
);
