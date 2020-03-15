sleep 90
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P ReallyStrongDevPassword#007 -d master -i create-database.sql
