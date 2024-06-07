using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using GorselFinal.Model;

namespace GorselFinal
{
    public partial class MainPage : ContentPage
    {
        private readonly FireBaseServices firebaseService;

        public MainPage()
        {
            InitializeComponent();
            firebaseService = new FireBaseServices();
        }

        private async void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ayarlar());
        }

        private void KayitLoginEkraniGoster(object sender, EventArgs e)
        {
            if (kayitEkran.IsVisible)
            {
                kayitEkran.IsVisible = false;
                loginEkran.IsVisible = true;
            }
            else
            {
                loginEkran.IsVisible = false;
                kayitEkran.IsVisible = true;
            }
        }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            var username = txtName.Text;
            var email = txtREmail.Text;
            var password = txtRPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Kayıt başarısız.", "Lütfen tüm boşlukları doldurun!", "Tamam");
            }
            else
            {
                var user = new GorselFinal.Model.User { Username = username, Email = email, Password = password };
                var result = await firebaseService.AddUser(user);
                if (result != null)
                {
                    await DisplayAlert("Başarılı", "Kayıt başarılı.", "Tamam");
                }
                else
                {
                    await DisplayAlert("Hata", "Kayıt işlemi sırasında bir hata oluştu.", "Tamam");
                }
            }
        }


        private async void LoginInClicked(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            var user = await firebaseService.GetUserByEmailAndPassword(email, password);

            if (user != null)
            {

                await DisplayAlert("Oturum açıldı", $"Hoşgeldiniz {user.Username}!", "Tamam");
                //await Navigation.PushAsync(new AnaSayfa() { Title = "Ana Sayfa" });
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Hata", "Kullanıcı adı şifre hatalı", "Tamam");
            }
        }
    }
}