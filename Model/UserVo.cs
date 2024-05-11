namespace MinimalApiTutorial.Model
{
    public class UserVo
    {
        public string? Name { get; set; }
        public string? Cellphone { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }

        public int Gender { get; set; } = 1;
        public string? NickName { get; set; }

        public string? Email { get; set; }

        public int CoupleId { get; set; } = -1;

        public int Id { get; set; }

    }
}
