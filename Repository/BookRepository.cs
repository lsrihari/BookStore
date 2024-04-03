﻿using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly BookStoreContext _context;
        public BookRepository(BookStoreContext context)
        {
            this._context = context;
        }
        public async Task <List<BookModel>> GetAllBooksAsync()
        {
            var records = await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).ToListAsync();

            return records;
        }
    }
}
