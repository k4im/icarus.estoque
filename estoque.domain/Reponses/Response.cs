namespace estoque.domain.Reponses
{
    public class Response<T>
    {
        public Response(List<T> data, int paginaAtual, int totalDePaginas, int totaldeItens)
        {
            Data = data;
            PaginaAtual = paginaAtual;
            TotalDePaginas = totalDePaginas;
            TotaldeItens = totaldeItens;
        }

        public List<T> Data { get; } = new List<T>();
        public int PaginaAtual { get; }
        public int TotalDePaginas { get; }
        public int TotaldeItens {get; }
    }
}