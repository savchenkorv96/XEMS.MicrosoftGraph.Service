using System.Threading.Tasks;

namespace XEMS.MicrosoftGraph.Service.Core.UseCases.Contracts
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }
}