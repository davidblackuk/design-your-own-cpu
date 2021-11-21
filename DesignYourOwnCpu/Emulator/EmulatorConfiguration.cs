using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    public class EmulatorConfig : IEmulatorConfig
    {
        private readonly IConfigurationRoot config;

        public EmulatorConfig(IConfigurationRoot config)
        {
            this.config = config;
        }

        public bool QuietOutput => config["quiet"] != null && config["quiet"].ToLowerInvariant() == "true";

        public string BinaryFilename => config["input"];

    }
}
