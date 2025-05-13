# AtmWeb

## Running the Frontend Project

The Angular ATM web application relies on Angular Material and Tailwind CSS 4.
Due to a dependency conflict with Tailwind CSS the `--force` switch is needed
when installing the dependency packages.
```
npm install --force
```
To run the project, run the following command:
```
npm run start
```
and navigate to `https://localhost:4200/` and verify the application runs without any issues.
If there is no data was loaded, it means the hard-coded API endpoint needs to be updated.
Locate the file `http.service.ts` and change the `localhost:7132/Accounts` endpoint to match the current API endpoint.

## Running the Backend Project

The restful API project was built using .NET Core 8 and PostgreSQL.
To setup the project, complete the following steps:

- Step 1: Install and run the Docker Desktop and run a PostgreSQL container. On Windows,
the Windows Terminal may not recognize the trailing `\` backslash, so
make the below command a one-liner if facing such an issue.

```
docker run --name postgresql \
-e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres \
-p 5432:5432 \
-v postgres-volume:/var/lib/postgresql/data \
--restart=unless-stopped \
-d postgres
```

- Step 2: Optionally, create a environment variable for the PostgreSQL connection string.
The hard-coded connection string in the project can be removed if its environment variable is setup. The PostgreSQL connection string was intentionally hard-coded for the convenience. It can be removed from the `appsettings.json` file of the `ATMApp.API` project and also from the `DatabaseContext.cs` file of the `ATMApp.Data` project.

```
ATM_ConnectionStrings__SqlConnectionString=Host=localhost;Port=5432;Database=atmapp_dev;Username=postgres;Password=postgres
```

- Step 3: Create database objects by running .NET EF Core migrations. From a Terminal, assuming the .NET Core SDK and Entity Framework Core tools were isntalled, change directory into the `ATMApp.Data` directory and issue the following command.

```
dotnet ef database update
```

If the command was not recognized then .NET Core 8 and Entity Framework Core tools need to be installed.
To install the EF Core tools, run the following command:

```
dotnet tool install --global dotnet-ef
```

Verify that the `atmapp_dev` database was created, a few tables were created, and a few recorded were seeded for the project.

- Step 4: Run the API project. The API project can be run via command line or via the Visual Studio 2022.
From a Terminal, in the `ATMApp` directory, issue the following command:

```
dotnet run ATMApp.sln --project ATMApp.API
```





