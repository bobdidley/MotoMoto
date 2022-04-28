using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataAccess;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class PartFlaggingService
    {
        public bool callFlagCreation(FlagModel flag)
        {
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            return partFlaggingDataAccess.createOrIncrementFlag(flag);
        }

        public int CallGetFlagCount(FlagModel flag)
        {
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            return partFlaggingDataAccess.getFlagCount(flag);
        }

        public bool CallDecrementFlagCount(FlagModel flag)
        {
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            return partFlaggingDataAccess.DecrementOrRemove(flag);
        }
    }
}
