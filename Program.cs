using Dapper;
using Microsoft.Data.SqlClient;
using MinimalApiTutorial;
using System.Data;

string dbServerDevelop = "47.113.113.91";
string dbServerProduction = "localhost";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Map("/", () => { return TypedResults.Ok("Demo Server Launches Successfully!"); });

RouteGroupBuilder grpBuilder = app.MapGroup("/student");
grpBuilder.MapGet("/{id}", GetStudentById);

 async Task<IResult> GetStudentById(int id) {

    using (IDbConnection conn = new SqlConnection("Server="+dbServerProduction+"; Database=Demo; User ID=leo;Password=Leoliao2008; TrustServerCertificate=True")) {
        Student stu = await conn.QuerySingleAsync<Student>("SELECT * FROM STUDENT WHERE id = @id",new { id = id});
        if (stu is null)
        {
            return TypedResults.NotFound();
        }
        else { 
            return TypedResults.Ok(stu);
        }
    }
}



app.Run();
