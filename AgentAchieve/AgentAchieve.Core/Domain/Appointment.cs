using AgentAchieve.Core.Common;
using System.ComponentModel.DataAnnotations;


namespace AgentAchieve.Core.Domain;
public class Appointment : BaseAuditableEntity
{
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
}
