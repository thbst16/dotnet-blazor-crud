#!/bin/bash

group="beckshome-container-apps-rg"
app="dotnet-blazor-crud"

az containerapp delete -g $group -n $app
# echo
# az group delete --name $group