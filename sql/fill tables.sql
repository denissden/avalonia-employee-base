insert into public."Departments" ("Address") values
('Москва, ул. Пушкина, 1'),
('Екатеринбург, ул. Лермонтова, 8');

insert into public."Employees" ("FirstName", "MiddleName", "LastName", "PhoneNumber", "EmailAddress", "DepartmentId") values
('Иван', 'Иванович', 'Иванов', '9167462647', 'ivan@mail.ru', 1),
('Екатерина', 'Павловна', 'Ключникова', '9776274625', 'kat284@gmail.com', 2),
('Геннадий', 'Геннадьевич', 'Горин', '9167777777', 'gorin@gorin.gorin', 1);


insert into public."Clients" ("FirstName", "MiddleName", "LastName", "PhoneNumber", "EmailAddress") values
('Мария', '', 'Сулейманова', '9167364524', 'mary@mail.ru'),
('Дмитрий', '', 'Гордон', '9168374638', 'dima1977@mail.ru'),
('Константин', 'Константинович', 'Сидоров', '9204827428', 'k228@mail.ru');

insert into public."Tasks" ("Name", "Description", "Responsible") values
('Сайт', 'Создать сайт по шаблону', 1),
('Дизайн лого', '', 2),
('Стать мемом', 'а описания не будет', 3),
('Дизайн веб-сайт', 'создать шаблон веб-сайта', 2);

insert into public."Applications" ("Description", "ClientId", "TaskId") values
('Заявка на выдачу заказа', 1, 2),
('Получение заказа',2, 1),
('Создать мем про меня',2, 3);
