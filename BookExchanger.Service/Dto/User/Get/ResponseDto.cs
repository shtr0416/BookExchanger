using BookExchanger.Service.Dto.Book;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookExchanger.Service.Dto.User.Get
{
    public class ResponseDto
    {
        public string UserId { get; set; }
        public string NickName { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public DateTime? LastSignAt { get; set; }
        public IList<BookDto> Books { get; set; }

        public static ResponseDto FromDbEntity(Application.User entity)
        {
            if (entity is null || string.IsNullOrEmpty(entity.UserId))
                throw new ArgumentNullException(nameof(Application.User));

            var responseDto = new ResponseDto();
            responseDto.Address = entity.Address;
            responseDto.City = entity.City;
            responseDto.Country = entity.Country;
            responseDto.Degree = entity.Degree;
            responseDto.District = entity.District;
            responseDto.LastSignAt = entity.LastSignAt;
            responseDto.NickName = entity.NickName;
            responseDto.Province = entity.Province;
            responseDto.UserId = entity.UserId;
            responseDto.Books = new List<BookDto>();

            if (entity.Books.Any())
            {
                var books = entity.Books.Select(item => new BookDto()
                {
                    BookId = item.BookId,
                    BookName = item.BookName,
                    Category = new Category.CategoryDto(item.Category.CategoryId, item.Category.CategoryName),
                    Level = item.Level,
                    PublishedAt = item.PublishedAt,
                    Tags = item.Tags.Contains(",") ? item.Tags.Split(",").ToList() : new List<string>() { item.Tags }
                }).ToList();

                responseDto.Books = books;
            }

            return responseDto;
        }
    }
}