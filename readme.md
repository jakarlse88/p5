# The Car Hub

[![Build Status](https://dev.azure.com/jakarlse88/The%20Car%20Hub/_apis/build/status/thecarhub%20-%20CI?branchName=master)](https://dev.azure.com/jakarlse88/The%20Car%20Hub/_build/latest?definitionId=1&branchName=master)

Student project for OpenClassrooms .NET Back-End Developer Path, built with .NET Core MVC 3.0. 

To view, either clone this repo or view the [web app on Azure](https://thecarhub.azurewebsites.net/).

Note that the app is configured to use a local SQL Express server in development. Depending on your development environment, you may need to change the connection strings (defined in ``appsettings.json``). *Assessors:* there are additional configuration contained in `secrets.json`, which is included in the project deliverables submitted to OpenClassrooms. These are set as application settings in Azure.

Current settings:
```json
"ConnectionStrings": {
    "AppReferentialLocal": "Server=.\\SQLEXPRESS; Database=P5Ref;Trusted_Connection=True;MultipleActiveResultSets=true",
    "AppIdentityLocal": "Server=.\\SQLEXPRESS; Database=P5Identity;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```

