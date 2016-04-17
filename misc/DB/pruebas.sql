INSERT INTO locations(
            id, name, call_price,active)
    VALUES (1,'tibas',500,true);

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
            id, name, location,active)
    VALUES (1,'Admin',1,true);

INSERT INTO users(
            id, name, email, username, password, role, location,active)
    VALUES (1,'Jonn','xxx','jonnch','$2a$10$R1Lt.GEYQx.NlAJjVBQt6ezG0K3PLCCrwJjMqr2.Wpb8iNUeIHG4u',1,1,true);

insert into employees (id,name,id_card,cms,location,active,salary,account) values (1,'Jonnathan charpentier','1-1542-0808',null,1,true,800000,'1-054-4804-01');

select * from users where username = 'jonnch' and password = '$2a$10$R1Lt.GEYQx.NlAJjVBQt6ezG0K3PLCCrwJjMqr2.Wpb8iNUeIHG4u';

