# Qlik Audition Project

## Implementation Architecture

* Server: REST API Implementation
    - ASP.NET 
    - IIS 10.0
    - .NET Framework 4.6.1
* Database: To store messages submitted.
    - SQL Server Express Edition 14.00.3015.40.v1
    - Database instance set up with AWS RDS
* Client UI: To save and retrieve messages
    - HTML\CSS with JQuery 3.3.1
    - IIS 10.0
    
### Database Architecture
* DB Instance: qlikmessagedb
* Endpoint: qlikmessagedb.cjf458mpgsxw.us-east-1.rds.amazonaws.com
* Table: dbo.messageTable

| Column           | Datatype        | Description                          |
| ---------------- | --------------- | ------------------------------------ |
| ID               | int             | Unique ID                            |
| MessageContent   | nvarchar(MAX)   | message content                      |
| isPalindrome     | bit             | 0 = not palindrome, 1 = palindrome   | 

## Sequence Diagram

![sequence diagram](https://user-images.githubusercontent.com/39939964/44486224-f5ee4e80-a620-11e8-96af-cedefdc78035.jpg)

## Application Deployment

* The application was built on a Windows 10 environment.
* The application was deployed using AWS Elastic Beanstalk

### Prerequisites
* Microsoft Visual Studio 2017
    - Microsoft Visual Studio Build Tools with MSBuild can also be used to build the application
* .NET Framework 4.6.1
* AWS Elastic Beanstalk Command Line Interface (eb CLI) for deploying the application to AWS.

### Building and Deploying the server
To build the REST API application, MSBuild is used to create the deployment package
```
msbuild AuditionProj.csproj /t:Package /p:DeployIisPath="Default Web Site"
```

To set up the Elastic Beanstalk CLI:
1. Navigate to the project folder .\AuditionProj
2. Run the 'eb init' command.
```
eb init AuditionProj --region us-east-1 --platform iis-10.0
```
3. Select the environment to be used with the 'eb use' command.
```
eb use AuditionProj-dev
```
Note: When no environment has been created previously, run `eb create AuditionProj-dev --single` to create an environment with no load balancers.
4. Open the config.yml file located at .\AuditionProj\\.elasticbeanstalk\config.yml and add the following lines
```
deploy:
    artifact: obj/Debug/Package/AuditionProj.zip
```
5. Deploy the application by running `eb deploy`.

### Building and Deploying the Client
To build the Client application, MSBuild is used to create the deployment package
```
msbuild AuditionWeb.csproj /t:Package /p:DeployIisPath="Default Web Site"
```

To set up the Elastic Beanstalk CLI:
1. Navigate to the project folder .\AuditionWeb
2. Run the 'eb init' command.
```
eb init AuditionWeb --region us-east-1 --platform iis-10.0
```
3. Select the environment to be used with the 'eb use' command.
```
eb use AuditionWeb-dev
```
Note: When no environment has been created previously, run `eb create AuditionWeb-dev --single` to create an environment with no load balancers.
4. Open the config.yml file located at .\AuditionWeb\\.elasticbeanstalk\config.yml and add the following lines
```
deploy:
    artifact: obj/Debug/Package/AuditionWeb.zip
```
5. Deploy the application by running `eb deploy`
6. To access the client, go to http://auditionweb-dev.3uvkwj8h7u.us-east-1.elasticbeanstalk.com/

## REST API Documentation

Public DNS: http://auditionproj-dev.us-east-1.elasticbeanstalk.com/

### Get All Messages
* URL: api/Message
* Method: GET
* URL Parameters: None
* Data Parameters: None
* Success Response:
    - Status Code: 200 OK
    - Content-type: application/json; charset=utf-8
    - Sample content:
        ```json
        [{
            "ID": 1,
            "MessageContent": "test",
            "IsPalindrome": false
        },
        {
            "ID": 2,
            "MessageContent": "level",
            "IsPalindrome": true
        }]
        ```
* Sample Call: 
    ```
    curl -X GET -H 'Content-Type: application/json' -i 'http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message'
    ```

### Get a specific Message
* URL: api/Message/ID
* Method: GET
* URL Parameters: ID=[integer]
* Data Parameters: None
* Success Response:
    - Status Code: 200 OK
    - Content-type: application/json; charset=utf-8
    - Sample content:
        ```json
        [{
            "ID": 1,
            "MessageContent": "test",
            "IsPalindrome": false
        }]
        ```
* Sample Call: 
    ```
    curl -X GET -H 'Content-Type: application/json' -i 'http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message/1'
    ```

### Submit a Message
* URL: api/Message
* Method: POST
* URL Parameters: None
* Data Parameters:
    ```json
    {
        "MessageContent": [string]
    }
    ```
* Success Response:
    - Status Code: 204 No Content
    - Content-type: application/json; charset=utf-8
    - Sample content: None
* Sample Call: 
    ```
    curl -X POST -H 'Content-Type: application/json' -i 'http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message' --data '{ "MessageContent": "test" }'
    ```

### Delete a Message
* URL: api/Message/ID
* Method: DELETE
* URL Parameters: ID=[integer]
* Data Parameters: None
* Success Response:
    - Status Code: 204 No Content
    - Content-type: application/json; charset=utf-8
    - Sample content: None
* Sample Call: 
    ```
    curl -X DELETE -H 'Content-Type: application/json' -i 'http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message/1'
    ```

### Update a Message
* URL: api/Message/ID
* Method: PUT
* URL Parameters: ID=[integer]
* Data Parameters:
    ```json
    {
        "MessageContent": [string]
    }
    ```
* Success Response:
    - Status Code: 204 No Content
    - Content-type: application/json; charset=utf-8
    - Sample content: None
* Sample Call: 
    ```
    curl -X PUT -H 'Content-Type: application/json' -i 'http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message/1' --data '{ "MessageContent": "test" }'
    ```

## To-DO
* Deploy application to a docker container
- Requirement: Windows 10 Pro or Enterprise Edition
