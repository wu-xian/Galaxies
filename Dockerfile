FROM microsoft/dotnet

COPY ./src/ /app/
WORKDIR /app/Lamp
RUN ["ls"]

RUN ["dotnet", "restore"]

RUN ["dotnet", "build"]

EXPOSE 9001

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:9001"]
