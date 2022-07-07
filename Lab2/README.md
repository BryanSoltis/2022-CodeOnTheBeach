# Lab 2 - Convert Azure Blob Storage JSON file to CSV

## Overview
This lab will create an Azure Logic App to process JSON files uploaded to an Azure Blob Storage account. The workflow will convert the JSON to CSV and save it as a new file within Azure Blob Storage. 

## Insutrctions
1. Create an Azure Storage Account
	- Select **Cate a Resource** (left navigation menu)
	- Search for **Storage**
	- Select **Storage Account**
	- Select **Create**
		- Subscription
			- Choose desired subscription
		- Resource Group
			- Choose desired resource group / create new
		- Storage account name
			- Enter a unique name
		- Region
			- Choose desired region
		- Performance
			- **Standard**
		- Redundancy
			- **Locally-redundant storage (LRS)**
		- Select **Review & Create**
		- Select **Create**
	- Select **Go to resource**
	- Select **Containers** (left navigation menu)
	- Select **+ Container** (top navigation menu)
		- Name
			- **files**
		- Public access level
			- **Private (no anonymous access)**
	- Select **+ Container** (top navigation menu)
		- Name
			- **processedfiles**
		- Public access level
			- **Private (no anonymous access)**
	- ![image](https://user-images.githubusercontent.com/13591910/177843837-19b5d465-0d38-49d0-9f00-b3228ab4d524.png)
	- Select **Access keys** (left navigation menu)
	- Select **Show keys** (top navigation menu)
	- Copy the following values (for later use)
		- **Storage account name**
		- **Key1 / Key**
	- ![image](https://user-images.githubusercontent.com/13591910/177843985-1a965c69-27b3-46bc-aae2-c45b5f4f53c9.png)

			
2. Create an Azure Logic App
	- In a new tab, Select **Create a Resource** (left navigation menu)
	- Search for **Logic**
	- Select **Logic App**
	- Select **Create Logic App**
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

3. Design workflow
	- Select **Blank Logic App** (listed under Templates)
	- Search for **Azure Blob Storage**
	- Select **Azure Blob Storage**
	- Select **When a blob is added or modified**
	- Enter the following values
		- Connection name
			- **COTB connection**
		- Authentication type
			- **Access key**
		- Azure Storage Account name
			- **[YOUR COPIED AZURE STORAGE ACCOUNT NAME]**
		- Azure Storage Account Access Key
			- **[YOUR COPIED AZURE STORAGE ACCOUNT ACCESS KEY]**
	- Click **Create**
	- Enter the following values
		- Storage account name
			- **Use connection settings ([your storage account name])**
		- Container
			- **files**
	- Click **+ New** step
	- Search for **Azure Blob Storage**
	- Select **Azure Blob Storage**
	- Select **Get blob content (V2)**
	- Enter the following values
		- Storage account name
			- **Use connection settings ([your storage account name])**
		- Blob
			- Dynamic content
				- **List of Files Path** (under When a blob is added or modified… action)
	- Click **+ New step**
	- Search for **Data Operations**
	- Select **Data Operations**
	- Select **Parse JSON**
	- Enter the following values
		- Content
			- Select **Expressions*
			- Select **json**
			- Click Dynamic Content
				- **File Content** (under the Get blob content V2 action)
				![image](https://user-images.githubusercontent.com/13591910/177842718-1c86ceb4-e8d3-4533-bd8a-d20b7834281b.png)

		- Schema
		 ```json
			{
				"items": {
				        "properties": {
				            "firstname": {
				                "type": "string"
				            },
				            "lastname": {
				                "type": "string"
				            }
				        },
				        "required": [
				            "firstname",
				            "lastname"
				        ],
				        "type": "object"
				    },
				"type": "array"
			}
		 ```
	- Click **+ New step**
	- Search for **Data Operations**
	- Select **Data Operations
	- Select **Create CSV table**
	- Enter the following values:
		- From
			- Dynamic content
			- **Body** (under Parse JSON action)
	- Click **+ New step**
	- Search for **Azure Blob Storage**
	- Select **Azure Blob Storage**
	- Select **Create blob (V2)**
	- Enter the following values
		- Storage account name
			- **Use connection settings ([your storage account name])**
		- Folder path
			- **processed**
		- Blob name
			- Expression
				- **utcNow()**
			- **.csv**
			- ![image](https://user-images.githubusercontent.com/13591910/177842824-c0462af0-13fa-4099-9092-6aa504d07392.png)
		- Blob content
			- Dynamic content
				- **Output** (under Create CSV table action)
	- Click **+ New step**
	- Search for **Azure Blob Storage**
	- Select **Azure Blob Storage**
	- Select **Delete blob (V2)**
	- Enter the following values
		- Storage account name
			- **Use connection settings ([your storage account name])**
		- Blob
			- Dynamic content
				- **List of Files Path** (under When a blob is added or modified… action)

4.  Review
	- Completed workflow
	- ![image](https://user-images.githubusercontent.com/13591910/177843401-3ae9ec34-bff2-4fdb-aefc-8883c1c700de.png)


5. Testing
	- Download the demo file from the following link
		- [Lab2DemoFile](lab2demofile.json)
	- Select your Azure Storage Account
	- Select **Containers** (left navigation menu)
	- Select **files**
	- Select **Upload**
	- Select the downloaded demo file
	- Select **Upload**
	- In a new tab, select you **Azure Logic App**
	- Select **Overview** (left navigation menu)
	- Select **Run trigger**
	- On the **Overview** tab, click **Refresh**
	- Under **Runs history**, select the top record
	- Review the run details
	- Expand the **Parse JSON** action
		- Review the **Content/Schema** values
	- Expand the **Create CSV table** action
		- Review the **OUTPUTS** data
	- Expand the **Create blob (V2)** action
		- Review the **Blob name**
	- Expand the **Set results variable** action
		- Review the data
	- ![image](https://user-images.githubusercontent.com/13591910/177849986-b26aa603-2aad-4564-9827-0c12fc4f0c04.png)
	- In a new tab, select your **Azure Storage Account**
	- Select **Containers** (left navigation menu)
	- Select **files**
		- Confirm the demo file is deleted
	- Select **Containers** (left navigation menu)
	- Select **processed**
		- Confirm the new file is uploaded
	- Download/view the file to confirm the contents
