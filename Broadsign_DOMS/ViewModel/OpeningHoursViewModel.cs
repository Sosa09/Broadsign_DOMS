using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using BUOH.Model;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;



namespace Broadsign_DOMS.ViewModel
{
    public class OpeningHoursViewModel : ObservableObject, IPageViewModel
    {

        string TimeToMinuteMaskTableCSV = @"C:\Users\Soufiane\OneDrive - Clear Channel International\Work\Programming\Projects\UAT\Resources\OpeningHours\TTMM.csv";
        string ConvertTimeToMinuteMaskCSV = String.Empty;
        bool isBusy;
        string total;

        ObservableCollection<TimeToMinuteMaskTable> ttmmTable = new ObservableCollection<TimeToMinuteMaskTable>();
        List<List<ConvertTimeToMinuteMask>> ttmmToConvert = new List<List<ConvertTimeToMinuteMask>>();
        //create a new instance of the class BSCMinuteMaskUpdateModel as a list to store your body to update the day part later in the program
        ObservableCollection<DayPartModel> list_bscMinuteMask = new ObservableCollection<DayPartModel>();
        //create a new instance of the class display unit as a list to store your csv values
        ObservableCollection<DisplayUnits> dus;
        ObservableCollection<LogModel> logModels;

        Visibility visibility;
        ICommand updateOH;
        ICommand selectFile;
        ICommand exportCommand;
        string bSCToken;


        public OpeningHoursViewModel()
        {

    
            //reads and stores the csv values into display units instance created earlier.
            using (StreamReader reader = new StreamReader(TimeToMinuteMaskTableCSV))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    ttmmTable.Add(new TimeToMinuteMaskTable
                    {
                        Day = values[0],
                        Hour = values[1],
                        MinuteMask = values[2]
                    });
                }
            }



        }

        public ObservableCollection<DisplayUnits> Dus
        {
            get
            {
                if (dus == null)
                    dus = new ObservableCollection<DisplayUnits>();
                return dus;
            }
            set
            {
                dus = value;
                OnPropertyChanged(nameof(Dus));
            }
        }

        public ObservableCollection<LogModel> LogModels
        {
            get
            {
                if (logModels == null)
                    logModels = new ObservableCollection<LogModel>();
                return logModels;
            }
            set
            {
                logModels = value;
                OnPropertyChanged(nameof(LogModels));
            }
        }

        public ICommand UpdateOH
        {
            get
            {
                if (updateOH == null)
                {
                    updateOH = new RelayCommand(updateOHMethod);

                }
                return updateOH;
            }
        }
        public ICommand SelectFile
        {
            get
            {
                if (selectFile == null)
                    selectFile = new RelayCommand(OpenFile);
                return selectFile;
            }
        }
        public Visibility Visibility
        {
            get
            {
                if (visibility.Equals(Visibility.Visible))
                    visibility = Visibility.Hidden;
                return visibility;
            }
            set
            {
                visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }
        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                    exportCommand = new RelayCommand(Export);
                return exportCommand;
            }
        }

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                this.isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        public string Total
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged(nameof(Total));
            }
        }
        public string BSCToken
        {
            get
            {
                return bSCToken;
            }
            set
            {
                bSCToken = value;
                OnPropertyChanged(nameof(BSCToken));
            }
        }
        async void updateOHMethod(object obj)
        {
            if (BSCToken == null)
            {
                System.Windows.MessageBox.Show("Please enter a token first");
                return;
            }
            var totalCompleted = 0;
            IsBusy = true;
            Visibility = Visibility.Visible;
            foreach (var item in Dus)
            {

                if (item.DayPartID != null)
                {

                    DayPartModel daypart = list_bscMinuteMask.FirstOrDefault(x => x.Id.ToString() == item.DayPartID);
                    daypart.Minute_mask = item.MinuteMask;
                    var body = JsonConvert.SerializeObject(new
                    {
                        active = daypart.Active,
                        day_mask= daypart.Day_mask,
                        domain_id= daypart.Domain_id,
                        end_date= daypart.End_date,
                        end_time= daypart.End_time,
                        id= daypart.Id,
                        impressions_per_hour= daypart.Impressions_per_hour,
                        minute_mask= daypart.Minute_mask,
                        name= daypart.Name,
                        start_date= daypart.Start_date,
                        start_time= daypart.Start_time,
                        virtual_end_date= daypart.Virtual_end_date,
                        virtual_start_date= daypart.Virtual_start_date,
                        weight= daypart.Weight
                    });

                    await Task.Run(() => Requests.SendRequest("/day_part/v5", BSCToken, RestSharp.Method.PUT, body));
                    LogModels.Add(new LogModel { DisplayUnit = item.DisplayUnitID, StatusCode = Requests.Response.StatusCode.ToString(), MinuteMask = daypart.Minute_mask });

                }
                totalCompleted++;
                Total = $"{totalCompleted}/{Dus.Count}";
            }
            IsBusy = false;
            Visibility = Visibility.Hidden;
            System.Windows.MessageBox.Show($"{totalCompleted} completed updates");

        }
        async void OpenFile(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            ConvertTimeToMinuteMaskCSV = openFileDialog.FileName;
            if (openFileDialog != null)
            {
                using (StreamReader reader = new StreamReader(ConvertTimeToMinuteMaskCSV))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        string du = "";
                        foreach (var item in values)
                        {

                            var splitter = item.Split(' ');
                            if (splitter.Length == 1)
                                du = item;
                            else
                            {
                                try
                                {
                                    ttmmToConvert.Add(new List<ConvertTimeToMinuteMask> { new ConvertTimeToMinuteMask { DisplayUnitID = du, Day = splitter[0], Start = splitter[1], End = splitter[2] } });

                                }
                                catch (Exception ex)
                                {
                                    System.Windows.MessageBox.Show(ex.ToString());
                                }
                            }


                        }
                    }
                    ConvertToMinuteMask();
                }

                //Requesting an API call from Broadsign control to get all day parts from the requesting display unit ids
                Requests.SendRequest("/day_part/v5", BSCToken, RestSharp.Method.GET);

                //stores the request api and parses the json results
                dynamic result = JsonConvert.DeserializeObject(Requests.Response.Content);


                int index = 1;
                //Going trough each Day part from the requested api
                foreach (var item in result["day_part"])
                {
                    //check if item is active if yes check if item exists in the display units instance (values stored from csv file)
                    if (item.active == "True")
                    {
                        DisplayUnits displayUnit = Dus.Where(x => x.DisplayUnitID == item.parent_id.ToString()).FirstOrDefault();
                        if (displayUnit != null)
                        {
                            //selecting display unit and adding the day part id
                            Dus.FirstOrDefault(x => x.DisplayUnitID == item.parent_id.ToString()).DayPartID = item.id;

                            //adding a list of day part to use as a body later for the update request
                            list_bscMinuteMask.Add(new DayPartModel
                            {
                                Active = item.active,
                                Day_mask = item.day_mask,
                                Domain_id = item.domain_id,
                                End_date = item.end_date,
                                End_time = item.end_time,
                                Id = item.id,
                                Impressions_per_hour = item.impressions_per_hour,
                                Minute_mask = item.minute_mask,
                                Name = item.name,
                                Start_date = item.start_date,
                                Start_time = item.start_time,
                                Virtual_end_date = item.virtual_end_date,
                                Virtual_start_date = item.virtual_start_date,
                                Weight = item.weight
                            });
                        }
                    }
                }

            }

        }
        void ConvertToMinuteMask()
        {
            if (ttmmToConvert == null)
            {
                throw new ArgumentNullException();
            }

            string mu = "";
            string currentDu;
            string lastDu = "";
            foreach (var t in ttmmToConvert)
            {

                int totalStart = 0;
                int totalEnd = 0;
                var day = t[0].Day;
                currentDu = t[0].DisplayUnitID;


                List<TimeToMinuteMaskTable> currentDay = ttmmTable.Where(x => x.Day == day).ToList();

                if (currentDay != null)
                {
                    bool startIsFound = false;
                    bool endIsFound = false;
                    double subStractedStartedHumanHoursLanguage = 0;
                    int subStractedEndedHumanHoursLanguage = 0;
                    if (lastDu == string.Empty || lastDu != currentDu)
                    {
                        if (lastDu != string.Empty)
                            Dus.Add(new DisplayUnits { DisplayUnitID = lastDu, MinuteMask = mu });
                        lastDu = t[0].DisplayUnitID;
                        mu = "";
                    }

                    foreach (var cDay in currentDay)
                    {
                        int currentMM = int.Parse(cDay.MinuteMask);


                        if (!startIsFound)
                        {
                            if (DateTime.ParseExact(cDay.Hour, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(t[0].Start, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture))
                            {
                                subStractedStartedHumanHoursLanguage = (int)double.Parse((DateTime.ParseExact(cDay.Hour, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) - DateTime.ParseExact(t[0].Start, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)).TotalMinutes.ToString());
                                totalStart = currentMM - (int)subStractedStartedHumanHoursLanguage;

                                mu = mu + $"{totalStart}-";
                                startIsFound = true;
                            }

                        }
                        if (!endIsFound)
                        {
                            if (DateTime.ParseExact(cDay.Hour, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) >= DateTime.ParseExact(t[0].End, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture))
                            {
                                subStractedEndedHumanHoursLanguage = (int)double.Parse((DateTime.ParseExact(cDay.Hour, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) - DateTime.ParseExact(t[0].End, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)).TotalMinutes.ToString());
                                totalEnd = (currentMM - subStractedEndedHumanHoursLanguage) - 1;
                                mu = mu + $"{totalEnd};";
                                endIsFound = true;
                                break;
                            }

                        }


                    }
                }
                if (t == ttmmToConvert.Last())
                    Dus.Add(new DisplayUnits { DisplayUnitID = lastDu, MinuteMask = mu });
            }
        }
        void Export(object obj)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();



            using (StreamWriter sr = new StreamWriter($"{fbd.SelectedPath.ToString()}\\logModel.csv"))
            {
                foreach (var log in LogModels)
                    sr.WriteLine($"{log.DisplayUnit}, {log.StatusCode}, {log.MinuteMask}");
            }
        }
    }
}

