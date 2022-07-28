# Lab 3- Deploy a container on a schedule

## Overview
This lab will create an Azure Logic App to create an Azure Containers Instance on a schedule. This lab demonstrates scheduler functionality, the Azure Container Instances connector, and the Control connector. 

## Details
  - Level: **Moderate**
  - Audience: **Developer, Infrastructure**
  - Requires deploying Azure Container Instances
  - Requires an Azure Container Registry (See instructions in [Learn more](#learn-more) links below)
  - REquires an image in your Azure Container Registry (See instructions in [Learn more](#learn-more) links below)

## Connectors/Actions
- Schedule
- Variables
- Azure Container Instances
- Dynamic Content
- Expressions

## Instructions
1. Create an Azure Logic App
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
	- Select the **Recurrence** trigger 
		 - Interval
		 	- **1**
		 - Frequency
		 	- **Month**
	- Select **+ New step**
	- Search for **Initialize**
	- Select **Variables** connector
	- Select **Initialize variable** action
		- Name
			- **containergroupname**
		- Type
			- **string**
		- Value:
			- Expression
				- **concat('codeonthebeachgroup', rand(0,100))**
		- Select the ellipses and choose **Rename**
			- Enter the following text for the name
			- **Set container group name**
	- Select **+ New step**
	- Search for **Variables**
	- Select **Variables** connector
	- Select **Initialize variable** action
		- Name
			- **containername**
		- Type
			- **string**
		- Value
			- Expression
				- **concat('codeonthebeachcontainer', rand(0,100))**
		- Select the ellipses and choose **Rename**
			- Enter the following text for the name
				- **Set container name**
	- Select **+ New step**
	- Search for **Variables**
	- Select **Variables** connector
	- Select **Initialize variable** action
		- Name
			- **results**
		- Type
			- **string**
		- Select the ellipses and choose **Rename**
			- Enter the following text for the name
				- **Initialize results**
	- Select **+ New step**
	- Search for **Azure Container Instance**
	- Select **Azure Container Instance** connector
	- Select **Create or update a container group** action
	- Authenticate to the Azure Subscription
	- Enter the following values:
		- Subscription Id
			- **[Your subscription id]**
		- Resource Group
			- **[Your resource group name]**
		- Container Group Name
			- Dynamic Content
				- Variables
					- **containergroupname**
		- Container Group Location
			- **eastus**
		- Container Name - 1
			- Dynamic Content
				- Variables
					- **containername**
		- Container Image - 1
			- **hello-world:latest**
		- Container Resource Requests Memory - 1
			- **3.5**
		- Container Resource Requests CPU - 1
			- **1**
		- ![image](https://user-images.githubusercontent.com/13591910/177854356-f39dc843-65e9-4732-84e9-c6fdde4f0fb7.png)
		- Select **Add new parameter** (at the bottom)
		- Select **ContainerGroup Image Registries**
		- ![image](https://user-images.githubusercontent.com/13591910/177854561-bf0b5d6d-f292-45ac-b678-cd0f553bf7be.png)
		- Click off the modal
		- Enter the following values:
			- Image Registry Server - 1
				- **[Your Azure Container Registry Name].azurecr.io**
			- Image Registry User - 1
				- **[Your Azure Container Registry Admin Login]**
			- Image Registry Password - 1
				- **[Your Azure Container Registry Access Key]**
			- ![image](https://user-images.githubusercontent.com/13591910/177855306-02abd202-6cfe-4961-8cfa-4eeb8bc447bc.png)
	- Select **+ New Step**
	- Search for **Schedule**
	- Select **Schedule** connector
	- Select **Delay" action
		- Count
			- **30**
		- Unit
			- **Seconds**
	- Select **+ New step**
	- Search for **Azure Container Instance**
	- Select **Azure Container Instance** connector
	- Select **Get logs from a container instance** action
	- Authenticate to the Azure Subscription
	- Enter the following values:
		- Subscription Id
			- **[Your subscription id]**
		- Resource Group
			- **[Your resource group name]**
		- Container Group Name
			- Dynamic Content
				- Variables
					- **containergroupname**
		- Container Name
			- Dynamic Content
				- Variables
					- **containername**
	- Select **+ New step**
	- Search for **Variables**
	- Select **Variables** connector
	- Select **Set variable** action
		- Name
			- Dynamic content
				- Variables
					- **results**
		- Value
			- Dynamic content
				- **content** (under the **Get logs from a container instance** action)					
		- Select the ellipses and choose **Rename**
		- Enter the following text for the name
			- **Set results variable**
	- Select **+ New step**
	- Search for **Azure Container Instance**
	- Select **Azure Container Instance** connector
	- Select **Delete a container group** action
	- Authenticate to the Azure Subscription
	- Enter the following values:
		- Subscription Id
			- **[Your subscription id]**
		- Resource Group
			- **[Your resource group name]**
		- Container Group Name
			- Dynamic Content
				- Variables
					- **containergroupname**

4. Review
	- Completed workflow
	![image](https://user-images.githubusercontent.com/13591910/177855569-748497b4-5cec-48d1-a8fd-0d19dde8f411.png)


5. Testing
	- Select **Overview** (left navigation menu)
	- Select **Run trigger**
	- On the **Overview** tab, click **Refresh**
	- Under **Runs history**, select the top record
	- Review the run details
	- Expand the **Set container group name** action
		- Reviewed the variable value
		- ![image](https://user-images.githubusercontent.com/13591910/177855687-2e3400af-4f04-4a56-89ad-c926138bde5b.png)
	- Expand the **Create or update a container group** action
		- Review the data
	- Expand the **Get logs from a container instance** action
		- Review the output data
		![image](https://user-images.githubusercontent.com/13591910/177855825-c54a1ad1-1efe-443e-b12e-7e79ff8c5343.png)
	- Expand the **Set results** variable action
		- Review the data
	
## Learn More

[Quickstart: Create an Azure container registry using the Azure portal](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-portal?tabs=azure-cli)

[Push your first image to your Azure container registry using the Docker CLI](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-docker-cli?tabs=azure-cli)

[Deploying an image from Azure Container Registry with Azure Logic Apps](https://soltisweb.com/blog/detail/2021-09-01-deployinganimagefromazurecontainerregistrytoazurelogicapps)

[ACI Logic Apps Integration](https://github.com/Azure-Samples/aci-logicapps-integration)
