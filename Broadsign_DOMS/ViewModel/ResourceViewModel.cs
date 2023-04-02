using Broadsign_DOMS.Resource;
using Broadsign_DOMS.Service;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Broadsign_DOMS.ViewModel
{
    public class ResourceViewModel : ObservableObject, IPageViewModel
    {
        private ICommand _selectCsvFile;
        private ICommand _checkedRadioButton;
        private string _nameCheckedRadioButton;

        public ICommand SelectCsvFile 
        {
            get 
            { 
                if(_selectCsvFile == null)
                   _selectCsvFile = new RelayCommand(_readCsvContent);
                return _selectCsvFile;
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
                    var player = CommonResources.Players.Where(x => x.Id == Convert.ToInt32(line[0])).First();
                    player.NewName = line[1];
                  

                }
            }
            
        }
    }
}