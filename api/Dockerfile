FROM microsoft/aspnetcore-build:2 AS builder
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -o /publish

FROM microsoft/aspnetcore:2
WORKDIR /app
COPY --from=builder /publish /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "api.dll"]
