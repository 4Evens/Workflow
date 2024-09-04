using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Infrastructure
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}
