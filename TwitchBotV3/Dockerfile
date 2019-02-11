FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY TwitchBotV3/TwitchBotV3.csproj TwitchBotV3/
RUN dotnet restore TwitchBotV3/TwitchBotV3.csproj
COPY . .
WORKDIR /src/TwitchBotV3
RUN dotnet build TwitchBotV3.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TwitchBotV3.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TwitchBotV3.dll"]
