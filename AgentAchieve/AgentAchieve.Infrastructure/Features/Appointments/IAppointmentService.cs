
namespace AgentAchieve.Infrastructure.Features.Appointments;

/// <summary>
/// Represents a service for managing Appointments.
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Creates a new appointment.
    /// </summary>
    /// <param name="appointmentDto">The appointment DTO.</param>
    /// <returns>The created appointment DTO.</returns>
    Task<AppointmentDto> CreateAppointmentAsync(AppointmentDto appointmentDto);

    /// <summary>
    /// Deletes an appointment by ID.
    /// </summary>
    /// <param name="id">The ID of the appointment to delete.</param>
    Task DeleteAppointmentAsync(int id);

    /// <summary>
    /// Gets all appointment DTOs.
    /// </summary>
    /// <returns>A collection of appointment DTOs.</returns>
    Task<IEnumerable<AppointmentDto>> GetAllDtoAsync();

    /// <summary>
    /// Gets an appointment DTO by ID.
    /// </summary>
    /// <param name="id">The ID of the appointment to retrieve.</param>
    /// <returns>The appointment DTO, or null if not found.</returns>
    Task<AppointmentDto?> GetDtoByIdAsync(int id);

    /// <summary>
    /// Updates an existing appointment.
    /// </summary>
    /// <param name="appointmentDto">The updated appointment DTO.</param>
    /// <returns>The updated appointment DTO.</returns>
    Task<AppointmentDto> UpdateAppointmentAsync(AppointmentDto appointmentDto);
}
