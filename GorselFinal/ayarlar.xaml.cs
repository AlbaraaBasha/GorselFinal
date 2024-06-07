using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace GorselFinal
{
    public partial class ayarlar : ContentPage
    {
        public ayarlar()
        {
            InitializeComponent();

            // Load the saved theme preference
            var savedTheme = Preferences.Get("AppTheme", "Default");
            themeSwitch.IsToggled = savedTheme == "Dark";
        }

        private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
        {
            var selectedTheme = e.Value ? "Dark" : "Light";
            ApplyTheme(selectedTheme);
        }

        private void ApplyTheme(string theme)
        {
            Preferences.Set("AppTheme", theme);

            switch (theme)
            {
                case "Light":
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;
                case "Dark":
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;
                default:
                    Application.Current.UserAppTheme = AppTheme.Unspecified;
                    break;
            }
        }
    }
}
