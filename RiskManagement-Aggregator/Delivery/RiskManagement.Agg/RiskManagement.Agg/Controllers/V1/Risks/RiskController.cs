using _4EPlatform_RiskManagement_Aggregator.Helpers.Enums;

namespace _4EPlatform_RiskManagement_Aggregator.Controllers.V1.Risks
{
    
    public class RiskController : BaseController
    {
        private readonly IRiskService _risk;
        public RiskController(IRiskService risk)
        {
            _risk = risk;
        }
        [HttpGet]
        [Route("GetAllRisks")]
        public async Task<ResponseDto> GetAllRisks([FromQuery] GetAllRisksPram pram )
        {
            return await _risk.GetAllRisks(pram);
        }

        [HttpGet]
        [Route("GetAllRisksCompleted")]
        public async Task<ResponseDto> GetAllRisksCompleted([FromQuery] GetAllRisksPram pram)
        {
            return await _risk.GetAllRisksCompleted(pram);
        }


        [HttpGet]
        [Route("GetAllAssignedRisks")]
        public async Task<ResponseDto> GetAllAssignedRisks([FromQuery] GetAllRisksPram pram)
        {
            return await _risk.GetAllAssignedRisks(pram);
        }
        [HttpGet]
        [Route("GetRiskById")]
        public async Task<ResponseDto> GetRiskById(Guid Id)
        {
            return await _risk.GetRiskById(Id);
        }

        [HttpGet]
        [Route("GetRiskTempById")]
        public async Task<ResponseDto> GetRiskByTempId(Guid Id)
        {
            return await _risk.GetRiskTempById(Id);
        }

        [HttpGet]
        [Route("GetDataByInstanceId")]
        public async Task<ResponseDto> GetDataByInstanceId(ServiceType serviceType, Guid requestInstanceId)
        {
            return await _risk.GetDataByInstanceId(serviceType, requestInstanceId);
        }

    }
}
