﻿using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class PartModel
    {
        public int partID { get; set; }
        public string? partName { get; set; }
        public string? rating { get; set; }
        public int ratingCount { get; set; }
        public string? productURL { get; set; }
        public double currentPrice { get; set; }
        public double newPrice { get; set; }
        public bool returnValue = true;
        public IEnumerable<IPartPriceHistory>? historicalPrices { get; set; }

        // This function is used as an overloaded constructor to parse data for retrival
        public PartModel ParseVehiclePartEntityToVehiclePartModel(IPartEntity part)
        {
            partID = part.partID;
            partName = part.partName;
            rating = part.rating;
            ratingCount = part.ratingCount;
            productURL = part.productURL;
            currentPrice = part.currentPrice;
            return this;
        }
        public PartModel ReturnValueInvalidation()
        {
            if (partID < 0 || partName == null)
            {
                returnValue = false;
                return this;
            }
            return this;
        }
    }
}
