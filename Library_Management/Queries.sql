use Library;


-- Insert sample data into the Bookes table
INSERT INTO [dbo].[Bookes] 
    ([Title], [Author], [TotalCopies], [AvailableCpoies], [PublishedDate], [Genre], [ISBN], [Language])
VALUES
    ('The Great Gatsby', 'F. Scott Fitzgerald', 5, 3, '1925-04-10', 'Fiction', '978-0-7432-7356-5', 'English'),
    ('To Kill a Mockingbird', 'Harper Lee', 7, 5, '1960-07-11', 'Fiction', '978-0-06-112008-4', 'English'),
    ('1984', 'George Orwell', 6, 4, '1949-06-08', 'Dystopian Fiction', '978-0-452-28423-4', 'English'),
    ('Pride and Prejudice', 'Jane Austen', 4, 2, '1813-01-28', 'Romance', '978-0-14-143951-8', 'English'),
    ('The Catcher in the Rye', 'J.D. Salinger', 5, 3, '1951-07-16', 'Fiction', '978-0-316-76948-0', 'English'),
    ('The Hobbit', 'J.R.R. Tolkien', 8, 6, '1937-09-21', 'Fantasy', '978-0-547-92822-7', 'English'),
    ('Lord of the Flies', 'William Golding', 4, 3, '1954-09-17', 'Fiction', '978-0-399-50148-7', 'English'),
    ('Harry Potter and the Sorcerer''s Stone', 'J.K. Rowling', 10, 7, '1997-06-26', 'Fantasy', '978-0-439-70818-8', 'English'),
    ('The Da Vinci Code', 'Dan Brown', 6, 4, '2003-03-18', 'Mystery', '978-0-307-47427-8', 'English'),
    ('The Alchemist', 'Paulo Coelho', 5, 5, '1988-01-01', 'Fiction', '978-0-06-231500-7', 'English'),
    ('Animal Farm', 'George Orwell', 4, 2, '1945-08-17', 'Satire', '978-0-452-28424-1', 'English'),
    ('The Book Thief', 'Markus Zusak', 5, 3, '2005-03-14', 'Historical Fiction', '978-0-375-83635-1', 'English'),
    ('The Chronicles of Narnia', 'C.S. Lewis', 7, 5, '1950-10-16', 'Fantasy', '978-0-06-440499-0', 'English'),
    ('The Hunger Games', 'Suzanne Collins', 6, 4, '2008-09-14', 'Dystopian Fiction', '978-0-439-02352-8', 'English'),
    ('The Girl with the Dragon Tattoo', 'Stieg Larsson', 5, 3, '2005-08-01', 'Mystery', '978-0-307-27998-7', 'English'),
    ('The Kite Runner', 'Khaled Hosseini', 5, 4, '2003-05-29', 'Fiction', '978-1-594-63193-1', 'English'),
    ('Life of Pi', 'Yann Martel', 4, 3, '2001-09-11', 'Adventure', '978-0-15-602732-8', 'English'),
    ('The Road', 'Cormac McCarthy', 3, 2, '2006-09-26', 'Post-Apocalyptic Fiction', '978-0-307-38789-9', 'English'),
    ('The Shining', 'Stephen King', 5, 3, '1977-01-28', 'Horror', '978-0-307-74365-7', 'English'),
    ('The Picture of Dorian Gray', 'Oscar Wilde', 4, 4, '1890-07-01', 'Gothic Fiction', '978-0-14-143957-0', 'English');   

-- Get all books in the library
SELECT * FROM [dbo].[Bookes];