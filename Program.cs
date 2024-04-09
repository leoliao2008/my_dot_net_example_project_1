using Microsoft.EntityFrameworkCore;
using MinimalApiTutorial;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDatabase>(optBuilder => optBuilder.UseInMemoryDatabase("TodoList"));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

RouteGroupBuilder todoItems = app.MapGroup("/todoitems");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete",GetCompletes);
todoItems.MapGet("/{id}", GetById);
todoItems.MapPost("/",PostTodo);
todoItems.MapPut("/{id}", PutToDo);
todoItems.MapDelete("/{id}", DeleteToDo);

static async Task<IResult> GetAllTodos(ToDoDatabase db) {

    return TypedResults.Ok(await db.Todos.Select(x=>new ToDoDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetCompletes(ToDoDatabase db) {
    return TypedResults.Ok(await db.Todos.Where(it=>it.IsComplete).Select(x => new ToDoDTO(x)).ToListAsync());
}

static async Task<IResult> GetById(int id, ToDoDatabase db) {
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        return TypedResults.Ok(new ToDoDTO(todo));
    }
    else { 
        return TypedResults.NotFound();
    }
}

static async Task<IResult> PostTodo(ToDoDTO dto, ToDoDatabase db) {
    Todo todo = new Todo();
    todo.Name = dto.Name;
    todo.IsComplete = dto.IsComplete;
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    dto = new ToDoDTO(todo);
    return TypedResults.Created($"/{dto.Id}",dto);
}

static async Task<IResult> PutToDo(int id, ToDoDTO dto, ToDoDatabase db) {
    if (await db.Todos.FindAsync(id) is Todo todo) { 
        todo.Name = dto.Name;
        todo.IsComplete = dto.IsComplete;
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }else
    {
        return TypedResults.NotFound();
    }

}

static async Task<IResult> DeleteToDo(int id, ToDoDatabase db) {
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
    else
    {
        return TypedResults.NotFound();
    }
}

app.Run();
