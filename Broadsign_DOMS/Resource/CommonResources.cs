using Broadsign_DOMS.Model;
using Broadsign_DOMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadsign_DOMS.Resource
{
    public abstract class CommonResources
    {
        public static ObservableCollection<ContainerModel> Container { get; set; }
        public static ObservableCollection<ContainerScopeModel> Container_scope { get; set; }
        public static ObservableCollection<ContainerScopeRelationModel> Container_scope_relation { get; set; }
        public static ObservableCollection<UserModel> User { get; set; }
    }
}
