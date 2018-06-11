FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY DemoApi.sln ./
COPY ../k8s_demo/k8s_demo.csproj ../k8s_demo/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/../k8s_demo
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "k8s_demo.dll"]
