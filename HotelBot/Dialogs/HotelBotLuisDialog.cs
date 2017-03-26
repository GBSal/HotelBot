using HotelBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HotelBot.Dialogs;
using Microsoft.Bot.Builder.FormFlow;

namespace HotelBot.Dialogs
{

    [LuisModel("7651e348-ae0d-4611-a8e0-0ebf53c8f77b", "b9f635eb5c3d4bacb0270c105b6ef8b7")]
    [Serializable]
    public class HotelBotLuisDialog : LuisDialog<RoomReservation>
    {

        private readonly BuildFormDelegate<RoomReservation> ReserveRoom;

        public HotelBotLuisDialog(BuildFormDelegate<RoomReservation> reserveRoom)
        {
           this. ReserveRoom = reserveRoom;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), CallBack);
        }

       
        [LuisIntent("Reservation")]
        public async Task RoomReservation(IDialogContext context, LuisResult result)
        {
            var enrolForm = new FormDialog<RoomReservation>(new RoomReservation(), 
                                                                this.ReserveRoom, 
                                                                FormOptions.PromptInStart);
            context.Call<RoomReservation>(enrolForm, CallBack);
        }


        [LuisIntent("QueryAmenities")]
        public async Task QueryAmenities(IDialogContext context, LuisResult result)
        {
            foreach (var entity in result.Entities.Where(e => e.Type == "amenity"))
            {
                var value = entity.Entity.ToLower();
                if (value == "pool" || value == "gym" || value == "wifi" || value == "towels")
                {
                   await context.PostAsync("Yes we have that!");
                    context.Wait(MessageReceived);
                    return;


                }
                else
                {
                    await context.PostAsync("I'm sorry I don't have that");
                    context.Wait(MessageReceived);
                    return;
                }

            }
            await context.PostAsync("I'm sorry I don't have that");
            context.Wait(MessageReceived);
            return;
        }


        private async Task CallBack(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

    }
}