using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.ModelsService.Models.DTOs.Schedule;
using Airline.ModelsService.Models.Statistical;
using Airline.Services.CouponAPI.Repositories;
using AutoMapper;

namespace Airline.Services.CouponAPI.Services.Implements
{
    public class InvoiceDetailService : IInvoiceDetailService
    {
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IMapper _mapper;

        public InvoiceDetailService(IInvoiceDetailRepository invoiceDetailRepository, IMapper mapper)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(InvoiceDetailDTO invoiceDetailDTO)
        {
            var invoiceDetail = _mapper.Map<InvoiceDetail>(invoiceDetailDTO);
            await _invoiceDetailRepository.AddAsync(invoiceDetail);
        }

        public async Task<IEnumerable<InvoiceDetailDTO>> GetAllAsync()
        {
            var invoices = await _invoiceDetailRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceDetailDTO>>(invoices);
        }

        public async Task<InvoiceDetailDTO> GetByIdAsync(int id)
        {
            var invoice = await _invoiceDetailRepository.GetByIdAsync(id);
            if (invoice != null)
            {
                return _mapper.Map<InvoiceDetailDTO>(invoice);
            }
            else
            {
                throw new KeyNotFoundException("invoice detail not found");
            }
        }
    }
}
