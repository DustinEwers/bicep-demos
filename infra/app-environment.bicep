param systemName string 
param environment string // example: d = dev, qa = qa, p = prod
param sequence string = '01'

param location string
param appSkuName string
param appSkuTier string

param dbSkuName string
param dbSkuTier string

var appServicePlanName = 'appservice-${systemName}-${environment}${sequence}'
var appInsightsName = 'insights-${systemName}-${environment}${sequence}'
var logWorkspaceName = 'workspace-${systemName}-${environment}${sequence}'
var serviceAppName = '${systemName}-service-${environment}${sequence}'

var dbServerName = '${systemName}-dbServer-${environment}${sequence}'
var dbName = '${systemName}-dbname-${environment}${sequence}'

module appInsights './app-insights.bicep' = {
  name: 'insights'
  params: {
    appInsightsName: appInsightsName
    location: location
    logWorkspaceName: logWorkspaceName
  }
}

module database './database.bicep' = {
  name: 'database'
  params: {
    dbName: dbName
    dbServerName: dbServerName
    dbSkuName: dbSkuName
    dbSkuTier: dbSkuTier
    location: location
  }
}

resource serverfarms_AppService 'Microsoft.Web/serverfarms@2020-09-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: appSkuName
    tier: appSkuTier
  }
  kind: 'linux'
  properties: {
    isSpot: false
    reserved: true
    hyperV: false
  }
}

resource sites_Service 'Microsoft.Web/sites@2021-03-01' = {
  name: serviceAppName
  kind: 'app,linux'
  location: location
  dependsOn: [
    appInsights
  ]
  properties: {
    enabled: true
    serverFarmId: serverfarms_AppService.id
    siteConfig: {
      netFrameworkVersion: 'v5.0'
      linuxFxVersion: 'DOTNETCORE|5.0'
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.outputs.InstrumentationKey
        }
        {
            name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
            value: appInsights.outputs.ConnectionString
        }
      ]
    }
  }
}
