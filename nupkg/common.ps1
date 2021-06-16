# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
    "DestinyCore.Modules"
)

# List of projects
$projects = (
    "DestinyCore.Modules/src/DestinyCore",
    "DestinyCore.Modules/src/DestinyCore.Aop",
    "DestinyCore.Modules/src/DestinyCore.AspNetCore",
    "DestinyCore.Modules/src/DestinyCore.AutoMapper",
    "DestinyCore.Modules/src/DestinyCore.Caching",
    "DestinyCore.Modules/src/DestinyCore.Caching.CSRedis",
    "DestinyCore.Modules/src/DestinyCore.CodeGenerator",
    "DestinyCore.Modules/src/DestinyCore.EntityFrameworkCore",
    "DestinyCore.Modules/src/DestinyCore.FluentValidation",
    "DestinyCore.Modules/src/DestinyCore.MiniProfiler",
    "DestinyCore.Modules/src/DestinyCore.MongoDB",
    "DestinyCore.Modules/src/DestinyCore.Swagger",
    "DestinyCore.Modules/src/DestinyCore.TestBase"
)