# EF Core commands in PowerShell script

# Dropping the database
Drop-Database -Force

# Adding a migration
Add-Migration InitialCreate

# Updating the database
Update-Database