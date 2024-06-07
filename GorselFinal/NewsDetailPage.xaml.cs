namespace GorselFinal;

public partial class NewsDetailPage : ContentPage
{

    private string newsLink;
    private string newsTitle;

    public NewsDetailPage(string link, string title)
    {
        InitializeComponent();

        newsLink = link;
        newsTitle = title;


        webView.Source = newsLink;
    }

    private async void ShareClicked(object sender, EventArgs e)
    {
        await ShareUri(newsLink, newsTitle);
    }

    public async Task ShareUri(string uri, string title)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = uri,
            Title = title
        });
    }



}