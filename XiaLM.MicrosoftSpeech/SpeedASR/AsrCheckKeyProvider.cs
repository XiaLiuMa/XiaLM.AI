using Microsoft.Bing.Speech;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace XiaLM.MicrosoftSpeech.SpeedASR
{
    public class AsrCheckKeyProvider : IAuthorizationProvider
    {
        private string apiKey = string.Empty;   //密钥
        /// <summary>
        /// The fetch token URI
        /// </summary>
        private const string FetchTokenUri = "https://api.cognitive.microsoft.com/sts/v1.0";

        public AsrCheckKeyProvider(string apikey)
        {
            this.apiKey = apikey;
        }

        /// <summary>
        /// Gets the authorization token asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous read operation. The value of the string parameter contains the next the authorization token.
        /// </returns>
        /// <remarks>
        /// This method should always return a valid authorization token at the time it is called.
        /// </remarks>
        public Task<string> GetAuthorizationTokenAsync()
        {
            return FetchToken(FetchTokenUri, apiKey);
        }

        /// <summary>
        /// Fetches the token.
        /// </summary>
        /// <param name="fetchUri">The fetch URI.</param>
        /// <param name="subscriptionKey">The subscription key.</param>
        /// <returns>An access token.</returns>
        private static async Task<string> FetchToken(string fetchUri, string subscriptionKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                var uriBuilder = new UriBuilder(fetchUri);
                uriBuilder.Path += "/issueToken";

                using (var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null).ConfigureAwait(false))
                {
                    return await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
        }
    }
}
