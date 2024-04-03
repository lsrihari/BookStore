using BookStore.API.Models;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookAsync(int bookId);
        Task<int> AddBookAsync(BookModel bookModel);
    }
}
