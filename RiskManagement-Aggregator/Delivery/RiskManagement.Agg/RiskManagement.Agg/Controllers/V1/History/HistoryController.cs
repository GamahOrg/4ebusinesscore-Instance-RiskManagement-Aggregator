
namespace _4EPlatform_RiskManagement_Aggregator.Controllers.V1.History
{
    public class HistoryController : BaseController
    {
        private readonly IHistoryService _history;
        public HistoryController(IHistoryService history)
        {
            _history = history;
        }
        [HttpGet]
        [Route("GetAllHistory")]
        public async Task<ResponseDto> GetAllHistory([FromQuery] GetHistoryPram pram)
        {
            return await _history.GetHistory(pram);
        }
    }

}
