
INSERT INTO locations(name,"callPrice",active) VALUES ( 'Tibas', 550, true);
INSERT INTO locations(name,"callPrice",active) VALUES ( 'San Pedro', 400, true);

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


INSERT INTO "RoleOperation" VALUES ('locations/add', 1);
INSERT INTO "RoleOperation" VALUES ('locations/modify', 1);
INSERT INTO "RoleOperation" VALUES ('locations/get', 1);
INSERT INTO "RoleOperation" VALUES ('locations/remove', 1);
INSERT INTO "RoleOperation" VALUES ('locations/activate', 1);
INSERT INTO "RoleOperation" VALUES ('employees/add', 1);
INSERT INTO "RoleOperation" VALUES ('employees/modify', 1);
INSERT INTO "RoleOperation" VALUES ('employees/get', 1);
INSERT INTO "RoleOperation" VALUES ('employees/remove', 1);
INSERT INTO "RoleOperation" VALUES ('employees/activate', 1);
INSERT INTO "RoleOperation" VALUES ('debits/add', 1);
INSERT INTO "RoleOperation" VALUES ('debits/modify', 1);
INSERT INTO "RoleOperation" VALUES ('debits/get', 1);
INSERT INTO "RoleOperation" VALUES ('debits/remove', 1);
INSERT INTO "RoleOperation" VALUES ('debits/activate', 1);
INSERT INTO "RoleOperation" VALUES ('penalty/add', 1);
INSERT INTO "RoleOperation" VALUES ('penalty/modify', 1);
INSERT INTO "RoleOperation" VALUES ('penalty/get', 1);
INSERT INTO "RoleOperation" VALUES ('penalty/remove', 1);
INSERT INTO "RoleOperation" VALUES ('extras/add', 1);
INSERT INTO "RoleOperation" VALUES ('extras/modify', 1);
INSERT INTO "RoleOperation" VALUES ('extras/get', 1);
INSERT INTO "RoleOperation" VALUES ('extras/remove', 1);
INSERT INTO "RoleOperation" VALUES ('users/add', 1);
INSERT INTO "RoleOperation" VALUES ('users/modify', 1);
INSERT INTO "RoleOperation" VALUES ('users/get', 1);
INSERT INTO "RoleOperation" VALUES ('users/remove', 1);
INSERT INTO "RoleOperation" VALUES ('users/activate', 1);
INSERT INTO "RoleOperation" VALUES ('roles/add', 1);
INSERT INTO "RoleOperation" VALUES ('roles/modify', 1);
INSERT INTO "RoleOperation" VALUES ('roles/get', 1);
INSERT INTO "RoleOperation" VALUES ('roles/remove', 1);
INSERT INTO "RoleOperation" VALUES ('roles/activate', 1);
INSERT INTO "RoleOperation" VALUES ('payroll/calculate', 1);
INSERT INTO "RoleOperation" VALUES ('payroll/aprove', 1);
INSERT INTO "RoleOperation" VALUES ('payroll/get', 1);
INSERT INTO "RoleOperation" VALUES ('debittypes/add', 1);
INSERT INTO "RoleOperation" VALUES ('debittypes/modify', 1);
INSERT INTO "RoleOperation" VALUES ('debittypes/get', 1);
INSERT INTO "RoleOperation" VALUES ('debittypes/remove', 1);
INSERT INTO "RoleOperation" VALUES ('employees/add', 2);
INSERT INTO "RoleOperation" VALUES ('employees/modify', 2);
INSERT INTO "RoleOperation" VALUES ('employees/get', 2);
INSERT INTO "RoleOperation" VALUES ('employees/remove', 2);
INSERT INTO "RoleOperation" VALUES ('employees/activate', 2);
INSERT INTO "RoleOperation" VALUES ('debits/add', 2);
INSERT INTO "RoleOperation" VALUES ('debits/modify', 2);
INSERT INTO "RoleOperation" VALUES ('debits/get', 2);
INSERT INTO "RoleOperation" VALUES ('debits/remove', 2);
INSERT INTO "RoleOperation" VALUES ('debits/activate', 2);
INSERT INTO "RoleOperation" VALUES ('penalty/add', 2);
INSERT INTO "RoleOperation" VALUES ('penalty/modify', 2);
INSERT INTO "RoleOperation" VALUES ('penalty/get', 2);
INSERT INTO "RoleOperation" VALUES ('penalty/remove', 2);
INSERT INTO "RoleOperation" VALUES ('extras/add', 2);
INSERT INTO "RoleOperation" VALUES ('extras/modify', 2);
INSERT INTO "RoleOperation" VALUES ('extras/get', 2);
INSERT INTO "RoleOperation" VALUES ('extras/remove', 2);
INSERT INTO "RoleOperation" VALUES ('payroll/calculate', 2);
INSERT INTO "RoleOperation" VALUES ('payroll/aprove', 2);
INSERT INTO "RoleOperation" VALUES ('payroll/get', 2);
INSERT INTO "RoleOperation" VALUES ('debittypes/add', 2);
INSERT INTO "RoleOperation" VALUES ('debittypes/modify', 2);
INSERT INTO "RoleOperation" VALUES ('debittypes/get', 2);
INSERT INTO "RoleOperation" VALUES ('debittypes/remove', 2);
INSERT INTO "RoleOperation" VALUES ('locations/add', 3);
INSERT INTO "RoleOperation" VALUES ('locations/modify', 3);
INSERT INTO "RoleOperation" VALUES ('locations/get', 3);
INSERT INTO "RoleOperation" VALUES ('locations/remove', 3);
INSERT INTO "RoleOperation" VALUES ('locations/activate', 3);
INSERT INTO "RoleOperation" VALUES ('employees/add', 3);
INSERT INTO "RoleOperation" VALUES ('employees/modify', 3);
INSERT INTO "RoleOperation" VALUES ('employees/get', 3);
INSERT INTO "RoleOperation" VALUES ('employees/remove', 3);
INSERT INTO "RoleOperation" VALUES ('employees/activate', 3);
INSERT INTO "RoleOperation" VALUES ('debits/add', 3);
INSERT INTO "RoleOperation" VALUES ('debits/modify', 3);
INSERT INTO "RoleOperation" VALUES ('debits/get', 3);
INSERT INTO "RoleOperation" VALUES ('debits/remove', 3);
INSERT INTO "RoleOperation" VALUES ('debits/activate', 3);
INSERT INTO "RoleOperation" VALUES ('penalty/add', 3);
INSERT INTO "RoleOperation" VALUES ('penalty/modify', 3);
INSERT INTO "RoleOperation" VALUES ('penalty/get', 3);
INSERT INTO "RoleOperation" VALUES ('penalty/remove', 3);
INSERT INTO "RoleOperation" VALUES ('extras/add', 3);
INSERT INTO "RoleOperation" VALUES ('extras/modify', 3);
INSERT INTO "RoleOperation" VALUES ('extras/get', 3);
INSERT INTO "RoleOperation" VALUES ('extras/remove', 3);
INSERT INTO "RoleOperation" VALUES ('users/add', 3);
INSERT INTO "RoleOperation" VALUES ('users/modify', 3);
INSERT INTO "RoleOperation" VALUES ('users/get', 3);
INSERT INTO "RoleOperation" VALUES ('users/remove', 3);
INSERT INTO "RoleOperation" VALUES ('users/activate', 3);
INSERT INTO "RoleOperation" VALUES ('roles/add', 3);
INSERT INTO "RoleOperation" VALUES ('roles/modify', 3);
INSERT INTO "RoleOperation" VALUES ('roles/get', 3);
INSERT INTO "RoleOperation" VALUES ('roles/remove', 3);
INSERT INTO "RoleOperation" VALUES ('roles/activate', 3);
INSERT INTO "RoleOperation" VALUES ('payroll/calculate', 3);
INSERT INTO "RoleOperation" VALUES ('payroll/aprove', 3);
INSERT INTO "RoleOperation" VALUES ('payroll/get', 3);
INSERT INTO "RoleOperation" VALUES ('debittypes/add', 3);
INSERT INTO "RoleOperation" VALUES ('debittypes/modify', 3);
INSERT INTO "RoleOperation" VALUES ('debittypes/get', 3);
INSERT INTO "RoleOperation" VALUES ('debittypes/remove', 3);

select * from employees;
--Employees
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES ( 'Adrián Antonio Marín Sosa', '114730338', 'ADRIAN MARIN',TRUE, 1, true, 0, '001-149541-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Alexander Morales Caballero', '114370688', 'ALEXANDER MORALES',TRUE, 1, true, 0, '001-0668692-3', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Arturo Clark Bustos', '108180394', 'ARTURO CLARK',TRUE, 1, true, 0, '962-0003476-2', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Cheril Eithel Fonseca Picado', '206100860', 'CHERIL FONSECA',TRUE, 1, true, 0, '001-1147624-9', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Danny Alberto Naranjo Obando', '110190812', 'DANNY NARANJO',TRUE, 1, true, 0, '001-1255216-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Douglas Ariel Salazar Salazar', '113400556', 'DOUGLAS SALAZAR',TRUE, 1, true, 0, '001-0594028-1', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Ever Román Rivera Suarez', '113270626', 'EVER RIVERA',TRUE, 1, true, 0, '001-0549916-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Fabián Andrés Arias Leiva', '112500502', 'FABIAN ARIAS',TRUE, 1, true, 0, '001-0455820-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Freddy José Enriquez Baltodano', '116710714', 'FREDDY ENRIQUEZ',TRUE, 1, true, 0, '001-1007818-5', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Freddy Manuel Mejía Gómez', '503180033', 'FREDDY MEJIA',TRUE, 1, true, 0, '001-0829071-7', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Geovanny Ordoñez Ugalde', '503790644', 'GEOVANNY ORDONEZ',TRUE, 1, true, 0, '001-1147625-7', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Greivin Alfredo Guido Bustos', '402170742', 'GREIVIN GUIDO',TRUE, 1, true, 0, '001-0569108-7', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Hadhit Sibaja Montero', '112300627', '',FALSE, 1, true, 253687.50, '001-1007815-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Hallen Gisselle Sánchez Vargas', '402020812', 'HALLEN SANCHEZ',TRUE, 1, true, 0, '001-1146082-2', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Haydee Delgado Briceño', '112970129', 'HAYDEE DELGADO',TRUE, 1, true, 0, '001-1269878-4', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Hellen Dayan Mendoza Fernández', '116370910', 'HELLEN MENDOZA',TRUE, 1, true, 0, '001-1738043-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Javier Quesada Cruz', '603310318', 'JAVIER QUESADA',TRUE, 1, true, 0, '001-1492754-3', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Jeimy Tatiana Picado Sánchez', '111310525', 'JEIMY PICADO',TRUE, 1, true, 0, '001-1659860-1', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'John Byron Márquez Chicas', '800690616', 'JOHN MARQUEZ',TRUE, 1, true, 0, '926-0007928-6', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Jorge Antonio Coto Ortega', '111400658', NULL,FALSE, 1, true, 304405.17, '001-1039245-9', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'José Fallas Robles', '115160254', 'JOSE FALLAS.',TRUE, 1, true, 0, '908-0000284-4', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'José Salas Rodríguez', '108390007', 'JOSE SALAS',TRUE, 1, true, 0, '001-0381976-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'José Vega Fallas', '110080377', 'JOSE VEGA',TRUE, 1, true, 0, '001-0427936-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Joselyn Vidaurre Bravo', '701700182', 'JOSELYN VIDAURRE',TRUE, 1, true, 0, '001-1490424-1', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Juan Gabriel Mora Araya', '206840920', 'JUAN MORA',TRUE, 1, true, 0, '001-1490512-4', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Karol Rebeca Pérez Abarca', '114040722', 'KAROL PEREZ',TRUE, 1, true, 0, '001-1490420-9', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Katherine Barrios Valenciano', '112660130', NULL,FALSE, 1, true, 304405.17, '001-0446790-6', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Leonardo Rodríguez Carrión', '800870767', NULL,FALSE, 1, false, 304405.17, '001-0309067-1', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Luis Diego Zamora Olmedo', '112170982', 'LUIS ZAMORA',TRUE, 1, true, 0, '001-0745677-8', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Magaly Fernández Trejos', '112900696', 'MAGALY FERNANDEZ',TRUE, 1, true, 0, '001-1403871-4', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Manuel Valverde Morales', '113500037', 'MANUEL VALVERDE',TRUE, 1, true, 0, '001-0801797-2', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Marcela Barrientos Castro', '206780817', 'MARCELA BARRIENTOS',TRUE, 1, true, 0, '001-1655772-7', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Marcial García Schmidt', '114620341', 'MARCIAL GARCIA',TRUE, 1, true, 0, '001-1655891-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Marco Vinicio Uribe Morelli', '112830671', 'MARCO URIBE',TRUE, 1, true, 0, '956-0010224-5', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'María Briceño Quirós', '502200745', 'MARIA BRICEÑO',TRUE, 1, true, 0, '001-0153627-3', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'María Castillo Ramírez', '107670112', 'MARIA CASTILLO',TRUE, 1, true, 0, '001-1732859-4', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Mario Botto Sibaja', '502930659', 'MARIA BOTTO',TRUE, 1, true, 0, '001-1495093-6', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Marvin Leiton Sánchez', '107620063', NULL,FALSE, 1, false, 304405.17, '960-0010351-9', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Michael Vásquez Calderón', '112220657', NULL,FALSE, 1, true, 304405.17, '001-0573164-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Oscar Arnulfo Alvarenga Torres', '122200557831', NULL,FALSE, 1, true, 304405.17, '485-0002524-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Paola Vargas Carvajal', '112590969', 'PAOLA VARGAS',TRUE, 1, true, 0, '001-1146089-0', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Pedro Rojas Duran', '205280848', 'PEDRO ROJAS',TRUE, 1, true, 0, '001-0485880-8', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Priscilla María Sibaja Porras', '112540130', 'PRISCILA SIBAJA',TRUE, 1, true, 0, '285-0035806-1', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Randall Abarca Hernández', '401840992', 'RANDAL ABARCA',TRUE, 1, true, 0, '366-0001939-9', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Raymond Sandoval Fernández', '112620423', 'RAYMOND SANDOVAL',TRUE, 1, true, 0, '001-1039460-5', 0);
INSERT INTO employees(name,"idCard",cms,iscms,location,active,salary,account,"negativeAmount") VALUES( 'Scarlet Tayrin Porras Jiménez', '702090625', NULL,FALSE, 1, false, 304405.17, '001-1287383-7', 0);

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
INSERT INTO Extras(employee, description, hours) values (13, 'Sin comentarios', 2);
INSERT INTO Extras(employee, description, hours) values (20, 'Sin comentarios', 6);
INSERT INTO Extras(employee, description, hours) values (27, 'Sin comentarios', 4);
INSERT INTO Extras(employee, description, hours) values (28, 'Sin comentarios', 3);
INSERT INTO Extras(employee, description, hours) values (40, 'Sin comentarios', 6);

--Savings
INSERT INTO Savings values (3, 100000);
INSERT INTO Savings values (6, 16000);
INSERT INTO Savings values (13, 10506);
INSERT INTO Savings values (9, 11000);
INSERT INTO Savings values (2, 140000);
INSERT INTO Savings values (27, 254300);
INSERT INTO Savings values (25, 120000);

--Debit_Types
 INSERT into Debit_Types (name, pays, "interestRate", location, type)
	values ('Debit_type1', 4, 9, 1, 'A');
 INSERT into Debit_Types (name, pays, "interestRate", location, type)
	values ('Debit_type2', 6, 7, 1, 'P');
 INSERT into Debit_Types (name, pays, "interestRate", location, type)
	values ('Debit_type3', 12, 5, 1, 'F');

--Debits
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-04-10', 'Sin comentarios', 25, 20000, 20000, 4, 0, 1, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-03-10', 'Sin comentarios', 1, 10000, 7500, 3, 1, 1, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-02-10', 'Sin comentarios', 4, 35000, 17500, 2, 2, 1, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-01-10', 'Sin comentarios', 5, 40000, 30000, 1, 3, 1, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-02-10', 'Sin comentarios', 6, 25000, 12500, 2, 2, 1, true);

INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-04-05', 'Sin comentarios', 4, 20000, 20000, 6, 0, 2, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-03-05', 'Sin comentarios', 6, 15000, 12500, 5, 1, 2, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-02-05', 'Sin comentarios', 9, 24000, 16000, 4, 2, 2, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-01-05', 'Sin comentarios', 13, 12000, 6000, 3, 3, 2, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-03-05', 'Sin comentarios', 2, 30000, 25000, 5, 1, 2, true);


INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-04-01', 'Sin comentarios', 3, 40200, 40200, 12, 0, 3, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-01-01', 'Sin comentarios', 6, 40200, 30150, 9, 3, 3, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-02-01', 'Sin comentarios', 13, 40200, 33500, 10, 2, 3, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-03-01', 'Sin comentarios', 9, 40200, 36850, 11, 1, 3, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-02-01', 'Sin comentarios', 2, 40200, 33500, 10, 2, 3, true);
INSERT INTO Debits ("initialDate", description, employee, "totalAmount", "remainingAmount", "remainingPays", "paysMade", "debitType", active)
	values('2016-04-01', 'Sin comentarios', 27, 40200, 40200, 12, 0, 3, true);

