using AutoMapper;
using BookStore.API.Models;
using BookStore.API.Data;

namespace BookStore.API.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Books, BookModel>().ReverseMap();
        }
    }
}
