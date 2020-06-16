using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayT.Domain.Events;
using PayT.Infrastructure.Events;

namespace PayT.Web.Dispatchers
{
    public interface IEventDispatcher
    {
        IEnumerable DispatchMany(IEvent @event);
    }
}
