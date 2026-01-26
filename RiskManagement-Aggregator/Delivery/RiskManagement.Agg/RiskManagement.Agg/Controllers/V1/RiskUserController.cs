
using _4EPlatform_RiskManagement_Aggregator.Dto.RiskUser;
using _4EPlatform_RiskManagement_Aggregator.Interfaces;

namespace _4EPlatform_RiskManagement_Aggregator.Controllers.V1
{
    public class RiskUserController : BaseController
    {
        private readonly IRiskUserService _riskUserService;

        public RiskUserController(IRiskUserService riskUserService)
        {
            _riskUserService = riskUserService;
        }
        [HttpGet("GetAllRiskUser")]
        public async Task<ResponseDto> GetAllRiskUser([FromQuery] RiskUserDto Dto)
        {
            return await _riskUserService.GetallUserRisk(Dto);
        }

        [HttpGet("GetAllRiskMangers")]
        public async Task<ResponseDto> GetAllRiskMangers([FromQuery] RiskMangerDto Dto)
        {
            return await _riskUserService.GetallMangersRisk(Dto);
        }


        [HttpGet("GetUserRiskbyUserid")]
        public async Task<ResponseDto> GetUserRiskbyUserid([FromQuery] GetRiskUserByidDto Dto)
        {
            return await _riskUserService.GetUserRiskbyUserid(Dto);
        }
        [HttpGet("GetriskUserbyOrgId")]
        public async Task<ResponseDto> GetriskUserbyOrgId([FromQuery] GetriskUserbyOrgIdRequestDto Dto)
        {
            return await _riskUserService.GetriskUserbyOrgId(Dto);
        }
    }
}
