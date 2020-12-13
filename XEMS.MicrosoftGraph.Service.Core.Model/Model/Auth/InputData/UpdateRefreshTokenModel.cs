using System;

namespace XEMS.MicrosoftGraph.Service.Core.Domain.Model.InputData.Auth
{
    public class UpdateRefreshTokenModel
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}