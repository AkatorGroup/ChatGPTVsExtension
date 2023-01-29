using ASPNETWithChatGPT.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ASPNETWithChatGPT
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var docView = await VS.Documents.GetActiveDocumentViewAsync();

            var selection = docView?.TextView.Selection.SelectedSpans.FirstOrDefault();

            if (selection.HasValue)
            {
                // Initialize the HttpClient
                var client = new HttpClient();

                // Set the API key and endpoint
                client.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
                var endpoint = "https://api.openai.com/v1/engines/text-davinci-002/completions";

                Console.Write("Enter a prompt: ");
                var input = selection.Value.GetText();

                // Create the request body
                var requestBody = new StringContent(JsonConvert.SerializeObject(new
                {
                    prompt = input,
                    max_tokens = 1000,
                    temperature = 0.5
                }), Encoding.UTF8, "application/json");

                // Send a request to the ChatGPT API to generate text
                var response = await client.PostAsync(endpoint, requestBody);

                // Read the response as a string
                var responseString = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response
                var responseData = JsonConvert.DeserializeObject<ChatGptModel>(responseString);

                // Replace the generated text to the console
                docView.TextBuffer.Replace(selection.Value, selection.Value.GetText() + "\n" + responseData.Choices[0].Text.Replace("</code>", "").Replace("<code>",""));
            }
        }
    }
}
