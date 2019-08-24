# AzureFunctionBlobTriggerIoTHubC2DMessageByCSSample

c2d-message ブロブコンテナにJSONファイルを格納すると、中身を指定されたIoT Hubに登録されたデバイスに送信する。 
ファイル名は、'device-id'-xyz.json  
xyzの部分はファイル名として利用可能でかつ '-' 以外の英数字で、その前の '-' より前の文字列が Azure IoT Hub に登録されたDeviceIdであること。