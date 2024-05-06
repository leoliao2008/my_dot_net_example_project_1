using Dapper;
using Microsoft.Data.SqlClient;
using MinimalApiTutorial;
using System.Data;

string dbServerDevelop = "47.113.113.91";
string dbServerProduction = "172.23.0.1";
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
//app.Environment.IsDevelopment
app.Map("/", () => { return TypedResults.Ok("Demo Server Launches Successfully!"); });

RouteGroupBuilder grpBuilder = app.MapGroup("/student");
grpBuilder.MapGet("/{id}", GetStudentById);

 async Task<IResult> GetStudentById(int id) {

    using (IDbConnection conn = new SqlConnection("Server="+ dbServerDevelop + "; Database=Demo; User ID=leo;Password=Leoliao2008; TrustServerCertificate=True")) {
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
