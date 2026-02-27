using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Smeaql.Compilers;
using SqlKata;

namespace Smeaql.Benchmarks;

[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class Benchmarks
{
    [Benchmark]
    public void CompileQuerySmeaql()
    {
        var query = new SqlQuery("Fruits")
            .Select("Id", "Name", "Color", "Taste")
            .Where("Name", "Apple")
            .Where("Color", "Red")
            .LeftJoin("NutritionData", "NutritionData.FoodId", "Fruit.Id")
            .LeftJoin("PurchaseHistory", "PurchaseHistory.ItemId", "Fruit.Id");
        _ = new SqlServerCompiler().Compile(query);
    }

    [Benchmark]
    public void CompileQuerySqlKata()
    {
        var query = new Query("Fruits")
            .Select("Id", "Name", "Color", "Taste")
            .Where("Name", "Apple")
            .Where("Color", "Red")
            .LeftJoin("NutritionData", "NutritionData.FoodId", "Fruit.Id")
            .LeftJoin("PurchaseHistory", "PurchaseHistory.ItemId", "Fruit.Id");
        _ = new SqlKata.Compilers.SqlServerCompiler().Compile(query);
    }
}
