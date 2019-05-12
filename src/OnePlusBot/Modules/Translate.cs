using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using OnePlusBot._Extensions;
using OnePlusBot.Services;
using System;

namespace OnePlusBot.Modules
{
    public class TraslateModule : ModuleBase<SocketCommandContext>
    {
        private readonly TranslateService _searches;
        private readonly IGoogleApiService _google;

        public TranslateCommands(TranslateService searches, IGoogleApiService google)
        {
            _searches = searches;
        }

        [Command("translate")]
        [Summary("Translates a string")]
        public async Task Translate(string langs, [Remainder] string text = null)
        {
            try
            {
                await Context.Channel.TriggerTypingAsync().ConfigureAwait(false);
                var translation = await _searches.Translate(langs, text).ConfigureAwait(false);
                await Context.Channel.EmbedAsync(new EmbedBuilder()
                .WithColor(9896005)
                .AddField(efb => efb.WithName("Translation: "+ langs).WithValue(translation).WithIsInline(false)));
            }
            catch(Exception ex)
            {
                var EmoteFalse = new Emoji("⚠");
                await Context.Message.AddReactionAsync(EmoteFalse);

                await Context.Channel.EmbedAsync(new EmbedBuilder()
                .WithColor(9896005)
                .WithDescription("Error: Bad input format, or something went wrong."));
                await ReplyAsync(ex.Message);
            }
        }
    }
}

