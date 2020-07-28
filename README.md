[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=tlaukkanen_pomotr-app&metric=alert_status)](https://sonarcloud.io/dashboard?id=tlaukkanen_pomotr-app)

# Pocket Money Tracker 💰

Application for families to track errands and tasks so that kids could collect some pocket money based on their completed tasks.

# Local Development

Requirements:
 * `docker-compose` - To host SQL Server for development purposes
 * .NET Core 3.x
 * `dotnet-ef` tool for Entity Framework Core database migrations

Setting up the local database:
 1. `> docker-compose up` - to build & start the SQL Server container
 2. `> dotnet ef database update` - to create app's DB schema
 3. Now you can start debugging!

# 3rd Party Attributions

PWA icon made by [Roundicons](https://www.flaticon.com/authors/roundicons) from [www.flaticon.com](https://www.flaticon.com/)