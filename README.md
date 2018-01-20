# nservicebus-endpoint-docker
This project configures a NServiceBus Endpoint on docker container based on .NET Core framework.
It was created based on ParticularDocs sample and customized to send and reply 10 times.

## Prerequisite Technologies

* [Git](https://git-scm.com/downloads)
* [.NET Core 2.0 SDK](https://www.microsoft.com/net/download)
* [Docker with compose version 1.13.0+](https://www.docker.com/get-docker)

## Getting Started

```
git clone https://github.com/danielcacheta/nservicebus-endpoint-docker.git
cd nservicebus-endpoint-docker
dotnet build
dotnet publish
docker-compose build
docker-compose up -d
```
After the messages are processed, you should be able to view logs for each container running:
```
docker-compose logs sender
docker-compose logs receiver
```
The following command stops and removes the containers:
```
docker-compose down
```

## Built With

* [NServiceBus](https://docs.particular.net/nservicebus/) - The Workflow Framework
* [NServiceBus Endpoint Configuration](https://docs.particular.net/samples/hosting/docker/) - Detailed steps for Endpoints configuration on Docker containers