# PruebaTecnicaMarceloAvalos

Versión .NET: 10.0

- Cómo correr el proyecto
1. Clonar repositorio:
git clone: https://github.com/blexye/PruebaTecnicaMarceloAvalos.git

2. Ejecutar el proyecto:
  Importar y correr el proyecto desde Visual Studio
  Se despliega el navegador
  Para las pruebas con swagger, insertar "/swagger" al final del link
  Ejemplo: https://localhost:7230/ -> https://localhost:7230/swagger

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
2. Agregado del campo X-APY-KEY en Swagger
3. Mejor optimización de la API en general
