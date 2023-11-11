![30258560111602294_7bfd_640x](https://github.com/gigachad1488/shkolakokokoli/assets/112997166/258bf74a-9c2f-4f42-bb70-6fddb5006099)

create table books
(
    book_id int auto_increment
        primary key,
    title   varchar(255) not null,
    author  varchar(255) not null
);

create table client
(
    id             int auto_increment
        primary key,
    firstname      text not null,
    surname        text not null,
    phone          int  not null,
    birthday       date null,
    last_language  text null,
    language_level text null,
    language_needs text null
);

create table genders
(
    id   int auto_increment
        primary key,
    Name varchar(255) charset utf8mb3 null
);

create table goods
(
    id    int auto_increment
        primary key,
    Name  varchar(255) charset utf8mb3 not null,
    Count int                          not null
);

create table language
(
    id   int auto_increment
        primary key,
    name text not null
);

create table payment
(
    id    int auto_increment
        primary key,
    name  text null,
    price int  not null
);

create table client_payment
(
    id      int auto_increment
        primary key,
    client  int        not null,
    payment int        not null,
    isPaid  tinyint(1) not null,
    constraint client
        foreign key (client) references client (id),
    constraint payment
        foreign key (payment) references payment (id)
);

create table storage
(
    id      int auto_increment
        primary key,
    Name    varchar(255) charset utf8mb3 not null,
    Address varchar(255) charset utf8mb3 not null
);

create table good_in_storage
(
    id         int auto_increment
        primary key,
    storage_id int null,
    good_id    int null,
    constraint Good_In_Storage_ibfk_1
        foreign key (storage_id) references storage (id),
    constraint Good_In_Storage_ibfk_2
        foreign key (good_id) references goods (id)
);

create index good_id
    on good_in_storage (good_id);

create index storage_id
    on good_in_storage (storage_id);

create table teacher
(
    id        int auto_increment
        primary key,
    firstname text not null,
    surname   text not null
);

create table course
(
    id          int auto_increment
        primary key,
    name        text not null,
    teacher_id  int  not null,
    language_id int  not null,
    constraint language_id
        foreign key (language_id) references language (id),
    constraint teacher_id
        foreign key (teacher_id) references teacher (id)
);

create table class
(
    id        int auto_increment
        primary key,
    name      text not null,
    places    int  not null,
    course_id int  not null,
    constraint course_id
        foreign key (course_id) references course (id)
);

create table client_on_class
(
    id        int auto_increment
        primary key,
    client_id int not null,
    class_id  int not null,
    constraint class_id
        foreign key (class_id) references class (id),
    constraint client_id
        foreign key (client_id) references client (id)
);

create table lesson
(
    id         int auto_increment
        primary key,
    class_id   int      null,
    time_start datetime not null,
    time_end   datetime not null,
    constraint Lesson_ibfk_1
        foreign key (class_id) references class (id)
);

create index class_id
    on lesson (class_id);

create table lessons_attendance
(
    id         int auto_increment
        primary key,
    lesson_id  int                  not null,
    client_id  int                  not null,
    attendance tinyint(1) default 0 not null,
    constraint lessons_attendance_ibfk_1
        foreign key (lesson_id) references lesson (id),
    constraint lessons_attendance_ibfk_2
        foreign key (client_id) references client (id)
);

create index client_id
    on lessons_attendance (client_id);

create index lesson_id
    on lessons_attendance (lesson_id);

create table user
(
    UserID   int auto_increment
        primary key,
    Name     varchar(255)                                       not null,
    Password varchar(255)                                       not null,
    Role     enum ('admin', 'accountant', 'employee', 'client') not null
);

create table accounts
(
    AccountID int auto_increment
        primary key,
    UserID    int            null,
    Balance   decimal(10, 2) not null,
    constraint Accounts_ibfk_1
        foreign key (UserID) references user (UserID)
);

create index UserID
    on accounts (UserID);

create table auditlog
(
    LogID            int auto_increment
        primary key,
    UserID           int      null,
    DateTime         datetime not null,
    EventDescription text     not null,
    Details          text     null,
    constraint AuditLog_ibfk_1
        foreign key (UserID) references user (UserID)
);

create index UserID
    on auditlog (UserID);

create table financialtransactions
(
    TransactionID   int auto_increment
        primary key,
    UserID          int                        null,
    Date            date                       not null,
    TransactionType enum ('income', 'expense') not null,
    Amount          decimal(10, 2)             not null,
    Description     text                       null,
    constraint FinancialTransactions_ibfk_1
        foreign key (UserID) references user (UserID)
);

create index UserID
    on financialtransactions (UserID);

create table taxliabilities
(
    TaxID        int auto_increment
        primary key,
    UserID       int            null,
    TaxType      varchar(255)   not null,
    TaxAmount    decimal(10, 2) not null,
    TaxEventDate date           not null,
    constraint TaxLiabilities_ibfk_1
        foreign key (UserID) references user (UserID)
);

create index UserID
    on taxliabilities (UserID);

create table users
(
    user_id    int auto_increment
        primary key,
    first_name varchar(255) not null,
    last_name  varchar(255) not null,
    gender     int          null,
    constraint gender
        foreign key (gender) references genders (id)
);

create table bookrentals
(
    rental_id  int auto_increment
        primary key,
    user_id    int  not null,
    book_id    int  not null,
    start_date date not null,
    end_date   date not null,
    constraint BookRentals_ibfk_1
        foreign key (user_id) references users (user_id),
    constraint BookRentals_ibfk_2
        foreign key (book_id) references books (book_id)
);

create index book_id
    on bookrentals (book_id);

create index user_id
    on bookrentals (user_id);

https://sites.google.com/khpk.ru/alinaviot/%D0%BC%D0%B4%D0%BA-01-01

