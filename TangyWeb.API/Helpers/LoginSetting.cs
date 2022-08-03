namespace TangyWeb.API.Helpers
{
    public class LoginSetting
    {
        public string SecretKey { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
    }
}
