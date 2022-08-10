namespace Task2.Helpers
{
    using AutoMapper;
    using Task2.DTO;
    using Task2.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateRequest -> User
            CreateMap<Book, BookDetailDTO>();

            // UpdateRequest -> User
            CreateMap<Review, ReviewDTO>();

            CreateMap<CreateOrUpdateBookDTO, Book>();
            CreateMap<CreateReviewDTO, Review>();
            CreateMap<ShortBookInfo, ShortBookInfoDTO>();
        }
    }
}
