﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class EventAccountVerificationModel
    {
        public string? username { get; set; }
        public int? rating { get; set; }
        public string? review { get; set; }

        public string? systemResponse { get; set; }
        public EventAccountVerificationModel GetResponse(ResponseModel.response _responseAction)
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