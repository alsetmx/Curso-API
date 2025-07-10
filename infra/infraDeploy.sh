#!bin/bash

#Despliega tus recursos en Azure
az group create --name "infra2" --location "eastus2"

az containerapp env create --name "demo-environment" --resource-group "infra2" --location "eastus2" 

#Crea tu imagen de Docker
cd ../src/DemoApi

docker build -t marcelamq/demoapi:latest .

docker push marcelamq/demoapi:latest

#Despliega tu contenedor en Azure Container Apps
az containerapp up --name "demoapi" --resource-group "infra2" --environment "demo-environment" --image "marcelamq/demoapi:latest" --target-port 8080 --ingress 'external'
