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
namespace Microsoft.WindowsAzure.Commands.ScenarioTest.CloudServiceTests
{
    using Common;
    using VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class StopAzureServiceScenarioTests : WindowsAzurePowerShellCertificateTest
    {
        public StopAzureServiceScenarioTests()
            : base("CloudService\\Common.ps1",
                   "CloudService\\CloudServiceTests.ps1")
        {

        }

        [TestInitialize]
        public override void TestSetup()
        {
            base.TestSetup();
            powershell.AddScript("Initialize-CloudServiceTest");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.CloudService)]
        [TestCategory(Category.BVT)]
        public void TestStopAzureServiceWithInvalidCredentials()
        {
            RunPowerShellTest("Test-WithInvalidCredentials { Stop-AzureService $(Get-CloudServiceName) }");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.CloudService)]
        [TestCategory(Category.BVT)]
        public void TestStopAzureServiceWithNonExistingService()
        {
            RunPowerShellTest("Test-StopAzureServiceWithNonExistingService");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.CloudService)]
        [TestCategory(Category.OneSDK)]
        [TestCategory(Category.CIT)]
        [TestCategory(Category.BVT)]
        public void TestStopAzureServiceWithProductionDeployment()
        {
            RunPowerShellTest("Test-StopAzureServiceWithProductionDeployment");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.CloudService)]
        [TestCategory(Category.BVT)]
        [Timeout(1200000)]
        public void TestStopAzureServiceWithStagingDeployment()
        {
            RunPowerShellTest("Test-StopAzureServiceWithStagingDeployment");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.CloudService)]
        [TestCategory(Category.BVT)]
        public void TestStopAzureServiceWithEmptyDeployment()
        {
            RunPowerShellTest("Test-StopAzureServiceWithEmptyDeployment");
        }

        [TestMethod]
        [TestCategory(Category.All)]
        [TestCategory(Category.CloudService)]
        [TestCategory(Category.BVT)]
        public void TestStopAzureServiceWithoutName()
        {
            RunPowerShellTest("Test-StopAzureServiceWithoutName");
        }
    }
}