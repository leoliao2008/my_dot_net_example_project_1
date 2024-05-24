namespace MinimalApiTutorial.Jwt
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = "";

        public string Audience { get; set; } = "";

        public string SecretKey { get; set; } = "";

        public int Expire { get; set; }

        public override string ToString() {
            return "Issure = "+Issuer+"\n"+"Audience="+Audience+"\n"+"SecretKey="+SecretKey+"\n";
        }
    }
}
