using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.FormFlow;
using HotelBot.Models;

namespace HotelBot.Dialogs
{
    public class HotelBotDialog
    {

        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
                    new RegexCase<IDialog<string>>(
                                        new Regex("^hi", RegexOptions.IgnoreCase),
                                        (context, text) =>
                                        {
                                            return Chain.ContinueWith(new GreetingDialog(), AfterGreetingContinuationAsync);
                                        }),
                                        new DefaultCase<string, IDialog<string>>(
                                        (context, text) =>
                                        {
                                            return Chain.ContinueWith(FormDialog.FromForm(RoomReservation.BuildForm, FormOptions.PromptInStart), AfterGreetingContinuationAsync);
                                        })


                ).Unwrap()
                 .PostToUser();

        private static async Task<IDialog<string>> AfterGreetingContinuationAsync(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            return Chain.Return($"Thank you for using the hotel bot: {name}");
        }
    }
}