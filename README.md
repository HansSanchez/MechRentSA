# MechRentSA
MechRentSA es una aplicación de gestión de alquiler de maquinaria pesada, desarrollada utilizando Blazor para el frontend y ASP.NET Core para el backend. La aplicación permite a los usuarios gestionar y alquilar diferentes tipos de excavadoras, así como realizar búsquedas y paginación en los listados de maquinaria.

## Características
- Gestión de excavadoras: crear, editar, eliminar y ver detalles de las excavadoras.
- Búsqueda de maquinaria: buscar excavadoras por tipo.
- Paginación: navegar por los listados de excavadoras con soporte para paginación.
- Notificaciones: notificaciones amigables utilizando SweetAlert.

## Tecnologías Utilizadas
- **Frontend**: Blazor WebAssembly
- **Backend**: ASP.NET Core Web API
- **Base de Datos**: Entity Framework Core con SQL Server
- **UI/UX**: Bootstrap
- **Notificaciones**: SweetAlert2

## Requisitos Previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Clonar el Repositorio

```bash
git clone https://github.com/HansSanchez/MechRentSA.git
```

### Navega al proyecto del backend:

```bash
cd MechRentSA
cd MechRentSA.Server
```

## Configuración del Backend

### Configura la cadena de conexión a la base de datos en appsettings.json:
```bash
{
  "ConnectionStrings": {
    "connection_string": "Server=(local); Database=DB_MechRentSA; User Id=TU_USUARIO; Password=TU_CONTRASEÑA; TrustServerCertificate=True;"
  },
}
```

### Aplica las migraciones de la base de datos:

```bash
dotnet ef database update
```

### Ejecuta el backend:
```bash
dotnet run
```

## Configuración del Frontend

### Navega al proyecto del frontend:

```bash
cd MechRentSA.Client
```

### Ejecuta el frontend:
```bash
dotnet run
```


### Navegación
Una vez que ambos proyectos estén en ejecución, abre tu navegador y navega a `https://localhost:5035` para ver la aplicación en funcionamiento.

## Estructura del Proyecto
```bash
MechRentSA/
├── MechRentSA.Client/           # Proyecto del frontend (Blazor)
├── MechRentSA.Server/           # Proyecto del backend (ASP.NET Core)
├── MechRentSA.Shared/           # Clases y modelos compartidos
├── .gitignore                   # Archivos y carpetas ignorados por Git
├── README.md                    # Documentación del proyecto
└── MechRentSA.sln               # Solución de Visual Studio
```

## API Endpoints

### ExcavatorController

- `GET /api/excavator/getExcavator`: Obtiene todas las excavadoras con paginación y búsqueda.
- `GET /api/excavator/getByIdExcavator/{id}`: Obtiene una excavadora por su ID.
- `POST /api/excavator/storeExcavator`: Crea una nueva excavadora.
- `PUT /api/excavator/updateExcavator/{id}`: Actualiza una excavadora existente.
- `DELETE /api/excavator/deleteExcavator/{id}`: Elimina una excavadora.

## Ejemplos de Uso
### Crear una Nueva Excavadora

1. Haz clic en el botón "Nueva excavadora".
2. Rellena el formulario con los detalles de la excavadora.
3. Haz clic en "Guardar" para añadir la excavadora a la base de datos.

### Buscar una Excavadora

1. Escribe el tipo de excavadora en el campo de búsqueda.
2. Haz clic en el botón "Buscar".
3. Los resultados de la búsqueda se mostrarán en la tabla.

### Navegar por Páginas

1. Utiliza los botones "Anterior" y "Siguiente" para navegar por las páginas de la tabla.

## Contacto
Para cualquier consulta o sugerencia, por favor contacta a [hanssanchez427@gmail.com](mailto:hanssanchez427@gmail.com).
