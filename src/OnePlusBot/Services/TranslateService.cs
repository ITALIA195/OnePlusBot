using System.Threading.Tasks;
using System;
using OnePlusBot._Extensions;

namespace OnePlusBot.Services
{
    public class TranslateService
    {
        private readonly IGoogleApiService _google;
        public async Task<string> Translate(string langs, string text = null)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text is empty or null", nameof(text));
            var langarr = langs.ToLowerInvariant().Split('>');
            if (langarr.Length != 2)
                throw new ArgumentException("Langs does not have 2 parts separated by a >", nameof(langs));
            var from = langarr[0];
            var to = langarr[1];
            text = text?.Trim();
            return (await _google.Translate(text, from, to).ConfigureAwait(false)).SanitizeMentions();
        }
    }
}
