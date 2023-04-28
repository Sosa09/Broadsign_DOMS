using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class DisplayUnits : ObservableObject
    {
        private string displayUnitID;
        private string minuteMask;
        private string dayPartID;

        public string DisplayUnitID 
        { 
            get => displayUnitID;
            set
            {
                displayUnitID = value;
                OnPropertyChanged(nameof(DisplayUnitID));
            }
        }
        public string MinuteMask 
        { 
            get => minuteMask;
            set
            {
                minuteMask = value;
                OnPropertyChanged(nameof(MinuteMask));
            }
        }
        public string DayPartID 
        { 
            get => dayPartID;
            set
            {
                dayPartID = value;
                OnPropertyChanged(nameof(DayPartID));
            }
        }
    }
}
