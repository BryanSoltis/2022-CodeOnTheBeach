# Lab 3- Deploy a container on a schedule

## Overview
This lab will create an Azure Logic App to create an Azure Containers Instance on a schedule. This lab demonstrates scheduler functionality, the Azure Container Instances connector, and the Control connector. 

## Instructions
1. Create an Azure Logic App
	- Select Create a Resource (left navigation menu)
	- Search for Logic
	- Select Logic App
		- Select Create Logic App
			- Subscription
				- Choose desired subscription
			- Resource Group
				- Choose desired resource group / create new
			- Name
				- Enter unique name
			- Publish
				- Workflow
			- Region
				- Choose desired region
			- Enable log analytics
				- Disabled
			- Plan type
				- Consumption
			- Zone redundancy
				- Disabled
			- Select Review & Create
				- Select Create
	- Select Go to resource
	- Select the Recurrence trigger 
		 - Interval
		 	- 1
		 - Frequency
		 	- MonthMonth
	- Select + New step
	- Search for Initialize
	- Select Variables 
	- Select Initialize variable
		- Name
			- containergroupname
		- Type
			- string
		- Value:
			- Expression
				- concat('codeonthebeachgroup', rand(0,100))
		- Select the ellipses and choose Rename
			- Enter the following text for the name
			- Set container group name
	- Select + New step
	- Search for Initialize
	- Select Variables
	- Select Initialize variable
		- Name
			- containername
		- Type
			- string
		- Value
			- Expression
				- concat('codeonthebeachcontainer', rand(0,100))
		- Select the ellipses and choose Rename
			- Enter the following text for the name
				- Set container name
	- Select + New step
	- Search for Initialize
	- Select Variables
	- Select Initialize variable
		- Name
			- results
		- Type
			- string
		- Select the ellipses and choose Rename
			- Enter the following text for the name
				- Initialize results
	- Select + New step
		 Search for Azure Container Instance
		 Select Azure Container Instance
		 Select Create or update a container group
		 Authenticate to the Azure Subscription
		 Enter the following values:
			 Subscription Id
				 [Your subscription id]
			 Resource Group
				 [Your resource group name]
			 Container Group Name
				 Dynamic Content
					 Variables
						◊ containergroupname
			 Container Group Location
				 eastus
			 Container Name - 1
				 Dynamic Content
					 Variables
						◊ containername
			 Container Image - 1
				 hello-world:latest
			 Container Resource Requests Memory - 1
				 3.5
			 Container Resource Requests CPU - 1
				 1
		 Select Add new parameter (at the bottom)
		 Select ContainerGroup Image Registries
		 Click off the modal
		 Enter the following values:
			 Image Registry Server - 1
				 bsoltiscotbdemocr.azurecr.io
			 Image Registry User - 1
				 bsoltiscotbdemocr
			 Image Registry Password - 1
				 XXA3mdEtBs7D9ZNDWzUsurBbguw+PC=t
	- Select + New Step
		 Search for Delay
		 Select Schedule
		 Select Delay
			 Count
				 30
			 Unit
				 Seconds
	- Select + New step
		 Search for Azure Container Instance
		 Select Azure Container Instance
		 Select Get logs from a container instance
		 Authenticate to the Azure Subscription
		 Enter the following values:
			 Subscription Id
				 [Your subscription id]
			 Resource Group
				 [Your resource group name]
			 Container Group Name
				 Dynamic Content
					 Variables
						◊ containergroupname
			 Container Name
				 Dynamic Content
					 Variables
						◊ containername
	- Select + New step
		 Search for Set variable
		 Select Variables
		 Select Set variable
			 Name
				 Dynamic content
					 Variables
						◊ results
			 Value
				 Dynamic content
					 Get logs from a container instance
						◊ content
			 Select the ellipses and choose Rename
				 Enter the following text for the name
					 Set results variable
	- Select + New step
		 Search for Azure Container Instance
		 Select Azure Container Instance
		 Select Delete a container group
		 Authenticate to the Azure Subscription
		 Enter the following values:
			 Subscription Id
				 [Your subscription id]
			 Resource Group
				 [Your resource group name]
			 Container Group Name
				 Dynamic Content
					 Variables
						◊ containergroupname
	- Testing
		 Select Overview (left navigation menu)
		 Select Run trigger
		 On the Overview tab, click Refresh
		 Under Runs history, select the top record
		 Review the run details
			 Expand the Set container group name action
				 Reviewed the variable value
			 Expand the Create or update a container group action
				 Review the data
			 Expand the Get logs from a container instance action
				 Review the output data
			 Expand the Set results variable action
				 Review the data
			 Expand the True action
				 Review the data
	
Learn More
Deploying an image from Azure Container Registry with Azure Logic Apps - Soltisweb
