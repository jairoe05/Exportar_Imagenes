# Exportar Imágenes (Save_Pictures)

Aplicación de escritorio para Windows que **extrae imágenes guardadas como binario (BLOB) en SQL Server y las exporta como archivos de imagen (`.png`)** a una carpeta elegida por el usuario.

Nació para resolver una necesidad concreta: sacar en lote las fotos de artículos almacenadas en la tabla `tblInItemPict` de un ERP y dejarlas disponibles como archivos normales.

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![.NET Framework 4.5.1](https://img.shields.io/badge/.NET%20Framework-4.5.1-512BD4)
![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-0078D6)
![SQL Server](https://img.shields.io/badge/DB-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)

## Funcionalidades

- Selección de la carpeta de destino con `FolderBrowserDialog`.
- Lectura de los BLOB con `SqlDataReader.GetBytes`, que copia el contenido de la columna binaria a un `byte[]`.
- Escritura de cada imagen en disco con `FileStream` + `BinaryWriter`, usando el identificador del artículo (`PictId`) como nombre de archivo.
- Barra de progreso que avanza mientras se procesan los registros.

## Cómo funciona

```
SQL Server (tblInItemPict)            Save_Pictures                 Disco
┌──────────────────────────┐   SELECT  ┌───────────────┐  .png   ┌──────────────┐
│ PictId  | PictItem (BLOB)│ ────────► │ GetBytes() →  │ ──────► │ <PictId>.png │
└──────────────────────────┘           │ BinaryWriter  │         └──────────────┘
                                       └───────────────┘
```

## Requisitos

- Windows con .NET Framework 4.5.1 o superior
- Visual Studio 2015 o posterior
- Acceso a una instancia de SQL Server con la tabla de imágenes

## Ejecución

1. Abre `Save_Pictures/Save_Pictures.sln` en Visual Studio.
2. En `Form1.cs`, ajusta la cadena de conexión y la consulta (`SELECT ... FROM tblInItemPict`) a tu base de datos.
3. Compila y ejecuta (F5).
4. Pulsa el botón, elige la carpeta de destino y espera a que termine la barra de progreso.

> **Nota:** si ya existe un archivo con el mismo nombre, no se sobrescribe y se produce una excepción (`FileMode.CreateNew`).

## Estructura

```
Save_Pictures/
├── Save_Pictures.sln
└── Save_Pictures/
    ├── Program.cs          # Punto de entrada
    ├── Form1.cs            # Lógica de lectura de BLOB y exportación
    ├── Form1.Designer.cs   # Interfaz (botón + barra de progreso)
    └── App.config
```

## Posibles mejoras

- Mover la cadena de conexión a `App.config` y no guardar credenciales en el código.
- Hacer configurables la tabla, las columnas y la extensión del archivo.
- Ejecutar la exportación en segundo plano (`BackgroundWorker` o `async/await`) para que la interfaz no se congele.
- Quitar del repositorio las carpetas `bin/` y `obj/`.
