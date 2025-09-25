namespace PMA00300
{
    public partial class ConnectToSQLCls
    {
        public class XMLLinkChecker
        {
            private static readonly HttpClient client = new HttpClient();

            public async Task CheckIfXMLLink(string url)
            {
                try
                {
                    // Kirim permintaan HTTP GET untuk URL
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Periksa apakah konten adalah XML
                        string contentType = response.Content.Headers.ContentType.MediaType.ToLower();

                        if (contentType.Contains("xml"))
                        {
                            Console.WriteLine("URL mengarah ke file XML: " + url);
                        }
                        else
                        {
                            Console.WriteLine("URL tidak mengarah ke file XML.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Permintaan ke URL gagal, status kode: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                }
            }
        }
    }
}

