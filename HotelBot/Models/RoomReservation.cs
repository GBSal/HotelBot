﻿using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBot.Models
{
    public enum BedSizeOptions
    {
        King,
        Queen,
        Single,
        Double
    }

    public enum AmenitiesOptions
    {
        Kitchen,
        ExtraTowels,
        GymAccess,
        Wifi

    }


    [Serializable]
    public class RoomReservation
    {

        public BedSizeOptions? Bedsize;
        public int? NumberOfOccupant;
        public DateTime? CheckInDate;
        public int? NumberOfDaysToStay;
        public List<AmenitiesOptions> Amenities;

        public static IForm<RoomReservation> BuildForm()
        {
            return new FormBuilder<RoomReservation>()
            .Message("Welcome to the hotel reservation bot!")
            .Build();
        }

    }
}