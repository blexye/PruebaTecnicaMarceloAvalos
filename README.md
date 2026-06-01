# Prueba Técnica - API REST .NET

## Versión de .NET

.NET 10.0

---

## Cómo correr el proyecto

### Clonar el repositorio

```bash
git clone https://github.com/blexye/PruebaTecnicaMarceloAvalos.git
```

### Ejecutar el proyecto

1. Abrir la solución en Visual Studio.
2. Ejecutar el proyecto.

### Acceder a Swagger

Una vez iniciada la aplicación, agregar `/swagger` al final de la URL.

Ejemplo:

```text
https://localhost:7230/swagger
```

---

## Crear la base de datos

### Crear la migración (si no existe)

```bash
dotnet ef migrations add InitialCreate
```

### Crear la base de datos y aplicar las migraciones

```bash
dotnet ef database update
```

---

## Headers requeridos

```http
Content-Type: application/json
X-API-KEY: contra123
```

## Funcionalidades implementadas

- CRUD de Usuarios
- CRUD de Direcciones
- Módulo de conversión de divisas
- Seguridad por API Key
- EntityFramework con SQLite
- Validaciones de las peticiones con FluentValidation
- Patrón CQRS
- Contraseñas hasheadas con BCrypt
- Swagger

---

## Funcionalidades no implementadas

- Filtrado de usuarios por `IsActive = true`
- Agregado automático del header `X-API-KEY` en Swagger
- Mejoras y optimizaciones adicionales de la API
