using System.Collections.Generic;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.Model;

namespace XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData
{
    public class GetListGroupMemberByIdOutputData
    {
        public List<UserModel> Users { get; set; }
    }
}