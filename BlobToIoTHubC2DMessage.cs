using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace embeddedgoerge
{
    public static class BlobToIoTHubC2DMessage
    {
        [FunctionName("BlobToIoTHubC2DMessage")]
        public static async void Run([BlobTrigger("c2d-messages/{name}", Connection = "CONNECTION_STORAGE")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            var iothubConnectionString = System.Environment.GetEnvironmentVariable("AZURE_IOT_HUB_CONNECTION_STRING_SERVICE",EnvironmentVariableTarget.Process);

            if(name.EndsWith(".json")){
                var receivedBlobNames = name.Split('-');
                
                if (receivedBlobNames.Length>0){
                    var targetDevice = receivedBlobNames[0];
                    for(int i=1;i<receivedBlobNames.Length-1;i++){
                        targetDevice+="-";
                        targetDevice+=receivedBlobNames[i];
                    }
                    var serviceClient =  Microsoft.Azure.Devices.ServiceClient.CreateFromConnectionString(iothubConnectionString);
                    await serviceClient.OpenAsync();

                    var methodInvocation = new Microsoft.Azure.Devices.CloudToDeviceMethod("SoundAlarm");
                    var reader = new System.IO.StreamReader(myBlob);
                    var jsonMessage = reader.ReadToEnd();
                    methodInvocation.SetPayloadJson(jsonMessage);
//                    var c2dMessage = new Microsoft.Azure.Devices.Message(myBlob);
//                    log.LogInformation($"Send received via blob to {targetDevice}");
                    log.LogInformation($"Invoke method via blob to {targetDevice}");
                    var invocationResult = await serviceClient.InvokeDeviceMethodAsync(targetDevice, methodInvocation);
                    var invokedStatus = invocationResult.Status;
                    var invokedPayload = invocationResult.GetPayloadAsJson();
                    log.LogInformation($"Invocation Result - {invokedStatus}:{invokedPayload}");
//                    await serviceClient.SendAsync(targetDevice,c2dMessage);
                    await serviceClient.CloseAsync();
                }
            }
        }
    }
}
