﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreLog 
    {
        public int? _logId { get; set; }
        public string? _levelName { get; set; }
        public string? _categoryName { get; set; }
        public string? _dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        public string? _userId { get; set; }
        public string? _description { get; set; }
    }
}
