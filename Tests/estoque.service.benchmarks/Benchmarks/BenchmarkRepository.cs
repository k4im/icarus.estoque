namespace estoque.service.benchmarks.Benchmarks;

[MemoryDiagnoser, RankColumn, ShortRunJob]
public class BenchmarkRepository
{

    public void PaginarComDapper()
    {
        try
        {
            using var conn = new SqliteConnection("Data Source=memory;:");
        }
        catch (Exception)
        {

        }
    }

    public void PaginarComEf()
    {

    }
}
