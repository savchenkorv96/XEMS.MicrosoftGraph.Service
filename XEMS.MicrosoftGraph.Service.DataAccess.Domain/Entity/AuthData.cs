using System;

namespace XEMS.MicrosoftGraph.Service.DataAccess.Domain.Entity
{
    public class AuthData
    {
        public int Id { get; set; }
        public int ServiceAccessLevel { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}