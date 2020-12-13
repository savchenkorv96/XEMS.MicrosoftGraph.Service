namespace XEMS.MicrosoftGraph.Service.Core.Activities.Contracts
{
    public interface IActivitiesFactory
    {
        T Create<T>();
    }
}