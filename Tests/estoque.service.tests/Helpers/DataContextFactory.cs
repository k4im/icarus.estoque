namespace estoque.service.tests.Helpers
{
    public static class DataContextFactory
    {
        public static DataContext factoryContext()
            => new DataContext(new DbContextOptionsBuilder().UseInMemoryDatabase(
                new Guid().ToString())
                .Options);
    }
}