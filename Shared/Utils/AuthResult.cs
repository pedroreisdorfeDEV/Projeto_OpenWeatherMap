using projetoGloboClima.Models.Entities;

namespace projetoGloboClima.Shared.Utils
{
    public class AuthResult
    {
        public UserEntity User { get; set; }
        public string Token { get; set; }
    }
}
