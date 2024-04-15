using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgentAchieve.Infrastructure.Features.Appointments
{
    /// <summary>
    /// Represents a service for managing appointments.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AppointmentService"/> class.
    /// </remarks>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="logger">The logger.</param>
    public class AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AppointmentService> logger) : ServiceBase<Appointment>(unitOfWork, mapper, logger)/*, IAppointmentService*/
, IAppointmentService
    {

        /// <summary>
        /// Gets all Appointment as DTOs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of Appointment DTOs.</returns>
        public async Task<IEnumerable<AppointmentDto>> GetAllDtoAsync()
        {
            return await GetAllDto<AppointmentDto>().ToListAsync();
        }

        /// <summary>
        /// Gets a Appointment DTO by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the Appointment.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Appointment DTO, or null if not found.</returns>
        public async Task<AppointmentDto?> GetDtoByIdAsync(int id)
        {
            return await GetDtoByIdAsync<AppointmentDto>(id);
        }

        /// <summary>
        /// Creates a new Appointment asynchronously.
        /// </summary>
        /// <param name="AppointmentDto">The Appointment DTO.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created Appointment DTO.</returns>
        public async Task<AppointmentDto> CreateAppointmentAsync(AppointmentDto appointmentDto)
        {
            return await InsertDtoAsync(appointmentDto);
        }

        /// <summary>
        /// Updates an existing Appointment asynchronously.
        /// </summary>
        /// <param name="appointmentDto">The Appointment DTO.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated Appointment DTO.</returns>
        public async Task<AppointmentDto> UpdateAppointmentAsync(AppointmentDto appointmentDto)
        {
            return await UpdateDtoAsync(appointmentDto.Id, appointmentDto);
        }

        /// <summary>
        /// Deletes a Appointment by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the Appointment to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteAppointmentAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}
