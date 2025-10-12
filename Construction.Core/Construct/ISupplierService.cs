using Construction.Models.APIModels.request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierRequestModel>> GetAllAsync(Guid organisationId);
        Task<SupplierRequestModel> AddAsync(SupplierRequestModel dto);
    }
}
