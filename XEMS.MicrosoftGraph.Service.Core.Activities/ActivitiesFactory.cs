using System;
using System.Collections.Generic;
using Microsoft.Graph;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Activities.Group;
using XEMS.MicrosoftGraph.Service.Core.Activities.Team;
using XEMS.MicrosoftGraph.Service.Core.Activities.User;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.OutputData;

namespace XEMS.MicrosoftGraph.Service.Core.Activities
{
    public class ActivitiesFactory : IActivitiesFactory
    {
        private readonly Dictionary<Type, object> collection = new Dictionary<Type, object>();
        private readonly Logger logger;

        public ActivitiesFactory(GraphServiceClient graph, Logger logger)
        {
            this.logger = logger;

            #region UserActivities

            collection.Add(typeof(IRequestActivity<GetUserByIdInputData, GetUserByIdOutputData>),
                new GetUserByIdActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<GetUserByEmailInputData, GetUserByEmailOutputData>),
                new GetUserByEmailActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<UpdateContactInfoByIdInputData, UpdateContactInfoByIdOutputData>),
                new UpdateContactInfoByIdActivity(graph, logger));

            #endregion

            #region GroupActivities

            collection.Add(typeof(IRequestActivity<GetGroupByIdInputData, GetGroupByIdOutputData>),
                new GetGroupByIdActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<GetGroupByNameInputData, GetGroupByNameOutputData>),
                new GetGroupByNameActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<GetListGroupInputData, GetListGroupOutputData>),
                new GetListGroupActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<GetListGroupMemberByIdInputData, GetListGroupMemberByIdOutputData>),
                new GetListGroupMemberByIdActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<CreateGroupInputData, CreateGroupOutputData>),
                new CreateGroupActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<JoinUserToGroupByIdInputData, JoinUserToGroupByIdOutputData>),
                new JoinUserToGroupByIdActivity(graph, logger));

            #endregion

            #region TeamActivities

            collection.Add(typeof(IRequestActivity<CreateOrAddTeamInputData, CreateOrAddTeamOutputData>),
                new CreateOrAddTeamActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<CrateTeamFromGroupByIdInputData, CrateTeamFromGroupByIdOutputData>),
                new CrateTeamFromGroupByIdActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<GetListTeamMemberByIdInputData, GetListTeamMemberByIdOutputData>),
                new GetListTeamMemberByIdActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<GetTeamByIdInputData, GetTeamByIdOutputData>),
                new GetTeamByIdActivity(graph, logger));
            collection.Add(typeof(IRequestActivity<JoinUsersToTeamByIdInputData, JoinUsersToTeamByIdOutputData>),
                new JoinUsersToTeamByIdActivity(graph, logger));

            #endregion
        }

        public T Create<T>()
        {
            var type = typeof(T);

            if (!collection.ContainsKey(type)) throw new MissingMemberException(type + "is missing in the collection");
            return (T) collection[type];
        }
    }
}