-- Garry Clawson
-- TSE Group 34 - Team Project
-- Team Software Engineering
-- 2nd February 2020

--
-- Create schema MICE (draft)
-- 

DROP DATABASE IF EXISTS MICE; 

--
-- Creation of data base using correct command
-- 

CREATE DATABASE MICE; 

USE MICE;

--
-- Creation of database tables complete with integrity constraints and language settings
-- 

CREATE TABLE IF NOT EXISTS `mice` (
  `patient_id` varchar(50) NOT NULL,
  `patient_sex` varchar(50) NOT NULL,
  `patient_group` varchar(50) NOT NULL,
  PRIMARY KEY (`patient_id`), 
  KEY `patient_id` (`patient_id`) 
  ) ENGINE=InnoDB DEFAULT CHARSET=latin1; 

CREATE TABLE IF NOT EXISTS `url` (
  `patient_id` varchar(100) NOT NULL,
  `urlString` varchar(100) NOT NULL,
  PRIMARY KEY (`urlString`),
  KEY `patient_id` (`patient_id`),
  KEY `urlString` (`urlString`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `mice` (`patient_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Add Items to mice table
-- 

insert into `mice` (`patient_id`,`patient_sex`,`patient_group`) values 
('AA123456789','F','A'),
('BB123456789','F','A'),
('CC123456789','M','A'),
('DD123456789','M','B'),
('EE123456789','M','B'),
('FF123456789','F','B'),
('GG123456789','F','C'),
('HH123456789','F','C');

--
-- Add Items to url table
-- 

insert into `url` (`patient_id`,`urlString`) values 
('AA123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse1.dcm'),
('BB123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse2.dcm'),
('CC123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse3.dcm'),
('DD123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse4.dcm'),
('EE123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse5.dcm'),
('FF123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse6.dcm'),
('GG123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse7.dcm'),
('HH123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse8.dcm');

--
-- END
-- 



























