using InsuranceContractService.Application.Interfaces;
using InsuranceContractService.Domain.Dtos.Contract;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace InsuranceContractService.Presentation.Controllers
{
    /// <summary>
    /// Controller para o gerênciamento de contratos
    /// </summary>
    [ApiController]
    [Route("v1/proposals")]
    public class ContractsController(ICreateContractUseCase createContractUseCase) : ControllerBase
    {
        private readonly ICreateContractUseCase _createContractUseCase = createContractUseCase;

        /// <summary>
        /// Cria uma contratação baseada em uma proposta aprovada
        /// </summary>
        /// <param name="proposalId">ID da proposta</param>
        /// <returns>Detalhes da contratação</returns>
        [HttpPost]
        [Route("{proposalId}/contracts")]
        [SwaggerOperation(Summary = "Cria um contrato de uma seguro", Tags = ["Contratos"])]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ContractResponseDto>> CreateContract([FromRoute, Required] Guid proposalId)
        {
            var contract = await _createContractUseCase.ExecuteAsync(proposalId);

            return Ok(contract);
        }
    }
}