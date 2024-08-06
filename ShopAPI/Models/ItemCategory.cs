﻿using System.Text.Json.Serialization;

namespace ShopAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ItemCategory
    {
        Unassigned = 1,

        Electronics = 2,

        Appliances = 3,

        Fashion = 4,

        Health = 5,

        Sports = 6,

        Media = 7,

        Toys = 8,

        Furniture = 9,

        Groceries = 10,

        Automotive = 11,

        Outdoors = 12


    }
}
