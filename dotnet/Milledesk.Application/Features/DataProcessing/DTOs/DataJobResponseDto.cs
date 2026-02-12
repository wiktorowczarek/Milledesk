using Milledesk.Domain.Enums;

namespace Milledesk.Application.Features.DataProcessing.DTOs
{
    public class DataJobResponseDto
    {
        public string JobId { get; set; } = string.Empty;
        public JobStatus Status { get; set; }
        public string? Data { get; set; }
    }
}
