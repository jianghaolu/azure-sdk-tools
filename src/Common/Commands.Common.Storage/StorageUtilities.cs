﻿using Microsoft.WindowsAzure.Commands.Utilities.Common;
using Microsoft.WindowsAzure.Management.Storage;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;

namespace Microsoft.WindowsAzure.Commands.Common.Storage
{
    public class StorageUtilities
    {
        public static CloudStorageAccount GenerateCloudStorageAccount(StorageManagementClient storageClient, string accountName)
        {
            var storageServiceResponse = storageClient.StorageAccounts.Get(accountName);
            var storageKeysResponse = storageClient.StorageAccounts.GetKeys(accountName);

            Uri fileEndpoint = null;

            if (storageServiceResponse.StorageAccount.Properties.Endpoints.Count >= 4)
            {
                fileEndpoint = GeneralUtilities.CreateHttpsEndpoint(storageServiceResponse.StorageAccount.Properties.Endpoints[3].ToString());
            }

            return new CloudStorageAccount(
                new StorageCredentials(storageServiceResponse.StorageAccount.Name, storageKeysResponse.PrimaryKey),
                GeneralUtilities.CreateHttpsEndpoint(storageServiceResponse.StorageAccount.Properties.Endpoints[0].ToString()),
                GeneralUtilities.CreateHttpsEndpoint(storageServiceResponse.StorageAccount.Properties.Endpoints[1].ToString()),
                GeneralUtilities.CreateHttpsEndpoint(storageServiceResponse.StorageAccount.Properties.Endpoints[2].ToString()),
                fileEndpoint);
        }
    }
}
