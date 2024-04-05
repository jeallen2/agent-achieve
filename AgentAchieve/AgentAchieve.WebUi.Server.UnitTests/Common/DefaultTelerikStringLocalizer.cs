using Telerik.Blazor.Services;

namespace AgentAchieve.WebUi.Server.UnitTests.Common;
public class DefaultTelerikStringLocalizer : ITelerikStringLocalizer
{
    public string this[string name] => name;

    public string this[string name, params object[] arguments] => string.Format(name, arguments);
}
