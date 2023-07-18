using AutoMapper;
using estoque.domain.Entity;

namespace estoque.infra.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Produto, ProdutoDisponivel>();
        }
    }
}