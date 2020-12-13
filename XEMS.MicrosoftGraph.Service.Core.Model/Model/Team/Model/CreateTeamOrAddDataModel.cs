using System.Collections.Generic;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.Model
{
    public class CreateTeamOrAddDataModel
    {
        public CreateTeamOrAddDataModel()
        {
        }

        public CreateTeamOrAddDataModel(string ownerGUID, List<string> participantsList) : this(ownerGUID)
        {
            ParticipantsList = participantsList;
        }

        public CreateTeamOrAddDataModel(string ownerGUID, string name) : this(ownerGUID)
        {
            Name = name;
        }

        public CreateTeamOrAddDataModel(string ownerGUID)
        {
            OwnerGUID = ownerGUID;
        }

        public string OwnerGUID { get; set; }

        public string Name { get; set; }

        public List<string> ParticipantsList { get; set; } = new List<string>();
    }
}