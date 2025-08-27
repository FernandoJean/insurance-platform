using InsuranceContractService.Application.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsuranceContractService.Application.Exceptions
{
    /// <summary>
    /// Exception lançada quando uma proposta não está no status "Approved" e, portanto, não pode ser contratada.
    /// </summary>
    public sealed class ProposalNotApprovedException(Guid id) : ValidationException(string.Format(ExceptionMessages.ProposalNotApprovedException, id))
    {
        [JsonPropertyName("id")]
        public Guid Id { get; }

        public static string SerializeToJson(ProposalNotApprovedException exception)
        {
            return JsonSerializer.Serialize(exception);
        }

        public static ProposalNotApprovedException DeserializeFromJson(string json)
        {
            return JsonSerializer.Deserialize<ProposalNotApprovedException>(json)
                   ?? throw new InvalidOperationException("Falha na desserialização da exceção.");
        }
    }
}