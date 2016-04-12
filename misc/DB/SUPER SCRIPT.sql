﻿
CREATE TABLE LOCATIONS(
ID BIGSERIAL,
NAME TEXT,
CALL_PRICE DECIMAL,
LAST_PAYROLL BIGINT,
CURRENT_PAYROLL BIGINT,
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
URL TEXT,
GROUP_ID TEXT,
CONSTRAINT PKOPERATION PRIMARY KEY (NAME),
CONSTRAINT FKOPERATION FOREIGN KEY (GROUP_ID) REFERENCES GROUPS
);

CREATE TABLE ROLES(
ID BIGSERIAL,
NAME TEXT,
LOCATION BIGINT,
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
CONSTRAINT PKPENALTYPE PRIMARY KEY (ID)
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
CONSTRAINT PKPENALTY PRIMARY KEY (ID),
CONSTRAINT FKPENALTY_PAYROLL FOREIGN KEY (PAYROLL) REFERENCES PAYROLLS,
CONSTRAINT FKPENALTY_EMPLOYEE FOREIGN KEY (EMPLOYEE) REFERENCES EMPLOYEES,
CONSTRAINT FKPENALTY_TYPE FOREIGN KEY (PENALTY_TYPE) REFERENCES PENALTY_TYPES
);

CREATE TABLE EXTRAS(
ID BIGSERIAL,
EMPLOYEE BIGINT,
DESCRIPTION TEXT,
AMOUNT DECIMAL,
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


