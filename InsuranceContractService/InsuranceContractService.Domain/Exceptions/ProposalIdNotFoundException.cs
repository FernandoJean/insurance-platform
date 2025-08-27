using InsuranceContractService.Domain.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceContractService.Domain.Exceptions
{
    /// <summary>
    /// Exceção lançada quando o id da proposta informada não existe na base dados.
    /// </summary>
    public sealed class ProposalIdNotFoundException(Guid id) : ValidationException(string.Format(ExceptionMessages.ProposalIdNotFoundException, id))
    {
        [JsonPropertyName("id")]
        public Guid Id { get; }

        public static string SerializeToJson(ProposalIdNotFoundException exception)
        {
            return JsonSerializer.Serialize(exception);
        }

        public static ProposalIdNotFoundException DeserializeFromJson(string json)
        {
            return JsonSerializer.Deserialize<ProposalIdNotFoundException>(json)
                   ?? throw new InvalidOperationException("Falha na desserialização da exceção.");
        }
    }
}