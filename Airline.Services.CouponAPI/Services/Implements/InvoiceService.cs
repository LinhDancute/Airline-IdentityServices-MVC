using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.ModelsService.Models.Statistical;
using Airline.Services.CouponAPI.Repositories;
using AutoMapper;

namespace Airline.Services.CouponAPI.Services.Implements
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(InvoiceDTO invoiceDTO)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDTO);
            await _invoiceRepository.AddAsync(invoice);
        }

        public async Task<IEnumerable<InvoiceDTO>> GetAllAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceDTO>>(invoices);
        }

        public async Task<InvoiceDTO> GetByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice != null)
            {
                return _mapper.Map<InvoiceDTO>(invoice);
            }
            else
            {
                throw new KeyNotFoundException("invoice not found");
            }
        }
    }
}
