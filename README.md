# Road Service Status App

A console app to get the status of Road using api provided by TFL. App can run with a user defined command line argument or while running the app without any args.
This app is deisgned to accept multiple Road Ids at a time to check the status.

### Steps Needed Before running app (Prereuisites):
1) Installation of .Net Core SDK.
2) Visual Studio 2022 as the app is created to target .net 6.0
3) Add your TFL api_key , app_key in appsettings.json if needed.

### Steps to Run Application:
1) Clone this git repo and change your directory to TFLRoadStatusApp/TFLRoadStatusApp.
2) Run command
    ```console
    dotnet restore
    ```
3) Now run 
    ```console
      dotnet build
    ```
4) Run application using
      ```console
        dotnet run
      ```
5) App will now ask for you to provide some Road Ids comma separated.
6) If all the Road Ids are found to be valid, app will show you the status of each roads. Otherwise it will show you the information about invalid message.
7) To check the exit status code, one can run the command for Exit Code.

### Step to Run Unit Test:
1) Go to directory TFLRoadStatusApp/TFLRoadStatusApp.Test.
2) Run the command 
      ```console
        dotnet test
      ```

### Assumptions:
1) To avoid mixup of data between output for user and application logs, app is configured to use SeriLog to put logs to a log file. If not needed same can be disabled via appsetting.json file.
2) App can run without giving any arguments while running the application. As it is configured to ask for user to provide Road Ids if not provided in command line argument.


