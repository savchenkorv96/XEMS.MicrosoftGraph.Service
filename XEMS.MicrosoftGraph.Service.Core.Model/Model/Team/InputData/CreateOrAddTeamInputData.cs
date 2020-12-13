using System.Collections.Generic;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.Model;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData
{
    public class CreateOrAddTeamInputData
    {
        public List<CreateTeamOrAddDataModel> List { get; set; }
    }
}