# !/bin/bash
# get the first parameter into a  variable
export MIGRATION_NAME=$1
# check if the variable is empty
if [ -z "$MIGRATION_NAME" ]; then
  echo "No migration name provided. Provide a migration as a first parameter."
  exit 1
fi
dotnet ef migrations add $MIGRATION_NAME --project IMS.Plugins/IMS.Plugins.EFCoreSqlServer --startup-project IMS.WebApp
