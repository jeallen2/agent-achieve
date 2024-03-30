using AgentAchieve.Core.Domain;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AgentAchieve.Infrastructure.Features.Identity;
public class ApplicationUserDto
{
   
    /// <summary>
    /// Gets or sets the primary key for this user.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the user name for this user.
    /// </summary>
    public virtual string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string? LastName { get; set; }
   
    /// <summary>
    /// Gets the full name of the user.
    /// </summary>
    [Display(Name = "Full Name")]
    public string? FullName
    {
        get
        {
            var parts = new List<string?>();
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                parts.Add(LastName);
            }

            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                parts.Add(FirstName);
            }

            return parts.Count != 0 ? string.Join(", ", parts) : UserName;
        }
    }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>(MemberList.None);
        }
    }
}
