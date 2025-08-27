using InsuranceQuoteService.Application.Interfaces;
using InsuranceQuoteService.Domain.Dtos.Proposal;
using InsuranceQuoteService.Domain.Enums;
using InsuranceQuoteService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace InsuranceQuoteService.Presentation.Controllers
{
    /// <summary>
    /// Controller para o gerênciamento de propostas
    /// </summary>
    [ApiController]
    [Route("v1/proposals")]
    public class ProposalsController(
        ICreateProposalUseCase createProposalUseCase,
        IListProposalsUseCase listProposalsUseCase,
        IGetProposalByIdUseCase getProposalByIdUseCase,
        IUpdateProposalsStatusUseCase updateProposalsStatusUseCase
        ) : ControllerBase
    {
        private readonly ICreateProposalUseCase _createProposalUseCase = createProposalUseCase;
        private readonly IListProposalsUseCase _listProposalsUseCase = listProposalsUseCase;
        private readonly IGetProposalByIdUseCase _getProposalByIdUseCase = getProposalByIdUseCase;
        private readonly IUpdateProposalsStatusUseCase _updateProposalsStatusUseCase = updateProposalsStatusUseCase;

        /// <summary> Inserir proposta </summary>
        /// <param name="createProposalRequestDto"> DTO de criação de proposta </param>
        /// <param name="ctx"> Token de cancelamento </param>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma proposta de seguro", Tags = ["Propostas"])]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateProposal([FromBody, Required] CreateProposalRequestDto createProposalRequestDto, CancellationToken ctx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proposal = await _createProposalUseCase.ExecuteAsync(createProposalRequestDto, ctx);

            return CreatedAtAction(nameof(GetProposalById), new { id = proposal.Id }, proposal);
        }

        /// <summary> Obter todas as propostas </summary>
        /// <param name="pageIndex"> Índice da página </param>
        /// <param name="pageSize"> Quantidade de itens </param>
        /// <param name="ctx"> Token de cancelamento </param>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Propostas" })]
        [ProducesResponseType(typeof(PageModel<ProposalResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListProposals([FromQuery] int pageIndex, [FromQuery] int pageSize, CancellationToken ctx)
        {
            Pagination pagination = new(pageIndex, pageSize);

            var proposals = await _listProposalsUseCase.ExecuteAsync(pagination, ctx);

            return Ok(proposals);
        }

        /// <summary> Consultar proposta </summary>
        /// <param name="id"> Id da proposta </param>
        /// <param name="ctx"> Token de cancelamento </param>
        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Consulta proposta por id", Tags = ["Propostas"])]
        [ProducesResponseType(typeof(ProposalResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProposalById([FromRoute, Required] Guid id, CancellationToken ctx)
        {
            var proposal = await _getProposalByIdUseCase.ExecuteAsync(id, ctx);

            return Ok(proposal);
        }

        /// <summary> Alterar status da proposta </summary>
        /// <param name="id"> Id da proposta </param>
        /// <param name="newStatus"> Novo status da proposta </param>
        /// <param name="ctx"> Token de cancelamento </param>
        [HttpPatch]
        [Route("{id}/status")]
        [SwaggerOperation(Summary = "Atualiza status da proposta por id", Tags = ["Propostas"])]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateProposalStatus([FromRoute, Required] Guid id, [FromBody, Required] ProposalStatus newStatus, CancellationToken ctx)
        {
            await _updateProposalsStatusUseCase.ExecuteAsync(id, newStatus, ctx);

            return NoContent();
        }
    }
}