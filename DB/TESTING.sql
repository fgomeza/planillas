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

INSERT INTO TGO_01 VALUES('users','Usuarios','eye-open',true);
INSERT INTO TGO_01 VALUES('roles','Roles de usuarios','wrench',true);
INSERT INTO TGO_01 VALUES('locations','Sedes','globe',false);
INSERT INTO TGO_01 VALUES('employees','Asociados','user',false);
INSERT INTO TGO_01 VALUES('debits','Débitos de asociados','eur',false);
INSERT INTO TGO_01 VALUES('payrolls','Planillas','list',false);
INSERT INTO TGO_01 VALUES('extras','Extras de asociados','plus',false);
INSERT INTO TGO_01 VALUES('recess','Penalizaciones de asociados','minus',false);

INSERT INTO TOP_01 VALUES ('add','Agregar usuarios','users');
INSERT INTO TOP_01 VALUES ('modify','Modificar usuarios','users');
INSERT INTO TOP_01 VALUES ('remove','Eliminar usuarios','users');
INSERT INTO TOP_01 VALUES ('get','Ver usuarios','users');

INSERT INTO TOP_01 VALUES ('add','Agregar roles','roles');
INSERT INTO TOP_01 VALUES ('modify','Modificar roles','roles');
INSERT INTO TOP_01 VALUES ('remove','Eliminar roles','roles');
INSERT INTO TOP_01 VALUES ('get','Ver roles','roles');

INSERT INTO TOP_01 VALUES ('add','Agregar sedes','locations');
INSERT INTO TOP_01 VALUES ('remove','Eliminar sedes','locations');
INSERT INTO TOP_01 VALUES ('get','Ver sedes','locations');

INSERT INTO TOP_01 VALUES ('add','Agregar empleados','employees');
INSERT INTO TOP_01 VALUES ('modify','Modificar empleados','employees');
INSERT INTO TOP_01 VALUES ('remove','Eliminar empleados','employees');
INSERT INTO TOP_01 VALUES ('get','Ver empleados','employees');

INSERT INTO TOP_01 VALUES ('add','Agregar debitos','debits');
INSERT INTO TOP_01 VALUES ('modify','Modificar debitos','debits');
INSERT INTO TOP_01 VALUES ('remove','Eliminar debitos','debits');
INSERT INTO TOP_01 VALUES ('get','Ver debitos','debits');
INSERT INTO TOP_01 VALUES ('addtypes','Agregar un tipo de debito','debits');
INSERT INTO TOP_01 VALUES ('modifytypes','Modificar un tipo de debito','debits');
INSERT INTO TOP_01 VALUES ('removetypes','Eliminar un tipo de debito','debits');
INSERT INTO TOP_01 VALUES ('gettypes','Ver los tipos de debitos','debits');

INSERT INTO TOP_01 VALUES ('callprice','Modificar precio de llamadas','payrolls');
INSERT INTO TOP_01 VALUES ('addcalls','Agregar registro de llamadas','payrolls');
INSERT INTO TOP_01 VALUES ('getcalls','Ver las llamadas','payrolls');
INSERT INTO TOP_01 VALUES ('generate','Generar planilla','payrolls');
INSERT INTO TOP_01 VALUES ('aprove','Aprovar planilla generada','payrolls');
INSERT INTO TOP_01 VALUES ('remove','Eliminar planillas del historico','payrolls');
INSERT INTO TOP_01 VALUES ('get','Ver planillas del historico','payrolls');

INSERT INTO TOP_01 VALUES ('add','Agregar extras','extras');
INSERT INTO TOP_01 VALUES ('modify','Modificar extras','extras');
INSERT INTO TOP_01 VALUES ('remove','Eliminar extras','extras');
INSERT INTO TOP_01 VALUES ('get','Ver extras','extras');

INSERT INTO TOP_01 VALUES ('add','Agregar penalizaciones','recess');
INSERT INTO TOP_01 VALUES ('modify','Modificar penalizaciones','recess');
INSERT INTO TOP_01 VALUES ('remove','Eliminar penalizaciones','recess');
INSERT INTO TOP_01 VALUES ('get','Ver penalizaciones','recess');

SELECT * FROM FOP_01(1,'add','employees');
SELECT * FROM FOP_01(1,'modify','employees');
SELECT * FROM FOP_01(1,'remove','employees');
SELECT * FROM FOP_01(1,'get','employees');
SELECT * FROM FOP_01(1,'get','users');
SELECT * FROM FOP_01(1,'get','extras');

select * from tprr_01;

SELECT * FROM FUS_01('Jonnathan','JonnCh','123',1,1,'jcharpentier@mobilixe.net');




