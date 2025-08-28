------------------- ----- -------------------
------------------- ----- -------------------
--            .NET Core Clase              --
--        Script de base de datos          --
--                  2025                   --
------------------- ----- -------------------
------------------- ----- -------------------



------------------- roles -------------------
create table roles (
  role_id serial not null primary key,
  name varchar(30),
  created_at timestamptz not null default(now())
);

insert into roles(name)
values
('Administrador'),
('Usuario');

------------------- users -------------------
create table users (
  user_id uuid not null primary key default(gen_random_uuid()),
  email_address varchar(100) not null unique,
  identification varchar(10) not null unique,
  first_name varchar(60),
  last_name varchar(60),
  password varchar(255) not null,
  is_active boolean not null default(true),
  role_id int not null references roles(role_id) default(1),
  created_at timestamptz not null default(now()),
  created_by uuid references users(user_id),
  updated_at timestamptz not null default(now()),
  updated_by uuid references users(user_id)
);

------------------- projects -------------------
create table projects_statuses (
  project_status_id serial not null primary key,
  name varchar(100) not null,
  created_at timestamptz not null default(now())
);

insert into projects_statuses(name)
values
('Sin empezar'),
('En proceso'),
('Completado');

-- Rol: Cualquiera
create table tasks (
  task_id uuid not null primary key default(gen_random_uuid()),
  name varchar(50) not null,
  description text not null,
  status_id int not null references tasks_statuses(task_status_id) default(1),
  created_at timestamptz not null default(now()),
  created_by uuid not null references users(user_id),
  updated_at timestamptz not null default(now()),
  updated_by uuid not null references users(user_id)
);

-- Rol: Administrador
create table projects (
  project_id uuid not null primary key default(gen_random_uuid()),
  name varchar(50) not null,
  description text not null,
  status_id int not null references projects_statuses(project_status_id) default(1),
  banner varchar(255) not null,
  created_at timestamptz not null default(now()),
  created_by uuid not null references users(user_id),
  updated_at timestamptz not null default(now()),
  updated_by uuid not null references users(user_id)
);

create table projects_users (
  project_user_id serial not null primary key,
  project_id uuid not null references projects(project_id) on delete cascade,
  user_id uuid not null references users(user_id),
  added_at timestamptz not null default(now())
);

create table projects_tasks (
  project_task_id serial not null primary key,
  project_id uuid not null references projects(project_id) on delete cascade,
  task_id uuid not null references tasks(task_id),
  added_at timestamptz not null default(now())
);

------------------- tasks -------------------
create table tasks_statuses (
  task_status_id serial not null primary key,
  name varchar(100) not null,
  created_at timestamptz not null default(now())
);

insert into tasks_statuses (name)
values
('Sin empezar'),
('En proceso'),
('Completada');
 
-- Rol: Cualquiera
create table tasks (
  task_id uuid not null primary key default(gen_random_uuid()),
  name varchar(50) not null,
  description text not null,
  status_id int not null references tasks_statuses(task_status_id) default(1),
  user_id uuid references users(user_id),
  project_id uuid references projects(project_id) on delete cascade,
  created_at timestamptz not null default(now()),
  created_by uuid not null references users(user_id),
  updated_at timestamptz not null default(now()),
  updated_by uuid not null references users(user_id),
  check (user_id is not null or project_id is not null)
);