# Presentation Notes

## Problem Statement

This project is a Healthcare Appointment and Care Analytics Platform. It helps a provider register patients, manage appointments, secure REST APIs for integration, and view operational analytics such as department load and completion rate.

## What I Used

- C# and ASP.NET Core for backend APIs and MVC/Razor dashboard.
- EF Core with SQLite for local persistence.
- ADO.NET for custom analytics queries.
- JWT for authentication and role-based authorization.
- xUnit for unit testing.
- Docker and GitHub Actions for DevOps readiness.

## Main Modules

- Patient module: create, update, list, and delete patients.
- Appointment module: book and update appointments with scheduling conflict validation.
- Analytics module: dashboard metrics and department load.
- Auth module: login with JWT token generation.

## Design Patterns

- Repository Pattern is used in `EfRepository<T>` to abstract database operations.
- Unit of Work is used in `UnitOfWork` to group repository changes and call `SaveChangesAsync`.
- DTO Pattern is used in `PatientDto`, `AppointmentDto`, and request classes.
- Dependency Injection is used throughout the API to inject services, repositories, DbContext, and JWT services.
- Layered Architecture separates Domain, Application, Infrastructure, API, and Angular UI.

## Business Rules

- A new appointment must be at least 15 minutes in the future.
- A patient email must be unique.
- A patient or doctor cannot have another scheduled appointment within 30 minutes of the selected time.
- Only Admin can delete patients.
- Admin and Receptionist can create patients and appointments.
- Doctor can view patients, view analytics, and update appointments.

## Demo Flow

1. Start the API and Angular app.
2. Log in with `admin / Admin@123`.
3. Show dashboard metrics.
4. Add a patient.
5. Book an appointment for that patient.
6. Explain that the dashboard uses ADO.NET while CRUD uses EF Core.
7. Show Swagger/OpenAPI endpoint and role-protected API behavior.
