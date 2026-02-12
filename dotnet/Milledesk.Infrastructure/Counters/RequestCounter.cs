using Milledesk.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milledesk.Infrastructure.Counters
{
    public class RequestCounter : IRequestCounter
    {
        private int _counter = 0;

        public int Increment()
        {
            return Interlocked.Increment(ref _counter);
        }
    }
}
