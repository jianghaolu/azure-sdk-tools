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

using System.Collections;
using System.Collections.Generic;
using Microsoft.Azure.Commands.Resources.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Resources
{
    /// <summary>
    /// Updates an existing resource group.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureResourceGroup"), OutputType(typeof(PSResourceGroup))]
    public class SetAzureResourceGroupCommand : ResourcesBaseCmdlet
    {
        [Alias("ResourceGroupName")]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, HelpMessage = "The resource group name.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true, HelpMessage = "An array of hashtables which represents resource tags.")]
        public List<Hashtable> Tags { get; set; }

        public override void ExecuteCmdlet()
        {
            UpdatePSResourceGroupParameters parameters = new UpdatePSResourceGroupParameters
            {
                ResourceGroupName = Name,
                Tags = Tags
            };

            WriteObject(ResourcesClient.UpdatePSResourceGroup(parameters));
        }
    }
}