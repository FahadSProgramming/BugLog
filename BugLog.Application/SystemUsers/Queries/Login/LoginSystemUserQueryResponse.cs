namespace BugLog.Application.SystemUsers.Queries.Login
{
    public class LoginSystemUserQueryResponse
    {
        public LoginSystemUserQueryResponse(string token) {
            Token = token;
        }
        public string Token { get; }
    }
}