namespace DevFreela.Application.ViewModels
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel(string email, string token)
        {
            Email = email;
            Token = token;
        }

        public string Email { get; private set; }        
        public string Token { get; private set; }
    }
}