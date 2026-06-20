create database LibraryDB;

use LibraryDB;

-- Create table books
create table Books 
(
    BookId Int IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(200) UNIQUE,
    Author VARCHAR(100),
    TotalCopies INT,
    AvailableCopies INT,
    PublishedDate DATE,
    Genre VARCHAR(50),
    Language VARCHAR(20)
)
go

-- Insert sample data into the Bookes table
INSERT INTO [dbo].[Books] 
    ([Title], [Author], [TotalCopies], [AvailableCopies], [PublishedDate], [Genre], [Language])
VALUES
    ('The Great Gatsby', 'F. Scott Fitzgerald', 5, 3, '1925-04-10', 'Fiction', 'English'),
    ('To Kill a Mockingbird', 'Harper Lee', 7, 5, '1960-07-11', 'Fiction', 'English'),
    ('1984', 'George Orwell', 6, 4, '1949-06-08', 'Dystopian', 'English'),
    ('Pride and Prejudice', 'Jane Austen', 4, 2, '1813-01-28', 'Romance', 'English'),
    ('The Catcher in the Rye', 'J.D. Salinger', 5, 3, '1951-07-16', 'Fiction', 'English'),
    ('The Hobbit', 'J.R.R. Tolkien', 8, 6, '1937-09-21', 'Fantasy', 'English'),
    ('Lord of the Flies', 'William Golding', 4, 3, '1954-09-17', 'Fiction', 'English'),
    ('Harry Potter and the Sorcerer''s Stone', 'J.K. Rowling', 10, 7, '1997-06-26', 'Fantasy', 'English'),
    ('The Da Vinci Code', 'Dan Brown', 6, 4, '2003-03-18', 'Mystery', 'English'),
    ('The Alchemist', 'Paulo Coelho', 5, 5, '1988-01-01', 'Fiction', 'English'),
    ('Animal Farm', 'George Orwell', 4, 2, '1945-08-17', 'Satire', 'English'),
    ('The Book Thief', 'Markus Zusak', 5, 3, '2005-03-14', 'Historical', 'English'),
    ('The Chronicles of Narnia', 'C.S. Lewis', 7, 5, '1950-10-16', 'Fantasy', 'English'),
    ('The Hunger Games', 'Suzanne Collins', 6, 4, '2008-09-14', 'Dystopian', 'English'),
    ('The Girl with the Dragon Tattoo', 'Stieg Larsson', 5, 3, '2005-08-01', 'Mystery', 'English'),
    ('The Kite Runner', 'Khaled Hosseini', 5, 4, '2003-05-29', 'Fiction', 'English'),
    ('Life of Pi', 'Yann Martel', 4, 3, '2001-09-11', 'Adventure', 'English'),
    ('The Road', 'Cormac McCarthy', 3, 2, '2006-09-26', 'Apocalyptic', 'English'),
    ('The Shining', 'Stephen King', 5, 3, '1977-01-28', 'Horror', 'English'),
    ('The Picture of Dorian Gray', 'Oscar Wilde', 4, 4, '1890-07-01', 'Gothic', 'English');   

-- Get all books in the library
SELECT * FROM [dbo].[Books];

-- Create table Users
create table Users
(
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    EmailId VARCHAR(50) PRIMARY KEY,
    Password VARCHAR(50) NOT NULL,
    Country VARCHAR(50) NOT NULL
)
GO

-- Insert data into Users table
INSERT INTO [dbo].[Users] 
    ([FirstName], [LastName], [EmailId], [Password], [Country])
VALUES
    ('Muthuraman', 'Sivalingkam', 'smuthuram47@gmail.com', '12204@Muthu', 'India');
GO

-- Query Users
SELECT * FROM Users;

-- Delete table
DROP TABLE Users