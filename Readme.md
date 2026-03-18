# EmployeeApp – Sistema de Gestión de Empleados y Contratación

Aplicación web desarrollada con ASP.NET Core para gestionar el ciclo de vida de los empleados y sus contratos, incluyendo autenticación, trazabilidad de cambios y validaciones de negocio.

Este proyecto se ha desarrollado como respuesta al reto técnico de construcción de una aplicación **Full Stack** basada en el ecosistema **.NET**, aplicando criterios de calidad, mantenibilidad y separación de responsabilidades.

**Contenido:** [Objetivo](#objetivo-del-proyecto) · [Stack](#stack-tecnológico) · [Requisitos](#requisitos-previos) · [Ejecución](#ejecución-del-proyecto) · [Credenciales](#credenciales-de-acceso) · [Funcionalidades](#funcionalidades-implementadas) · [Tests y CI](#tests) · [Notas](#notas-y-posibles-problemas) · [Dificultades](#dificultades-encontradas)

---

## Objetivo del proyecto

La solución permite:

- Autenticar usuarios para acceder a la plataforma.
- Gestionar el registro y consulta de empleados.
- Crear y editar contratos asociados a cada empleado.
- Mantener un histórico completo de los cambios realizados sobre los contratos.
- Garantizar la calidad del código mediante tests unitarios y pipeline de CI.

---

## Stack tecnológico

- **.NET 10**
- **ASP.NET Core Razor Pages**
- **NHibernate** + **Fluent NHibernate**
- **PostgreSQL**
- **FluentValidation**
- **xUnit** + **Moq**
- **GitHub Actions** para Integración Continua

---

## Requisitos previos

Antes de ejecutar el proyecto, es necesario tener instalado:

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) o Docker Engine
- Git

---

## Ejecución del proyecto

### 1. Clonar el repositorio

```bash
git clone <URL_DEL_REPOSITORIO>
cd <nombre-del-repositorio>
```

### 2. Levantar la base de datos

Desde la raíz del repositorio:

```bash
docker compose down
docker compose up -d
```

Esto levantará la instancia de PostgreSQL necesaria para la aplicación.

### 3. Ejecutar la aplicación

Desde la carpeta del proyecto web (o desde la raíz con `-p`):

```bash
cd src/EmployeApp
dotnet run
```

O desde la raíz del repositorio: `dotnet run -p src/EmployeApp`.

En la primera ejecución, la aplicación crea el esquema de base de datos mediante NHibernate Schema Export.

Una vez iniciada, la aplicación estará disponible en una URL similar a:

```
https://localhost:7xxx
```

El puerto exacto se mostrará en la consola durante el arranque.

## Credenciales de acceso

Por defecto, la aplicación incluye un usuario administrador de prueba:

- **Email:** `Admin@employeeApp.com`
- **Contraseña:** `Login123@`

## Funcionalidades implementadas

### Seguridad

- Sistema de login para acceder a la plataforma.
- Protección de las páginas privadas mediante autenticación con cookies.
- Uso de `[Authorize]` en las zonas restringidas (empleados y contratos).

### Gestión de empleados

- Listado de empleados existentes.
- Alta de nuevos empleados con: nombre, apellidos, número de la CASS, email, teléfono (y dirección/ciudad).
- Vista de detalle de cada empleado (ficha de empleado).

### Gestión de contratos

- Creación de contratos desde la ficha del empleado.
- Campos del contrato: fecha de inicio, fecha de fin (opcional), lugar de trabajo, salario mensual, horas semanales (y tipo/estado/jornada).
- Edición de contratos existentes centrada en salario mensual y horas semanales.

### Trazabilidad e histórico

- Cada creación o modificación de un contrato genera una entrada en el histórico.
- Se registra qué ha cambiado, cuándo se ha modificado y un motivo opcional del cambio.
- El histórico se consulta desde la ficha del empleado (enlace «Ver Historial» por cada contrato).

### Validaciones implementadas

Se ha aplicado lógica de validación para garantizar la consistencia de la información:

- Formato correcto del email.
- Formato del número de la CASS: 6 cifras + 1 letra mayúscula (ej.: `000000A`).
- Salario mensual y horas semanales superiores a 0 (horas máximo 168).
- Coherencia entre fechas: la fecha de fin no puede ser anterior a la fecha de inicio.
- Control de datos obligatorios en los formularios principales.

Las validaciones se han implementado con FluentValidation en la capa de servicios, separando la lógica de la presentación.

### Arquitectura y decisiones de diseño

El proyecto sigue una estructura inspirada en **Clean Architecture** y principios de **DDD**, con separación clara de responsabilidades.

**Capas principales:**

- **Domain (Domain.Core):** entidades del dominio, sin dependencias de infraestructura.
- **Infrastructure.Contracts:** interfaces de repositorios y unidad de trabajo.
- **Infrastructure.Data:** acceso a datos con NHibernate (mappings, persistencia, seeding).
- **Services / Services.Contracts:** casos de uso, DTOs, requests y validaciones; orquestación entre web e infraestructura.
- **Web (EmployeApp):** aplicación ASP.NET Core con Razor Pages; interfaz de usuario y flujo HTTP.

**Decisiones relevantes:**

- Razor Pages por simplicidad, claridad del flujo y rapidez de desarrollo.
- NHibernate para persistencia y mapeo de entidades, tal como se valoraba en el enunciado.
- Trazabilidad de los contratos con una entidad de histórico dedicada, sin sobrescribir datos sin registro.
- Validación externalizada a la capa de servicios para mantener el código limpio y facilitar los tests.
- Organización de la solución para añadir nuevos módulos o reglas de negocio con facilidad.

### Estructura del repositorio

```
EmployeApp/
|-- EmployeApp.sln
|-- docker-compose.yml
|-- src/
|    |-- EmployeApp/                      # Aplicación web (Razor Pages)
|    |-- EmployeeApp.Domain.Core/         # Entidades y configuración de dominio
|    |-- EmployeeApp.Infrastructure.Contracts/  # Interfaces de repositorios y UoW
|    |-- EmployeeApp.Infrastructure.Data/        # NHibernate, mappings y persistencia
|    |-- EmployeeApp.Services/            # Servicios de aplicación
|    |-- EmployeeApp.Services.Contracts/  # DTOs, requests y validaciones
|    |-- EmployeeApp.Tests/               # Tests unitarios (xUnit, Moq)
|-- .github/workflows/ci.yml             # Pipeline de CI
```

### Tests

Desde la raíz del repositorio:

```bash
dotnet test EmployeApp.sln -c Release
```

La solución incluye tests unitarios (xUnit, Moq) que cubren los casos de uso principales de los servicios (empleados y contratos), sin dependencias reales de base de datos.

### Integración continua

El proyecto incluye un pipeline de **GitHub Actions** (`.github/workflows/ci.yml`) que ejecuta:

- Restauración de dependencias
- Compilación en Release
- Ejecución de todos los tests

El pipeline se dispara en cada push y pull request a las ramas `main` o `master`.

---

## Notas y posibles problemas

- **Base de datos:** PostgreSQL se ejecuta en el puerto **5438** (mapeado desde el 5432 del contenedor). Si el puerto está en uso, puedes cambiarlo en `docker-compose.yml` y la connection string en `src/EmployeApp/appsettings.json` (o `appsettings.Development.json`).
- **Primera ejecución:** Las tablas se crean automáticamente al arrancar la aplicación (NHibernate Schema Export). No es necesario ejecutar migraciones manuales.
- **Certificado HTTPS:** En desarrollo es posible que el navegador muestre un aviso por el certificado autofirmado; se puede aceptar para `localhost`.

### Aspectos mejorables / evolución futura

Aunque la solución cubre los requisitos del reto, se podrían añadir:

- Paginación y filtros avanzados en el listado de empleados.
- Gestión de errores y mensajes funcionales más explícitos.
- Logs estructurados (por ejemplo Serilog).
- Mayor cobertura de tests (validadores, páginas).
- Contenerización completa de la aplicación (Dockerfile).
- Organización por vertical slices para nuevos módulos.

## Dificultades encontradas

Durante el desarrollo del proyecto, las principales dificultades han sido las siguientes:

- **Configuración del pipeline de GitHub Actions**  
  Ha sido una de las partes exigentes del reto, ya que no había configurado nunca antes un pipeline completo de CI con GitHub Actions. Esto me ha obligado a investigar cómo estructurar correctamente el proceso de **restore**, **build** y **ejecución automática de los tests**, así como a entender mejor el funcionamiento de la integración continua en un entorno real de desarrollo.

- **Trabajo con NHibernate**  
  Otra dificultad importante, la mas exigente ha sido el uso de **NHibernate**, ya que mi experiencia previa era principalmente con **Entity Framework**. Para poder implementar correctamente la persistencia, los mappings y la gestión de entidades, he tenido que investigar el funcionamiento de NHibernate y adaptarme a una forma de trabajar distinta a la que estaba acostumbrado. Aun así, este proceso me ha permitido ampliar conocimientos y entender mejor otros enfoques dentro del ecosistema .NET.

## Por qué he escogido esta arquitectura

He escogido una arquitectura por capas inspirada en **Clean Architecture** porque quería mantener el proyecto ordenado, legible y fácil de evolucionar. Aunque se trata de una prueba técnica y la aplicación no es muy grande, ya existen varias responsabilidades diferenciadas: autenticación, gestión de empleados, contratos, validaciones e histórico de cambios.

Organizar la solución por capas me ha ayudado a separar mejor la lógica de negocio de la persistencia y de la capa web, lo que hace que el código sea más mantenible y más sencillo de probar. Además, esta estructura encaja bien con los objetivos de la prueba, donde se valoraban buenas prácticas como **SOLID**, **Clean Architecture** y una separación clara de responsabilidades.

También he buscado que el proyecto no solo funcionara, sino que tuviera una base lo suficientemente limpia como para poder crecer en el futuro sin convertirse en un código difícil de mantener.
