using System;
using System.Collections.Generic;
using Serilog.Core;
using XEMS.MicrosoftGraph.Service.Core.Activities.Contracts;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Group.OutputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.Team.OutputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.InputData;
using XEMS.MicrosoftGraph.Service.Core.Model.Model.User.OutputData;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Group;
using XEMS.MicrosoftGraph.Service.Core.UseCases.Team;
using XEMS.MicrosoftGraph.Service.Core.UseCases.User;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases
{
    public class UseCaseFactory : IUseCaseFactory
    {
        private readonly Dictionary<Type, object> collection = new Dictionary<Type, object>();
        private readonly Logger logger;

        public UseCaseFactory(IActivitiesFactory activitiesFactory, Logger logger)
        {
            this.logger = logger;

            #region User

            collection.Add(typeof(IUseCase<GetUserByIdInputData, GetUserByIdOutputData>),
                new GetUserByIdUseCase(
                    activitiesFactory.Create<IRequestActivity<GetUserByIdInputData, GetUserByIdOutputData>>(), logger
                ));

            collection.Add(typeof(IUseCase<GetUserByEmailInputData, GetUserByEmailOutputData>),
                new GetUserByEmailUserCase(
                    activitiesFactory.Create<IRequestActivity<GetUserByEmailInputData, GetUserByEmailOutputData>>(),
                    logger
                ));

            collection.Add(typeof(IUseCase<UpdateContactInfoByIdInputData, UpdateContactInfoByIdOutputData>),
                new UpdateContactInfoByIdUseCase(
                    activitiesFactory
                        .Create<IRequestActivity<UpdateContactInfoByIdInputData, UpdateContactInfoByIdOutputData>>(),
                    logger
                ));

            #endregion

            #region Group

            collection.Add(typeof(IUseCase<CreateGroupInputData, CreateGroupOutputData>),
                new CreateGroupUseCase(
                    activitiesFactory.Create<IRequestActivity<CreateGroupInputData, CreateGroupOutputData>>(), logger
                ));

            collection.Add(typeof(IUseCase<GetGroupByIdInputData, GetGroupByIdOutputData>),
                new GetGroupByIdUseCase(
                    activitiesFactory.Create<IRequestActivity<GetGroupByIdInputData, GetGroupByIdOutputData>>(), logger
                ));

            collection.Add(typeof(IUseCase<GetGroupByNameInputData, GetGroupByNameOutputData>),
                new GetGroupByNameUseCase(
                    activitiesFactory.Create<IRequestActivity<GetGroupByNameInputData, GetGroupByNameOutputData>>(),
                    logger
                ));

            collection.Add(typeof(IUseCase<GetListGroupMemberByIdInputData, GetListGroupMemberByIdOutputData>),
                new GetListGroupMemberByIdUseCase(
                    activitiesFactory
                        .Create<IRequestActivity<GetListGroupMemberByIdInputData, GetListGroupMemberByIdOutputData>>(),
                    logger
                ));

            collection.Add(typeof(IUseCase<GetListGroupInputData, GetListGroupOutputData>),
                new GetListGroupUseCase(
                    activitiesFactory.Create<IRequestActivity<GetListGroupInputData, GetListGroupOutputData>>(), logger
                ));

            collection.Add(typeof(IUseCase<JoinUserToGroupByIdInputData, JoinUserToGroupByIdOutputData>),
                new JoinUserToGroupByIdUseCase(
                    activitiesFactory
                        .Create<IRequestActivity<JoinUserToGroupByIdInputData, JoinUserToGroupByIdOutputData>>(), logger
                ));

            #endregion

            #region Team

            collection.Add(typeof(IUseCase<CrateTeamFromGroupByIdInputData, CrateTeamFromGroupByIdOutputData>),
                new CrateTeamFromGroupByIdUseCase(
                    activitiesFactory
                        .Create<IRequestActivity<CrateTeamFromGroupByIdInputData, CrateTeamFromGroupByIdOutputData>>(),
                    logger
                ));

            collection.Add(typeof(IUseCase<CreateOrAddTeamInputData, CreateOrAddTeamOutputData>),
                new CreateOrAddTeamUseCase(
                    activitiesFactory.Create<IRequestActivity<CreateOrAddTeamInputData, CreateOrAddTeamOutputData>>()
                ));

            collection.Add(typeof(IUseCase<GetListTeamMemberByIdInputData, GetListTeamMemberByIdOutputData>),
                new GetListTeamMemberByIdUseCase(
                    activitiesFactory
                        .Create<IRequestActivity<GetListTeamMemberByIdInputData, GetListTeamMemberByIdOutputData>>(),
                    logger
                ));

            collection.Add(typeof(IUseCase<GetTeamByIdInputData, GetTeamByIdOutputData>),
                new GetTeamByIdUseCase(
                    activitiesFactory.Create<IRequestActivity<GetTeamByIdInputData, GetTeamByIdOutputData>>(), logger
                ));

            collection.Add(typeof(IUseCase<JoinUsersToTeamByIdInputData, JoinUsersToTeamByIdOutputData>),
                new JoinUsersToTeamByIdUseCase(
                    activitiesFactory
                        .Create<IRequestActivity<JoinUsersToTeamByIdInputData, JoinUsersToTeamByIdOutputData>>(), logger
                ));

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