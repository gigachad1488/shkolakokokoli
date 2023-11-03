![30258560111602294_7bfd_640x](https://github.com/gigachad1488/shkolakokokoli/assets/112997166/258bf74a-9c2f-4f42-bb70-6fddb5006099)

create table Books
(
    book_id int auto_increment
        primary key,
    title   varchar(255) not null,
    author  varchar(255) not null
);

create table Client
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

create table Genders
(
    id   int auto_increment
        primary key,
    Name varchar(255) charset utf8 null
);

create table Goods
(
    id    int auto_increment
        primary key,
    Name  varchar(255) charset utf8 not null,
    Count int                       not null
);

create table Language
(
    id   int auto_increment
        primary key,
    name text not null
);

create table Storage
(
    id      int auto_increment
        primary key,
    Name    varchar(255) charset utf8 not null,
    Address varchar(255) charset utf8 not null
);

create table Good_In_Storage
(
    id         int auto_increment
        primary key,
    storage_id int null,
    good_id    int null,
    constraint Good_In_Storage_ibfk_1
        foreign key (storage_id) references Storage (id),
    constraint Good_In_Storage_ibfk_2
        foreign key (good_id) references Goods (id)
);

create index good_id
    on Good_In_Storage (good_id);

create index storage_id
    on Good_In_Storage (storage_id);

create table Teacher
(
    id        int auto_increment
        primary key,
    firstname text not null,
    surname   text not null
);

create table Course
(
    id          int auto_increment
        primary key,
    name        text not null,
    teacher_id  int  not null,
    language_id int  not null,
    price       int  not null,
    constraint language_id
        foreign key (language_id) references Language (id),
    constraint teacher_id
        foreign key (teacher_id) references Teacher (id)
);

create table Class
(
    id        int auto_increment
        primary key,
    name      text not null,
    places    int  not null,
    course_id int  not null,
    constraint course_id
        foreign key (course_id) references Course (id)
);

create table Client_on_class
(
    id        int auto_increment
        primary key,
    client_id int not null,
    class_id  int not null,
    constraint class_id
        foreign key (class_id) references Class (id),
    constraint client_id
        foreign key (client_id) references Client (id)
);

create table Lesson
(
    id         int auto_increment
        primary key,
    class_id   int      null,
    time_start datetime not null,
    time_end   datetime not null,
    constraint Lesson_ibfk_1
        foreign key (class_id) references Class (id)
);

create index class_id
    on Lesson (class_id);

create table User
(
    UserID   int auto_increment
        primary key,
    Name     varchar(255)                                       not null,
    Password varchar(255)                                       not null,
    Role     enum ('admin', 'accountant', 'employee', 'client') not null
);

create table Accounts
(
    AccountID int auto_increment
        primary key,
    UserID    int            null,
    Balance   decimal(10, 2) not null,
    constraint Accounts_ibfk_1
        foreign key (UserID) references User (UserID)
);

create index UserID
    on Accounts (UserID);

create table AuditLog
(
    LogID            int auto_increment
        primary key,
    UserID           int      null,
    DateTime         datetime not null,
    EventDescription text     not null,
    Details          text     null,
    constraint AuditLog_ibfk_1
        foreign key (UserID) references User (UserID)
);

create index UserID
    on AuditLog (UserID);

create table FinancialTransactions
(
    TransactionID   int auto_increment
        primary key,
    UserID          int                        null,
    Date            date                       not null,
    TransactionType enum ('income', 'expense') not null,
    Amount          decimal(10, 2)             not null,
    Description     text                       null,
    constraint FinancialTransactions_ibfk_1
        foreign key (UserID) references User (UserID)
);

create index UserID
    on FinancialTransactions (UserID);

create table TaxLiabilities
(
    TaxID        int auto_increment
        primary key,
    UserID       int            null,
    TaxType      varchar(255)   not null,
    TaxAmount    decimal(10, 2) not null,
    TaxEventDate date           not null,
    constraint TaxLiabilities_ibfk_1
        foreign key (UserID) references User (UserID)
);

create index UserID
    on TaxLiabilities (UserID);

create table Users
(
    user_id    int auto_increment
        primary key,
    first_name varchar(255) not null,
    last_name  varchar(255) not null,
    gender     int          null,
    constraint gender
        foreign key (gender) references Genders (id)
);

create table BookRentals
(
    rental_id  int auto_increment
        primary key,
    user_id    int  not null,
    book_id    int  not null,
    start_date date not null,
    end_date   date not null,
    constraint BookRentals_ibfk_1
        foreign key (user_id) references Users (user_id),
    constraint BookRentals_ibfk_2
        foreign key (book_id) references Books (book_id)
);

create index book_id
    on BookRentals (book_id);

create index user_id
    on BookRentals (user_id);
