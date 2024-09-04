using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Infrastructure
{
    public interface IMessageQueueService
    {
        void SendMessage(string message);
    }
}
