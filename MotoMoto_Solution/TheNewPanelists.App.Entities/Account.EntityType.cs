﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.App.DataStoreEntities;

namespace TheNewPanelists.App.Entities
{
    public class EntityType : IBaseUser
    {
        public int _typeId { get; set; }
        public string? _typeName { get; set; }
    }
}
