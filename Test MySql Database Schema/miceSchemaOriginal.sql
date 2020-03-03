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
  `patient_gene` varchar(50) NOT NULL,
  `parameter_name` varchar(100) NOT NULL,
  `phenotyping_center` varchar(50) NOT NULL,
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

insert into `mice` (`patient_id`,`patient_sex`,`patient_gene`,`parameter_name`,`phenotyping_center`) values 
('AA123456789','F','Rab15','body','WTSI'),
('BB123456789','F','Rab15','head','TCP'),
('CC123456789','M','Rab15','body','ICS'),
('DD123456789','M','Rab15','body','ICS'),
('EE123456789','M','Rab15','paw','TCP'),
('FF123456789','F','Rab15','paw','WTSI'),
('GG123456789','F','Rab18','body','ICS'),
('HH123456789','F','Rab15','head','WTSI'),
('JJ123456789','F','Rab15','head','TCP'),
('KK123456789','M','Rab15','head','ICS'),
('LL123456789','M','Rab15','body','ICS'),
('MM123456789','M','Rab15','body','TCP'),
('NN123456789','F','Rab15','paw','WTSI'),
('PP123456789','F','Rab18','paw','ICS'),
('RR123456789','F','Rab18','body','ICS'),
('SS123456789','F','Rab18','head','WTSI');


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
('HH123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse8.dcm'),
('JJ123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse9.dcm'),
('KK123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse10.dcm'),
('LL123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse11.dcm'),
('MM123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse12.dcm'),
('NN123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse13.dcm'),
('PP123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse14.dcm'),
('RR123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse15.dcm'),
('SS123456789','https://raw.githubusercontent.com/18685030/DicomTestImages/master/dicomImages/mouse16.dcm');

--
-- END
-- 


























