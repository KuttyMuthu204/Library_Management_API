namespace Library_Management.Utilities
{
    public static class Routes
    {
        // Collection route for books
        public const string GetBooks = "books";

        // Route to get a single book by id
        public const string GetBookById = "book/{id}";

        // Route to add a book
        public const string AddBook = "book";

        // Route to update a book
        public const string UpdateBooks = "book/{id}";

        // Route to delete a book
        public const string DeleteBooks = "book/{id}";
    }
}
