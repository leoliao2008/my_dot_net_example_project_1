namespace ControllerApiTutorial.Models
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string? User_Name { get; set; }

        public string? Nick_name { get; set; }

        public string? Email { get; set; }

        public string? Pwd { get; set; }

        public int CoupleId { get; set; } = -1;

        public string? Phone { get; set; }

        public int Gender { get; set; }

        public DateTime Created { get; set; }

    }
}
