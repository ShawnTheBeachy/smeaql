using Smeaql;
using Smeaql.Compilers;

var query = new SqlQuery("Fruits").Select("Color").Where("Name", "Apple").Where("Color", "Red");
var compiledQuery = new SqlServerCompiler().Compile(query);
Console.WriteLine(compiledQuery.Sql);

foreach (var parameter in compiledQuery.Parameters)
    Console.WriteLine($"{parameter.Key}: {parameter.Value}");
