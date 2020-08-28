# The Car Hub

Student project for OpenClassrooms .NET Back-End Developer Path, built with .NET Core MVC 3.0. The application is seeded with an admin user with the login `admin@admin.com` and password `P@ssword123`. 

Note that the app is configured to use a local SQL Express server in development. Depending on your development environment, you may need to change the connection strings (defined in ``appsettings.json``).

Current settings:
```json
"ConnectionStrings": {
    "AppReferentialLocal": "Server=.\\SQLEXPRESS; Database=P5Ref;Trusted_Connection=True;MultipleActiveResultSets=true",
    "AppIdentityLocal": "Server=.\\SQLEXPRESS; Database=P5Identity;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```

