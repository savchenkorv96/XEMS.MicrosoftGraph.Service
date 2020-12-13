using System.Collections.Generic;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData
{
    public class JoinUsersToTeamByIdInputData
    {
        public string GUID { get; set; }
        public List<string> UsersGUID { get; set; }
    }
}