using Broadsign_DOMS.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Model
{
    public class CloneUserModel : ObservableObject
    {
        private ObservableCollection<ContainerScopeRelationModel> _group_ids;
        private ObservableCollection<ContainerScopeModel> _scopingRelation;
        private ObservableCollection<GroupModel> _group_names;

        public string Username { get; set; }
        public int Id { get;  set; }
        public ObservableCollection<GroupModel> Groups 
        { 
            get => _group_names;
            set
            {
                _group_names = value;
                OnPropertyChanged("Groups");
            }
        }
        public ObservableCollection<ContainerScopeRelationModel> Group_ids { get; set; }
        public ObservableCollection<ContainerScopeModel> ScopingRelation 
        { 
            get => _scopingRelation;
            set
            {
                _scopingRelation = value;
                OnPropertyChanged("ScopingRelations");
            }
        }
        public string Name { get; set; }
        public int UserContainer_id { get; set; }
    }
}
