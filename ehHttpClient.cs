using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
public class EhHttpClient
{
    private string sasToken;
    private string ehUrl;
    private HttpClient httpClient = new HttpClient();
    public EhHttpClient(string resourceUri, string keyName, string keydata)
    {
        TimeSpan sinceEpoch = DateTime.UtcNow - new DateTime(1970, 1, 1);
        var week = 60 * 60 * 24 * 7;
        var expiry = Convert.ToString((int)sinceEpoch.TotalSeconds + week);
        // SAS Code borrowed from alberto gorni @ https://github.com/algorni/EventHubSASTokenGenerator/blob/master/code/SASTokenGenerator/Program.cs
        string stringToSign = HttpUtility.UrlEncode(resourceUri) + "\n" + expiry;
        HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(keydata));
        var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
        var _sasToken = String.Format(CultureInfo.InvariantCulture, "sr={0}&sig={1}&se={2}&skn={3}", HttpUtility.UrlEncode(resourceUri), HttpUtility.UrlEncode(signature), expiry, keyName);
        sasToken = _sasToken;
        ehUrl = resourceUri;
    }
    public async Task SendSingleMessageAsync(object sendWhat)
    {
        var hrm = new HttpRequestMessage(HttpMethod.Post, ehUrl);       
        hrm.Headers.Authorization = new AuthenticationHeaderValue("SharedAccessSignature", sasToken);
        string content = JsonSerializer.Serialize(sendWhat);
        StringContent stc = new StringContent(content,Encoding.UTF8,"application/json");
        hrm.Content = stc; 
        //Console.WriteLine($"Sending HTTP Payload of {content.Length} chars (in UTF-8) to URI {ehUrl} with SAS {sasToken}");
        var htr = await httpClient.SendAsync(hrm);
        //htr.EnsureSuccessStatusCode();
        //Console.WriteLine($"Status : {htr.StatusCode} {await htr.Content.ReadAsStringAsync()}");
    }

    public async Task SendMessageBatchAsync(object[] sendwhat)
    {
        var hrm = new HttpRequestMessage(HttpMethod.Post, ehUrl);
        hrm.Headers.Authorization = new AuthenticationHeaderValue("SharedAccessSignature", sasToken);
        
        string content = JsonSerializer.Serialize(sendwhat.Select( e => new {Body = JsonSerializer.Serialize(e)}));      
        StringContent stc = new StringContent(content,Encoding.UTF8,"application/vnd.microsoft.servicebus.json");
        stc.Headers.ContentType.CharSet = string.Empty;
        hrm.Content = stc; 
        var htr = await httpClient.SendAsync(hrm);
        Console.WriteLine($"{content.Length} Chars - {htr.StatusCode} {await htr.Content.ReadAsStringAsync()}");

        // if (htr.StatusCode.ToString() == "ServiceUnavailable")
        // {
            
        //    await Task.Delay(250);
        //    var htr2 = await httpClient.SendAsync(hrm);
        //    Console.WriteLine($"retriedwith: {htr2.StatusCode} {await htr.Content.ReadAsStringAsync()}");

        //     if (htr.StatusCode.ToString() == "ServiceUnavailable")
        //     {
                
        //         await Task.Delay(1000);
        //         var htr3 = await httpClient.SendAsync(hrm);
        //         Console.WriteLine($"retriedagainwith: {htr3.StatusCode} {await htr.Content.ReadAsStringAsync()}");

        //         if (htr.StatusCode.ToString() == "ServiceUnavailable")
        //         {
        //             Console.WriteLine($"YOU WIN: {htr3.StatusCode} {await htr.Content.ReadAsStringAsync()}");
        //         }
        //     }
        // }
    }
}