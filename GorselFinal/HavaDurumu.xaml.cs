

using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
namespace GorselFinal;

public partial class HavaDurumu : ContentPage
{
    public ObservableCollection<SehirHavaDurumu> Sehirler { get; set; }

    public HavaDurumu()
    {
        InitializeComponent();
        Sehirler = new ObservableCollection<SehirHavaDurumu>();
        CitiesCollectionView.ItemsSource = Sehirler;
    }

    private async void OnAddCityButtonClicked(object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("Şehir:", "Şehir ismi", "OK", "Cancel");

        if (!string.IsNullOrWhiteSpace(sehir))
        {
            sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);

            sehir = sehir.Replace('Ç', 'C');
            sehir = sehir.Replace('Ğ', 'G');
            sehir = sehir.Replace('İ', 'I');
            sehir = sehir.Replace('Ö', 'O');
            sehir = sehir.Replace('Ü', 'U');
            sehir = sehir.Replace('Ş', 'S');

            Sehirler.Add(new SehirHavaDurumu() { Name = sehir });
        }
    }
}

public class SehirHavaDurumu
{
    public string Name { get; set; }
    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
}

