using _4EPlatform_RiskManagement_Aggregator.Interface.ProcessingPlan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4EPlatform_RiskManagement_Aggregator.Controllers.V1.ProcessingPlan
{
  
    public class ProcessingPlanController : BaseController
    {
        private readonly IProcessingPlanService _processingPlan;
        public ProcessingPlanController(IProcessingPlanService processingPlan)
        {
            _processingPlan = processingPlan;
        }




        [HttpGet]
        [Route("GetProcessingPlanByRiskId")]
        public async Task<ResponseDto> GetProcessingPlanByRiskId(Guid Id)
        {
            return await _processingPlan.GetProcessingPlanByRiskId(Id);
        }

    }
}
