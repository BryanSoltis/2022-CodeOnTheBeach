# Lab 4 - Call an Azure Function and Post to Slack

## Overview
This lab will create an Azure Logic App that processed text using an Azure Function. After processing, the workflow will post a message to a Slack chnnel. 

## Details
  - Level: **Advanced**
  - Audience: **Developer**
  - Interaction with Azure Function
  - Requires building/deploying an Azure Function (demo source code provided in lab)
  - Data manipulation

## Connectors/Actions
- HTTP Requests
- Variables
- Azure Functions
- Slack
- Dynamic Content

## Instructions
1. Create an Azure Function
	- Select **Create a Resource** (left navigation menu)
	- Search for **Function App**
	- Select **Function App**
	- Select Function App
		- Subscription
			- Choose desired subscription
		- Resource Group
			- Choose desired resource group / create new
		- Name
			- Enter unique name
		- Publish
			- **Code**
		- Runtime stack
			- **.NET**
		- Version
			- **6**
		- Region
			- Choose desired region
		- Operating System
			- **Windows**
		- Plan type
			- **Consumption**
    - Select **Monitoring** from the top navigation
    - Enable Application Insights
      - **No**   
		- Select **Review & Create**
		- Select **Create**

2. Deploy the demo code
  - Download the **Lab4DemoFunctions** project
  - Publish to Azure Function using Visual Studio
  - Confirm DataManiulator400Function is displayed
  - ![image](https://user-images.githubusercontent.com/13591910/177861571-235dac94-09d7-4619-933e-2fec851285dd.png)

3. Create an Azure Logic App
	- Select **Create a Resource** (left navigation menu)
	- Search for **Logic**
	- Select **Logic App**
	- Select Create Logic App
		- Subscription
			- Choose desired subscription
		- Resource Group
			- Choose desired resource group / create new
		- Name
			- Enter unique name
		- Publish
			- **Workflow**
		- Region
			- Choose desired region
		- Enable log analytics
			- **Disabled**
		- Plan type
			- **Consumption**
		- Zone redundancy
			- **Disabled**
		- Select **Review & Create**
		- Select **Create**
	- Select **Go to resource**
	- Select the **When a HTTP request is received** trigger 
	- Select **Use a sample payload to generate schema**
    	```json
      		{
			  "originaltext":"something cool"
		}
    	```
	- Select **+ New step**
	- Search for **Azure Functions**
	- Select **Azure Functions** connector
	- Select your Azure Function
	- Select **DataManipulator400Funcion**
		- Request Body
			- Dynamic content
				- **originaltext** (under the **When a HTTP request is received** action)
	- Sekect **+ New step**
	- Search for **Slack**
	- Select **Slack** connector
	- Select **Post a message (V2)** action
		- Workspace URL
			- **2022codeonthe-iww8795* 
		- Channel Name
			- **general**
		- Message Text
			- "The following text has been processed: **
			- Dynamic content
			- 	- **Body** (under **DataManiuplator4000Function** action)

4. Review
	- Completed workflow
	![image](https://user-images.githubusercontent.com/13591910/177865459-508f3b15-82a4-4140-a1de-b4d090338656.png)


5. Testing
	- Select **Overview** (left navigation menu)
	- Select **Run trigger**
	- Select **Run with payload**
	- Entewr the following values
		- Body
		```json
      		{
			  "originaltext":"!SEITRAPRETFA TSEB EHT SAH HCAEb EHT NO EDOc"
		}
		```
	- On the **Overview** tab, click **Refresh**
	- Under **Runs history**, select the top record
	- Review the run details
	- Expand the **DataManipulator400Function** action
		- Reviewed the **OUTPUTS** data
		- ![image](https://user-images.githubusercontent.com/13591910/177865787-a845ab34-96c1-4bee-8844-2213935ba7e4.png)
	- Open Slack to the **2022CodeOnTheBeachAzureLogicAppWorkShop** team
	- View the **demo** channel
		- Confirm the message is displayed
		- ![image](https://user-images.githubusercontent.com/13591910/177866494-e711ed83-54b5-49f3-b750-2dd379295e7b.png)


