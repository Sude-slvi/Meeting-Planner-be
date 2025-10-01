using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingPlanning.Domain.Exceptions
{
    public class TeamExceptions : Exception
    {
        public TeamExceptions(string message) : base(message) { }

    }
}
