SELECT TOP (1000) [BookId]
      ,[Title]
      ,[Author]
      ,[TotalCopies]
      ,[AvailableCopies]
      ,[PublishedDate]
      ,[Genre]
      ,[Language]
  FROM [Library].[dbo].[Books]

INSERT INTO [Library].[dbo].[Books] ([Title], [Author], [TotalCopies], [AvailableCopies], [PublishedDate], [Genre], [Language])
VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', 5, 5, '1925-04-10', 'Novel', 'English'),
('To Kill a Mockingbird', 'Harper Lee', 10, 8, '1960-07-11', 'Novel', 'English'),
('1984', 'George Orwell', 7, 6, '1949-06-08', 'Dystopian', 'English'),
('Pride and Prejudice', 'Jane Austen', 12, 12, '1813-01-28', 'Romance', 'English'),
('Moby-Dick', 'Herman Melville', 6, 5, '1851-10-18', 'Adventure', 'English'),
('War and Peace', 'Leo Tolstoy', 15, 14, '1869-01-01', 'Historical', 'Russian'),
('Crime and Punishment', 'Fyodor Dostoevsky', 9, 9, '1866-01-01', 'Psychological', 'Russian'),
('The Catcher in the Rye', 'J.D. Salinger', 8, 7, '1951-07-16', 'Novel', 'English'),
('Brave New World', 'Aldous Huxley', 10, 10, '1932-01-01', 'Dystopian', 'English'),
('The Hobbit', 'J.R.R. Tolkien', 20, 18, '1937-09-21', 'Fantasy', 'English'),
('The Lord of the Rings', 'J.R.R. Tolkien', 25, 22, '1954-07-29', 'Fantasy', 'English'),
('Harry Potter and the Philosopher''s Stone', 'J.K. Rowling', 30, 28, '1997-06-26', 'Fantasy', 'English'),
('Harry Potter and the Chamber of Secrets', 'J.K. Rowling', 28, 26, '1998-07-02', 'Fantasy', 'English'),
('Harry Potter and the Prisoner of Azkaban', 'J.K. Rowling', 28, 25, '1999-07-08', 'Fantasy', 'English'),
('Harry Potter and the Goblet of Fire', 'J.K. Rowling', 28, 24, '2000-07-08', 'Fantasy', 'English'),
('Harry Potter and the Order of the Phoenix', 'J.K. Rowling', 28, 23, '2003-06-21', 'Fantasy', 'English'),
('Harry Potter and the Half-Blood Prince', 'J.K. Rowling', 28, 22, '2005-07-16', 'Fantasy', 'English'),
('Harry Potter and the Deathly Hallows', 'J.K. Rowling', 28, 21, '2007-07-21', 'Fantasy', 'English'),
('The Da Vinci Code', 'Dan Brown', 18, 17, '2003-03-18', 'Thriller', 'English'),
('Angels & Demons', 'Dan Brown', 18, 16, '2000-05-01', 'Thriller', 'English'),
('Inferno', 'Dan Brown', 18, 15, '2013-05-14', 'Thriller', 'English'),
('Origin', 'Dan Brown', 18, 14, '2017-10-03', 'Thriller', 'English'),
('The Alchemist', 'Paulo Coelho', 20, 19, '1988-01-01', 'Adventure', 'Portuguese'),
('Veronika Decides to Die', 'Paulo Coelho', 15, 14, '1998-01-01', 'Drama', 'Portuguese'),
('Eleven Minutes', 'Paulo Coelho', 15, 13, '2003-01-01', 'Drama', 'Portuguese'),
('The Pilgrimage', 'Paulo Coelho', 15, 12, '1987-01-01', 'Adventure', 'Portuguese'),
('Don Quixote', 'Miguel de Cervantes', 12, 11, '1605-01-16', 'Novel', 'Spanish'),
('One Hundred Years of Solitude', 'Gabriel García Márquez', 14, 13, '1967-06-05', 'Magic Realism', 'Spanish'),
('Love in the Time of Cholera', 'Gabriel García Márquez', 14, 12, '1985-01-01', 'Romance', 'Spanish')