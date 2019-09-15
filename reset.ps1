# az login
az account set -s 0411d67e-2a15-4793-b0d6-286ebb39573e

$vault_name = "kvcontrivedexmifar"

$secret_list = az keyvault secret list `
    --vault-name $vault_name `
    --query '[].{id:id}'

$parsed_secret_list = $secret_list | ConvertFrom-Json

foreach ($secret_id in $parsed_secret_list) { `
    az keyvault secret delete --id $secret_id.id `
    Write-Output "deleted secret..."
}

az keyvault delete-policy `
    --name $vault_name `
    --object-id 4e1deafe-5064-43bd-94ab-ea799bb19979

$demo_registration = az ad app list `
    --display-name "demo-registration" `
    --query "[0].{ AppId : appId }"

$parsed_demo_registration = $demo_registration | ConvertFrom-Json

az ad app delete --id $parsed_demo_registration.AppId
Write-Output "deleted app registration"

az webapp identity remove `
    --name "contrivedexCoreConfig" `
    --resource-group "appsvc_rg_windows_centralus"

Write-Output "removed managed id"

az webapp deployment source delete `
    --name "contrivedexCoreConfig" `
    --resource-group "appsvc_rg_windows_centralus"

Write-Output "deleted deployment"