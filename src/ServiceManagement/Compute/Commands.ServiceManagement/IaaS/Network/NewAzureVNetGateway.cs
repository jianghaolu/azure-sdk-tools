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

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.IaaS
{
    using System.Management.Automation;
    using Utilities.Common;
    using Management.Network.Models;

    [Cmdlet(VerbsCommon.New, "AzureVNetGateway"), OutputType(typeof(ManagementOperationContext))]
    public class NewAzureVNetGatewayCommand : ServiceManagementBaseCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Virtual network name.")]
        public string VNetName { get; set; }

        [Parameter(Position = 1, Mandatory = false, HelpMessage = "The type of routing that the gateway will use. This will default to StaticRouting if no value is provided.")]
        public GatewayType GatewayType { get; set; }

        protected override void OnProcessRecord()
        {
            GatewayCreateParameters parameters = new GatewayCreateParameters()
            {
                GatewayType = this.GatewayType,
            };

            ServiceManagementProfile.Initialize();
            ExecuteClientActionNewSM(
                null,
                this.CommandRuntime.ToString(),
                () => this.NetworkClient.Gateways.Create(this.VNetName, parameters));
        }
    }
}
