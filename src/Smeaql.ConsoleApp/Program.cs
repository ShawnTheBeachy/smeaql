using Smeaql;
using Smeaql.Compilers;

var query = new SqlQuery("Fruits").Select("Color").WhereColumns("Name", "Apple");
var compiledQuery = new SqlServerCompiler().Compile(query);
Console.WriteLine(compiledQuery);
