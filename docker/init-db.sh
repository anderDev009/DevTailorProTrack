#!/usr/bin/env bash
set -euo pipefail

SQLCMD="/opt/mssql-tools/bin/sqlcmd"
SERVER="sqlserver,1433"
DATABASE="TESTING_GARCIA"
MAX_ATTEMPTS=60

for attempt in $(seq 1 "$MAX_ATTEMPTS"); do
  if "$SQLCMD" -S "$SERVER" -U sa -P "$SA_PASSWORD" -C -b -Q "SELECT 1" >/dev/null 2>&1; then
    break
  fi

  echo "Waiting for SQL Server..."
  sleep 5

  if [ "$attempt" -eq "$MAX_ATTEMPTS" ]; then
    echo "SQL Server was not ready after $((MAX_ATTEMPTS * 5)) seconds."
    exit 1
  fi
done

"$SQLCMD" -S "$SERVER" -U sa -P "$SA_PASSWORD" -C -b -Q "IF DB_ID(N'$DATABASE') IS NULL CREATE DATABASE [$DATABASE];"

if "$SQLCMD" -S "$SERVER" -U sa -P "$SA_PASSWORD" -C -b -d "$DATABASE" -h -1 -W -Q "SET NOCOUNT ON; SELECT CASE WHEN OBJECT_ID(N'dbo.__DockerDatabaseInit', N'U') IS NULL THEN 0 ELSE 1 END;" | grep -q "1"; then
  echo "Database already initialized. Skipping CREATEDB.sql."
  exit 0
fi

echo "Initializing database from CREATEDB.sql..."
"$SQLCMD" -S "$SERVER" -U sa -P "$SA_PASSWORD" -C -b -i /scripts/CREATEDB.sql
"$SQLCMD" -S "$SERVER" -U sa -P "$SA_PASSWORD" -C -b -d "$DATABASE" -Q "IF OBJECT_ID(N'dbo.__DockerDatabaseInit', N'U') IS NULL CREATE TABLE dbo.__DockerDatabaseInit (Id int NOT NULL CONSTRAINT PK___DockerDatabaseInit PRIMARY KEY, AppliedAt datetime2 NOT NULL CONSTRAINT DF___DockerDatabaseInit_AppliedAt DEFAULT SYSUTCDATETIME()); MERGE dbo.__DockerDatabaseInit AS target USING (SELECT 1 AS Id) AS source ON target.Id = source.Id WHEN NOT MATCHED THEN INSERT (Id) VALUES (source.Id);"
