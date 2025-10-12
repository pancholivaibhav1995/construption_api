using Construction.Entity.Models;
using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
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
        Task<SupplierResponseModel> AddAsync(SupplierRequestModel dto);

        Task<SupplierResponseModel> UpdateAsync(SupplierRequestModel dto);
    }
}
