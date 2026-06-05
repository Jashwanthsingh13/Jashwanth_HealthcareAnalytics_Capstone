# Jashwanth_HealthcareAnalytics_Capstone

# Healthcare Analytics 

Healthcare Analytics  is a layered ASP.NET Core solution for managing patient appointments with JWT-secured REST APIs and SQL Server persistence. The solution is designed for healthcare appointment tracking, secure access, and future Azure container deployment.

## Business Goal

Improve care delivery by providing a compliant platform to manage patient appointments, secure user access, and a foundation for analytics-ready healthcare workflows.

## Solution Overview

The solution follows a layered architecture:

- HealthcareAnalytics.Domain: core entities and repository contracts
- HealthcareAnalytics.Application: application layer placeholder for business services and validation
- HealthcareAnalytics.Infrastructure: EF Core DbContext and repository implementations
- HealthcareAnalytics.API: REST API, JWT authentication, Swagger, and controller endpoints
- HealthcareAnalytics.MVC: UI project scaffold for future Razor/MVC screens
- HealthcareAnalytics.Tests: unit test project scaffold

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Swagger / OpenAPI
- xUnit for testing

## Core Domain Model

- Patient: stores patient profile details such as name, email, phone number, date of birth, gender, and address
- Appointment: stores appointment date, status, notes, and PatientId foreign key
- User: stores login information and role association
- Role: stores role names such as Admin, Doctor, or Receptionist

## Project Structure

- HealthcareAnalytics.API: API startup, authentication, controllers, Swagger
- HealthcareAnalytics.Domain: entities and interfaces
- HealthcareAnalytics.Infrastructure: database context and repositories
- HealthcareAnalytics.Application: business logic layer placeholder
- HealthcareAnalytics.MVC: future web UI
- HealthcareAnalytics.Tests: automated tests

## Current API Features

- Get all patients
- Get patient by id
- Create patient
- Get all appointments with patient details
- Create appointment
- Generate JWT token from login endpoint

## Authentication

The API uses JWT bearer authentication. The token settings are defined in appsettings.json:

- Jwt:Key
- Jwt:Issuer
- Jwt:Audience

The login endpoint currently issues a token for an Admin claim set. This is suitable for demo use and should be replaced with database-backed credential validation for production.

## Database

The application uses SQL Server through EF Core. The context defines these sets:

- Patients
- Appointments
- Users
- Roles

Relationships:

- One Patient has many Appointments
- One Role has many Users

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 or VS Code with C# tooling

### Configuration

Update the connection string and JWT settings in HealthcareAnalytics.API/appsettings.json.

### Run the API

1. Restore packages
2. Build the solution
3. Run the HealthcareAnalytics.API project
4. Open Swagger in development mode

## Example Endpoints

- GET /api/patients
- GET /api/patients/{id}
- POST /api/patients
- GET /api/appointments
- POST /api/appointments
- POST /api/auth/login

## Testing

The test project is included and should be expanded with:

- repository tests
- controller tests
- validation tests
- auth/token tests

## Development Notes

- Repository pattern is used to isolate data access
- EF Core includes are used for appointment-to-patient loading
- JSON reference cycles are handled in API serialization
- Swagger is enabled in development

## Future Enhancements

- Add FluentValidation for MVC and API validation
- Secure login against user records in the database
- Add role-based authorization policies
- Add DTOs and mapping layer
- Add EF Core migrations and seed data
- Add Dockerfile and GitHub Actions workflow
- Add Azure App Service deployment pipeline

## License

Include your academic or organizational submission terms here.
