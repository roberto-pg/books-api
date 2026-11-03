# Etapa 1: build e publish
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copia csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# copia todo o código e publica
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: imagem final (somente runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# garante que a pasta exista
RUN mkdir -p /app/wwwroot/images

# expõe porta da API
EXPOSE 8080

ENTRYPOINT ["dotnet", "Books.dll"]
