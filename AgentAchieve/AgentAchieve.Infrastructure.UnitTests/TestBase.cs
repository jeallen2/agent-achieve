using Microsoft.Extensions.Logging;
using Moq.AutoMock;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AgentAchieve.Infrastructure.UnitTests
{
    /// <summary>
    /// Base class for unit tests that provides common functionality.
    /// </summary>
    /// <typeparam name="T">The type of the class being tested.</typeparam>
    public class TestBase<T>
    {
        protected readonly AutoMocker AutoMocker;
        protected readonly ILogger<T> Logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase{T}"/> class.
        /// </summary>
        /// <param name="outputHelper">The output helper for writing test output.</param>
        public TestBase(ITestOutputHelper outputHelper)
        {
            AutoMocker = new AutoMocker();

            // Create an XunitLogger that writes to the xUnit test output
            Logger = outputHelper.ToLogger<T>();

            AutoMocker.Use(Logger);
        }

        /// <summary>
        /// Logs the description of the test method.
        /// </summary>
        /// <param name="callerName">The name of the calling method.</param>
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
}
