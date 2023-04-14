﻿using Broadsign_DOMS.Model;
using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class ResourceViewModel : ObservableObject, IPageViewModel
    {
        private ICommand _selectCsvFileCommand;
        private ICommand _checkedRadioButton;
        private ICommand _renameResourceCommand;
        private string _search;
        Domain _domain;
        private string _nameCheckedRadioButton;
        private ObservableCollection<object> _resourceList;

        public ResourceViewModel()
        {
            Messenger.Default.Register<Domain>(this, "DomainResourceViewModel", x => Domain = x, true);
            Messenger.Default.Register<string>(this, "SearchResourceViewModel", x => Search = x, true);
        }

        public ICommand SelectCsvFileCommand 
        {
            get 
            { 
                if(_selectCsvFileCommand == null)
                   _selectCsvFileCommand = new RelayCommand(_readCsvContent);
                return _selectCsvFileCommand;
            } 
        }
        public ICommand CheckedRadioButton
        {
            get
            {
                if (_checkedRadioButton == null)
                    _checkedRadioButton = new RelayCommand(_storecheckedButtonName);
                return _checkedRadioButton;
            }
        }
        public ICommand RenameResourceCommand
        {
            get 
            {
                return _renameResourceCommand ?? (new RelayCommand(
                    param => _renameResources()
                    )) ;
            }
        }
        public ObservableCollection<object> ResourceList
        {
            get
            {
                if (_resourceList == null)
                    _resourceList = new ObservableCollection<object>();
                return _resourceList;
            }
            set
            {
                _resourceList = value;
                OnPropertyChanged(nameof(ResourceList));
            }
        }

        public Domain Domain 
        {
            get
            {
                return _domain ?? new Domain();
            } 
            set
            {
                _domain = value;
                OnPropertyChanged(nameof(Domain));
            }
        }

        public string Search 
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged("Search");
                _searchByName(value);
            } 
        }

        private void _searchByName(string value)
        {
            if (_nameCheckedRadioButton == "Frame")
                ResourceList = new ObservableCollection<object>(CommonResources.Frames.Where(x => x.Name.Contains(value)));
            else if (_nameCheckedRadioButton == "DisplayUnit")
                ResourceList = new ObservableCollection<object>(CommonResources.DisplayUnits.Where(x => x.Name.Contains(value)));
            else if (_nameCheckedRadioButton == "Player")
                ResourceList = new ObservableCollection<object>(CommonResources.Players.Where(x => x.Name.Contains(value)));
        }

        private void _renameResources()
        {
            if (_nameCheckedRadioButton == "Frame")
                //FrameModel.UpdateFrame();
                FrameModel.UpdateRename(Domain, ResourceList);

            else if (_nameCheckedRadioButton == "DisplayUnit")
                DisplayUnitModel.UpdateDisplayUnits(Domain, ResourceList);

            else
                PlayerModel.UpdatePlayers(Domain, ResourceList);

        }

        private void _storecheckedButtonName(object obj)
        {
            ResourceList.Clear();
            ObservableCollection<object> bsObjects = new ObservableCollection<object>();
            if((string)obj == "0")
            {
                _nameCheckedRadioButton = "Frame";
                bsObjects = new ObservableCollection<object>(CommonResources.Frames);
            }
            else if((string)obj == "1")
            {
                _nameCheckedRadioButton = "DisplayUnit";
                bsObjects = new ObservableCollection<object>(CommonResources.DisplayUnits);
            }
            else
            {
                _nameCheckedRadioButton = "Player";
                bsObjects = new ObservableCollection<object>(CommonResources.Players);
            }


            ResourceList = bsObjects;
        }

        private void _readCsvContent(object obj)
        {
            //open file location
            var file = new OpenFileDialog();
            file.ShowDialog();

            //TODO check file dialog and select file open file
            //TDOO implement found increment to show to the end user how many resources were found
            if (file.FileName == "")
                return;
            ResourceList.Clear();
            StreamReader sr = new StreamReader(file.FileName);

            while (!sr.EndOfStream)
            {
               
                var line = sr.ReadLine().Split(',', ';');
                if (_nameCheckedRadioButton == "0")
                {
                    var frameObject = CommonResources.Frames.Where(x => x.Id == Convert.ToInt32(line[0]));
                    if (frameObject.Count() > 0)
                    {
                        frameObject.First().NewName = $"{line[1]} {frameObject.First().Name}";
                        ResourceList.Add(frameObject.First());
                    }
                }
                else if (_nameCheckedRadioButton == "1")
                {
                    var duObject = CommonResources.DisplayUnits.Where(x => x.Id == Convert.ToInt32(line[0]));
                    if (duObject.Count() > 0)
                    {
                        duObject.First().NewName = $"{line[1]} {duObject.First().Name}";
                        ResourceList.Add(duObject.First());
                    }
             
                }
                   
                
                else
                {
                    var playerObject = CommonResources.Players.Where(x => x.Id == Convert.ToInt32(line[0]));
                    if (playerObject.Count() > 0)
                    {
                        playerObject.First().NewName = $"{line[1]} {playerObject.First().Name}";
                        ResourceList.Add(playerObject.First());
                    }


                }
            }
            
        }
    }
}