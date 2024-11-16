# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
#EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AudioProcessing.API/AudioProcessing.API.csproj", "AudioProcessing.API/"]
COPY ["AudioProcessing.Aplication/AudioProcessing.Aplication.csproj", "AudioProcessing.Aplication/"]
COPY ["ChatGPT_APP.Infrastructure/ChatGPT_APP.Infrastructure.csproj", "ChatGPT_APP.Infrastructure/"]
COPY ["ChatGPT_APP.Services/ChatGPT_APP.Aplication.csproj", "ChatGPT_APP.Services/"]
COPY ["ChatGPT_APP.Models/ChatGPT_APP.Domain.csproj", "ChatGPT_APP.Models/"]
COPY ["AudioProcessing.Infrastructure/AudioProcessing.Infrastructure.csproj", "AudioProcessing.Infrastructure/"]
RUN dotnet restore "./AudioProcessing.API/AudioProcessing.API.csproj"
COPY . .
WORKDIR "/src/AudioProcessing.API"
RUN dotnet build "./AudioProcessing.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AudioProcessing.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AudioProcessing.API.dll"]