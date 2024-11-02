CREATE TABLE Login (
    usernameOrEmail NVARCHAR(255) NOT NULL, 
	 password NVARCHAR(255) NOT NULL,         
    confirmPassword NVARCHAR(255) NOT NULL   
);

INSERT INTO Login (usernameOrEmail, password, confirmPassword)
VALUES ('abc123@gmail.com', '123456', '123456'),  
       ('abc123', '123456', '123456');       
	   
	 

CREATE TABLE Transactions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(50),
    Money NVARCHAR(50),
    Date DATE,
	Category NVARCHAR(50),
    Source NVARCHAR(50),
    Description NVARCHAR(255),
    Hour INT,
    Minute INT
);

INSERT INTO Transactions (Type, Money, Date, Category, Source, Description, Hour, Minute) VALUES 
('Thu', '5,000,000 VND', '2024-10-01', 'Luong', 'Ngan hang', 'Luong thang 10', 9, 30),
('chitieu', '1,500,000 VND', '2024-10-05', 'Mua sam', 'The tin dung', 'Mua quan ao', 14, 45),
('Thu', '2,000,000 VND', '2024-09-20', 'Ban hang', 'Tien mat', 'Ban hang online', 11, 0),
('chitieu', '1,000,000 VND', '2024-01-15', 'Giai tri', 'Vi dien tu', 'Di xem phim', 19, 15),
('Thu', '3,500,000 VND', '2023-12-31', 'Dau tu', 'Chuyen khoan', 'Lai dau tu', 10, 20);
 


CREATE TABLE Accounts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    AccountType NVARCHAR(50),
    Balance NVARCHAR(50)
);
INSERT INTO Accounts (AccountType, Balance) VALUES
('Vi', '2,000,000 VND'),
('Quy', '5,000,000 VND'),
('Tien mat', '3,500,000 VND'),
('Tai khoan ngan hang', '10,000,000 VND');


CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    CategoryType NVARCHAR(50) NOT NULL
);
INSERT INTO Categories (CategoryName, CategoryType) VALUES 
('Luong', 'thunhap'),
('Thuong', 'thunhap'),
('Kinh doanh', 'thunhap'),
('Lai ngan hang', 'thunhap'),
('An uong', 'chitieu'),
('Mua sam', 'chitieu'),
('Dien nuoc', 'chitieu'),
('Du lich', 'chitieu');


CREATE TABLE Reports (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ReportType NVARCHAR(50), 
    Thunhap DECIMAL(18, 2) NULL,  
    chitieu DECIMAL(18, 2) NULL,
Tong AS (Thunhap - chitieu),
 Sukien NVARCHAR(100) NULL, 
   Danhmuc NVARCHAR(50) NULL 
);

INSERT INTO Reports (ReportType, Thunhap, chitieu)
VALUES
('Tong Quan', 10000000, 5000000),
('Tong Quan', 12000000, 7000000);

INSERT INTO Reports (ReportType, Danhmuc, chitieu)
VALUES
('chitieu Tieu', 'Thuc pham', 2000000),
('chitieu Tieu', 'Giai tri', 1000000),
('chitieu Tieu', 'chitieu Phi Khac', 4000000);

INSERT INTO Reports (ReportType, Sukien, Thunhap, chitieu)
VALUES
('Su kien', 'Chuyen di A', 5000000, 2000000),
('Su kien', 'Su kien B', 8000000, 3000000);


delete from Login where usernameOrEmail='sdf12@gmail.com'

select * from Transactions



