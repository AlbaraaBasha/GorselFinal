using Newtonsoft.Json;

using static GorselFinal.KurItem;

namespace GorselFinal;


public partial class Doviz : ContentPage
{

    public Doviz()
    {
        InitializeComponent();
    }

    private static Doviz instance;
    public static Doviz page
    {
        get
        {
            if (instance == null)
                instance = new Doviz();
            return instance;
        }
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Load();
    }

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        await Load();
    }

    AltinDoviz kurlar;
    async Task Load()
    {

        string jsondata = await GetAltinDovizGuncelKurlar();
        kurlar = JsonConvert.DeserializeObject<AltinDoviz>(jsondata);

        //  kurlar = JsonSerializer.Deserialize<AltinDoviz>(jsondata);

        dovizliste.ItemsSource = new List<KurItem>()
        {



            new KurItem()
            {
                Doviz = "Dolar",
                Alis = kurlar.USD.Alis,
                Satis = kurlar.USD.Satis,
                Fark = kurlar.USD.Degisim,
                Yon = GetImage(kurlar.USD.Degisim),

            },

             new KurItem()
            {
                Doviz = "Euro",
                Alis = kurlar.EUR.Alis,
                Satis = kurlar.EUR.Satis,
                Fark = kurlar.EUR.Degisim,
                Yon = GetImage(kurlar.EUR.Degisim),

            },

              new KurItem()
            {
                Doviz = "Sterlin",
                Alis = kurlar.GBP.Alis,
                Satis = kurlar.GBP.Satis,
                Fark = kurlar.GBP.Degisim,
                Yon = GetImage(kurlar.GBP.Degisim),

            },

                  new KurItem()
            {
                Doviz = "Suudi Riyali",
                Alis = kurlar.SAR.Alis,
                Satis = kurlar.SAR.Satis,
                Fark = kurlar.SAR.Degisim,
                Yon = GetImage(kurlar.SAR.Degisim),

            },

                        new KurItem()
            {
                Doviz = "Kuveyt Dinarı",
                Alis = kurlar.KWD.Alis,
                Satis = kurlar.KWD.Satis,
                Fark = kurlar.KWD.Degisim,
                Yon = GetImage(kurlar.KWD.Degisim),

            },

              new KurItem()
            {
                Doviz = "Mısır Lirası",
                Alis = kurlar.EGP.Alis,
                Satis = kurlar.EGP.Satis,
                Fark = kurlar.EGP.Degisim,
                Yon = GetImage(kurlar.EGP.Degisim),

            },

                new KurItem()
            {
                Doviz = "Frank",
                Alis = kurlar.CHF.Alis,
                Satis = kurlar.CHF.Satis,
                Fark = kurlar.CHF.Degisim,
                Yon = GetImage(kurlar.CHF.Degisim),

            },


               new KurItem()
            {
                Doviz = "--Altın--",
                Alis = "",
                Satis = "",
                Fark = "",
                Yon = "",

            },



               new KurItem()
            {
                Doviz = "Gram Altın",
                Alis = kurlar.Gram_Altin.Alis,
                Satis = kurlar.Gram_Altin.Satis,
                Fark = kurlar.Gram_Altin.Degisim,
                Yon = GetImage(kurlar.Gram_Altin.Degisim),

            },

                  new KurItem()
            {
                Doviz = "Çeyrek Altın",
                Alis = kurlar.Ceyrek_Altin.Alis,
                Satis = kurlar.Ceyrek_Altin.Satis,
                Fark = kurlar.Ceyrek_Altin.Degisim,
                Yon = GetImage(kurlar.Ceyrek_Altin.Degisim),

            },

                        new KurItem()
            {
                Doviz = "Cumhuriyet Altını",
                Alis = kurlar.Cumhuriyet_Altini.Alis,
                Satis = kurlar.Cumhuriyet_Altini.Satis,
                Fark = kurlar.Cumhuriyet_Altini.Degisim,
                Yon = GetImage(kurlar.Cumhuriyet_Altini.Degisim),

            },

               new KurItem()
            {
                Doviz = "Gümüş",
                Alis = kurlar.gumus.Alis,
                Satis = kurlar.gumus.Satis,
                Fark = kurlar.gumus.Degisim,
                Yon = GetImage(kurlar.gumus.Degisim),

            },




        };

    }

    private string GetImage(string str)
    {


        if (str.Contains('-'))
            return "down.png";
        if (str.Equals("%0,00"))
            return "minus.png";

        return "up.png";


    }

    private async Task<string> GetAltinDovizGuncelKurlar()
    {

        string url = "https://finans.truncgil.com/today.json";
        using HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();

        //  HttpClient client = new HttpClient();
        //  string url = "https://finans.truncgil.com/today.json";
        //  using HttpResponseMessage response = await client.GetAsync(url);
        //  response.EnsureSuccessStatusCode();
        //  string jsondata = await response.Content.ReadAsStringAsync();
        //  return jsondata;


    }
}
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class AltinDoviz
{
    public string Update_Date { get; set; }
    public Currency USD { get; set; }
    public Currency EUR { get; set; }
    public Currency GBP { get; set; }
    public Currency CHF { get; set; }
    public Currency CAD { get; set; }
    public Currency RUB { get; set; }
    public Currency AED { get; set; }
    public Currency AUD { get; set; }
    public Currency DKK { get; set; }
    public Currency SEK { get; set; }
    public Currency NOK { get; set; }
    public Currency JPY { get; set; }
    public Currency KWD { get; set; }
    public Currency ZAR { get; set; }
    public Currency BHD { get; set; }
    public Currency LYD { get; set; }
    public Currency SAR { get; set; }
    public Currency IQD { get; set; }
    public Currency ILS { get; set; }
    public Currency IRR { get; set; }
    public Currency INR { get; set; }
    public Currency MXN { get; set; }
    public Currency HUF { get; set; }
    public Currency NZD { get; set; }
    public Currency BRL { get; set; }
    public Currency IDR { get; set; }
    public Currency CZK { get; set; }
    public Currency PLN { get; set; }
    public Currency RON { get; set; }
    public Currency CNY { get; set; }
    public Currency ARS { get; set; }
    public Currency ALL { get; set; }
    public Currency AZN { get; set; }
    public Currency BAM { get; set; }
    public Currency CLP { get; set; }
    public Currency COP { get; set; }
    public Currency CRC { get; set; }
    public Currency DZD { get; set; }
    public Currency EGP { get; set; }
    public Currency HKD { get; set; }
    public Currency ISK { get; set; }
    public Currency HRK { get; set; }
    public Currency JOD { get; set; }
    public Currency KRW { get; set; }
    public Currency KZT { get; set; }
    public Currency LBP { get; set; }
    public Currency LKR { get; set; }
    public Currency MAD { get; set; }
    public Currency MDL { get; set; }
    public Currency MKD { get; set; }
    public Currency MYR { get; set; }
    public Currency OMR { get; set; }
    public Currency PEN { get; set; }
    public Currency PHP { get; set; }
    public Currency PKR { get; set; }
    public Currency QAR { get; set; }
    public Currency RSD { get; set; }
    public Currency SGD { get; set; }
    public Currency SYP { get; set; }
    public Currency THB { get; set; }
    public Currency TWD { get; set; }
    public Currency UAH { get; set; }
    public Currency UYU { get; set; }
    public Currency GEL { get; set; }
    public Currency TND { get; set; }
    public Currency BGN { get; set; }

    [JsonProperty("gram-altin")]
    public GoldCurrency Gram_Altin { get; set; }
    [JsonProperty("gram-has-altin")]
    public GoldCurrency Gram_Has_Altin { get; set; }
    [JsonProperty("ceyrek-altin")]
    public GoldCurrency Ceyrek_Altin { get; set; }
    [JsonProperty("yarim-altin")]
    public GoldCurrency Yarım_Altin { get; set; }
    [JsonProperty("tam-altin")]
    public GoldCurrency Tam_Altin { get; set; }
    [JsonProperty("cumhuriyet-altini")]
    public GoldCurrency Cumhuriyet_Altini { get; set; }
    [JsonProperty("ata-altin")]
    public GoldCurrency Ata_Altin { get; set; }
    [JsonProperty("14-ayar-altin")]
    public GoldCurrency On_Dort_Ayar_Altin { get; set; }
    [JsonProperty("18-ayar-altin")]
    public GoldCurrency On_Sekiz_Ayar_Altin { get; set; }
    [JsonProperty("22-ayar-bilezik")]
    public GoldCurrency Yirmi_Iki_Ayar_Bilezik { get; set; }
    [JsonProperty("ikibucuk-altin")]
    public GoldCurrency Ikibucuk_Altin { get; set; }
    [JsonProperty("besli-altin")]
    public GoldCurrency Besli_Altin { get; set; }
    [JsonProperty("gremse-altin")]
    public GoldCurrency Gremse_Altin { get; set; }
    [JsonProperty("resat-altin")]
    public GoldCurrency Resat_Altin { get; set; }
    [JsonProperty("hamit-altin")]
    public GoldCurrency Hamit_Altin { get; set; }
    [JsonProperty("gumus")]
    public GoldCurrency gumus { get; set; }
}

public class Currency
{
    [JsonProperty("Alış")]
    public string Alis { get; set; }
    [JsonProperty("Tür")]
    public string Tur { get; set; }
    [JsonProperty("Satış")]
    public string Satis { get; set; }
    [JsonProperty("Değişim")]
    public string Degisim { get; set; }
}

public class GoldCurrency
{
    [JsonProperty("Alış")]
    public string Alis { get; set; }
    [JsonProperty("Tür")]
    public string Tur { get; set; }
    [JsonProperty("Satış")]
    public string Satis { get; set; }
    [JsonProperty("Değişim")]
    public string Degisim { get; set; }
}

public class KurItem
{
    public string Doviz { get; set; }
    public string Alis { get; set; }
    public string Satis { get; set; }
    public string Fark { get; set; }
    public string Yon { get; set; }
}
