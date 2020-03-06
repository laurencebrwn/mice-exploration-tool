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
  `urlString` varchar(100) NULL,
  KEY `patient_id` (`patient_id`),
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
('SS123456789','F','Rab18','head','WTSI'),
('AA123456789L2','F','Rab15','body','WTSI'),
('BB123456789L2','F','Rab15','head','TCP'),
('CC123456789L2','M','Rab15','body','ICS'),
('DD123456789L2','M','Rab15','body','ICS'),
('EE123456789L2','M','Rab15','paw','TCP'),
('FF123456789L2','F','Rab15','paw','WTSI'),
('GG123456789L2','F','Rab18','body','ICS'),
('HH123456789L2','F','Rab15','head','WTSI'),
('JJ123456789L2','F','Rab15','head','TCP'),
('KK123456789L2','M','Rab15','head','ICS'),
('LL123456789L2','M','Rab15','body','ICS'),
('MM123456789L2','M','Rab15','body','TCP'),
('NN123456789L2','F','Rab15','paw','WTSI'),
('PP123456789L2','F','Rab18','paw','ICS'),
('RR123456789L2','F','Rab18','body','ICS'),
('SS123456789L2','F','Rab18','head','WTSI');


--
-- Add Items to url table
-- 

insert into `url` (`patient_id`) values 
('AA123456789'),
('BB123456789'),
('CC123456789'),
('DD123456789'),
('EE123456789'),
('FF123456789'),
('GG123456789'),
('HH123456789'),
('JJ123456789'),
('KK123456789'),
('LL123456789'),
('MM123456789'),
('NN123456789'),
('PP123456789'),
('RR123456789'),
('SS123456789'),
('AA123456789L2'),
('BB123456789L2'),
('CC123456789L2'),
('DD123456789L2'),
('EE123456789L2'),
('FF123456789L2'),
('GG123456789L2'),
('HH123456789L2'),
('JJ123456789L2'),
('KK123456789L2'),
('LL123456789L2'),
('MM123456789L2'),
('NN123456789L2'),
('PP123456789L2'),
('RR123456789L2'),
('SS123456789L2');

--
-- END
-- 




























