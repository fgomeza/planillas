
CREATE TABLE locations(
ID BIGSERIAL,
NAME TEXT,
CALL_PRICE DECIMAL,
LAST_payroll BIGINT,
CURRENT_payroll BIGINT,
ACTIVE BOOLEAN,
CONSTRAINT PKLOCATION PRIMARY KEY(ID)
);

CREATE TABLE employees(
ID BIGSERIAL,
NAME TEXT,
ID_CARD TEXT,
CMS TEXT,
LOCATION BIGINT,
ACTIVE BOOLEAN,
SALARY DECIMAL,
ACCOUNT TEXT,
NEGATIVEAMOUNT DECIMAL,
CONSTRAINT PKEMPLOYEE PRIMARY KEY(ID),
CONSTRAINT FKEMPLOYEE_LOCATION FOREIGN KEY(LOCATION) REFERENCES locations,
CONSTRAINT UKEMPLOYEE_IDCARD UNIQUE (ID_CARD),
CONSTRAINT UKEMPLOYEE_CMS UNIQUE (CMS)
);

CREATE TABLE GROUPS(
NAME TEXT,
DESCRIPTION TEXT,
ICON TEXT,
CONSTRAINT PKGROUP PRIMARY KEY (NAME)
);

CREATE TABLE OPERATIONS(
NAME TEXT,
DESCRIPTION TEXT,
GROUP_ID TEXT,
CONSTRAINT PKOPERATION PRIMARY KEY (NAME),
CONSTRAINT FKOPERATION FOREIGN KEY (GROUP_ID) REFERENCES GROUPS
);

CREATE TABLE roles(
ID BIGSERIAL,
NAME TEXT,
LOCATION BIGINT,
ACTIVE BOOLEAN,
CONSTRAINT PKROLE PRIMARY KEY(ID),
CONSTRAINT FKROLE_LOCATION FOREIGN KEY(LOCATION) REFERENCES locations,
CONSTRAINT UKROLE_NAME UNIQUE (NAME)
);

CREATE TABLE users(
ID BIGSERIAL,
NAME TEXT,
EMAIL TEXT,
USERNAME TEXT,
PASSWORD TEXT,
ROLE BIGINT,
LOCATION BIGINT,
ACTIVE BOOLEAN,
CONSTRAINT PKUSER PRIMARY KEY (ID),
CONSTRAINT FKUSER_ROLE FOREIGN KEY(ROLE) REFERENCES roles,
CONSTRAINT FKUSER_LOCATION FOREIGN KEY(LOCATION) REFERENCES locations,
CONSTRAINT UKUSER_EMAIL UNIQUE (EMAIL),
CONSTRAINT UKUSER_USERNAME UNIQUE (USERNAME)
);

CREATE TABLE ADMINISTRATORS(
USER_ID BIGINT,
LOCATION BIGINT,
CONSTRAINT PKADMINISTRATOR PRIMARY KEY (USER_ID,LOCATION),
CONSTRAINT FKADMINISTRATOR_USER FOREIGN KEY (USER_ID) REFERENCES users,
CONSTRAINT FKADMINISTRATOR_LOCATION FOREIGN KEY (LOCATION) REFERENCES locations
);
CREATE TABLE payrollS(
ID BIGSERIAL,
END_DATE DATE,
USER_ID BIGINT,
CALL_PRICE DECIMAL,
LOCATION BIGINT,
CONSTRAINT PKpayroll PRIMARY KEY(ID),
CONSTRAINT FKpayroll_LOCATION FOREIGN KEY(LOCATION) REFERENCES locations,
CONSTRAINT FKpayroll_USER FOREIGN KEY(USER_ID) REFERENCES users,
CONSTRAINT UKpayroll_ENDDATE UNIQUE (END_DATE)
);

CREATE TABLE CALLS(
EMPLOYEE BIGINT,
DATE DATE,
CALLS BIGINT,
TIME TIME,
payroll BIGINT,
CONSTRAINT PKCALL PRIMARY KEY(EMPLOYEE,DATE),
CONSTRAINT FKCALL_EMPLOYEE FOREIGN KEY(EMPLOYEE) REFERENCES employees,
CONSTRAINT FKCALL_payroll FOREIGN KEY(payroll) REFERENCES payrollS
);

CREATE TABLE SALARY(
ID BIGSERIAL,
payroll BIGINT,
EMPLOYEE BIGINT,
SALARY DECIMAL,
NET_SALARY DECIMAL,
CONSTRAINT PKSALARY PRIMARY KEY (ID),
CONSTRAINT FKSALARY_EMPLOYEE FOREIGN KEY(EMPLOYEE) REFERENCES employees,
CONSTRAINT FKSALARY_payroll FOREIGN KEY(payroll) REFERENCES payrollS,
CONSTRAINT UKSALARY_EMPLOYEE UNIQUE (EMPLOYEE)
);

CREATE TABLE penalty_TYPES(
ID BIGSERIAL,
NAME TEXT,
PRICE DECIMAL,
LOCATION BIGINT,
CONSTRAINT PKpenaltyPE PRIMARY KEY (ID),
CONSTRAINT FKpenaltyTYPE_LOCATION FOREIGN KEY (LOCATION) REFERENCES locations
);

CREATE TABLE PENALTIES(
ID BIGSERIAL,
payroll BIGINT,
EMPLOYEE BIGINT,
DESCRIPTION TEXT,
penalty_TYPE BIGINT,
AMOUNT BIGINT,
penalty_PRICE DECIMAL,
DATE DATE,
ACTIVE BOOLEAN,
CONSTRAINT PKpenalty PRIMARY KEY (ID),
CONSTRAINT FKpenalty_payroll FOREIGN KEY (payroll) REFERENCES payrollS,
CONSTRAINT FKpenalty_EMPLOYEE FOREIGN KEY (EMPLOYEE) REFERENCES employees,
CONSTRAINT FKpenalty_TYPE FOREIGN KEY (penalty_TYPE) REFERENCES penalty_TYPES
);

CREATE TABLE extras(
ID BIGSERIAL,
EMPLOYEE BIGINT,
DESCRIPTION TEXT,
HOURS BIGINT,
CONSTRAINT PKEXTRA PRIMARY KEY (ID),
CONSTRAINT FKEXTRA_EMPLOYEE FOREIGN KEY (EMPLOYEE) REFERENCES employees
);

CREATE TABLE SAVINGS(
EMPLOYEE BIGINT,
AMOUNT DECIMAL,
CONSTRAINT PKSAVING PRIMARY KEY (EMPLOYEE),
CONSTRAINT FKSAVING FOREIGN KEY (EMPLOYEE) REFERENCES employees
);

CREATE TABLE DEBIT_TYPES(
ID BIGSERIAL,
NAME TEXT,
MONTHS BIGINT,
INTEREST_RATE DECIMAL,
LOCATION BIGINT,
PAYMENT BOOLEAN,
CONSTRAINT PKDEBITTYPE PRIMARY KEY (ID),
CONSTRAINT FKDEBITTYPE_LOCATION FOREIGN KEY (LOCATION) REFERENCES locations
);

CREATE TABLE debits(
ID BIGSERIAL,
INITIAL_DATE DATE,
DESCRIPTION TEXT,
EMPLOYEE BIGINT,
TOTAL_AMOUNT DECIMAL,
REMAINING_AMOUNT DECIMAL,
REMAINING_MONTHS BIGINT,
INTEREST_RATE DECIMAL,
PAID_MONTHS BIGINT,
TYPE BIGINT,
ACTIVE BOOLEAN,
PAYMENT BOOLEAN,
CONSTRAINT PKDEBIT PRIMARY KEY (ID),
CONSTRAINT FKDEBIT_EMPLOYEE FOREIGN KEY (EMPLOYEE) REFERENCES employees,
CONSTRAINT FKDEBIT_TYPE FOREIGN KEY (TYPE) REFERENCES DEBIT_TYPES
);


CREATE TABLE DEBIT_PAYMENTS(
ID BIGSERIAL,
DEBIT BIGINT,
DATE DATE,
REMAINING_AMOUNT DECIMAL,
INTEREST_RATE DECIMAL,
AMMOUNT DECIMAL,
CONSTRAINT PKDEBITPAYMENT PRIMARY KEY (ID),
CONSTRAINT UKDEBITPAYMENT UNIQUE(DEBIT,DATE),
CONSTRAINT FKDEBITPAYMENT FOREIGN KEY (DEBIT) REFERENCES debits
);

CREATE TABLE ERRORS(
ID BIGINT,
MESSAGE TEXT,
CONSTRAINT PKERROR PRIMARY KEY (MESSAGE)
);


CREATE TABLE "OperationEntityRoleEntities"
(
"OperationEntity_Name" text NOT NULL,
  "RoleEntity_id" bigint NOT NULL,
  CONSTRAINT "RoleOperations_pkey" PRIMARY KEY ("OperationEntity_Name", "RoleEntity_id")
);

ALTER TABLE locations ADD CONSTRAINT FKLOCATION_LASTpayroll FOREIGN KEY (LAST_payroll) REFERENCES payrollS;
ALTER TABLE locations ADD CONSTRAINT FKLOCATION_CURRENTpayroll FOREIGN KEY (CURRENT_payroll) REFERENCES payrollS;



INSERT INTO locations(id,name,call_price,active) VALUES (1, 'Tibas', 550, true);
INSERT INTO locations(id,name,call_price,active) VALUES (2, 'San Pedro', 400, true);

INSERT INTO groups VALUES ('locations', 'Sedes', '');
INSERT INTO groups VALUES ('employees', 'Empleados', '');
INSERT INTO groups VALUES ('debits', 'Débitos', '');
INSERT INTO groups VALUES ('penalty', 'Penalizaciones', '');
INSERT INTO groups VALUES ('extras', 'extras', '');
INSERT INTO groups VALUES ('users', 'Usuarios', '');
INSERT INTO groups VALUES ('roles', 'roles', '');
INSERT INTO groups VALUES ('payroll', 'Planillas', '');
INSERT INTO groups VALUES ('debittypes', 'Tipos de Débitos', '');


INSERT INTO operations VALUES ('locations/add', 'Agregar', 'locations');
INSERT INTO operations VALUES ('locations/modify', 'Modificar', 'locations');
INSERT INTO operations VALUES ('locations/get', 'Ver', 'locations');
INSERT INTO operations VALUES ('locations/remove', 'Eliminar', 'locations');
INSERT INTO operations VALUES ('locations/activate', 'Activar', 'locations');
INSERT INTO operations VALUES ('employees/add', 'Agregar', 'employees');
INSERT INTO operations VALUES ('employees/modify', 'Modificar', 'employees');
INSERT INTO operations VALUES ('employees/get', 'Ver', 'employees');
INSERT INTO operations VALUES ('employees/remove', 'Eliminar', 'employees');
INSERT INTO operations VALUES ('employees/activate', 'Activar', 'employees');
INSERT INTO operations VALUES ('debits/add', 'Agregar', 'debits');
INSERT INTO operations VALUES ('debits/modify', 'Modificar', 'debits');
INSERT INTO operations VALUES ('debits/get', 'Ver', 'debits');
INSERT INTO operations VALUES ('debits/remove', 'Eliminar', 'debits');
INSERT INTO operations VALUES ('debits/activate', 'Activar', 'debits');
INSERT INTO operations VALUES ('penalty/add', 'Agregar', 'penalty');
INSERT INTO operations VALUES ('penalty/modify', 'Modificar', 'penalty');
INSERT INTO operations VALUES ('penalty/get', 'Ver', 'penalty');
INSERT INTO operations VALUES ('penalty/remove', 'Eliminar', 'penalty');
INSERT INTO operations VALUES ('extras/add', 'Agregar', 'extras');
INSERT INTO operations VALUES ('extras/modify', 'Modificar', 'extras');
INSERT INTO operations VALUES ('extras/get', 'Ver', 'extras');
INSERT INTO operations VALUES ('extras/remove', 'Eliminar', 'extras');
INSERT INTO operations VALUES ('users/add', 'Agregar', 'users');
INSERT INTO operations VALUES ('users/modify', 'Modificar', 'users');
INSERT INTO operations VALUES ('users/get', 'Ver', 'users');
INSERT INTO operations VALUES ('users/remove', 'Eliminar', 'users');
INSERT INTO operations VALUES ('users/activate', 'Activar', 'users');
INSERT INTO operations VALUES ('roles/add', 'Agregar', 'roles');
INSERT INTO operations VALUES ('roles/modify', 'Modificar', 'roles');
INSERT INTO operations VALUES ('roles/get', 'Ver', 'roles');
INSERT INTO operations VALUES ('roles/remove', 'Eliminar', 'roles');
INSERT INTO operations VALUES ('roles/activate', 'Activar', 'roles');
INSERT INTO operations VALUES ('payroll/calculate', 'Agregar', 'payroll');
INSERT INTO operations VALUES ('payroll/aprove', 'Aprovar', 'payroll');
INSERT INTO operations VALUES ('payroll/get', 'Ver', 'payroll');
INSERT INTO operations VALUES ('debittypes/add', 'Agregar', 'debittypes');
INSERT INTO operations VALUES ('debittypes/modify', 'Modificar', 'debittypes');
INSERT INTO operations VALUES ('debittypes/get', 'Ver', 'debittypes');
INSERT INTO operations VALUES ('debittypes/remove', 'Eliminar', 'debittypes');


INSERT INTO roles VALUES (1, 'ADMIN',1, true);
INSERT INTO roles VALUES (2, 'payrollER',1, true);
INSERT INTO roles VALUES (3, 'ADMIN2',2, true);

INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (1, 'Admin', 'admin@mobilize.net', 'admin', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (2, 'Jonnathan Ch', 'jcharpentier@mobilize.net', 'jonnch', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (3, 'Jafet Román', 'jafet21@hotmail.es', 'tutox', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 2, 1);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (4, 'Francisco Gomez', 'fgomeza25@gmail.com', 'fgomez', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 2, 1);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (5, 'Oscar Chaves', 'andresch1009@gmail.com', 'oscar', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 3, 2);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (6, 'No me acuerdo su nombre', 'nomeacuerdosunombre@gmail.com', 'nomeacuerdo', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);


INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/calculate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/aprove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/activate', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/activate', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/calculate', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/aprove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('locations/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('employees/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debits/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('penalty/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('extras/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('users/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('roles/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/calculate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/aprove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('payroll/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('debittypes/remove', 3);

INSERT INTO employees VALUES (1, 'Adrián Antonio Marín Sosa', '114730338', NULL, 1, true, 500000, '001-149541-0', 0);
INSERT INTO employees VALUES (2, 'Alexander Morales Caballero', '114370688', NULL, 1, true, 500000, '001-0668692-3', 0);
INSERT INTO employees VALUES (3, 'Arturo Clark Bustos', '108180394', NULL, 1, true, 500000, '962-0003476-2', 0);
INSERT INTO employees VALUES (4, 'Cheril Eithel Fonseca Picado', '206100860', NULL, 1, true, 500000, '001-1147624-9', 0);
INSERT INTO employees VALUES (5, 'Danny Alberto Naranjo Obando', '110190812', NULL, 1, true, 500000, '001-1255216-0', 0);
INSERT INTO employees VALUES (6, 'Douglas Ariel Salazar Salazar', '113400556', NULL, 1, true, 500000, '001-0594028-1', 0);
INSERT INTO employees VALUES (7, 'Ever Román Rivera Suarez', '113270626', NULL, 1, true, 500000, '001-0549916-0', 0);
INSERT INTO employees VALUES (8, 'Fabián Andrés Arias Leiva', '112500502', NULL, 1, true, 500000, '001-0455820-0', 0);
INSERT INTO employees VALUES (9, 'Freddy José Enrique Baltodano', '116710714', NULL, 1, true, 500000, '001-1007818-5', 0);
INSERT INTO employees VALUES (10, 'Freddy Manuel Mejía Gómez', '503180033', NULL, 1, true, 500000, '001-0829071-7', 0);
INSERT INTO employees VALUES (11, 'Geovanny Ordoñez Ugalde', '503790644', NULL, 1, true, 500000, '001-1147625-7', 0);
INSERT INTO employees VALUES (12, 'Greivin Alfredo Guido Bustos', '402170742', NULL, 1, true, 500000, '001-0569108-7', 0);
INSERT INTO employees VALUES (13, 'Hadhit Sibaja Montero', '112300627', NULL, 1, false, 500000, '001-1007815-0', 0);
INSERT INTO employees VALUES (14, 'Hallen Gisselle Sánchez Vargas', '402020812', NULL, 1, true, 500000, '001-1146082-2', 0);
INSERT INTO employees VALUES (15, 'Haydee Delgado Briceño', '112970129', NULL, 1, true, 500000, '001-1269878-4', 0);
INSERT INTO employees VALUES (16, 'Hellen Dayan Mendoza Fernández', '116370910', NULL, 1, true, 500000, '001-1738043-0', 0);
INSERT INTO employees VALUES (17, 'Javier Quesada Cruz', '603310318', NULL, 1, true, 500000, '001-1492754-3', 0);
INSERT INTO employees VALUES (18, 'Jeimy Tatiana Picado Sánchez', '111310525', NULL, 1, true, 500000, '001-1659860-1', 0);
INSERT INTO employees VALUES (19, 'John Byron Márquez Chicas', '800690616', NULL, 1, true, 500000, '926-0007928-6', 0);
INSERT INTO employees VALUES (20, 'Jorge Antonio Coto Ortega', '111400658', NULL, 1, true, 500000, '001-1039245-9', 0);
INSERT INTO employees VALUES (21, 'José Fallas Robles', '115160254', NULL, 1, true, 500000, '908-0000284-4', 0);
INSERT INTO employees VALUES (22, 'José Salas Rodríguez', '108390007', NULL, 1, true, 500000, '001-0381976-0', 0);
INSERT INTO employees VALUES (23, 'José Vega Fallas', '110080377', NULL, 1, true, 500000, '001-0427936-0', 0);
INSERT INTO employees VALUES (24, 'Joselyn Vidaurre Bravo', '701700182', NULL, 1, true, 500000, '001-1490424-1', 0);
INSERT INTO employees VALUES (25, 'Juan Gabriel Mora Araya', '206840920', NULL, 1, true, 500000, '001-1490512-4', 0);
INSERT INTO employees VALUES (26, 'Karol Rebeca Pérez Abarca', '114040722', NULL, 1, true, 500000, '001-1490420-9', 0);
INSERT INTO employees VALUES (27, 'Katherine Barrios Valenciano', '112660130', NULL, 1, true, 500000, '001-0446790-6', 0);
INSERT INTO employees VALUES (28, 'Leonardo Rodríguez Carrión', '800870767', NULL, 1, false, 500000, '001-0309067-1', 0);
INSERT INTO employees VALUES (29, 'Luis Diego Zamora Olmedo', '112170982', NULL, 1, true, 500000, '001-0745677-8', 0);
INSERT INTO employees VALUES (30, 'Magaly Fernández Trejos', '112900696', NULL, 1, true, 500000, '001-1403871-4', 0);
INSERT INTO employees VALUES (31, 'Manuel Valverde Morales', '113500037', NULL, 1, true, 500000, '001-0801797-2', 0);
INSERT INTO employees VALUES (32, 'Marcela Barrientos Castro', '206780817', NULL, 1, true, 500000, '001-1655772-7', 0);
INSERT INTO employees VALUES (33, 'Marcial García Schmidt', '114620341', NULL, 1, true, 500000, '001-1655891-0', 0);
INSERT INTO employees VALUES (34, 'Marco Vinicio Uribe Morelli', '112830671', NULL, 1, true, 500000, '956-0010224-5', 0);
INSERT INTO employees VALUES (35, 'María Briceño Quirós', '502200745', NULL, 1, true, 500000, '001-0153627-3', 0);
INSERT INTO employees VALUES (36, 'María Castillo Ramírez', '107670112', NULL, 1, true, 500000, '001-1732859-4', 0);
INSERT INTO employees VALUES (37, 'Mario Botto Sibaja', '502930659', NULL, 1, true, 500000, '001-1495093-6', 0);
INSERT INTO employees VALUES (38, 'Marvin Leiton Sánchez', '107620063', NULL, 1, false, 500000, '960-0010351-9', 0);
INSERT INTO employees VALUES (39, 'Michael Vásquez Calderón', '112220657', NULL, 1, false, 500000, '001-0573164-0', 0);
INSERT INTO employees VALUES (40, 'Oscar Arnulfo Alvarenga Torres', '122200557831', NULL, 1, false, 500000, '485-0002524-0', 0);
INSERT INTO employees VALUES (41, 'Paola Vargas Carvajal', '112590969', NULL, 1, true, 500000, '001-1146089-0', 0);
INSERT INTO employees VALUES (42, 'Pedro Rojas Duran', '205280848', NULL, 1, true, 500000, '001-0485880-8', 0);
INSERT INTO employees VALUES (43, 'Priscilla María Sibaja Porras', '112540130', NULL, 1, true, 500000, '285-0035806-1', 0);
INSERT INTO employees VALUES (44, 'Randall Abarca Hernández', '401840992', NULL, 1, true, 500000, '366-0001939-9', 0);
INSERT INTO employees VALUES (45, 'Raymond Sandoval Fernández', '112620423', NULL, 1, true, 500000, '001-1039460-5', 0);
INSERT INTO employees VALUES (46, 'Scarlet Tayrin Porras Jiménez', '702090625', NULL, 1, false, 500000, '001-1287383-7', 0);
