# 💼 Aplicación Fullstack – Vue.js + .NET Core API + SQL Server

Una aplicación fullstack desarrollada con **Vue 3** (frontend) y **ASP.NET Core 6** (backend), conectada a **SQL Server** y asegurada con **JWT**. Este proyecto demuestra habilidades en desarrollo de APIs REST, autenticación, arquitectura en capas, consumo de APIs desde frontend moderno y persistencia de datos.

---

## 🚀 Tecnologías utilizadas

- **Frontend:** Vue 3 + Vite, Axios
- **Backend:** ASP.NET Core 6, Entity Framework Core
- **Autenticación:** JSON Web Tokens (JWT)
- **Base de datos:** SQL Server

---

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

## 📦 Instalación y ejecución

### 🔧 Backend

1. Clonar el proyecto y abrir la carpeta `Backend` en Visual Studio o VS Code.
2. Ejecutar el script `database/init.sql` en SQL Server para crear la base de datos.
3. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=VentasDb;Trusted_Connection=True;"
   }
