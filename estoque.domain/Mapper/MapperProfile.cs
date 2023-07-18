using AutoMapper;
using estoque.domain.Entity;

namespace estoque.domain.Mapper
{

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Produto, ProdutoDisponivel>();
        }
    }
}