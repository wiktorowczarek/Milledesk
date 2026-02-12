using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milledesk.Application.Features.DataProcessing.Commands
{
    public record StartDataJobCommand(string ClientId) : IRequest<string>;
}
