using Newtonsoft.Json;
using ProjectDaanDeWilde.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaanDeWilde.Repositories
{
    public static class CommentRepository
    {
        private const string _BASEURI = "https://daandewildeapidp.azurewebsites.net/api/v1/comments/";
        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromMinutes(10);
            return client;
        }

        public static async Task<List<Comment>> GetCommentsAsync(int chargerid)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = $"{_BASEURI}chargers/{chargerid}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        return JsonConvert.DeserializeObject<List<Comment>>(json);
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        public static async Task PostCommentAsync(Comment comment)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = $"{_BASEURI}chargers/{comment.ChargerId}";

                string json = JsonConvert.SerializeObject(comment);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong while posting comment :(");
                }
            }
        }

        public static async Task PutCommentAsync(Comment comment)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = $"{_BASEURI}{comment.CommentId}";

                string json = JsonConvert.SerializeObject(comment);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong while updating comment :(");
                }
            }
        }

        public static async Task DelCommentAsync(Comment comment)
        {
            using (HttpClient client = GetHttpClient())
            {
                string url = $"{_BASEURI}{comment.CommentId}";

                var response = await client.DeleteAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong while deleting comment :(");
                }
            }
        }
    }
}
