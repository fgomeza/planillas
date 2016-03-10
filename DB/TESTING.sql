INSERT INTO TERR_01(CDE_02) VALUES('El asociado no existe');
INSERT INTO TERR_01(CDE_02) VALUES('El débito no existe');
INSERT INTO TERR_01(CDE_02) VALUES('El extra no existe');
INSERT INTO TERR_01(CDE_02) VALUES('La penalizacion no existe');
INSERT INTO TERR_01(CDE_02) VALUES('Operación ya asignada');
INSERT INTO TERR_01(CDE_02) VALUES('La operación no existe');
INSERT INTO TERR_01(CDE_02) VALUES('El role no existe');
INSERT INTO TERR_01(CDE_02) VALUES('Operación no asignada');
INSERT INTO TERR_01(CDE_02) VALUES('El nombre del role ya está siendo utilizado');
INSERT INTO TERR_01(CDE_02) VALUES('EL nombre de la sede ya está siendo utilizada');
INSERT INTO TERR_01(CDE_02) VALUES('La sede no existe');
INSERT INTO TERR_01(CDE_02) VALUES('El número de cédula ya está registrado');
INSERT INTO TERR_01(CDE_02) VALUES('El identificador CMS ya está registrado');
INSERT INTO TERR_01(CDE_02) VALUES('El usuario no existe');
INSERT INTO TERR_01(CDE_02) VALUES('La contraseña es incorrecta');
INSERT INTO TERR_01(CDE_02) VALUES('EL monto a pagar es superior ala deuda actual');
INSERT INTO TERR_01(CDE_02) VALUES('La planilla no existe');
INSERT INTO TERR_01(CDE_02) VALUES('Error al conectar a la base de datos');
INSERT INTO TERR_01(CDE_02) VALUES('El grupo no existe');
INSERT INTO TERR_01(CDE_02) VALUES('La operacón no existe');
INSERT INTO TERR_01(CDE_02) VALUES('No has iniciado sesión');
INSERT INTO TERR_01(CDE_02) VALUES('No cuentas con los privilegios para realizar esta operación');
INSERT INTO TERR_01(CDE_02) VALUES('No coincide el número de parametros esperado');
INSERT INTO TERR_01(CDE_02) VALUES('No coincide el tipo de los parametros esperado');

INSERT INTO TCALL_01(CPR_02) VALUES(550);

SELECT * FROM FSE_01('Sede de Tibas');

SELECT * FROM FRO_01('ADMINISTRADOR',1);

INSERT INTO TGO_01 VALUES('users','Usuarios','',true);
INSERT INTO TGO_01 VALUES('roles','Roles','',true);
INSERT INTO TGO_01 VALUES('locations','Sedes','',false);
INSERT INTO TGO_01 VALUES('employees','Asociados','',false);
INSERT INTO TGO_01 VALUES('debits','Débitos','',false);
INSERT INTO TGO_01 VALUES('payrolls','Planillas','',false);
INSERT INTO TGO_01 VALUES('extras','Extras','',false);
INSERT INTO TGO_01 VALUES('recess','Penalizaciones','',false);

INSERT INTO TOP_01 VALUES ('add','','users');
INSERT INTO TOP_01 VALUES ('modify','','users');
INSERT INTO TOP_01 VALUES ('remove','','users');
INSERT INTO TOP_01 VALUES ('get','','users');

INSERT INTO TOP_01 VALUES ('add','','roles');
INSERT INTO TOP_01 VALUES ('modify','','roles');
INSERT INTO TOP_01 VALUES ('remove','','roles');
INSERT INTO TOP_01 VALUES ('get','','roles');

INSERT INTO TOP_01 VALUES ('add','','locations');
INSERT INTO TOP_01 VALUES ('remove','','locations');
INSERT INTO TOP_01 VALUES ('get','','locations');

INSERT INTO TOP_01 VALUES ('add','','employees');
INSERT INTO TOP_01 VALUES ('modify','','employees');
INSERT INTO TOP_01 VALUES ('remove','','employees');
INSERT INTO TOP_01 VALUES ('get','','employees');

INSERT INTO TOP_01 VALUES ('add','','debits');
INSERT INTO TOP_01 VALUES ('modify','','debits');
INSERT INTO TOP_01 VALUES ('remove','','debits');
INSERT INTO TOP_01 VALUES ('get','','debits');
INSERT INTO TOP_01 VALUES ('addtypes','','debits');
INSERT INTO TOP_01 VALUES ('modifytypes','','debits');
INSERT INTO TOP_01 VALUES ('removetypes','','debits');
INSERT INTO TOP_01 VALUES ('gettypes','','debits');

INSERT INTO TOP_01 VALUES ('callprice','','payrolls');
INSERT INTO TOP_01 VALUES ('addcalls','','payrolls');
INSERT INTO TOP_01 VALUES ('getcalls','','payrolls');
INSERT INTO TOP_01 VALUES ('generate','','payrolls');
INSERT INTO TOP_01 VALUES ('aprove','','payrolls');
INSERT INTO TOP_01 VALUES ('remove','','payrolls');
INSERT INTO TOP_01 VALUES ('get','','payrolls');

INSERT INTO TOP_01 VALUES ('add','','extras');
INSERT INTO TOP_01 VALUES ('modify','','extras');
INSERT INTO TOP_01 VALUES ('remove','','extras');
INSERT INTO TOP_01 VALUES ('get','','extras');

INSERT INTO TOP_01 VALUES ('add','','recess');
INSERT INTO TOP_01 VALUES ('modify','','recess');
INSERT INTO TOP_01 VALUES ('remove','','recess');
INSERT INTO TOP_01 VALUES ('get','','recess');

SELECT * FROM FOP_01(1,'add','employees');
SELECT * FROM FOP_01(1,'modify','employees');
SELECT * FROM FOP_01(1,'remove','employees');
SELECT * FROM FOP_01(1,'get','employees');

SELECT * FROM FUS_01('Jonnathan','JonnCh','123',1,1,'jcharpentier@mobilixe.net');




