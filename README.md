# 🏪 Sistema Punto de Venta - Backend API

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?logo=docker)](https://www.docker.com/)
[![Swagger](https://img.shields.io/badge/Swagger-Documented-85EA2D?logo=swagger)](https://swagger.io/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/sql-server)

> Backend RESTful API para un sistema de punto de venta desarrollado con .NET Core 8 y arquitectura por capas. Diseñado para integrarse con un frontend en Angular 14.

---

## 📋 Tabla de Contenidos

- [Descripción](#-descripción)
- [Arquitectura](#-arquitectura)
- [Tecnologías](#-tecnologías)
- [Características](#-características)
- [Prerequisitos](#-prerequisitos)
- [Instalación y Ejecución](#-instalación-y-ejecución)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [API Endpoints](#-api-endpoints)
- [Base de Datos](#-base-de-datos)
- [Docker](#-docker)
- [Swagger/OpenAPI](#-swaggeropenapi)
- [Consideraciones Técnicas](#-consideraciones-técnicas)

---

## 🎯 Descripción

API backend empresarial para gestión de punto de venta que permite administrar productos, categorías, ventas, usuarios y roles. Implementa una arquitectura limpia por capas siguiendo principios SOLID y patrones de diseño enterprise.

**Funcionalidades principales:**
- ✅ Gestión completa de productos y categorías
- ✅ Sistema de ventas con detalle de transacciones
- ✅ Control de inventario en tiempo real
- ✅ Administración de usuarios con sistema de roles
- ✅ Menús dinámicos basados en permisos
- ✅ Generación automática de números de documento

---

## 🏗️ Arquitectura

El proyecto sigue una **arquitectura por capas (N-Tier Architecture)** con separación clara de responsabilidades:

```
┌─────────────────────────────────────────────┐
│         SistemaVenta.API (Presentation)     │
│            Controllers + Middleware          │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│         SistemaVenta.BLL (Business)         │
│          Services + Business Logic           │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│         SistemaVenta.DAL (Data Access)      │
│       Repositories + Entity Framework        │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│              SQL Server Database            │
│                  (DBVENTA)                   │
└─────────────────────────────────────────────┘
```

### Capas del Proyecto

| Capa | Proyecto | Responsabilidad |
|------|----------|-----------------|
| **Presentación** | `SistemaVenta.API` | Controladores REST, configuración de Swagger, CORS y middleware |
| **Lógica de Negocio** | `SistemaVenta.BLL` | Servicios, validaciones y reglas de negocio |
| **Acceso a Datos** | `SistemaVenta.DAL` | Repositorios, Entity Framework, consultas a BD |
| **Modelos** | `SistemaVenta.Model` | Entidades de dominio y modelos de base de datos |
| **DTOs** | `SistemaVenta.DTO` | Objetos de transferencia de datos |
| **IoC** | `SistemaVenta.IOC` | Inyección de dependencias y configuración de servicios |
| **Utilidades** | `SistemaVenta.Utility` | Helpers, extensiones y clases auxiliares |

---

## 🛠️ Tecnologías

### Backend
- **.NET Core 8.0** - Framework principal
- **ASP.NET Core Web API** - Construcción de API REST
- **Entity Framework Core** - ORM para acceso a datos
- **AutoMapper** - Mapeo de objetos
- **LINQ** - Consultas y manipulación de datos

### Base de Datos
- **SQL Server 2022** - Base de datos relacional
- **T-SQL** - Stored procedures y scripts

### DevOps & Tools
- **Docker & Docker Compose** - Containerización
- **Swagger/OpenAPI** - Documentación interactiva
- **Git** - Control de versiones

### Patrones y Principios
- Repository Pattern
- Dependency Injection
- SOLID Principles
- Clean Architecture
- DTO Pattern

---

## ✨ Características

### Funcionales
- 🛒 **CRUD Completo de Productos**: Gestión de inventario con categorías
- 📊 **Sistema de Ventas**: Registro de ventas con detalle de productos
- 👥 **Gestión de Usuarios**: Control de accesos basado en roles
- 🔐 **Sistema de Roles y Permisos**: Autorización granular
- 📋 **Menús Dinámicos**: Interfaz adaptativa según permisos
- 🔢 **Numeración Automática**: Generación de números de documento

### Técnicas
- ✅ API RESTful con convenciones HTTP
- ✅ Respuestas estandarizadas con `Response<T>`
- ✅ Manejo centralizado de excepciones
- ✅ Inyección de dependencias nativa
- ✅ CORS configurado para integración con SPA
- ✅ Swagger UI para documentación interactiva
- ✅ Containerización completa con Docker

---

## 📦 Prerequisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (recomendado)
- SQL Server 2022 (o usar el contenedor incluido)
- [Git](https://git-scm.com/)

---

## 🚀 Instalación y Ejecución

### Opción 1: Docker (Recomendado) 🐳

**La forma más rápida de ejecutar el proyecto completo:**

```bash
# Clonar el repositorio
git clone https://github.com/drusystem/PUNTO-VENTA-API.git
cd PUNTO-VENTA-API

# Levantar todos los servicios (BD + API)
docker-compose up -d

# Verificar que los contenedores estén corriendo
docker-compose ps
```

**¡Listo!** La API estará disponible en:
- 🌐 API: http://localhost:5000
- 📚 Swagger: http://localhost:5000/swagger
- 🗄️ SQL Server: localhost:1433

### Opción 2: Ejecución Local

```bash
# Clonar el repositorio
git clone https://github.com/drusystem/PUNTO-VENTA-API.git
cd PUNTO-VENTA-API

# Restaurar dependencias
dotnet restore

# Configurar la cadena de conexión en appsettings.json
# Ejecutar scripts SQL en orden:
# 1. sql/00-bd.sql
# 2. sql/01-tablas.sql  
# 3. sql/02-datos.sql

# Compilar y ejecutar
cd SistemaVenta.API
dotnet run
```

### Detener los Servicios

```bash
# Detener contenedores
docker-compose down

# Detener y eliminar volúmenes (limpia la BD)
docker-compose down -v
```

---

## 📁 Estructura del Proyecto

```
PUNTO-VENTA-API/
│
├── SistemaVenta.API/              # 🎯 Capa de Presentación
│   ├── Controllers/               # Controladores REST
│   │   ├── ProductoController.cs
│   │   ├── VentaController.cs
│   │   ├── UsuarioController.cs
│   │   └── ...
│   ├── Utilidad/                  # Helpers de API
│   │   └── Response.cs
│   ├── Program.cs                 # Configuración de la aplicación
│   └── appsettings.json          # Configuración de servicios
│
├── SistemaVenta.BLL/              # 💼 Lógica de Negocio
│   ├── Services/
│   │   ├── Interfaces/
│   │   │   └── IProductoService.cs
│   │   └── ProductoService.cs
│   └── ...
│
├── SistemaVenta.DAL/              # 🗄️ Acceso a Datos
│   ├── Repositories/
│   │   ├── Interfaces/
│   │   └── GenericRepository.cs
│   ├── DBContext/
│   │   └── DbventaContext.cs
│   └── ...
│
├── SistemaVenta.Model/            # 📦 Modelos de Dominio
│   ├── Usuario.cs
│   ├── Producto.cs
│   ├── Venta.cs
│   └── ...
│
├── SistemaVenta.DTO/              # 🔄 Data Transfer Objects
│   ├── ProductoDTO.cs
│   ├── VentaDTO.cs
│   └── ...
│
├── SistemaVenta.IOC/              # 🔌 Inyección de Dependencias
│   └── Dependencia.cs
│
├── SistemaVenta.Utility/          # 🛠️ Utilidades Compartidas
│   └── AutoMapperProfile.cs
│
├── sql/                           # 📊 Scripts de Base de Datos
│   ├── 00-bd.sql                 # Creación de BD
│   ├── 01-tablas.sql             # Esquema de tablas
│   └── 02-datos.sql              # Datos iniciales
│
├── Dockerfile                     # 🐳 Configuración Docker API
├── docker-compose.yml            # 🐳 Orquestación de servicios
├── APISistemaVenta.sln           # 📋 Solución de Visual Studio
└── README.md                      # 📖 Este archivo
```

---

## 🔌 API Endpoints

### Productos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| `GET` | `/api/Producto/Lista` | Obtener todos los productos |
| `POST` | `/api/Producto/Guardar` | Crear un nuevo producto |
| `PUT` | `/api/Producto/Editar` | Actualizar un producto |
| `DELETE` | `/api/Producto/Eliminar/{id}` | Eliminar un producto |

### Ejemplo de Respuesta

```json
{
  "status": true,
  "value": [
    {
      "idProducto": 1,
      "nombre": "Laptop HP",
      "idCategoria": 1,
      "stock": 10,
      "precio": 1500.00,
      "esActivo": true
    }
  ],
  "msg": null
}
```

> 📚 **Documentación completa**: Accede a `/swagger` para explorar todos los endpoints disponibles de manera interactiva.

---

## 🗄️ Base de Datos

### Modelo de Datos

El sistema utiliza **SQL Server 2022** con el siguiente esquema:

```sql
DBVENTA
├── Rol                    # Roles del sistema
├── Menu                   # Menús de la aplicación
├── MenuRol               # Relación menús-roles
├── Usuario               # Usuarios del sistema
├── Categoria             # Categorías de productos
├── Producto              # Catálogo de productos
├── NumeroDocumento       # Control de numeración
├── Venta                 # Registro de ventas
└── DetalleVenta          # Líneas de venta
```

### Relaciones Principales

- `Usuario` → `Rol` (Many-to-One)
- `Producto` → `Categoria` (Many-to-One)
- `Venta` → `DetalleVenta` (One-to-Many)
- `DetalleVenta` → `Producto` (Many-to-One)
- `MenuRol` ↔ `Menu` + `Rol` (Many-to-Many)

### Scripts de Inicialización

Los scripts SQL se ejecutan automáticamente al levantar Docker:

1. **00-bd.sql**: Crea la base de datos `DBVENTA`
2. **01-tablas.sql**: Define el esquema completo
3. **02-datos.sql**: Inserta datos iniciales (roles, categorías, usuarios demo)

---

## 🐳 Docker

### Arquitectura de Contenedores

El proyecto está completamente dockerizado con 3 servicios:

```yaml
┌─────────────────────────────────────────────┐
│  api (punto-venta-api)                      │
│  Puerto: 5000:8080                          │
│  Depende de: db-init                        │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│  db-init (mssql-tools)                      │
│  Ejecuta scripts SQL una vez               │
│  Depende de: db                             │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│  db (sqlserver)                             │
│  Puerto: 1433:1433                          │
│  SQL Server 2022 Developer                  │
└─────────────────────────────────────────────┘
```

### Características Docker

- ✅ **Multi-stage build**: Optimización del tamaño de imagen
- ✅ **Health checks**: Validación de disponibilidad de SQL Server
- ✅ **Init containers**: Inicialización automática de BD
- ✅ **Volúmenes**: Los scripts SQL se montan desde `./sql`
- ✅ **Variables de entorno**: Configuración flexible
- ✅ **Orquestación**: Arranque ordenado de dependencias

### Comandos Útiles

```bash
# Ver logs de la API
docker-compose logs -f api

# Ver logs de SQL Server
docker-compose logs -f db

# Reiniciar solo la API
docker-compose restart api

# Reconstruir imagen de la API
docker-compose build api

# Conectarse al contenedor de la API
docker exec -it punto-venta-api bash

# Conectarse a SQL Server
docker exec -it sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P 'yourStrong(!)Password' -C
```

---

## 📚 Swagger/OpenAPI

### Acceso a la Documentación

Una vez que la API esté corriendo, accede a:

🔗 **http://localhost:5000/swagger**

### Características de Swagger

- ✅ Documentación interactiva de todos los endpoints
- ✅ Prueba de APIs directamente desde el navegador
- ✅ Esquemas de request/response
- ✅ Códigos de estado HTTP
- ✅ Modelos de datos con ejemplos

### Ejemplo de Uso

1. Abre Swagger UI en tu navegador
2. Selecciona un endpoint (ej: `GET /api/Producto/Lista`)
3. Click en "Try it out"
4. Click en "Execute"
5. Observa la respuesta y el código HTTP

---

## 💡 Consideraciones Técnicas

### Arquitectura y Diseño

- **Separación de Capas**: Cada capa tiene una responsabilidad clara y específica
- **Inyección de Dependencias**: Implementada nativamente con el contenedor de .NET
- **Repository Pattern**: Abstracción del acceso a datos
- **DTO Pattern**: Desacoplamiento entre capas
- **Generic Repository**: Reutilización de código en operaciones CRUD

### Seguridad

⚠️ **Nota**: Este proyecto es una demostración. Para producción considerar:
- Implementar JWT Authentication
- Hashear contraseñas con BCrypt/Argon2
- Validación de inputs
- Rate limiting
- HTTPS obligatorio
- Secrets management

### Escalabilidad

El diseño actual permite:
- Migrar a microservicios separando capas
- Implementar caching (Redis)
- Agregar message queuing (RabbitMQ)
- Implementar CQRS
- Agregar logging centralizado (ELK Stack)

### Testing

Estructura preparada para:
- Unit Tests (xUnit/NUnit)
- Integration Tests
- API Tests (Postman/Newman)
- Performance Tests (K6/JMeter)

---

## 👨‍💻 Autor

**drusystem**

- GitHub: [@drusystem](https://github.com/drusystem)
- Proyecto: [PUNTO-VENTA-API](https://github.com/drusystem/PUNTO-VENTA-API)

---

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

---

## 🤝 Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

---

## 📞 Soporte

Si tienes preguntas o encuentras algún problema:

1. Revisa la [documentación de Swagger](http://localhost:5000/swagger)
2. Abre un [Issue en GitHub](https://github.com/drusystem/PUNTO-VENTA-API/issues)
3. Contacta al autor

---

<div align="center">

**⭐ Si este proyecto te fue útil, considera darle una estrella en GitHub ⭐**

Hecho con ❤️ por drusystem

</div>