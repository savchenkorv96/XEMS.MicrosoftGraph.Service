namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts
{
    public interface IUseCaseFactory
    {
        T Create<T>();
    }
}