# CRUD - React & .NET 8

Este proyecto es una aplicación web que permite gestionar una agenda de personas mediante operaciones CRUD completas (Crear, Leer, Actualizar y Eliminar). Está diseñado con una interfaz limpia y utiliza persistencia de datos.

## Tecnologías Utilizadas

* **Frontend:** React (inicializado con Vite) y CSS personalizado.
* **Backend:** ASP.NET Core 8 
* **Base de Datos:** SQLite junto con Entity Framework Core (`Microsoft.EntityFrameworkCore.Sqlite`) para un almacenamiento persistente, ligero y sin necesidad de instalar servidores de bases de datos externos.

## Requisitos Previos

Para ejecutar este proyecto en tu máquina local, asegúrate de tener instalado lo siguiente:
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Node.js](https://nodejs.org/) (incluye npm)

## Instrucciones de Ejecución

El proyecto está dividido en dos entornos (backend y frontend) que deben ejecutarse en terminales simultáneas.

### 1. Iniciar el Backend
El backend maneja la lógica de negocio y la creación automática de la base de datos.

1. Abre una terminal y navega hasta la carpeta raíz del backend.
2. Ejecuta el servidor con el siguiente comando:
   ```bash
   dotnet run
   ```

### 2. Iniciar el Frontend
El frontend se comunica con el backend para mostrar la interfaz de usuario.

Abre una nueva terminal (sin cerrar la del backend) y navega hacia la carpeta del frontend.

Instala las dependencias necesarias (esto solo se hace la primera vez):

```Bash
npm install
```
Levanta el servidor de desarrollo:
```Bash
npm run dev
```
La terminal te dará un enlace local (generalmente http://localhost:5173). Abre ese enlace en tu navegador web para empezar a usar la aplicación.
