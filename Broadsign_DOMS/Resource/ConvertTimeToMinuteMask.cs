using System;
using System.Collections.Generic;
using System.Text;

namespace BUOH.Model
{
    public class ConvertTimeToMinuteMask
    {
        string displayUnitID;
        string finalMinuteMask;
        string day;
        string start;
        string startMM;
        string end;
        string endMM;

        public string Day { get => day; set => day = value; }
        public string Start { get => start; set => start = value; }
        public string StartMM { get => startMM; set => startMM = value; }
        public string End { get => end; set => end = value; }
        public string EndMM { get => endMM; set => endMM = value; }
        public string DisplayUnitID { get => displayUnitID; set => displayUnitID = value; }
        public string FinalMinuteMask { get => finalMinuteMask; set => finalMinuteMask = value; }
    }
}
