# RespiraAMS

## How to start

Run the docker container to start database container

```bash
docker run --name respira-ams -e "POSTGRES_USER=admin" -e "POSTGRES_PASSWORD=12345" -p 5432:5432 -d postgres:18.1-alpine3.22
```

Create database

```bash
cd backend/RespiraAMS
dotnet ef database update --project Infrastructure/RespiraAMS.Infrastructure --startup-project Presentation/RespiraAMS.API

```

Run the project

```bash
dotnet run --project Presentation/RespiraAMS.API
```

Visit the API document at: `http://localhost:5000/scalar`
