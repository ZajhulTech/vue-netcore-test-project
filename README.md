# 💼 Aplicación Fullstack – Vue.js + .NET Core API + SQL Server

Una aplicación fullstack desarrollada con **Vue 3** (frontend) y **ASP.NET Core 6** (backend), conectada a **SQL Server** y asegurada con **JWT**. Este proyecto demuestra habilidades en desarrollo de APIs REST, autenticación, arquitectura en capas, consumo de APIs desde frontend moderno y persistencia de datos.

---

## 📁 Estructura del Proyecto

```
/api
  └── WebApi                  # Proyecto principal ASP.NET Core Web API
  └── infrastructure          # Acceso a datos e implementación de repositorios
  └── interfaces              # Interfaces para aplicar inversión de dependencias
  └── UserStories             # Lógica de negocio (casos de uso)
  └── Models                  # DTOs y modelos compartidos
/vue-code
  └── src
      ├── components          # Componentes reutilizables (Header, Footer, Sidebar, etc.)
      ├── views               # Vistas principales (LoginView, SalesView)
      ├── services            # Servicios de comunicación con API
      ├── domain              # Lógica de dominio (auth, tokens, etc.)
      ├── styles              # Variables y estilos globales
```

## 🚀 Tecnologías utilizadas

- **Frontend:** Vue 3 + Vite, Axios
- **Backend:** ASP.NET Core 8, Entity Framework Core
- **Autenticación:** JSON Web Tokens (JWT)
- **Base de datos:** SQL Server

---
## 🛠️ Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/)
- Gestor de base de datos SQL Server
- * Opcional [Visual Studio 2022 Community](https://visualstudio.microsoft.com/es/vs/community/) (con soporte para ASP.NET y Docker)
- * Opcional [Docker y Docker Compose](https://www.docker.com/)

---

## 🧱 Instalación de la Base de Datos

1. Abrir SQL Server Management Studio o tu cliente preferido.
2. Ejecutar los scripts SQL: 
    🔹/sql/krispy_sales_schema.sql  
    🔹/sql/vwSaleDetails.sql
    🔹/sql/data_test_generator.sql
3. Asegúrate de que la cadena de conexión en `appsettings.json` coincida con tu entorno.

Esto iniciará la Web API en la URL: https://localhost:7299 (o como esté configurado en launchSettings.json) 
Swagger disponible en: http://localhost:7299/swagger

Esto iniciará la app Vue en: http://localhost:55508


---

## ▶️ Ejecución del Proyecto

### Opción 1: Visual Studio Community

1. Abrir la solución `KrispyKremeSales.sln`.
2. El proyecto `WebApi` y `vue-code` estan por defaault como aplicaciones de inicio.
3. Click en "Iniciar" para levantar el backend.

### Opción 2: Viual studio code | Batch 

#### BackEnd
1. En el explorador de soluciones, hacer clic derecho sobre la carpeta `webApi` y seleccionar "Abrir con Terminal".
2. Ejecuta los siguientes comandos:
```bash
dotnet restore
dotnet build
dotnet run
```
Esto iniciará la Web API en la URL: https://localhost:7299 (o como esté configurado en launchSettings.json).

#### FrontEnd
1. En el explorador de soluciones, hacer clic derecho sobre la carpeta `vue-code` y seleccionar "Abrir con Terminal".
2. Ejecutar:
```bash
npm install
npm run dev
```
Esto iniciará la app Vue en: http://localhost:55508

## ⚙️ Funcionalidades

### 🔹 Backend (.NET Core)
- API REST estructurada en capas (Controllers, Services, Repositories)
- Endpoint de autenticación con credenciales fijas (JWT)
- Listado público de ventas
- Creación de ventas protegida con JWT
- Validaciones básicas y manejo de errores
- Script SQL incluido para generar las tablas necesarias

### 🔹 Frontend (Vue.js)
- Pantalla de login con validación y manejo de sesión JWT
- Pantalla principal:
  - Lista de ventas (sin autenticación)
  - Formulario para agregar venta (requiere token)
- Gestión del token con `localStorage`
- Axios para consumo de API y manejo de errores

---


## 🐳 PLUS Levantamiento en Docker 

### Crear imagen backend:
```bash
docker build -t sales-module-api:master -f api/WebApi/Dockerfile .
```

### Subir imagen:
```bash
docker push sales-module-api:master
```

### Ejecutar backend y frontend:
```bash
docker-compose up --build
```

---