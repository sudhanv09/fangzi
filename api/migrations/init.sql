create table users
(
    name      varchar   not null,
    email     varchar unique  not null,
    id        serial
        primary key,
    join_date timestamp not null,
    password  varchar   not null
);

create table listings
(
    title       varchar not null,
    contact       varchar not null,
    address     varchar not null,
    cost        varchar not null,
    description varchar not null,
    images      varchar not null,
    user_id     integer not null
        references users,
    utilites    character varying[],
    meta        character varying[],
    id          serial
        primary key,
    status      boolean not null
);