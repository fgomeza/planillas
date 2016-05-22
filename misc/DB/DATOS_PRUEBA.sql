
INSERT INTO locations(name, "callPrice", "workingDaysPerMonth", "workingHoursPerDay", active) VALUES ( 'Tibas', 550,24,8, true);
INSERT INTO locations(name, "callPrice", "workingDaysPerMonth", "workingHoursPerDay", active) VALUES ( 'San Pedro', 400,24,8, true);

INSERT INTO groups VALUES ('locations', 'Sedes', '');
INSERT INTO groups VALUES ('employees', 'Empleados', '');
INSERT INTO groups VALUES ('debits', 'Débitos', '');
INSERT INTO groups VALUES ('penalty', 'Penalizaciones', '');
INSERT INTO groups VALUES ('extras', 'extras', '');
INSERT INTO groups VALUES ('users', 'Usuarios', '');
INSERT INTO groups VALUES ('roles', 'roles', '');
INSERT INTO groups VALUES ('payroll', 'Planillas', '');
INSERT INTO groups VALUES ('debittypes', 'Tipos de Débitos', '');


INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated") VALUES ('locations/add', 'Agregar', 'locations', false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('locations/modify', 'Modificar', 'locations',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('locations/get', 'Ver', 'locations',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('locations/remove', 'Eliminar', 'locations',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('locations/activate', 'Activar', 'locations',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('employees/add', 'Agregar', 'employees',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('employees/modify', 'Modificar', 'employees',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('employees/get', 'Ver', 'employees',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('employees/remove', 'Eliminar', 'employees',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('employees/activate', 'Activar', 'employees',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debits/add', 'Agregar', 'debits',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debits/modify', 'Modificar', 'debits',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debits/get', 'Ver', 'debits',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debits/remove', 'Eliminar', 'debits',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debits/activate', 'Activar', 'debits',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('penalty/add', 'Agregar', 'penalty',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('penalty/modify', 'Modificar', 'penalty',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('penalty/get', 'Ver', 'penalty',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('penalty/remove', 'Eliminar', 'penalty',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('extras/add', 'Agregar', 'extras',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('extras/modify', 'Modificar', 'extras',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('extras/get', 'Ver', 'extras',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('extras/remove', 'Eliminar', 'extras',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('users/add', 'Agregar', 'users',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('users/modify', 'Modificar', 'users',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('users/get', 'Ver', 'users',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('users/remove', 'Eliminar', 'users',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('users/activate', 'Activar', 'users',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('roles/add', 'Agregar', 'roles',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('roles/modify', 'Modificar', 'roles',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('roles/get', 'Ver', 'roles',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('roles/remove', 'Eliminar', 'roles',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('roles/activate', 'Activar', 'roles',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('payroll/calculate', 'Calcular', 'payroll',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('payroll/aprove', 'Aprovar', 'payroll',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('payroll/get', 'Ver', 'payroll',false);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debittypes/add', 'Agregar', 'debittypes',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debittypes/modify', 'Modificar', 'debittypes',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debittypes/get', 'Ver', 'debittypes',true);
INSERT INTO operations(name, "Description", group_id, "isPayrollCalculationRelated")  VALUES ('debittypes/remove', 'Eliminar', 'debittypes',true);


INSERT INTO roles(name,location,active) VALUES ( 'ADMIN',1, true);
INSERT INTO roles(name,location,active) VALUES ( 'payrollER',1, true);
INSERT INTO roles(name,location,active) VALUES ( 'ADMIN2',2, true);

INSERT INTO users(name,email,"userName",password,active,role,location) VALUES ( 'Admin', 'admin@mobilize.net', 'admin', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);
INSERT INTO users(name,email,"userName",password,active,role,location) VALUES ( 'Jonnathan Ch', 'jcharpentier@mobilize.net', 'jonnch', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);
INSERT INTO users(name,email,"userName",password,active,role,location) VALUES ( 'Jafet Román', 'jafet21@hotmail.es', 'tutox', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 2, 1);
INSERT INTO users(name,email,"userName",password,active,role,location) VALUES ( 'Francisco Gomez', 'fgomeza25@gmail.com', 'fgomez', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 2, 1);
INSERT INTO users(name,email,"userName",password,active,role,location) VALUES ( 'Oscar Chaves', 'andresch1009@gmail.com', 'oscar', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 3, 2);
INSERT INTO users(name,email,"userName",password,active,role,location) VALUES ( 'No me acuerdo su nombre', 'nomeacuerdosunombre@gmail.com', 'nomeacuerdo', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);


INSERT INTO "RoleOperations" VALUES ('locations/add', 1);
INSERT INTO "RoleOperations" VALUES ('locations/modify', 1);
INSERT INTO "RoleOperations" VALUES ('locations/get', 1);
INSERT INTO "RoleOperations" VALUES ('locations/remove', 1);
INSERT INTO "RoleOperations" VALUES ('locations/activate', 1);
INSERT INTO "RoleOperations" VALUES ('employees/add', 1);
INSERT INTO "RoleOperations" VALUES ('employees/modify', 1);
INSERT INTO "RoleOperations" VALUES ('employees/get', 1);
INSERT INTO "RoleOperations" VALUES ('employees/remove', 1);
INSERT INTO "RoleOperations" VALUES ('employees/activate', 1);
INSERT INTO "RoleOperations" VALUES ('debits/add', 1);
INSERT INTO "RoleOperations" VALUES ('debits/modify', 1);
INSERT INTO "RoleOperations" VALUES ('debits/get', 1);
INSERT INTO "RoleOperations" VALUES ('debits/remove', 1);
INSERT INTO "RoleOperations" VALUES ('debits/activate', 1);
INSERT INTO "RoleOperations" VALUES ('penalty/add', 1);
INSERT INTO "RoleOperations" VALUES ('penalty/modify', 1);
INSERT INTO "RoleOperations" VALUES ('penalty/get', 1);
INSERT INTO "RoleOperations" VALUES ('penalty/remove', 1);
INSERT INTO "RoleOperations" VALUES ('extras/add', 1);
INSERT INTO "RoleOperations" VALUES ('extras/modify', 1);
INSERT INTO "RoleOperations" VALUES ('extras/get', 1);
INSERT INTO "RoleOperations" VALUES ('extras/remove', 1);
INSERT INTO "RoleOperations" VALUES ('users/add', 1);
INSERT INTO "RoleOperations" VALUES ('users/modify', 1);
INSERT INTO "RoleOperations" VALUES ('users/get', 1);
INSERT INTO "RoleOperations" VALUES ('users/remove', 1);
INSERT INTO "RoleOperations" VALUES ('users/activate', 1);
INSERT INTO "RoleOperations" VALUES ('roles/add', 1);
INSERT INTO "RoleOperations" VALUES ('roles/modify', 1);
INSERT INTO "RoleOperations" VALUES ('roles/get', 1);
INSERT INTO "RoleOperations" VALUES ('roles/remove', 1);
INSERT INTO "RoleOperations" VALUES ('roles/activate', 1);
INSERT INTO "RoleOperations" VALUES ('payroll/calculate', 1);
INSERT INTO "RoleOperations" VALUES ('payroll/aprove', 1);
INSERT INTO "RoleOperations" VALUES ('payroll/get', 1);
INSERT INTO "RoleOperations" VALUES ('debittypes/add', 1);
INSERT INTO "RoleOperations" VALUES ('debittypes/modify', 1);
INSERT INTO "RoleOperations" VALUES ('debittypes/get', 1);
INSERT INTO "RoleOperations" VALUES ('debittypes/remove', 1);
INSERT INTO "RoleOperations" VALUES ('employees/add', 2);
INSERT INTO "RoleOperations" VALUES ('employees/modify', 2);
INSERT INTO "RoleOperations" VALUES ('employees/get', 2);
INSERT INTO "RoleOperations" VALUES ('employees/remove', 2);
INSERT INTO "RoleOperations" VALUES ('employees/activate', 2);
INSERT INTO "RoleOperations" VALUES ('debits/add', 2);
INSERT INTO "RoleOperations" VALUES ('debits/modify', 2);
INSERT INTO "RoleOperations" VALUES ('debits/get', 2);
INSERT INTO "RoleOperations" VALUES ('debits/remove', 2);
INSERT INTO "RoleOperations" VALUES ('debits/activate', 2);
INSERT INTO "RoleOperations" VALUES ('penalty/add', 2);
INSERT INTO "RoleOperations" VALUES ('penalty/modify', 2);
INSERT INTO "RoleOperations" VALUES ('penalty/get', 2);
INSERT INTO "RoleOperations" VALUES ('penalty/remove', 2);
INSERT INTO "RoleOperations" VALUES ('extras/add', 2);
INSERT INTO "RoleOperations" VALUES ('extras/modify', 2);
INSERT INTO "RoleOperations" VALUES ('extras/get', 2);
INSERT INTO "RoleOperations" VALUES ('extras/remove', 2);
INSERT INTO "RoleOperations" VALUES ('payroll/calculate', 2);
INSERT INTO "RoleOperations" VALUES ('payroll/aprove', 2);
INSERT INTO "RoleOperations" VALUES ('payroll/get', 2);
INSERT INTO "RoleOperations" VALUES ('debittypes/add', 2);
INSERT INTO "RoleOperations" VALUES ('debittypes/modify', 2);
INSERT INTO "RoleOperations" VALUES ('debittypes/get', 2);
INSERT INTO "RoleOperations" VALUES ('debittypes/remove', 2);
INSERT INTO "RoleOperations" VALUES ('locations/add', 3);
INSERT INTO "RoleOperations" VALUES ('locations/modify', 3);
INSERT INTO "RoleOperations" VALUES ('locations/get', 3);
INSERT INTO "RoleOperations" VALUES ('locations/remove', 3);
INSERT INTO "RoleOperations" VALUES ('locations/activate', 3);
INSERT INTO "RoleOperations" VALUES ('employees/add', 3);
INSERT INTO "RoleOperations" VALUES ('employees/modify', 3);
INSERT INTO "RoleOperations" VALUES ('employees/get', 3);
INSERT INTO "RoleOperations" VALUES ('employees/remove', 3);
INSERT INTO "RoleOperations" VALUES ('employees/activate', 3);
INSERT INTO "RoleOperations" VALUES ('debits/add', 3);
INSERT INTO "RoleOperations" VALUES ('debits/modify', 3);
INSERT INTO "RoleOperations" VALUES ('debits/get', 3);
INSERT INTO "RoleOperations" VALUES ('debits/remove', 3);
INSERT INTO "RoleOperations" VALUES ('debits/activate', 3);
INSERT INTO "RoleOperations" VALUES ('penalty/add', 3);
INSERT INTO "RoleOperations" VALUES ('penalty/modify', 3);
INSERT INTO "RoleOperations" VALUES ('penalty/get', 3);
INSERT INTO "RoleOperations" VALUES ('penalty/remove', 3);
INSERT INTO "RoleOperations" VALUES ('extras/add', 3);
INSERT INTO "RoleOperations" VALUES ('extras/modify', 3);
INSERT INTO "RoleOperations" VALUES ('extras/get', 3);
INSERT INTO "RoleOperations" VALUES ('extras/remove', 3);
INSERT INTO "RoleOperations" VALUES ('users/add', 3);
INSERT INTO "RoleOperations" VALUES ('users/modify', 3);
INSERT INTO "RoleOperations" VALUES ('users/get', 3);
INSERT INTO "RoleOperations" VALUES ('users/remove', 3);
INSERT INTO "RoleOperations" VALUES ('users/activate', 3);
INSERT INTO "RoleOperations" VALUES ('roles/add', 3);
INSERT INTO "RoleOperations" VALUES ('roles/modify', 3);
INSERT INTO "RoleOperations" VALUES ('roles/get', 3);
INSERT INTO "RoleOperations" VALUES ('roles/remove', 3);
INSERT INTO "RoleOperations" VALUES ('roles/activate', 3);
INSERT INTO "RoleOperations" VALUES ('payroll/calculate', 3);
INSERT INTO "RoleOperations" VALUES ('payroll/aprove', 3);
INSERT INTO "RoleOperations" VALUES ('payroll/get', 3);
INSERT INTO "RoleOperations" VALUES ('debittypes/add', 3);
INSERT INTO "RoleOperations" VALUES ('debittypes/modify', 3);
INSERT INTO "RoleOperations" VALUES ('debittypes/get', 3);
INSERT INTO "RoleOperations" VALUES ('debittypes/remove', 3);

select * from employees;
--Employees
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES ( 'Adrián Antonio Marín Sosa', 'Tibas' ,'114730338', 'ADRIAN MARIN',TRUE, 1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Alexander Morales Caballero','San Pedro',  '114370688', 'ALEXANDER MORALES',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Arturo Clark Bustos', '108180394','Tibas' , 'ARTURO CLARK',TRUE, 1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Cheril Eithel Fonseca Picado','Tibas' , '206100860', 'CHERIL FONSECA',TRUE, 1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Danny Alberto Naranjo Obando', 'Tibas' ,'110190812', 'DANNY NARANJO',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Douglas Ariel Salazar Salazar','San Pedro' , '113400556', 'DOUGLAS SALAZAR',TRUE, 1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Ever Román Rivera Suarez','Tibas' , '113270626', 'EVER RIVERA',TRUE, 1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Fabián Andrés Arias Leiva','Tibas' , '112500502', 'FABIAN ARIAS',TRUE, 1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Freddy José Enriquez Baltodano','Tibas' , '116710714', 'FREDDY ENRIQUEZ',TRUE, 1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Freddy Manuel Mejía Gómez','Tibas' , '503180033', 'FREDDY MEJIA',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Geovanny Ordoñez Ugalde', 'Tibas' ,'503790644', 'GEOVANNY ORDONEZ',TRUE, 1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Greivin Alfredo Guido Bustos', 'Tibas' ,'402170742', 'GREIVIN GUIDO',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Hadhit Sibaja Montero','Tibas' , '112300627', NULL,TRUE, 1,253687.50,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Hallen Gisselle Sánchez Vargas','Tibas' , '402020812', 'HALLEN SANCHEZ',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Haydee Delgado Briceño','Tibas' , '112970129', 'HAYDEE DELGADO',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Hellen Dayan Mendoza Fernández', 'San Pedro', '116370910', 'HELLEN MENDOZA',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Javier Quesada Cruz','Tibas' , '603310318', 'JAVIER QUESADA',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Jeimy Tatiana Picado Sánchez','Tibas' ,'111310525', 'JEIMY PICADO',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'John Byron Márquez Chicas','Tibas' , '800690616', 'JOHN MARQUEZ',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Jorge Antonio Coto Ortega','Tibas' , '111400658', NULL,TRUE, 1,304405.17,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'José Fallas Robles','Tibas' , '115160254', 'JOSE FALLAS.',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'José Salas Rodríguez','Tibas' , '108390007', 'JOSE SALAS',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'José Vega Fallas', 'Tibas' ,'110080377', 'JOSE VEGA',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Joselyn Vidaurre Bravo','Tibas' , '701700182', 'JOSELYN VIDAURRE',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
VALUES( 'Juan Gabriel Mora Araya','Tibas' , '206840920', 'JUAN MORA',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Karol Rebeca Pérez Abarca', 'San Pedro', '114040722', 'KAROL PEREZ',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Katherine Barrios Valenciano','Tibas' , '112660130', NULL,TRUE,  1,304405.17,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Leonardo Rodríguez Carrión','Tibas' , '800870767', NULL,TRUE,  1,304405.17,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Luis Diego Zamora Olmedo','Tibas' , '112170982', 'LUIS ZAMORA',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'Magaly Fernández Trejos','Tibas' , '112900696', 'MAGALY FERNANDEZ',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Manuel Valverde Morales', 'Tibas' ,'113500037', 'MANUEL VALVERDE',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Marcela Barrientos Castro','Tibas' , '206780817', 'MARCELA BARRIENTOS',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'Marcial García Schmidt','Tibas' , '114620341', 'MARCIAL GARCIA',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'Marco Vinicio Uribe Morelli','San Pedro',  '112830671', 'MARCO URIBE',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'María Briceño Quirós', 'Tibas' ,'502200745', 'MARIA BRICEÑO',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'María Castillo Ramírez','Tibas' , '107670112', 'MARIA CASTILLO',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'Mario Botto Sibaja', 'Tibas' ,'502930659', 'MARIA BOTTO',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'Marvin Leiton Sánchez', 'Tibas' ,'107620063', NULL,TRUE,  1,304405.17,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Michael Vásquez Calderón','Tibas' , '112220657', NULL,TRUE,  1,304405.17,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'Oscar Arnulfo Alvarenga Torres','San Pedro',  '122200557831', NULL,TRUE,  1,304405.17,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'Paola Vargas Carvajal','Tibas' , '112590969', 'PAOLA VARGAS',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Pedro Rojas Duran','Tibas' ,'205280848', 'PEDRO ROJAS',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Priscilla María Sibaja Porras','Tibas' , '112540130', 'PRISCILA SIBAJA',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Randall Abarca Hernández','Tibas' , '401840992', 'RANDAL ABARCA',TRUE,  1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince")
 VALUES( 'Raymond Sandoval Fernández','Tibas' , '112620423', 'RAYMOND SANDOVAL',TRUE,   1,0,'001-149541-0',0,0,0, current_date - interval '15 days');
INSERT INTO employees(name, location, "idCard", cms,active, "locationId",  salary, account, "negativeAmount", "avalaibleVacations", "workedDays", "activeSince") 
VALUES( 'Scarlet Tayrin Porras Jiménez', 'San Pedro', '702090625', NULL,TRUE,  1,304405.17,'001-149541-0',0,0,0, current_date - interval '15 days');

--Administrators
INSERT INTO Administrators values (2, 1);
INSERT INTO Administrators values (5, 2);
select users.id, users.name, locations.name from users, locations, administrators where users.id = administrators.user_id and administrators.location = locations.id;


--Calls
INSERT INTO Calls values (1, '2016-04-01', 20, '6:00', NULL);
INSERT INTO Calls values (1, '2016-04-02', 16, '3:00', NULL);
INSERT INTO Calls values (1, '2016-04-03', 15, '4:00', NULL);
INSERT INTO Calls values (1, '2016-04-04', 10, '2:30', NULL);
INSERT INTO Calls values (2, '2016-04-01', 15, '4:15',NULL);
INSERT INTO Calls values (2, '2016-04-15', 7, '2:15',NULL);
INSERT INTO Calls values (2, '2016-04-16', 18, '3:45',NULL);
INSERT INTO Calls values (3, '2016-04-30', 5, '2:02', NULL);
INSERT INTO Calls values (4, '2016-04-30', 7, '3:10', NULL);
INSERT INTO Calls values (5, '2016-04-30', 16, '5:32', NULL);
INSERT INTO Calls values (6, '2016-04-30', 23, '6:52', NULL);
INSERT INTO Calls values (7, '2016-04-30', 20, '6:00', NULL);
INSERT INTO Calls values (8, '2016-04-30', 15, '4:15',NULL);
INSERT INTO Calls values (9, '2016-04-30', 5, '2:02', NULL);
INSERT INTO Calls values (10, '2016-04-30', 7, '3:10', NULL);
INSERT INTO Calls values (11, '2016-04-30', 16, '5:32', NULL);
INSERT INTO Calls values (12, '2016-04-30', 23, '6:52', NULL);
INSERT INTO Calls values (14, '2016-04-30', 20, '6:00', NULL);
INSERT INTO Calls values (15, '2016-04-30', 15, '4:15', NULL);
INSERT INTO Calls values (16, '2016-04-30', 5, '2:02', NULL);
INSERT INTO Calls values (17, '2016-04-30', 7, '3:10', NULL);
INSERT INTO Calls values (18, '2016-04-30', 16, '5:32', NULL);
INSERT INTO Calls values (19, '2016-04-30', 23, '6:52', NULL);
INSERT INTO Calls values (21, '2016-04-30', 20, '6:00', NULL);
INSERT INTO Calls values (22, '2016-04-30', 15, '4:15', NULL);
INSERT INTO Calls values (23, '2016-04-30', 5, '2:02', NULL);
INSERT INTO Calls values (24, '2016-04-30', 7, '3:10', NULL);
INSERT INTO Calls values (25, '2016-04-30', 16, '5:32', NULL);
INSERT INTO Calls values (26, '2016-04-30', 23, '6:52', NULL);
INSERT INTO Calls values (29, '2016-04-30', 23, '6:52', NULL);
INSERT INTO Calls values (30, '2016-04-30', 20, '6:00', NULL);
INSERT INTO Calls values (31, '2016-04-30', 15, '4:15', NULL);
INSERT INTO Calls values (32, '2016-04-30', 5, '2:02', NULL);
INSERT INTO Calls values (33, '2016-04-30', 7, '3:10', NULL);
INSERT INTO Calls values (34, '2016-04-30', 16, '5:32', NULL);
INSERT INTO Calls values (35, '2016-04-30', 23, '6:52', NULL);
INSERT INTO Calls values (36, '2016-04-30', 23, '6:52', NULL);
INSERT INTO Calls values (37, '2016-04-30', 23, '6:52', NULL);
INSERT INTO Calls values (41, '2016-04-30', 20, '6:00', NULL);
INSERT INTO Calls values (42, '2016-04-30', 15, '4:15', NULL);
INSERT INTO Calls values (43, '2016-04-30', 5, '2:02', NULL);
INSERT INTO Calls values (44, '2016-04-30', 7, '3:10', NULL);
INSERT INTO Calls values (45, '2016-04-30', 16, '5:32', NULL);
INSERT INTO Calls values (46, '2016-04-30', 23, '6:52', NULL);

--Penalty_Types
INSERT INTO Penalty_Types(name, price, location) 
	values ('Erroneas', 500, 1);
INSERT INTO Penalty_Types(name, price, location) 
	values ('CIBEL', 1000, 1);
--Penalties

INSERT INTO Penalties("Description", employee, penalty_type, "Amount", "PenaltyPrice", "Date") 
	values ('Penalty', 3, 1, 4, 500, '2016-04-12');
INSERT INTO Penalties("Description", employee, penalty_type, "Amount", "PenaltyPrice", "Date") 
	values ('Penalty', 6, 2, 2, 1000, '2016-04-04');
INSERT INTO Penalties("Description", employee, penalty_type, "Amount", "PenaltyPrice", "Date")  
	values ('Penalty', 4, 1, 3, 500, '2016-04-15');
INSERT INTO Penalties("Description", employee, penalty_type, "Amount", "PenaltyPrice", "Date") 
	values ('Penalty', 9, 2, 1, 1000, '2016-04-08');
INSERT INTO Penalties("Description", employee, penalty_type, "Amount", "PenaltyPrice", "Date") 
	values ('Penalty', 2, 2, 7, 1000, '2016-04-07');

--Extras
INSERT INTO Extras(employee, description, hours,date) values (13, 'Sin comentarios', 2,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (20, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (27, 'Sin comentarios', 4,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (28, 'Sin comentarios', 3,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (2, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (3, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (4, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (5, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (6, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (7, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (8, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (9, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (10, 'Sin comentarios', 6,current_date - interval '5 days');
INSERT INTO Extras(employee, description, hours,date) values (11, 'Sin comentarios', 6,current_date - interval '5 days');


--Savings
INSERT INTO Savings values (3, 100000);
INSERT INTO Savings values (6, 16000);
INSERT INTO Savings values (13, 10506);
INSERT INTO Savings values (9, 11000);
INSERT INTO Savings values (2, 140000);
INSERT INTO Savings values (27, 254300);
INSERT INTO Savings values (25, 120000);

--Debit_Types
 INSERT into Debit_Types (name, pays,period, "interestRate", location, type)
	values ('Amortization Debit', 12,15, 15, 1, 'A');
 INSERT into Debit_Types (name, pays,period, "interestRate", location, type)
	values ('Payment Debit 5%', 12,15, 5, 1, 'P');
INSERT into Debit_Types (name, pays,period, "interestRate", location, type)
	values ('Payment Debit 2%', 12,15, 2, 1, 'P');
 INSERT into Debit_Types (name, pays,period, "interestRate", location, type)
	values ('Fixed Debit', 0,15, 0, 1, 'F');

--Debits
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 25, 1000000, 1000000, 12, 0, 1, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 1, 1000000, 1000000, 12, 0, 1, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 4, 1000000, 1000000, 12, 0, 1, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 5, 500000, 500000, 6, 0, 2, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 6, 500000, 500000, 6, 0, 2, true,current_date - interval '15 days',15,0);

INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 7, 5000, 0, 0, 0, 4, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 8, 5000, 0, 0, 0, 4, true,current_date - interval '15 days',30,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 9, 50000, 50000, 1, 0,3, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 10, 1000000, 1000000, 12, 0, 2, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 11, 2000000, 2000000, 24, 0, 2, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 12, 5000, 0, 0, 0, 4, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 13, 5000, 0, 0, 0, 4, true,current_date - interval '15 days',30,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 14, 50000, 50000, 1, 0,3, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 15, 1000000, 1000000, 12, 0, 2, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 17, 2000000, 2000000, 24, 0, 2, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 18, 5000, 0, 0, 0, 4, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 19, 5000, 0, 0, 0, 4, true,current_date - interval '15 days',30,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 20, 50000, 50000, 1, 0,3, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 21, 1000000, 1000000, 12, 0, 2, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 22, 2000000, 2000000, 24, 0, 2, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 23, 5000, 0, 0, 0, 4, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 24, 5000, 0, 0, 0, 4, true,current_date - interval '15 days',30,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 25, 50000, 50000, 1, 0,3, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 26, 1000000, 1000000, 12, 0, 2, true,current_date - interval '15 days',15,0);
INSERT INTO Debits ("initialDate", description, "employeeId", "totalAmount", "remainingAmount", "remainingPays", "paysMade", type, active,"activeSince", period, "pastDays")
	values(current_date - interval '15 days', 'Sin comentarios', 27, 2000000, 2000000, 24, 0, 2, true,current_date - interval '15 days',15,0);





