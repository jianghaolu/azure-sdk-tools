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

namespace Microsoft.WindowsAzure.Commands.Websites.WebJobs
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using Microsoft.WindowsAzure.Commands.Utilities.Websites.Services.WebJobs;
    using Utilities.Websites;
    using Utilities.Websites.Common;
using Microsoft.WindowsAzure.WebSitesExtensions.Models;
    
    [Cmdlet(VerbsCommon.Get, "AzureWebsiteJob"), OutputType(typeof(List<WebJob>))]
    public class GetAzureWebsiteJobCommand : WebsiteContextBaseCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "The web job name.")]
        public string JobName { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "The web job type.")]
        [ValidateSet(new string[] { "Triggered", "Continuous" }, IgnoreCase = true)]
        public string JobType { get; set; }

        public override void ExecuteCmdlet()
        {
            WebJobFilterOptions options = new WebJobFilterOptions() { Name = Name, Slot = Slot, JobName = JobName, JobType = JobType };
            WriteObject(WebsitesClient.FilterWebJobs(options), true);
        }
    }
}
