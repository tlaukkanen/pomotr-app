FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

ENV SA_PASSWORD ReallyStringDevPassword#007
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

COPY ./entrypoint.sh /

ENTRYPOINT [ "/bin/bash", "entrypoint.sh" ]
CMD [ "/opt/mssql/bin/sqlservr" ]
