# API Personal

![ProyectoFlyer](https://github.com/user-attachments/assets/2f805a61-e6ed-460b-aa43-faa10f2b5144)

Esta API te permite gestionar tu portafolio personal, incluyendo proyectos, habilidades técnicas y más. Esta API está diseñada para ser fácilmente actualizable y mantenible, permitiendo futuras expansiones. Siéntete libre de utilizarla como desees. Si crees que puedes aportar mejoras valiosas, ¡son bienvenidas! Si solo necesitas un backend sencillo y rápido para tu portafolio personal, aquí lo tienes.

## 🚀 Características principales

- **📂 Diseño e implementación de la base de datos** utilizando MySQL.

- **💻 Desarrollo del servidor** realizado con C# y .NET.

- **🔒 Seguridad con autenticación y autorización JWT** lo que garantiza que solo tú puedas utilizar los endpoints de manera segura.

- **🛠️ Inyección de dependencias** para facilitar el mantenimiento, la escalabilidad y la testabilidad de la aplicación, promoviendo un diseño desacoplado y modular.

- **📦 Controlador base genérico** para facilitar la reutilización y reducir la duplicación de código.

## 🛠️ Tecnologías utilizadas

- **.NET 8**
- **Backend:** C# y ASP.NET
- **Base de datos:** MySQL
- **ORM:** Entity Framework Core
- **Autenticación y autorización:** JWT
- **Infraestructura:** Azure (Opcional)
- **Envío de correos electrónicos:** MailKit

## 💻 Instalación

## ⚙️ Requisitos Previos

Antes de instalar y ejecutar el proyecto, asegurate de tener los siguientes componentes instalados:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- [MySQL](https://dev.mysql.com/downloads/mysql/)
- Un cliente API como [Postman](https://www.postman.com/) o [Insomnia](https://insomnia.rest/) para probar los endpoints

## **📝 Pasos para la instalación**

Sigue estos pasos para descargar y ejecutar la API en tu máquina local.

**1. Clonar el repositorio**

Utiliza Git para clonar el proyecto en tu máquina local.

```bash
git clone https://github.com/danielbarros01/Portfolio.git
cd Portfolio
```

**2. Configuración de <i>appsettings.json</i>**

En el archivo appsettings.json, debes modificar las configuraciones para tu entorno local, como la cadena de conexión de la base de datos y las configuraciones de autenticación.

```bash
"ConnectionStrings": {
    "DefaultConnection": "your-connection"
  },

  "MailSettings": {
    "Mail": "Email address configured to send",
    "DestinationEmail": "Email address where the message will arrive",
    "Password": "Password for the configured email",
    "Host": "SMTP server for mail",
    "Port": "SMTP port number",
    "Subject": "Default email subject"
  },

  "Authentication": {
    "SecretKey": "secret key>",
    "Issuer": "app name",
    "UserCreationCode": "<code to register user>"
  },

  //Optional, you must also delete it from Startup.cs
  "ApplicationInsights": {
    "ConnectionString": "Application Insights Connection String"
  }
```

**3. Configurar la base de datos**

Si estás utilizando MySQL, asegúrate de tener la base de datos creada, puedes encontrar el archivo como [SQLPUBLIC.sql](https://github.com/danielbarros01/Portfolio/blob/master/SQLPUBLIC.sql) en el repositorio.

Este es el diagrama EER por si te interesa.

![eer diagram](https://github.com/user-attachments/assets/3a7cc6ff-5b21-4a83-9b91-9e9136c94d86)

**4. Añadir política CORS**

En el archivo <i>Startup.cs</i> puedes encontrar esta configuración. Es para permitir que desde el cliente se pueda utilizar la API. Cambia los valores si es necesario; a la hora de desplegar tu frontend, deberás agregar el origen aquí.

```bash
services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500")
                   .AllowAnyHeader()
                   .AllowAnyMethod();

            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
```

Puedes ignorar el paso anterior y permitir el acceso a tu API desde cualquier sitio, aunque no te lo recomiendo, ya que estarías quitando una capa de seguridad a la API.

```bash
services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
```

**5. Ejecuta la API**

Inicia la API localmente con el siguiente comando:

```bash
dotnet run
```

Esto lanzará el servidor y la API estará disponible en https://localhost:7189 (o similar, el puerto puede ser otro).

<br>

## **📑 Uso de la API**

Te explicaré cada endpoint con ejemplos básicos para que sepas cómo interactuar con la API.

**Recuerda:**
Algunas rutas están protegidas por autenticación y roles, por lo que es necesario pasar el token correspondiente.

## 1. Autenticación

### a. Crear usuario

**URL**: `POST /api/users/create`

| Campo      | Tipo     | Descripción                               |
| ---------- | -------- | ----------------------------------------- |
| `username` | `string` | Nombre de usuario                         |
| `email`    | `string` | Correo electrónico                        |
| `password` | `string` | Contraseña para el ingreso                |
| `code`     | `string` | Codigo que configuramos en el appsettings |
| `role`     | `string` | Utiliza la cadena "Admin"                 |

### b. Iniciar sesión

**URL**: `POST /api/users/login`

| Campo      | Tipo     | Descripción                |
| ---------- | -------- | -------------------------- |
| `username` | `string` | Nombre de usuario          |
| `email`    | `string` | Correo electrónico         |
| `password` | `string` | Contraseña para el ingreso |

**Response**

```json
{
 "token": "",
 "expiration": ""
}
```

## 2. Categorías

### a. Crear y actualizar categoría

**URL de creación**: `POST /api/categories`

**URL de actualización**: `PUT /api/categories/{id}`

**Content-Type**: `application/json`

**Requiere autenticación**: Sí

### Parámetros

| Campo  | Tipo     | Descripción         |
| ------ | -------- | ------------------- |
| `name` | `string` | Nombre de categoría |

### b. Eliminar categoría

**URL**: `DELETE /api/categories/{id}`

**Requiere autenticación**: Sí

### c. Obtener categoría

**URL**: `GET /api/categories/{id}`

**Requiere autenticación**: No

**Response**

```json
{
 "id": "int",
 "name": "string"
}
```

### d. Obtener categorías

**URL**: `GET /api/categories

**Requiere autenticación**: No

**Response**

```json
[
 {
  "id": "int",
  "name": "string"
 }
 //...Más categorías
]
```

## 3. Tecnologías

### a. Crear y actualizar tecnología

**URL de creación**: `POST /api/technologies`

**URL de actualización**: `PUT /api/technologies/{id}`

**Content-Type**: `multipart/form-data`

**Requiere autenticación**: Sí

### Parámetros

| Campo        | Tipo     | Descripción                                               |
| ------------ | -------- | --------------------------------------------------------- |
| `name`       | `string` | El nombre de la tecnología                                |
| `image`      | `file`   | Archivo de imagen para el ícono de la tecnología          |
| `categoryId` | `int`    | Id de la categoría a la que debe pertenecer la tecnología |

### b. Eliminar tecnología

**URL**: `DELETE /api/technologies/{id}`

**Requiere autenticación**: Sí

### c. Obtener tecnología

**URL**: `GET /api/technologies/{id}`

**Requiere autenticación**: No

**Response**

```json
{
 "id": "int",
 "name": "string",
 "imageUrl": "string",
 "categoryId": "int"
}
```

### d. Obtener tecnologías

**URL**: `GET /api/technologies`
**Requiere autenticación**: No
**Response**

```json
[
 {
  "id": "int",
  "name": "string",
  "imageUrl": "string",
  "categoryId": "int"
 }
 // ... más tecnologías
]
```

## 4. Soft Skills

### a. Crear y actualizar

**URL de creación**: `POST /api/skills/soft`

**URL de actualización**: `PUT /api/skills/soft/{id}`

**Content-Type**: `application/json`

**Requiere autenticación**: Sí

### Parámetros

| Campo           | Tipo     | Descripción                                 |
| --------------- | -------- | ------------------------------------------- |
| `title`         | `string` | El título de la habilidad blanda            |
| `description`   | `string` | Una breve descripción                       |
| `technologyIds` | `array`  | Ids de las tecnologías que quieres vincular |

### b. Eliminar

**URL**: `DELETE /api/skills/soft/{id}`

**Requiere autenticación**: Sí

### c. Obtener habilidad blanda

**URL**: `GET /api/skills/soft/{id}`

**Requiere autenticación**: No

**Response**

```json
{
 "id": "int",
 "title": "string",
 "description": "string",
 "technologies": [
  {
   "id": "int",
   "name": "string",
   "imageUrl": "string",
   "categoryId": "int"
  }
  //...Más tecnologías
 ]
}
```

### d. Obtener habilidades blandas

**URL**: `GET /api/skills/soft/{id}`

**Query Parameters (Opcional)**:

- page: Número de página
- recordsPerPage: Cantidad de registros por página

**Requiere autenticación**: No

**Response**

```json
[
 {
  "id": "int",
  "title": "string",
  "description": "string",
  "technologies": [
   {
    "id": "int",
    "name": "string",
    "imageUrl": "string",
    "categoryId": "int"
   }
   //...Más tecnologías
  ]
 }
 //...Más habilidades blandas
]
```

## 5. Technical Skills

### a. Crear y actualizar

**URL de creación**: `POST /api/skills/technical`

**URL de actualización**: `PUT /api/skills/technical/{id}`

**Content-Type**: `application/json`

**Requiere autenticación**: Sí

### Parámetros

| Campo          | Tipo     | Descripción                                                                         |
| -------------- | -------- | ----------------------------------------------------------------------------------- |
| `proficiency`  | `string` | Una breve descripción de tu nivel de dominio en esta habilidad                      |
| `order`        | `int`    | Las habilidades tienen un orden porque puede que quieras mostrar una antes que otra |
| `technologyId` | `int`    | Id de la tecnología que quieres vincular                                            |

### b. Eliminar

**URL**: `DELETE /api/skills/technical/{id}`

**Requiere autenticación**: Sí

### c. Obtener habilidad técnica

**URL**: `GET /api/skills/technical/{id}`

**Requiere autenticación**: No

**Response**

```json
{
 "id": "int",
 "proficiency": "string",
 "order": "int",
 "technology": {
  "id": "int",
  "name": "string",
  "imageUrl": "string",
  "categoryId": "int"
 }
}
```

### d. Obtener habilidades técnicas

**URL**: `GET /api/skills/technical`

**Query Parameters (Opcional)**:

- page: Número de página
- recordsPerPage: Cantidad de registros por página

**Requiere autenticación**: No

**Response**

```json
[
 {
  "id": "int",
  "proficiency": "string",
  "order": "int",
  "technology": {
   "id": "int",
   "name": "string",
   "imageUrl": "string",
   "categoryId": "int"
  }
 }
 //...Más habilidades técnicas
]
```

## 6. Educaciones

### a. Crear y actualizar

**URL de creación**: `POST /api/educations`

**URL de actualización**: `PUT /api/educations/{id}`

**Content-Type**: `multipart/form-data`

**Requiere autenticación**: Sí

### Parámetros

| Campo           | Tipo            | Descripción                                                                         |
| --------------- | --------------- | ----------------------------------------------------------------------------------- |
| `title`         | `string`        | Título obtenido                                                                     |
| `order`         | `int`           | Las habilidades tienen un orden porque puede que quieras mostrar una antes que otra |
| `description`   | `string`        | Una breve descripción                                                               |
| `institution`   | `string`        | Nombre de la institución donde se curso                                             |
| `image`         | `file/image`    | Logo u imagen de la institución                                                     |
| `readme`        | `file/markdown` | Markdown con mas información sobre la institución                                   |
| `readmeES`      | `file/markdown` | Markdown en español                                                                 |
| `technologyIds` | `array`         | IDs de las tecnologías que quieres vincular                                         |

### b. Eliminar

**URL**: `DELETE /api/educations/{id}`

**Requiere autenticación**: Sí

### c. Obtener educación

**URL**: `GET /api/educations/{id}`

**Requiere autenticación**: No

**Response**

```json
{
 "id": "int",
 "order": "int",
 "title": "string",
 "description": "string",
 "imageUrl": "string",
 "readmeUrl": "string",
 "readmeUrlES": "string",
 "institution": "string",
 "technologies": [
  {
   "id": "int",
   "name": "string",
   "imageUrl": "string",
   "categoryId": "int"
  }
  //...Más tecnologías
 ]
}
```

### d. Obtener educaciones

**URL**: `GET /api/educations`

**Requiere autenticación**: No

**Response**

```json
[
 {
  "id": "int",
  "order": "int",
  "title": "string",
  "description": "string",
  "imageUrl": "string",
  "readmeUrl": "string",
  "readmeUrlES": "string",
  "institution": "string",
  "technologies": [
   {
    "id": "int",
    "name": "string",
    "imageUrl": "string",
    "categoryId": "int"
   }
   //...Más tecnologías
  ]
 }
 //...Más educaciones
]
```

## 7. Proyectos

### a. Crear y actualizar

**URL de creación**: `POST /api/projects`

**URL de actualización**: `PUT /api/projects/{id}`

**Content-Type**: `multipart/form-data`

**Requiere autenticación**: Sí

### Parámetros

| Campo           | Tipo            | Descripción                                    |
| --------------- | --------------- | ---------------------------------------------- |
| `title`         | `string`        | Título del proyecto                            |
| `description`   | `string`        | Una breve descripción                          |
| `url`           | `string`        | Sitio web del proyecto                         |
| `linkGithub1`   | `string`        | Repositorio del proyecto                       |
| `linkGithub2`   | `string`        | Segundo repositorio del proyecto si existiese  |
| `image`         | `file/image`    | Logo u imagen del proyecto                     |
| `readme`        | `file/markdown` | Markdown con mas información sobre el proyecto |
| `readmeES`      | `file/markdown` | Markdown en español                            |
| `technologyIds` | `array`         | IDs de las tecnologías que quieres vincular    |

### b. Eliminar

**URL**: `DELETE /api/projects/{id}`

**Requiere autenticación**: Sí

### c. Obtener proyecto

**URL**: `GET /api/projects/{id}`

**Requiere autenticación**: No

**Response**

```json
{
 "id": "int",
 "title": "string",
 "description": "string",
 "url": "string",
 "imageUrl": "string",
 "readmeUrl": "string",
 "readmeUrlES": "string",
 "linkGithub1": "string",
 "linkGithub2": "string",
 "technologies": [
  {
   "id": "int",
   "name": "int",
   "imageUrl": "string",
   "categoryId": "int"
  }
  //...Más tecnologías
 ]
}
```

### d. Obtener proyectos

**URL**: `GET /api/projects`

**Requiere autenticación**: No

**Response**

```json
[
 {
  "id": "int",
  "title": "string",
  "description": "string",
  "url": "string",
  "imageUrl": "string",
  "readmeUrl": "string",
  "readmeUrlES": "string",
  "linkGithub1": "string",
  "linkGithub2": "string",
  "technologies": [
   {
    "id": "int",
    "name": "int",
    "imageUrl": "string",
    "categoryId": "int"
   }
   //...Más tecnologías
  ]
 }
 //...Más proyectos
]
```

## 8. MAIL

### a. Enviar correo electrónico

Utiliza este endpoint para que las personas puedan enviarte un correo electrónico.

**URL**: `POST /api/mail`

**Content-Type**: `application/json`

**Requiere autenticación**: No

### Parámetros

| Campo     | Tipo     | Descripción                            |
| --------- | -------- | -------------------------------------- |
| `name`    | `string` | Nombre de la persona                   |
| `email`   | `string` | Mail de la persona                     |
| `message` | `string` | Mensaje que la persona quiere enviarte |


## ☁️ Despliegue

En mi caso, elegí Azure para desplegar la aplicación. Sin embargo, puedes optar por la plataforma que mejor se adapte a tus preferencias y necesidades.


## 🌟 Comunidad

Siéntete libre de contribuir al proyecto si lo deseas. Estaré encantado de recibir tus recomendaciones, mejoras y nuevas ideas.
