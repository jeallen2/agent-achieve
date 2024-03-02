using AgentAchieve.Core.Common;
using System.ComponentModel.DataAnnotations;

namespace AgentAchieve.Core.Domain;

/// <summary>
/// Represents a client entity.
/// </summary>
public class Client : BaseAuditableEntity<int>
{
    public Client(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Gets or sets the first name of the client.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the client.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the client.
    /// </summary>
    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the email address of the client.
    /// </summary>
    [MaxLength(256)]
    public string? Email { get; set; }

    [MaxLength(100)]
    public string? Street { get; set; }

    [MaxLength(50)]
    public string? City { get; set; }

    [MaxLength(2)]
    public string? State { get; set; }

    [MaxLength(10)]
    public string? ZipCode { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }

    public DateTime? Birthdate { get; set; }

    [MaxLength(50)]
    public string? Occupation { get; set; }

    [MaxLength(100)]
    public string? Employer { get; set; }

    [MaxLength(100)]
    public string? ReferredBy { get; set; }

    public string? Notes { get; set; }
}
