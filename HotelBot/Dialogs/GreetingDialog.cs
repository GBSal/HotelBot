using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace HotelBot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hi, I'm John bot");

            context.Wait(MessageReceivedAsync);
        }

        public virtual  async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {

            var message = await argument;
            string userName = string.Empty;
            var getName = false;

            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName)
            {

                userName = message.Text;
                context.UserData.SetValue<string>("Name", userName);
                


            }


            if (string.IsNullOrEmpty(userName) || getName==false)
            {
                await context.PostAsync("What is your name ?");
                context.UserData.SetValue<bool>("GetName",true);
                
                
            }
            else {
                await context.PostAsync($"Hi {userName}, How can I help you today ?");
            }

            context.Wait(MessageReceivedAsync);

        }
    }
} 