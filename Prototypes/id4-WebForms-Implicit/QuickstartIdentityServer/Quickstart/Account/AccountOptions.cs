using System;

namespace IdentityServer4.Quickstart.UI
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin { get; set; } = true;

        public static bool AllowRememberLogin { get; set; } = true;

        public static TimeSpan RememberMeLoginDuration { get; set; } = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt { get; set; } = true;

        public static bool AutomaticRedirectAfterSignOut { get; set; } = false;

        // to enable windows authentication, the host (IIS or IIS Express) also must have
        // windows auth enabled.
        public static bool WindowsAuthenticationEnabled { get; set; } = true;

        public static bool IncludeWindowsGroups { get; set; } = false;

        // specify the Windows authentication scheme and display name
        public static string WindowsAuthenticationSchemeName { get; } = "Windows";

        public static string InvalidCredentialsErrorMessage { get; set; } = "Invalid username or password";
    }
}
