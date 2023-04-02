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
        public static ObservableCollection<ContainerModel> Containers { get; set; }
        public static ObservableCollection<PlayerModel> Players { get; set; }
        public static ObservableCollection<UserModel> Users { get; set; }
        public static ObservableCollection<GroupModel> Groups { get; set; }
        public static ObservableCollection<DisplayUnitModel> DisplayUnits { get; set; }
        public static ObservableCollection<FrameModel> Frames { get; set; }
        public static ObservableCollection<ConfigProfileModel> ConfigProfiles { get; set; }
        public static ObservableCollection<ContainerScopeModel> Container_Scopes { get; set; }
        public static ObservableCollection<ContainerScopeRelationModel> Container_Scope_Relations { get; set; }
               
        public static ObservableCollection<BundleModel> Bundles { get; set; }
        public static ObservableCollection<CampaignModel> Campaigns { get; set; }        
        public static ObservableCollection<ContentModel> Contents { get; set; }
        public static ObservableCollection<DayPartModel> DayParts { get; set; }
        public static ObservableCollection<IncidentModel> Incidents { get; set; }
        public static ObservableCollection<LoopPolicyModel> LoopPolicies { get; set; }              
        public static ObservableCollection<LoopSlotModel> LoopSlots { get; set; }

    }
}
