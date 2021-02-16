using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace GG.Extensions
{
    public static class WebExtensions
    {
        /// <summary>
        /// Quickly pings a URL and returns if it is accessible
        /// Defaults to ping google, cause if you cant reach google then the world has already ended.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CanReachInternet(string toPing = "http://google.com")
        {
            UnityWebRequest request = new UnityWebRequest(toPing);
            await request.SendWebRequest();

            return !request.isNetworkError;
        }
        
        /// <summary>
        ///     Setup a web request to send a JSON in the body
        /// </summary>
        /// <param name="json">The json to send</param>
        /// <param name="uri">The URI to send to</param>
        public static UnityWebRequest SetupPostWebRequest(string json, string uri)
        {
            UnityWebRequest request = UnityWebRequest.Post(uri, "");
            byte[] bodyRaw = new UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw) {contentType = "application/json"};
            request.downloadHandler = new DownloadHandlerBuffer();
            return request;
        }
		
        /// <summary>
        /// Adds a Parameter value to the url
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        public static void AddParameter(this UnityWebRequest request, string parameter, string value)
        {
            if (request.url.Contains("?"))
            {
                request.url += $"&{parameter}={value}";
            }
            else
            {
                request.url += $"?{parameter}={value}";
            }
        }
    }
}
