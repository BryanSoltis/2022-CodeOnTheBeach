# Lab 5 - Automate GitHub Pull Request Merging with Azure Logic Apps

## Overview
This lab demosntrates how to automate the merging of a GitHub PulL Reuwest using a workflow.

## Details
  - Level: **Moderate**
  - Audience: **General, Developer**
  - Interaction with GitHub
  - Requires GitHub project

## Connectors/Action
- GitHub
- Control

## Instructions
1. Create a blank GitHub Repo
    - Create a new GitHub Repo within your GitHub account
    - Ensure the repo is marked as **Public**
    - Check the **Add a README file** option

2. Create an Azure Logic App
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

3. Deisgn Azure Logic App
    - Select **Blank Logic App** (under **Templates**)
    - Select **Parameters** (in the top navigation menu)
    - Select **Add Parameter**
    - Enter the following values
      - Name
        - GitHubOwner
      - Type
        - **string**
      - Default Value
        - **[Your GitHub User ID]**
    - Select **Add Parameter**
    - Enter the following values
      - Name
        - GitHubRepo
      - Type
        - **string**
      - Default Value
        - **[Your GitHub Repo Name]**
    - ![image](https://user-images.githubusercontent.com/13591910/178013080-efe4275b-bb44-47f1-b789-a0f90402cf66.png)
    - Search for **GitHub**
    - Select *GitHub** connector
    - Select **When a pull request is created or modified (Vpreview)** action
    - Autheticate to your GitHub account
    - Enter the following values
      - Repository Owner
        - Dynamic content
          - Parameters
            - **GitHubOwner**
      - Repository Name
        - Dynamic content
          - Parameters
            - **GitHubRepo**
    - ![image](https://user-images.githubusercontent.com/13591910/178013551-8be1a654-ff49-4540-ba20-ff4e2908a8dd.png)
    - Select **+ New step**
	  - Search for **Control**
	  - Select **Control** connector
	  - Select **Condition** action
	  - Enter the following values
	    - Choose a value (left side)
	      - Dyanmic content
	        -  **Action Performed** (under the **When a pull request is created or modified** action)
      - is equal to
      - Choose a value (right side)
        - **opened**
      - Select the ellipses and choose **Rename**
			- Enter the following text for the name
			  - **Check PR action**
    - Under the **True** branch, select **Add an action**
    - Search for "Control**
	  - Select **Control** connector
	  - Select **Condition** action
	  - Enter the following values
	    - Choose a value (left side)
	      - Dyanmic content
	        -  **Login" (under the **When a pull request is created or modified** action)
	          - There will be several **login** preoprties. Select the bottom. To confirm you havce the correct one, hover over the selected value and confirm it says **triggerBody()?['pull_request']?['user']?['loing']**
	          - ![image](https://user-images.githubusercontent.com/13591910/178018555-dc207e3d-2ec5-48ff-bbaa-d7ce2d3bf309.png)
      - is equal to
      - Choose a value (right side)
        - Dynamic content
          - Parameters
            - **GitHubOwner**
      - Select the ellipses and choose **Rename**
			- Enter the following text for the name
			  - **Check if PR user is the Repo Owner**
    - Under the **True** branch, select **Add an action**
    - Search for "GitHub**
	  - Select **GitHub** connector
	  - Select **Merge a pull request (Preview)** action
	  - Enter the following values
      - Repository Owner
        - Dynamic content
          - Parameters
            - **GitHubOwner**
      - Repository Name
        - Dynamic content
          - Parameters
            - **GitHubRepo**
      - Pull Number
        - Dynamic content
          - **Pull Request Number** (under the **When a pull request is creaed or modified** action)

4. Review
    - Completed workflow
    - ![image](https://user-images.githubusercontent.com/13591910/178018817-7d9776ce-d41e-4df1-8e74-7c2e16063229.png)
    - In your GitHub account, review the newly created webhook
    - In GitHub, select the **Settings** tab (in the top navigation menu)
    - Select **WEbhooks* (in the left navigation menu)
    - Confirm the webhook is displayed
    - ![image](https://user-images.githubusercontent.com/13591910/178016179-388fafd7-e90d-4602-99ac-fdf564fa435e.png)


5. Testing
    - In your GitHub repo, create a new branch
    - On the **Code** page, change to your new branch
    - Edit the **README** file with any desired text
    - Commit the changes to the branch
    - Select **Pull requests** (in the top navigation menu)
    - Select **New pull request**
    - Select the new branch in the **compare** drop down
    - Select **Create pull request**
    - Select **Create pull request**
    - In you rAzure Logic App, select the **Overview** tab, and the select **Refresh**
	  - Under  **Runs history**, select the 2nd record
	    - Note: The Logic will execute twice, once for the creation of the pull request, and once for the merging. Ensure you select the **oldest** run of the two for full details. 
	  - Review the run details

https://docs.microsoft.com/en-us/connectors/github/#webhookpullrequestresponse
