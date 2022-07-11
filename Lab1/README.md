# Lab 1 - Basic HTTP Request (HTTP Request)

## Overview
This lab will create an Azure Logic App to handle HTTP requests with JSON payloads. The lab will demonstrate variable creation/assignment, as well as custom responses. 

## Details
- Level: **Beginner**
- Audience: **General**
- Easy to implement in any envinroment
- Basic data handling

## Connectors/Actions
- HTTP Requests
- Variables
- Responses
- Dynamic Content

## Instructions
1. Create an Azure Logic App
	- Select **Create a Resource** (left navigation menu)
	- Search for **Logic**
	- Select **Logic App**
	- Select **Create**

  	 ![image](https://user-images.githubusercontent.com/13591910/177834162-6edadc36-eb19-4d5c-843a-ea778e188a3a.png)

	- Enter Logic App details
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
	
2. Review the designer options
	- Intro video
	- Common triggers
	- Templates

3. Design workflow
	- Select the **When a HTTP request is received** trigger
	- Select **Use a sample payload to generate schema**

    ```json
      {
			  "name":"Space Ghost",
			  "origin":"Earth",
			  "alive":true
		}
    ```

	- Select **+ New step**
	- Search for **Variables**
	- Select **Variables** connector
	- Select **Initialize variable** action
		- Name:
		  - **isalive**
		- Type: 
		  - **bool**
		- Value:
			- Dynamic content
				- **When a HTTP request is received**
				  - **Alive**
		- Select the ellipses and choose **Rename**
			- Enter the following text for the name
			  - **Set isalive variable**
	- Sekect **+ New step**
	- Search for **Control**
	- Select **Control** connector
	- Select **Condition** action
		- Choose a value
			- Dynamic content
				- Variables
				-   **isalive**
		- **Is equal to**
		- Choose a value
		  - **True**
	- Under True
		- Search for **Request**
		- Select **Request** connector
		- Select **Response** action
		- Under Body
			- Enter the following text
				- **Long live** 
			- Dynamic content
				- When a HTTP request is received
					- **name**

        ![image](https://user-images.githubusercontent.com/13591910/177833732-80cc1344-dc0c-4f01-9134-9394b958fccb.png)

	- Select **Save**
	- Expand **When a HTTP request is received**
	- Note the **HTTP PORT URL** 
		- This is the URL for the workflow with access key.
		- This can be used to call the workflow from other applications/systems (Postman, etc.).

4. Review
	- Completed workflow
  
       ![image](https://user-images.githubusercontent.com/13591910/177835507-80e0a0e3-fc74-4690-ad22-83c8b463fbb7.png)

	- Select **Logic app code view** (left navigation menu)
	- Review the workflow details
	  - Note the syntax for referencing request data / variables

5. Testing
	- Select **Overview** (left navigation menu)
	- Select **Run trigger** (top menu)
	  - Select **Run with payload**
	    - Body
        ```json
        {
			    "name":"Zorak",
			    "origin":"Dokarian",
			    "alive":true
		    }
        ```
	- Review Response body
	- On the **Overview** tab, click  **Refresh**
	- Under  **Runs history**, select the top record
	- Review the run details
	  - Expand the **When a HTTP request is received** action
	    - Select the **Show raw outputs** option
	    - Review the incoming data
	  - Expand the **Set isalive variable** action
	    - Review the data
	  - Expand the **True** action
			- Review the data
		
    ![image](https://user-images.githubusercontent.com/13591910/177837063-19de0575-87f7-42bd-9089-8b843214fccf.png)
