using System;
using Newtonsoft.Json;
using XEMS.MicrosoftGraph.Service.Core.Domain.Enums;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.Auth.OutputData
{
    public class AuthResultModel
    {
        public int Id { get; set; }
        public ServiceAccessLevel ServiceAccessLevel { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string Login { get; set; }

        [JsonIgnore] public string Password { get; set; }

        public bool IsActive { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}