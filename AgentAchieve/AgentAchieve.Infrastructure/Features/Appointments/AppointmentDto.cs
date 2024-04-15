using AgentAchieve.Core.Common;
using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Clients;
using AutoMapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgentAchieve.Infrastructure.Features.Appointments;
[Description("Appointment")]
public class AppointmentDto : IEntityPk
{
    /// <summary>
    /// Gets or sets the ID of the appointment.
    /// </summary>
    [Display(Name = "Id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the start date and time of the appointment.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Gets or sets the end date and time of the appointment.
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Gets or sets the title of the appointment.
    /// </summary>
    [Required]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the appointment.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the appointment is an all-day event.
    /// </summary>
    public bool IsAllDay { get; set; }

    /// <summary>
    /// Gets or sets the recurrence rule of the appointment.
    /// </summary>
    public string? RecurrenceRule { get; set; }

    /// <summary>
    /// Gets or sets the ID of the recurring appointment.
    /// </summary>
    public int? RecurrenceId { get; set; }

    /// <summary>
    /// Gets or sets the recurrence exceptions of the appointment.
    /// </summary>
    public string? RecurrenceExceptions { get; set; }

    /// <summary>
    /// Represents a mapping configuration for the <see cref="Appointment"/> and <see cref="AppointmentDto"/> classes.
    /// </summary>
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
        }
    }
}
