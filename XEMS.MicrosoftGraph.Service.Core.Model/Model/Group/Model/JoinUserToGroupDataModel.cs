using System.Collections.Generic;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.Model
{
    public class JoinUserToGroupDataModel
    {
        public string GroupId { get; set; }
        public List<string> UsersId { get; set; }
    }
}