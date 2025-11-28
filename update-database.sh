# !/bin/bash
# get the first parameter into a  variable
export MIGRATION_NAME=$1
dotnet ef database update $MIGRATION_NAME --project IMS.Plugins/IMS.Plugins.EFCoreSqlServer --startup-project IMS.WebApp