# amega-group

This is a simple API where users can view the price of financial instruments and also subscribe via websocket to live price updates. 

The applications consist of two running components, an hosted service for getting the live updates from the service provider and also a Rest API fontend with can be accessed via the swagger page 

The base url for the app can gotten from the ***launchSettings.json***  file or the endpoints below 

 -  Rest API : ***https://localhost:7188***
 - Websocket : ***wss://localhost:7188/api/fx/ws***

> *The developer certificate needs to be trusted to use https*

	

### How to Run
The API key for Tiingo service needs to be inserted into the appsettings.json file under the section:  **The application will fail to start if the settings are missing. A validation of the setting is  been done on startup** 
> *"Tiingo": {
		  &nbsp;&nbsp;&nbsp;&nbsp;"BaseUrl": "https://api.tiingo.com/tiingo/fx/",
		   &nbsp;&nbsp;&nbsp;&nbsp;"ApiKey": "api key here",
		   &nbsp;&nbsp;&nbsp;&nbsp;"WebSocketUrl": "wss://ws.kraken.com"
	}*
	
Execute the following commands 
 - **cd FinancialInstrument.API**
 - **dotnet run** 

## Subscribing for price updates

The websocket endpoint can be accessed on *wss://localhost:7188/api/fx/ws*
### Subscription Request 
Send the following json request to the websocket endpoint
####Request
>{
&nbsp;&nbsp;&nbsp;&nbsp;"event"  :  "subscribe",
&nbsp;&nbsp;&nbsp;&nbsp;"ticker"  :  "XBT/USD"
}

#### Response
>{
&nbsp;&nbsp;&nbsp;&nbsp;"SubscriptionId":  "9f34a264-21d5-4524-b16f-16fa4c5b909d"
}

This will be followed by list of price updates, 

The Tickers currently supported are 
>- EUR/USD,
 >- USD/JPY,
 >- BTC/USD,
