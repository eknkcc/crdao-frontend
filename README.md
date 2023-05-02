# crdao-frontend

This repository only contains frontend project of the CRDAO portal.<br><br>

Repo containing full microservice architecture:<br>
https://github.com/eknkcc/ServicesDAO_OnChain<br><br>

Run the web app using .NET 6<br>
Application is configured to use backend services at "http://18.222.206.248:8896" (CRDAO Api Gateway test environment)<br><br>

## Usage
-	To log in to the DAO, you must first sign up. <br>
-	Press the “Sign Up” button and fill in the information requested.<br>
-	Check the box “I accept the terms and conditions” and click the sign up button.<br>
-	Afterwards, an activation e-mail to sent your e-mail address.<br>
-	In order to receive this activation e-mail, you must enter the valid SMTP information in the required place in the appsettings.json file.<br>
-	If you have entered this SMTP information, you will receive an activation e-mail.<br>
-	Click on the link specified in the activation e-mail. <br>
-	You will be redirected to the application url using your default browser. <br>
-	Once the page appeared, you should see a toaster indicating that the activation was successful.<br>
-	This is required to create a user because the registration process is completed with the activation mail.<br>
-	To activate a user without email registration, the IsActive and KYCStatus columns in the daodb.Users table in the database should be set to 1 manually.<br>

### Dashboard Page
-	On this page, you can see the amount of available reputation, the number of jobs you have created, the amount of auctions and votes you have given. <br>
-	The last comments given to the jobs are displayed on this page. <br>
-	You can follow the stages of the jobs you have created and the jobs you have done. <br>

### All Jobs Page
-	It is the page where all jobs are displayed. <br>
-	You can also add a job yourself from the “Post New Job” button at the top right. (The process of adding a job is explained in detail under the heading “My Jobs”).<br>
-	You can search according to the title of the job from the search button at the top.<br>
-	You can filter according to the status of the jobs from the selectbox at the top.<br>
-	If you click on the job, you can go to the detail page of the job.<br>

### My Jobs Page
-	If you want to add a job, you click on the “Post New Job” button. <br>
-	There are some rules for adding jobs. You have to add jobs by following these rules. <br>
-	After reading the rules, you can follow the steps below to add jobs. <br>
	1- Need to define a title for the job.<br>
	2- Set a price for the job in Euros.<br>
	3- Set a timeframe for the job to be completed in days.<br>
	4- Determine the relevant coding languages and tags.<br>
	5- Add the repository address or the code file where the job will be done.<br>
	6- Need to add a detailed description of the job.<br>
-	After filling this information, you will press the “Submit Job” button to add a job.<br>
-	You will see 2 conditions. If you accept these, you can create a job by saying "continue".<br>
-	After creating the job, you can make edits again if you want.<br>
-	By clicking “Comment”, you can both comment and access previous comments. And you can reply to other comments.<br>
-	It must be approved by the admin in order to perform the transactions related to the created jobs.<br>
-	Otherwise, bids cannot be given for jobs.<br>
-	The label of the jobs not approved by the admin is “Pending”. <br><br>

_If you are an **Associate**, you must pay a fee to be able to job._ <br>
_If you are a **VotingAssociate**, you can directly recruit._ <br>

### Auction Page
-	**Active Internal Auctions:**
	-	Only VotingAssociates can bid.
	-	And you cannot bid on your own affairs.
	-	You can see and approve bids for the job you have created from the "Show Bids" button.
	-	If the job owner approves the bid, that person starts doing the job.
	-	There is a certain time. If no bid is given by the VotingAssociates within this period, this job falls to "Active Public Auctions". <br>
-	**Active Public Auctions:**
	-	Associates can bid for jobs that are not bid by VotingAssociates.<br>


### Approval of Job:
-	After the job owner approves one of the bids, the approved person starts to do the job. <br>
-	After the job is done, the person doing the job should press the “Show Informal Voting” button on the “Job Details” page of the job. In this way, the job to be voted on. <br>
-	Voting process details are also under the “Voting Page” heading. <br>


### Voting Page
-	**Active Informal Votes:**
	-	The job done comes here and is voted here only by VotingAssociates.
	-	The person cannot vote for his/her own job. 
	-	When voting, you must choose one of the two options and enter the reputation amount.
	-	If the votes here exceed the quorum, they can move to the “Active Formal Votes” section.
-	**Active Formal Votes:**
	-	Everyone can vote here.
	-	The job is approved or rejected according to the votes cast.

### Reputation History
-	On this page, you can find all the details about your reputation. <br>

### Payment History
-	On this page, you can find all the details about the payments.


## Develop
All applications can be built and run in their own docker containers with the following command;
```shell
docker build -f "./Dockerfile" -t [IMAGE_NAME]:dev "[SolutionPath]\ServicesDAO"
docker run -p [application access port]:80 -name:[NAME]  [IMAGE_NAME]:dev --
```

For the application to work, all containers should be created in the same network also database, rabbitmq and api endpoints can be redefined.<br>

After .NET 6 and .NET SDK 6 are installed on the environment, all applications can be built individually by running the commands below under the project solution folder;

```shell
 dotnet build ./DAO_WebPortal/DAO_WebPortal.csproj
```

### Projects
WebPortal:<br>
Provides application and user interactions with user interface.<br>
- Registering to the application
- Job Posting
- Voting
- Forum

Helpers Library:<br>
Contains application models, constants, application wide generic methods(MySQL connection, RabbitMQ subscription, Json Serializing, Encryption etc...) of the application.
