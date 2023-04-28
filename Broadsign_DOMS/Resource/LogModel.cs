
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Broadsign_DOMS.Model
{
    public class LogModel : ObservableObject
    {
        string displayUnit;
        string statusCode;
        string minuteMask;
        string time;

        public string DisplayUnit 
        { 
            get => displayUnit;
            set
            {
                displayUnit = value;
                OnPropertyChanged(nameof(displayUnit));
            }
        }
        public string StatusCode 
        { 
            get => statusCode;
            set
            {
                statusCode = value;
                OnPropertyChanged(nameof(StatusCode));
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
        public string Time { get => time; set => time = value; }
    }
}
