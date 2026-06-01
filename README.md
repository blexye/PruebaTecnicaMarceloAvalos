# PruebaTecnicaMarceloAvalos

Versión .NET: 10.0

- Cómo correr el proyecto
1. Clonar repositorio:
git clone: https://github.com/blexye/PruebaTecnicaMarceloAvalos.git

- Crear base de datos (si no está creada aún):
1. Se crea la migración si no existe:
dotnet ef migrations add InitialCreate

2. Se crea la base de datos y se aplican las migraciones:
dotnet ef database update

- API KEY de prueba:
Content-Type: application/json
X-API-KEY: contra123

- Lo que no se implementó:
1. Filtrado de usuario por IsActive = true
2. Mejor optimización de la API en general
