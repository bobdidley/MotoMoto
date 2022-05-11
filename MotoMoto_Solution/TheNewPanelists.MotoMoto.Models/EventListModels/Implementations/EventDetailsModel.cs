﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;


namespace TheNewPanelists.MotoMoto.Models
{
    public class EventDetailsModel : IEventDetailsModel
    {
        public int eventID { get; set; }
        //public string? eventLocation { get; set; }
        public string? eventTime { get; set; }
        public string? eventDate { get; set; }
        public IEnumerable<string>? registeredUsers { get; set; }

        // USED FOR MEETING POINT DIRECTIONS
        public string? eventStreetAddress { get; set; }
        public string? eventCity { get; set; }
        public string? eventState { get; set; }
        public string? eventCountry { get; set; }
        public string? eventZipCode { get; set; }
        public string? eventTitle { get; set; }
        public string? systemResponse { get; set; }

        public EventDetailsModel GetResponse(ResponseModel.response _responseAction)
        {
            if (systemResponse != null)
            {
                return this;
            }
            if (systemResponse == "success" || systemResponse == null)
            {
                systemResponse = _responseAction.ToString();
                return this;
            }
            return this;
        }

    }
}
