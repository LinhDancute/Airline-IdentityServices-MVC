<<<<<<< HEAD
﻿using Airline.ModelsService.Models.Airline;
using Airline.ModelsService.Models.DTOs.Coupon;
using Airline.Services.CouponAPI.Repositories;
=======
﻿using Airline.Services.CouponAPI.Repositories;
using Airline.WebClient.Models.DTOs.Coupon;
using App.Models.Airline;
>>>>>>> 015933b5a74e5f2f345a2bfbb51871285fa0aac9
using AutoMapper;

namespace Airline.Services.CouponAPI.Services.Implements
{
    public class TicketClassService : ITicketClassService
    {
        private readonly ITicketClassRepository _repository;
        private readonly IMapper _mapper;

        public TicketClassService(ITicketClassRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketClassDTO>> GetAllAsync()
        {
            var ticketClasses = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TicketClassDTO>>(ticketClasses);
        }

        public async Task<TicketClassDTO> GetByIdAsync(int id)
        {
            var ticketClass = await _repository.GetByIdAsync(id);
            if (ticketClass == null)
            {
                throw new Exception($"The ticket class with ID {id} does not exist.");
            }
            return _mapper.Map<TicketClassDTO>(ticketClass);
        }

        public async Task CreateAsync(TicketClassCreateDTO ticketClassDTO)
        {
            var ticketClass = _mapper.Map<TicketClass>(ticketClassDTO);
            await _repository.AddAsync(ticketClass);
        }

        public async Task CreateBulkAsync(IEnumerable<TicketClassCreateDTO> ticketClassDTOs)
        {
            var ticketClasses = _mapper.Map<IEnumerable<TicketClass>>(ticketClassDTOs);
            await _repository.AddRangeAsync(ticketClasses);
        }

        public async Task UpdateAsync(int id, TicketClassCreateDTO ticketClassDTO)
        {
            var ticketClass = await _repository.GetByIdAsync(id);
            if (ticketClass == null)
            {
                throw new Exception($"The ticket class with ID {id} does not exist.");
            }

            _mapper.Map(ticketClassDTO, ticketClass);
            await _repository.UpdateAsync(ticketClass);
        }

        public async Task DeleteAsync(int id)
        {
            var ticketClass = await _repository.GetByIdAsync(id);
            if (ticketClass == null)
            {
                throw new Exception($"The ticket class with ID {id} does not exist.");
            }

            await _repository.DeleteAsync(ticketClass);
        }
    }
}
