# Binding 情報の設定 
## ローカルデバッグ用の設定  

local.setting.json の接続情報を更新する 
- CONNECTION_STORAGE → Azure Storage Accountのアクセスキーから接続文字列をコピペ 
- AZURE_IOT_HUB_CONNECTION_STRING_SERVICE → Azure IoT Hub のアクセスキーの Service ロールの接続文字列をコピペ 

## Azure Function側の設定 
Azure Portal で、”Function App の設定”→”アプリケーション設定の管理”で、”＋新しいアプリ設定”をクリックして、ローカルデバッグ用の設定で追加した二つの変数を追加する。 
- CONNECTION_STORAGE → Azure Storage Accountのアクセスキーから接続文字列をコピペ 
- AZURE_IOT_HUB_CONNECTION_STRING_SERVICE → Azure IoT Hub のアクセスキーの Service ロールの接続文字列をコピペ 
