using System.Threading.Tasks;
using Microsoft.Graph;

namespace XEMS.MicrosoftGraph.Service.Core.Activities.Contracts
{
    public interface IRequestActivity<RequestEvent, ResponseEvent>
    {
        GraphServiceClient Graph { get; }
        Task<ResponseEvent> Execute(RequestEvent request);
    }
}