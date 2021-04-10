using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Pokedex.External.Interface.Model
{
    public class TranslationResponse
    {
        public TranslationResponse()
        {
            Success = new Success();
            Contents = new Contents();
        }

        [JsonPropertyName("success")]
        public Success Success { get; set; }

        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }
    }

    public class Success
    {
        public Success()
        {
            Total = 0;
        }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class Contents
    {
        public Contents()
        {
            Translated = string.Empty;
            Text = string.Empty;
            Translation = string.Empty;
        }
        [JsonPropertyName("translated")]
        public string Translated { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("translation")]
        public string Translation { get; set; }

    }
}
