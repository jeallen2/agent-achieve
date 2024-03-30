using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AgentAchieve.UnitTests.Common;

/// <summary>
/// Base class for logging in unit tests.
/// </summary>
/// <typeparam name="TClass">The type of the class where the logger is being used.</typeparam>
public abstract class LoggingTestBase<TClass>
{
    /// <summary>
    /// Logger instance.
    /// </summary>
    protected ILogger<TClass> Logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingTestBase{TClass}"/> class.
    /// </summary>
    /// <param name="outputHelper">The output helper to write logs to.</param>
    protected LoggingTestBase(ITestOutputHelper outputHelper)
    {
        Logger = outputHelper.ToLogger<TClass>();
    }

    /// <summary>
    /// Logs the description of the calling method.
    /// </summary>
    /// <param name="callerName">The name of the calling method. This is automatically populated by the runtime.</param>
    protected void LogDescription([CallerMemberName] string callerName = "")
    {
        var callingMethod = GetType().GetMethod(callerName);
        var traits = TraitHelper.GetTraits(callingMethod);
        var descriptionTrait = traits.FirstOrDefault(t => t.Key == "Description"); // Find the 'Description' trait

        if (descriptionTrait.Value != null) // Check if the trait's Value is null
        {
            Logger.LogInformation("Test Description: {Description}", descriptionTrait.Value);
        }
    }
}
