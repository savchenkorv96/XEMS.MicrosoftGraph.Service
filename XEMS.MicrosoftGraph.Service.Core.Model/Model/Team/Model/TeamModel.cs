using System.Collections.Generic;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.Model
{
    public class TeamModel
    {
        public TeamModel()
        {
        }

        public TeamModel(Microsoft.Graph.Team team)
        {
            GUID = team.Id;
            Name = team.DisplayName;
        }

        public string GUID { get; set; }
        public string Name { get; set; }
        public List<TeamOwnerModel> TeamOwnerModel { get; set; }
    }
}