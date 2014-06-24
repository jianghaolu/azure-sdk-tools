﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Commands.ExpressRoute
{
    using Microsoft.WindowsAzure.Management.ExpressRoute.Models;
    using System.Management.Automation;
    
    [Cmdlet(VerbsCommon.Get, "AzureDedicatedCircuitLink"), OutputType(typeof(AzureDedicatedCircuitLink))]
    public class GetAzureDedicatedCircuitLinkCommand : ExpressRouteBaseCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true,
            HelpMessage = "Service Key representing the Azure Dedicated Circuit")]
        [ValidateGuid]
        [ValidateNotNullOrEmpty]
        public string ServiceKey { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true,
            HelpMessage = "Vnet Name")]
        [ValidateNotNullOrEmpty]
        public string VNetName { get; set; }

        public override void ExecuteCmdlet()
        {
            if (!string.IsNullOrEmpty(VNetName))
            {
                GetByVNetName();
            }
            else
            {
                GetNoVNetName();
            }          
        }

        private void GetByVNetName()
        {
            var link = ExpressRouteClient.GetAzureDedicatedCircuitLink(ServiceKey, VNetName);
            WriteObject(link);
        }

        private void GetNoVNetName()
        {
            var links = ExpressRouteClient.ListAzureDedicatedCircuitLink(ServiceKey);
                    WriteObject(links, true);
        }


    }
}
