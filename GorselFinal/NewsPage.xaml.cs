using GorselFinal.Model;
using System.Text.Json;

namespace GorselFinal;

public partial class NewsPage : ContentPage
{
    public NewsPage()
    {
        InitializeComponent();
        lstKategori.ItemsSource = haberKategoriList;

        selectedCategory = haberKategoriList[0];
    }

    List<HaberKategori> haberKategoriList = new List<HaberKategori>()
    {
         new HaberKategori(){Baslik = "Manset", Link = "https://www.trthaber.com/manset_articles.rss"},
         new HaberKategori(){Baslik = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
         new HaberKategori(){Baslik = "Spor", Link = "https://www.trthaber.com/spor_articles.rss"},
         new HaberKategori(){Baslik = "Gündem", Link = "https://www.trthaber.com/gundem_articles.rss"},
         new HaberKategori(){Baslik = "Güncel Makleler", Link = "https://www.trthaber.com/guncel_articles.rss"},
    };

    HaberKategori selectedCategory = null;
    private async void LoadHaberler(object sender, EventArgs e)
    {


        Load();
        refreshView.IsRefreshing = false;

    }

    async Task Load()
    {


        string jsondata = await NewsServices.GetNews(selectedCategory.Link);
        if (!string.IsNullOrEmpty(jsondata))
        {
            var haberler = JsonSerializer.Deserialize<HaberRoot>(jsondata);
            lstHaberler.ItemsSource = haberler.items;
        }
    }

    private async void lstKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedCategory = lstKategori.SelectedItem as HaberKategori;
        await Load();
    }

    private void lstHaberler_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedHaber = lstHaberler.SelectedItem as Item;
        if (selectedHaber != null)
        {
            var url = selectedHaber.link;
            string title = selectedHaber.title;
            NewsDetailPage page = new NewsDetailPage(url, title);
            Navigation.PushAsync(page);
        }
        // var url = (lstHaberler.SelectedItem as Item).link;
        //  NewsDetailPage page = new NewsDetailPage(url);
        //  Navigation.PushAsync(page);
    }


}

public class HaberKategori
{
    public string Baslik { get; set; }
    public string Link { get; set; }
}