#!/bin/bash

group="beckshome-container-apps-rg"
location="eastus"
env="dotnet-blazor-env"
app="dotnet-blazor-crud"
image="thbst16/dotnet-blazor-crud:latest"

# az group create --name $group --location $location
# echo
# az containerapp env create -n $env -g $group --location $location
# echo
az containerapp create -n $app -g $group --image $image --environment $env --min-replicas 0
echo
az containerapp ingress enable -n $app -g $group --type external --target-port 0 --transport auto