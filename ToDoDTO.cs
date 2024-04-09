namespace MinimalApiTutorial
{
    public class ToDoDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;

        public bool IsComplete { get; set; }

        public ToDoDTO() {

        }

        public ToDoDTO(Todo todo) => (Id, Name, IsComplete) = (todo.Id,todo.Name,todo.IsComplete);
    }
}
