using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETWithChatGPT.Models
{
    public class ChatGptContent
    {
        public string Text { get; set; }
        public string Index { get; set; }
        public string Logprobs { get; set; }
        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }
}
