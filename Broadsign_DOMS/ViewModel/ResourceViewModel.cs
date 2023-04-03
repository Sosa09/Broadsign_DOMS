using Broadsign_DOMS.Model;
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

        Domains _domain;
        private string _nameCheckedRadioButton;
        private ObservableCollection<object> _resourceList;

        public ResourceViewModel()
        {
       
            Messenger.Default.Register<Domains>(this, "ResourceViewModel", x => Domain = x, true);
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
        public ObservableCollection<dynamic> ResourceList
        {
            get
            {
                if (_resourceList == null)
                    _resourceList = new ObservableCollection<dynamic>();
                return _resourceList;
            }
            set
            {
                _resourceList = value;
                OnPropertyChanged(nameof(ResourceList));
            }
        }

        public Domains Domain 
        {
            get
            {
                return _domain;
            } 
            set
            {
                _domain = value;
                OnPropertyChanged(nameof(Domain));
            }
        }
        private void _renameResources()
        {
            if (_nameCheckedRadioButton == "0")
                //FrameModel.UpdateFrame();
                MessageBox.Show("Frame");
            else if (_nameCheckedRadioButton == "1")
                //DisplayUnitModel.UpdateDisplayUnits();
                MessageBox.Show("DU");
            else
                PlayerModel.UpdatePlayers(Domain.Token, null, ResourceList);

        }



        private void _storecheckedButtonName(object obj)
        {
            _nameCheckedRadioButton = obj as string;
        }

        private void _readCsvContent(object obj)
        {
            //open file location
            var file = new OpenFileDialog();
            file.ShowDialog();
            
            //TODO check file dialog and select file open file
            //TDOO implement found increment to show to the end user how many resources were found
            StreamReader sr = new StreamReader(file.FileName);
            while (!sr.EndOfStream)
            {
                 var line = sr.ReadLine().Split(',', ';');                
                if (_nameCheckedRadioButton == "0")
                    throw new ArgumentNullException();
                else if (_nameCheckedRadioButton == "1")
                    throw new ArgumentNullException();
                else
                {
                    var playerObject = CommonResources.Players.Where(x => x.Id == Convert.ToInt32(line[0]));
                    if(playerObject.Count() > 0)
                    {
                        playerObject.First().NewName = $"{line[1]} {playerObject.First().Name}";
                        ResourceList.Add(playerObject.First());
                    }
                    
                        
                }
            }
            
        }
    }
}