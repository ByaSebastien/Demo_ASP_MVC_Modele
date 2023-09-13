using System.Text.Json;

namespace GUI
{
    public class SessionManager
    {
        private readonly ISession _Session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _Session = httpContextAccessor.HttpContext.Session;
        }

        public MemberSession CurrentMember
        {
            get
            {
                if (_Session.GetString("user") is not null)
                {
                    return JsonSerializer.Deserialize<MemberSession>(_Session.GetString("user"));
                }
                return null;
            }

            set
            {
                _Session.SetString("user", JsonSerializer.Serialize(value));
            }
        }
        public void Logout()
        {
            CurrentMember = null;
        }
    }
}
