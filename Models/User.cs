namespace ControllerApiTutorial.Models
{
    public class User
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string? NickName { get; set; }

        public string? Email { get; set; }

        public string Pwd { get; set; }

        public string? CoupleId { get; set; }

        public string Phone { get; set; }

        public int Gender { get; set; }

        public DateTime Created { get; set; }
    }
}
