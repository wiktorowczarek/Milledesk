using MediatR;
using Milledesk.Application.Features.DataProcessing.DTOs;

namespace Milledesk.Application.Features.DataProcessing.Queries
{
    public record GetDataJobQuery(string JobId) : IRequest<DataJobResponseDto?>;
}
