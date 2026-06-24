# 1. Aşama: Uygulamayı derlemek için .NET 10 SDK imajını kullanıyoruz
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Sadece proje dosyasını kopyalayıp bağımlılıkları yüklüyoruz (Cache avantajı için)
COPY ["PortfolioAdminPanel.csproj", "./"]
RUN dotnet restore "PortfolioAdminPanel.csproj"

# Tüm kodları kopyalayıp derliyoruz
COPY . .
RUN dotnet build "PortfolioAdminPanel.csproj" -c Release -o /app/build

# 2. Aşama: Uygulamayı yayına hazırlıyoruz (Publish)
FROM build AS publish
RUN dotnet publish "PortfolioAdminPanel.csproj" -c Release -o /app/publish

# 3. Aşama: Sadece çalıştırma ortamını (Runtime) içeren .NET 10 imajı
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Uygulamanın dinleyeceği portu belirtiyoruz
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Konteyner ayağa kalktığında çalışacak komut
ENTRYPOINT ["dotnet", "PortfolioAdminPanel.dll"]