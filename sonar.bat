1dotnet sonarscanner begin /k:"project-GOLDCONF" /d:sonar.login="9a1b45da759fa1419037af1d34dfcba77a45b431" /d:sonar.host.url="https://sonar.lumenes.tk"
2dotnet build SONAR_PRUEBA.sln
3dotnet sonarscanner end /d:sonar.login="9a1b45da759fa1419037af1d34dfcba77a45b431"