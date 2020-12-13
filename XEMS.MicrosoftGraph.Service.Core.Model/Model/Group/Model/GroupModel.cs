using System.Collections.Generic;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.Model
{
    public class GroupModel
    {
        public GroupModel()
        {
        }

        public GroupModel(Microsoft.Graph.Group group)
        {
            GUID = group.Id;
            Name = group.DisplayName;
        }

        public string GUID { get; set; }
        public string Name { get; set; }
        public List<GroupOwnerModel> GroupOwnerModel { get; set; }
    }
}