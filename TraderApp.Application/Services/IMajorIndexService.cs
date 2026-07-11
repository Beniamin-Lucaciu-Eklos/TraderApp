using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderApp.Domain.Models;

namespace TraderApp.Application.Services
{
    public interface IMajorIndexService
    {
        Task<MajorIndex> GetMajorIndexAsync(MajorIndexType indexType);
    }
}
