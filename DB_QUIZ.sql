USE  DBQUIZ


CREATE TABLE TBL_ADMIN
(
AD_ID INT IDENTITY PRIMARY KEY,
AD_NAME NVARCHAR(20) NOT NULL UNIQUE,
AD_PASSWORD NVARCHAR(20) NOT NULL
)

CREATE TABLE TBL_QUESTIONS
(
QUESTION_ID INT IDENTITY PRIMARY KEY,
Q_TEXT NVARCHAR(MAX) NOT NULL,
OPA NVARCHAR(20) NOT NULL UNIQUE,
OPB NVARCHAR(20) NOT NULL UNIQUE,
OPC NVARCHAR(20) NOT NULL UNIQUE,
OPD NVARCHAR(20) NOT NULL UNIQUE,
COP NVARCHAR(20) NOT NULL

)

CREATE TABLE TBL_STUDENT
(

S_ID INT IDENTITY PRIMARY KEY,
S_NAME NVARCHAR(20) NOT NULL UNIQUE,
S_PASSWORD NVARCHAR(20) NOT NULL,
S_IMAGE NVARCHAR(MAX) NOT NULL
)

CREATE TABLE TBL_SETEXAM
(
EXAM_ID INT IDENTITY PRIMARY KEY,
EXAM_DATE DATETIME,
EXAM_FK_STU INT FOREIGN KEY REFERENCES TBL_STUDENT (S_ID),
EXAM_NAME NVARCHAR(50) NOT NULL,
EXAM_STD_SCORE INT
)




select * from sys.tables

create table tbl_category
(
cat_id int identity primary key,
cat_name nvarchar(50) not null,
cat_fk_adid int foreign key references TBL_ADMIN(ad_id)
)

alter table TBL_QUESTIONS
add q_fk_catid int foreign key references tbl_category(cat_id)

select * from TBL_QUESTIONS

insert into TBL_ADMIN values('root','admin')

select * from TBL_ADMIN
select * from tbl_category

------------------------
alter table tbl_category

add cat_encryptedstring varchar(max)







