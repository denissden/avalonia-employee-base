alter table public."Employees" add constraint FK_Department foreign key ("DepartmentId") references public."Departments" ("Id");

alter table public."Tasks" add constraint FK_Employee foreign key ("Responsible") references public."Employees" ("Id");

alter table public."Applications" add constraint FK_Client foreign key ("ClientId") references public."Clients" ("Id");
alter table public."Applications" add constraint FK_Task foreign key ("TaskId") references public."Tasks" ("Id");

