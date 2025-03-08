# Construction de l'application avec l'image .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copie du fichier .csproj et restauration des dépendances
COPY *.csproj ./  
RUN dotnet restore  

# Copie du reste du code source dans l'image
COPY . ./  
RUN dotnet publish -c Release -o /out  

# Création de l'image de production (runtime) à partir de l'image .NET ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copie des fichiers publiés de l'étape précédente
COPY --from=build /out .  
# Exposer les ports nécessaires
EXPOSE 5000  
EXPOSE 5001  

# Définition du point d'entrée du conteneur
ENTRYPOINT ["dotnet", "action_elle_apk.dll"] 

# configuration de l'URL d'écoute dans Docker
ENV ASPNETCORE_URLS="http://+:5000"  
