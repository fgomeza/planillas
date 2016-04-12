INSERT INTO locations(
            id, name, call_price)
    VALUES (1,'tibas',500);

INSERT INTO groups(
            name, description, icon)
    VALUES ('debits','/debits/','');

INSERT INTO operations(
            name, description, group_id)
    VALUES ('debits/add','add/','debits');

INSERT INTO operations(
            name, description, group_id)
    VALUES ('debits/delete','delete/','debits');
    
INSERT INTO operations(
            name, description, group_id)
    VALUES ('debits/update','update/','debits');


INSERT INTO roles(
            id, name, location)
    VALUES (1,'Admin',1);

INSERT INTO users(
            id, name, email, username, password, role, location)
    VALUES (1,'Jonn','xxx','JonnCh','$2a$10$R1Lt.GEYQx.NlAJjVBQt6ezG0K3PLCCrwJjMqr2.Wpb8iNUeIHG4u',1,1);

select * from users where username = 'JonnCh' and password = '$2a$10$R1Lt.GEYQx.NlAJjVBQt6ezG0K3PLCCrwJjMqr2.Wpb8iNUeIHG4u';
