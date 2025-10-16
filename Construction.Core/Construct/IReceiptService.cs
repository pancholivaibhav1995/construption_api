using Construction.Models.APIModels.request;
using Construction.Models.APIModels.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Construction.Core.Construct
{
    public interface IReceiptService
    {
        Task<IEnumerable<ReceiptResponseModel>> GetAllAsync(Guid organisationId);
        Task<ReceiptResponseModel> AddAsync(ReceiptRequestModel dto);
        Task<ReceiptResponseModel> UpdateAsync(ReceiptRequestModel dto);
    }
}
