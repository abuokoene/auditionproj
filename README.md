# Qlik Audition Project

Public DNS: http://auditionproj-dev.us-east-1.elasticbeanstalk.com/

## Implementation Architecture

* Server: REST API Implementation
    - ASP.NET 
    - IIS 10.0
    - .NET Framework 4.6.1
* Database: To store messages submitted.
    - SQL Server Express Edition 14.00.3015.40.v1
    - Database instance set up with AWS RDS
    - Endpoint: qlikmessagedb.cjf458mpgsxw.us-east-1.rds.amazonaws.com
* Client UI: To save and retrieve messages
    - HTML\CSS with JQuery 3.3.1

## Sequence Diagram

## Application Deployment

* The application was built on a Windows 10 environment.
* The application was deployed using AWS Elastic Beanstalk

### Prerequisites
* Microsoft Visual Studio 2017
    - Microsoft Visual Studio Build Tools with MSBuild can also be used to build the application
* .NET Framework 4.6.1
* AWS Elastic Beanstalk Command Line Interface (eb CLI)

To build the REST API application, MSBuild is used to create the deployment package
'msbuild AuditionProj.csproj /t:Package /p:DeployIisPath="Default Web Site"'

To set up the Elastic Beanstalk CLI:
1. Navigate to the project folder .\AuditionProj
2. Run the 'eb init' command.
'eb init AuditionProj --region us-east-1 --platform iis-10.0'
3. Select the environment to be used with the 'eb use' command.
'eb use AuditionProj-dev'
    - When no environment has been created previously, run 'eb create AuditionProj-dev --single' to create an environment with no load balancers.
4. Open the config.yml file located at .\AuditionProj\.elasticbeanstalk\config.yml and add the following lines
'deploy:
  artifact: obj/Debug/Package/AuditionProj.zip'
5. Deploy the application by running 'eb deploy'.

## REST API Documentation

### Get All Messages
* URL: api/Message
* Method: GET
* URL Parameters: None
* Data Parameters:
    '{
        "MessageContent": [string]
    }'
* Success Response:
    - Status Code: 200 OK
    - Content-type: application/json; charset=utf-8
    - Sample content:
        '[{
            "ID": 1,
            "MessageContent": "test",
            "IsPalindrome": false
        },
        {
            "ID": 2,
            "MessageContent": "level",
            "IsPalindrome": true
        }]'
* Sample Call: 
    'curl -X GET -H 'Content-Type: application/json' -i 'http://auditionproj-dev.us-east-1.elasticbeanstalk.com/api/Message''