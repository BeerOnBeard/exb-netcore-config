FROM microsoft/dotnet:2-sdk AS builder
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -o /publish

FROM microsoft/dotnet:2-runtime
WORKDIR /app
COPY --from=builder /publish /app
ENV BEERONBEARD_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "console.dll"]
