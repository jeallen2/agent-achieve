using AgentAchieve.Core.Domain;
using AgentAchieve.Infrastructure.Features.Appointments;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace AgentAchieve.Infrastructure.UnitTests.Services;
public class AppointmentServiceTests(ITestOutputHelper outputHelper, DatabaseFixture dbFixture) : TestBase<AppointmentService>(outputHelper, dbFixture)
{
    [Trait("Description", "Verifies that all appointments are returned")]
    [Fact]
    public async Task GetAllDtoAsync_ShouldReturnAllAppointments()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating AppointmentService and adding appointments");
        var appointmentService = CreateAppointmentService();
        var appointment1 = new Appointment { Start = DateTime.Now, End = DateTime.Now.AddHours(1), Title = "Appointment 1" };
        var appointment2 = new Appointment { Start = DateTime.Now.AddHours(2), End = DateTime.Now.AddHours(3), Title = "Appointment 2" };

        await AddAsync(appointment1);
        await AddAsync(appointment2);

        // Act
        Logger.LogInformation("Calling GetAllDtoAsync");
        var result = await appointmentService.GetAllDtoAsync();

        // Assert
        Logger.LogInformation("Asserting that all appointments are returned");
        result.Should().HaveCount(2);
        Logger.LogInformation("All appointments returned successfully");
    }

    [Trait("Description", "Verifies that the correct appointment is returned by ID")]
    [Fact]
    public async Task GetDtoByIdAsync_ShouldReturnCorrectAppointment()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating AppointmentService and adding an appointment");
        var appointmentService = CreateAppointmentService();
        var appointment = new Appointment { Start = DateTime.Now, End = DateTime.Now.AddHours(1), Title = "Appointment 1" };
        await AddAsync(appointment);

        // Act
        Logger.LogInformation("Calling GetDtoByIdAsync");
        var result = await appointmentService.GetDtoByIdAsync(appointment.Id);

        // Assert
        Logger.LogInformation("Asserting that the correct appointment is returned");
        result.Should().NotBeNull();
        result!.Id.Should().Be(appointment.Id);
        result.Title.Should().Be(appointment.Title);
        Logger.LogInformation("Correct appointment returned successfully");
    }

    [Trait("Description", "Verifies that an appointment is added to the database")]
    [Fact]
    public async Task CreateAppointmentAsync_ShouldAddAppointmentToDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating AppointmentService and an appointment DTO");
        var appointmentService = CreateAppointmentService();
        var appointmentDto = new AppointmentDto
        {
            Start = DateTime.Now,
            End = DateTime.Now.AddHours(1),
            Title = "Appointment 1"
        };

        // Act
        Logger.LogInformation("Calling CreateAppointmentAsync");
        var result = await appointmentService.CreateAppointmentAsync(appointmentDto);

        // Assert
        Logger.LogInformation("Asserting that the appointment is added to the database");
        result.Should().NotBeNull();
        var dbAppointment = await FindAsync<Appointment>(result.Id);
        dbAppointment.Should().NotBeNull();
        dbAppointment!.Title.Should().Be(appointmentDto.Title);
        Logger.LogInformation("Appointment added successfully");
    }

    [Trait("Description", "Verifies that an appointment is updated in the database")]
    [Fact]
    public async Task UpdateAppointmentAsync_ShouldUpdateAppointmentInDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating AppointmentService, adding an appointment, and creating an appointment DTO");
        var appointmentService = CreateAppointmentService();
        var appointment = new Appointment { Start = DateTime.Now, End = DateTime.Now.AddHours(1), Title = "Appointment 1" };
        await AddAsync(appointment);
        var appointmentDto = new AppointmentDto { Id = appointment.Id, Start = DateTime.Now, End = DateTime.Now.AddHours(1), Title = "Updated Appointment" };

        // Act
        Logger.LogInformation("Calling UpdateAppointmentAsync");
        var result = await appointmentService.UpdateAppointmentAsync(appointmentDto);

        // Assert
        Logger.LogInformation("Asserting that the appointment is updated in the database");
        result.Should().NotBeNull();
        var dbAppointment = await FindAsync<Appointment>(result.Id);
        dbAppointment.Should().NotBeNull();
        dbAppointment!.Title.Should().Be(appointmentDto.Title);
        Logger.LogInformation("Appointment updated successfully");
    }

    [Trait("Description", "Verifies that an appointment is removed from the database")]
    [Fact]
    public async Task DeleteAppointmentAsync_ShouldRemoveAppointmentFromDatabase()
    {
        LogDescription();

        // Arrange
        Logger.LogInformation("Creating AppointmentService and adding an appointment");
        var appointmentService = CreateAppointmentService();
        var appointment = new Appointment { Start = DateTime.Now, End = DateTime.Now.AddHours(1), Title = "Appointment 1" };
        await AddAsync(appointment);

        // Act
        Logger.LogInformation("Calling DeleteAppointmentAsync");
        await appointmentService.DeleteAppointmentAsync(appointment.Id);

        // Assert
        Logger.LogInformation("Asserting that the appointment is removed from the database");
        var dbAppointment = await FindAsync<Appointment>(appointment.Id);
        dbAppointment.Should().BeNull();
        Logger.LogInformation("Appointment removed successfully");
    }
}
