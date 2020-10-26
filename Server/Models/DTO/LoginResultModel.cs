namespace Server.Models.DTO
{
    public class LoginResultModel
    {
        public string Username { get; private set; }

        public string Token { get; private set; }

        public LoginResultModel(string username, string token)
        {
            this.Username = username;
            this.Token = token;
        }

    }
}
