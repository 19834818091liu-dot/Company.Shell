using Company.Application.Share.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Config.ViewModels
{
    public class ConfigSoftwareViewModel
    {
        public ISystemConfigManager SystemConfigManager { get; set; }

        public ConfigSoftwareViewModel(ISystemConfigManager systemConfigManager)
        {
            SystemConfigManager = systemConfigManager;
        }
    }
}
