
CREATE TABLE LOCATIONS(
ID BIGSERIAL,
NAME TEXT,
CALL_PRICE DECIMAL,
LAST_PAYROLL BIGINT,
CURRENT_PAYROLL BIGINT,
ACTIVE BOOLEAN,
CONSTRAINT PKLOCATION PRIMARY KEY(ID)
);

CREATE TABLE EMPLOYEES(
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
CONSTRAINT FKEMPLOYEE_LOCATION FOREIGN KEY(LOCATION) REFERENCES LOCATIONS,
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

CREATE TABLE ROLES(
ID BIGSERIAL,
NAME TEXT,
LOCATION BIGINT,
ACTIVE BOOLEAN,
CONSTRAINT PKROLE PRIMARY KEY(ID),
CONSTRAINT FKROLE_LOCATION FOREIGN KEY(LOCATION) REFERENCES LOCATIONS,
CONSTRAINT UKROLE_NAME UNIQUE (NAME)
);

CREATE TABLE USERS(
ID BIGSERIAL,
NAME TEXT,
EMAIL TEXT,
USERNAME TEXT,
PASSWORD TEXT,
ROLE BIGINT,
LOCATION BIGINT,
ACTIVE BOOLEAN,
CONSTRAINT PKUSER PRIMARY KEY (ID),
CONSTRAINT FKUSER_ROLE FOREIGN KEY(ROLE) REFERENCES ROLES,
CONSTRAINT FKUSER_LOCATION FOREIGN KEY(LOCATION) REFERENCES LOCATIONS,
CONSTRAINT UKUSER_EMAIL UNIQUE (EMAIL),
CONSTRAINT UKUSER_USERNAME UNIQUE (USERNAME)
);

CREATE TABLE ADMINISTRATORS(
USER_ID BIGINT,
LOCATION BIGINT,
CONSTRAINT PKADMINISTRATOR PRIMARY KEY (USER_ID,LOCATION),
CONSTRAINT FKADMINISTRATOR_USER FOREIGN KEY (USER_ID) REFERENCES USERS,
CONSTRAINT FKADMINISTRATOR_LOCATION FOREIGN KEY (LOCATION) REFERENCES LOCATIONS
);
CREATE TABLE PAYROLLS(
ID BIGSERIAL,
END_DATE DATE,
USER_ID BIGINT,
CALL_PRICE DECIMAL,
LOCATION BIGINT,
CONSTRAINT PKPAYROLL PRIMARY KEY(ID),
CONSTRAINT FKPAYROLL_LOCATION FOREIGN KEY(LOCATION) REFERENCES LOCATIONS,
CONSTRAINT FKPAYROLL_USER FOREIGN KEY(USER_ID) REFERENCES USERS,
CONSTRAINT UKPAYROLL_ENDDATE UNIQUE (END_DATE)
);

CREATE TABLE CALLS(
EMPLOYEE BIGINT,
DATE DATE,
CALLS BIGINT,
TIME TIME,
PAYROLL BIGINT,
CONSTRAINT PKCALL PRIMARY KEY(EMPLOYEE,DATE),
CONSTRAINT FKCALL_EMPLOYEE FOREIGN KEY(EMPLOYEE) REFERENCES EMPLOYEES,
CONSTRAINT FKCALL_PAYROLL FOREIGN KEY(PAYROLL) REFERENCES PAYROLLS
);

CREATE TABLE SALARY(
ID BIGSERIAL,
PAYROLL BIGINT,
EMPLOYEE BIGINT,
SALARY DECIMAL,
NET_SALARY DECIMAL,
CONSTRAINT PKSALARY PRIMARY KEY (ID),
CONSTRAINT FKSALARY_EMPLOYEE FOREIGN KEY(EMPLOYEE) REFERENCES EMPLOYEES,
CONSTRAINT FKSALARY_PAYROLL FOREIGN KEY(PAYROLL) REFERENCES PAYROLLS,
CONSTRAINT UKSALARY_EMPLOYEE UNIQUE (EMPLOYEE)
);

CREATE TABLE PENALTY_TYPES(
ID BIGSERIAL,
NAME TEXT,
PRICE DECIMAL,
LOCATION BIGINT,
CONSTRAINT PKPENALTYPE PRIMARY KEY (ID),
CONSTRAINT FKPENALTYTYPE_LOCATION FOREIGN KEY (LOCATION) REFERENCES LOCATIONS
);

CREATE TABLE PENALTIES(
ID BIGSERIAL,
PAYROLL BIGINT,
EMPLOYEE BIGINT,
DESCRIPTION TEXT,
PENALTY_TYPE BIGINT,
AMOUNT BIGINT,
PENALTY_PRICE DECIMAL,
DATE DATE,
ACTIVE BOOLEAN,
CONSTRAINT PKPENALTY PRIMARY KEY (ID),
CONSTRAINT FKPENALTY_PAYROLL FOREIGN KEY (PAYROLL) REFERENCES PAYROLLS,
CONSTRAINT FKPENALTY_EMPLOYEE FOREIGN KEY (EMPLOYEE) REFERENCES EMPLOYEES,
CONSTRAINT FKPENALTY_TYPE FOREIGN KEY (PENALTY_TYPE) REFERENCES PENALTY_TYPES
);

CREATE TABLE EXTRAS(
ID BIGSERIAL,
EMPLOYEE BIGINT,
DESCRIPTION TEXT,
HOURS BIGINT,
CONSTRAINT PKEXTRA PRIMARY KEY (ID),
CONSTRAINT FKEXTRA_EMPLOYEE FOREIGN KEY (EMPLOYEE) REFERENCES EMPLOYEES
);

CREATE TABLE SAVINGS(
EMPLOYEE BIGINT,
AMOUNT DECIMAL,
CONSTRAINT PKSAVING PRIMARY KEY (EMPLOYEE),
CONSTRAINT FKSAVING FOREIGN KEY (EMPLOYEE) REFERENCES EMPLOYEES
);

CREATE TABLE DEBIT_TYPES(
ID BIGSERIAL,
NAME TEXT,
MONTHS BIGINT,
INTEREST_RATE DECIMAL,
LOCATION BIGINT,
PAYMENT BOOLEAN,
CONSTRAINT PKDEBITTYPE PRIMARY KEY (ID),
CONSTRAINT FKDEBITTYPE_LOCATION FOREIGN KEY (LOCATION) REFERENCES LOCATIONS
);

CREATE TABLE DEBITS(
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
CONSTRAINT FKDEBIT_EMPLOYEE FOREIGN KEY (EMPLOYEE) REFERENCES EMPLOYEES,
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
CONSTRAINT FKDEBITPAYMENT FOREIGN KEY (DEBIT) REFERENCES DEBITS
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

ALTER TABLE LOCATIONS ADD CONSTRAINT FKLOCATION_LASTPAYROLL FOREIGN KEY (LAST_PAYROLL) REFERENCES PAYROLLS;
ALTER TABLE LOCATIONS ADD CONSTRAINT FKLOCATION_CURRENTPAYROLL FOREIGN KEY (CURRENT_PAYROLL) REFERENCES PAYROLLS;



INSERT INTO locations(id,name,call_price,active) VALUES (1, 'Tibas', 550, true);
INSERT INTO locations(id,name,call_price,active) VALUES (2, 'San Pedro', 400, true);

INSERT INTO groups VALUES ('Locations', 'Sedes', '');
INSERT INTO groups VALUES ('Employees', 'Empleados', '');
INSERT INTO groups VALUES ('Debits', 'Débitos', '');
INSERT INTO groups VALUES ('Penalty', 'Penalizaciones', '');
INSERT INTO groups VALUES ('Extras', 'Extras', '');
INSERT INTO groups VALUES ('Users', 'Usuarios', '');
INSERT INTO groups VALUES ('Roles', 'Roles', '');
INSERT INTO groups VALUES ('Payroll', 'Planillas', '');
INSERT INTO groups VALUES ('DebitTypes', 'Tipos de Débitos', '');


INSERT INTO operations VALUES ('Locations/add', 'Agregar', 'Locations');
INSERT INTO operations VALUES ('Locations/modify', 'Modificar', 'Locations');
INSERT INTO operations VALUES ('Locations/get', 'Ver', 'Locations');
INSERT INTO operations VALUES ('Locations/remove', 'Eliminar', 'Locations');
INSERT INTO operations VALUES ('Locations/activate', 'Activar', 'Locations');
INSERT INTO operations VALUES ('Employees/add', 'Agregar', 'Employees');
INSERT INTO operations VALUES ('Employees/modify', 'Modificar', 'Employees');
INSERT INTO operations VALUES ('Employees/get', 'Ver', 'Employees');
INSERT INTO operations VALUES ('Employees/remove', 'Eliminar', 'Employees');
INSERT INTO operations VALUES ('Employees/activate', 'Activar', 'Employees');
INSERT INTO operations VALUES ('Debits/add', 'Agregar', 'Debits');
INSERT INTO operations VALUES ('Debits/modify', 'Modificar', 'Debits');
INSERT INTO operations VALUES ('Debits/get', 'Ver', 'Debits');
INSERT INTO operations VALUES ('Debits/remove', 'Eliminar', 'Debits');
INSERT INTO operations VALUES ('Debits/activate', 'Activar', 'Debits');
INSERT INTO operations VALUES ('Penalty/add', 'Agregar', 'Penalty');
INSERT INTO operations VALUES ('Penalty/modify', 'Modificar', 'Penalty');
INSERT INTO operations VALUES ('Penalty/get', 'Ver', 'Penalty');
INSERT INTO operations VALUES ('Penalty/remove', 'Eliminar', 'Penalty');
INSERT INTO operations VALUES ('Extras/add', 'Agregar', 'Extras');
INSERT INTO operations VALUES ('Extras/modify', 'Modificar', 'Extras');
INSERT INTO operations VALUES ('Extras/get', 'Ver', 'Extras');
INSERT INTO operations VALUES ('Extras/remove', 'Eliminar', 'Extras');
INSERT INTO operations VALUES ('Users/add', 'Agregar', 'Users');
INSERT INTO operations VALUES ('Users/modify', 'Modificar', 'Users');
INSERT INTO operations VALUES ('Users/get', 'Ver', 'Users');
INSERT INTO operations VALUES ('Users/remove', 'Eliminar', 'Users');
INSERT INTO operations VALUES ('Users/activate', 'Activar', 'Users');
INSERT INTO operations VALUES ('Roles/add', 'Agregar', 'Roles');
INSERT INTO operations VALUES ('Roles/modify', 'Modificar', 'Roles');
INSERT INTO operations VALUES ('Roles/get', 'Ver', 'Roles');
INSERT INTO operations VALUES ('Roles/remove', 'Eliminar', 'Roles');
INSERT INTO operations VALUES ('Roles/activate', 'Activar', 'Roles');
INSERT INTO operations VALUES ('Payroll/calculate', 'Agregar', 'Payroll');
INSERT INTO operations VALUES ('Payroll/aprove', 'Aprovar', 'Payroll');
INSERT INTO operations VALUES ('Payroll/get', 'Ver', 'Payroll');
INSERT INTO operations VALUES ('DebitTypes/add', 'Agregar', 'DebitTypes');
INSERT INTO operations VALUES ('DebitTypes/modify', 'Modificar', 'DebitTypes');
INSERT INTO operations VALUES ('DebitTypes/get', 'Ver', 'DebitTypes');
INSERT INTO operations VALUES ('DebitTypes/remove', 'Eliminar', 'DebitTypes');


INSERT INTO roles VALUES (1, 'ADMIN',1, true);
INSERT INTO roles VALUES (2, 'PAYROLLER',1, true);
INSERT INTO roles VALUES (3, 'ADMIN2',2, true);

INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (1, 'Admin', 'admin@mobilize.net', 'admin', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (2, 'Jonnathan Ch', 'jcharpentier@mobilize.net', 'jonnch', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (3, 'Jafet Román', 'jafet21@hotmail.es', 'tutox', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 2, 1);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (4, 'Francisco Gomez', 'fgomeza25@gmail.com', 'fgomez', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 2, 1);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (5, 'Oscar Chaves', 'andresch1009@gmail.com', 'oscar', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 3, 2);
INSERT INTO users(id,name,email,username,password,active,role,location) VALUES (6, 'No me acuerdo su nombre', 'nomeacuerdosunombre@gmail.com', 'nomeacuerdo', '$2a$10$hsWmqrs7Z5kW6RpEJHV/Ve6/OBqZ3tNZMivur9SYZT2OBvIymxU.2', true, 1, 1);


INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/activate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/calculate', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/aprove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/add', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/modify', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/get', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/remove', 1);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/activate', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/activate', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/calculate', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/aprove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/add', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/modify', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/get', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/remove', 2);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Locations/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Employees/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Debits/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Penalty/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Extras/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Users/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/remove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Roles/activate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/calculate', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/aprove', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('Payroll/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/add', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/modify', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/get', 3);
INSERT INTO "OperationEntityRoleEntities" VALUES ('DebitTypes/remove', 3);

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
