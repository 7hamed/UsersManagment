create database DBusers

use DBusers

create table Users
(
	ID int identity(1, 1) not null,
	UserName nvarchar(50) not null,
	Email nvarchar(100) not null,
	Password nvarchar(30) not null,

	constraint PK_Users primary key (ID)
)

alter table Users add ImagePath nvarchar(250) null;


INSERT INTO Users (UserName, Email, Password, ImagePath)
VALUES
('john_doe',        'john.doe@example.com',        'Pass123!', NULL),
('alice_smith',     'alice.smith@example.com',     'Alice@2024', NULL),
('mike_jordan',     'mike.jordan@example.com',     'MJ#45pass', NULL),
('sarah_connor',    'sarah.connor@example.com',    'Terminator1', NULL),
('david_lee',       'david.lee@example.com',       'DLsecure99', NULL),
('emma_watson',     'emma.watson@example.com',     'Hermione7', NULL),
('chris_evans',     'chris.evans@example.com',     'CapAmerica', NULL),
('linda_brown',     'linda.brown@example.com',     'Linda#2023', NULL),
('robert_king',     'robert.king@example.com',     'King$456', NULL),
('nina_williams',   'nina.williams@example.com',   'NinaTekken', NULL);

