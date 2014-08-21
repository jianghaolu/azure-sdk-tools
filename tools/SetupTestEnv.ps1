# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

$scriptFolder = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
. ($scriptFolder + '.\SetupEnv.ps1')

function Get-RegistryKeyValues()
{
    param
    (
        [Parameter(Mandatory=1)][string]$regKeyPath,
        [Parameter(Mandatory=1)][string]$regKeyValueName
    )

    $regKeyValue = ""

    $regKeyValueObject = Get-ItemProperty -Path $regKeyPath -Name $regKeyValueName  -ErrorAction SilentlyContinue
    if ($regKeyValueObject -ne $null) {
        $regKeyValue = $regKeyValueObject.$regKeyValueName
    }
    return $regKeyValue
}

#Get WebPI CMD
$WebPi="$scriptFolder\test\WebpiCmd.exe"

$allWebPIVersions = Get-ChildItem HKLM:\SOFTWARE\Microsoft\WebPlatformInstaller -ea SilentlyContinue | 
    ForEach-Object {  
        if($_.GetValue("InstallPath", $null) -ne $null)  
        {
            $WebPi = $_.GetValue("InstallPath")  + "WebpiCmd.exe"
        }
    }

Write-Host "using webpi command: $WebPi"

$programFiles = $env:ProgramFiles
if (Test-Path "$env:ProgramW6432"){
    $programFiles = $env:ProgramW6432
}

if (!(Test-Path "HKLM:\SOFTWARE\Microsoft\Microsoft SDKs\ServiceHosting\v2.4")) {
    Write-Host installing Azure Authoring Tools
    Start-Process "$WebPi" "/Install /products:WindowsAzureSDK_Only.2.4 /accepteula" -Wait
}

$detectKey = "HKLM:\SOFTWARE\Microsoft\Windows Azure Emulator";
$producteVersion = Get-RegistryKeyValues $detectKey "FullVersion"
if (!($producteVersion.StartsWith("2.4."))) {
    Write-Host installing Azure Compute Emulator
    Start-Process "$WebPi" "/Install /products:WindowsAzureEmulator_Only.2.4 /accepteula" -Wait
}

$detectKey = "HKLM:\SOFTWARE\Microsoft\Windows Azure Storage Emulator"
if (${env:ADX64Platform}){
    $detectKey = "HKLM:\SOFTWARE\Wow6432Node\Microsoft\Windows Azure Storage Emulator"
}
$producteVersion = Get-RegistryKeyValues $detectKey "FullVersion"
if (!($producteVersion.StartsWith("3.3"))) {
    Write-Host installing Azure Storage Emulator
    Start-Process "$WebPi" "/Install /products:WindowsAzureStorageEmulator.3.3 /accepteula" -Wait
}

try {
  git.exe| Out-Null
}
catch [System.Management.Automation.CommandNotFoundException] {
    if (Test-Path "$env:ADXSDKProgramFiles\Git\bin") {
        Write-Host Adding Git installation folder to the PATH environment variable, needed for some unit tests.
        $env:path = $env:path + ";$env:ADXSDKProgramFiles\Git\bin"
    }
}

#The detecting logic for django is not decent, but the best we can do so far.    
if (!(Test-Path "$env:SystemDrive\Python27")) {
    Write-Host "download Python, Pip and Django to $tempFileShare"
    $tempFileShare = $env:temp
    $client = New-Object System.Net.WebClient
    $client.DownloadFile("https://www.python.org/ftp/python/2.7.5/python-2.7.5.msi", "$tempFileShare\python-2.7.5.msi")
    $client.DownloadFile("https://raw.github.com/pypa/pip/master/contrib/get-pip.py", "$tempFileShare\get-pip.py");        
    Write-Host "Install..."
    Start-Process msiexec.exe "/i $tempFileShare\python-2.7.5.msi /passive" -Wait
    Start-Process "$env:SystemDrive\Python27\python.exe" "$tempFileShare\get-pip.py" -Wait
    Start-Process "$env:SystemDrive\Python27\scripts\pip.exe" "install Django==1.5" -Wait
    Remove-Item "$tempFileShare\python-2.7.5.msi"
    Remove-Item "$tempFileShare\get-pip.py"
}
