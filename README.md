# Transcard Payments Portal

A secure internal Payments Web Application built using Clean Architecture principles with ASP.NET Core Web API and Blazor Web App (.NET 8).

This project demonstrates:

- Secure frontend-to-backend communication
- Clean separation of concerns
- Proper validation and state handling
- Enterprise-grade Git workflow
- Responsive and accessible UI design

---

## 🏗 Architecture Overview

This solution follows a Clean Architecture pattern:
TranscardSolution
│
├── src/
│ ├── Transcard.Domain
│ ├── Transcard.Application
│ ├── Transcard.Infrastructure
│ ├── Transcard.WebAPI
│ ├── Transcard.WebApp
│
├── tests/
│ ├── Transcard.UnitTests
│--- README.md

### Layer Responsibilities

### 🔹 Domain
- Core business entities
- Business rules and invariants
- No external dependencies

### 🔹 Application
- DTOs
- Interfaces
- Use cases
- Validation logic
- Orchestration layer

### 🔹 Infrastructure
- Repository implementations
- External service integrations
- Security handlers

### 🔹 WebAPI
- REST endpoints
- Authentication & Authorization
- API exposure

### 🔹 WebApp (Blazor)
- UI Components
- State management
- Secure API communication
- User feedback handling

---

## 🔐 Secure Frontend Communication

### Authentication Strategy

- Secure HTTP-only cookies
- No JWT stored in localStorage or sessionStorage
- No tokens exposed in JavaScript

### Token Storage

Tokens are stored in:

- HTTP-only secure cookies

This prevents:
- Token theft via XSS
- Client-side token manipulation
- Token exposure in browser storage

### Protection Against Attacks

#### ✔ Token Leakage
- No secrets in UI
- No tokens in browser storage
- No tokens logged

#### ✔ XSS Mitigation
- Blazor auto-encodes content
- No raw HTML injection
- No MarkupString usage
- CSP-ready backend configuration

#### ✔ Over-posting Protection
- UI uses DTOs
- Domain entities are never directly bound
- Backend re-validates input

---

## 🎨 UI Features

The Payments screen includes:

- Responsive layout (Mobile / Tablet / Desktop)
- Real-time validation
- Inline validation messages
- Toast notifications for:
  - Success
  - Error
  - Unauthorized (401)
  - Forbidden (403)
- Loading state handling
- Disabled button during processing

### UX Principles Applied

- Clear validation feedback
- Accessible markup (labels + validation)
- Touch-friendly controls
- Controlled state transitions
- No unnecessary animations

---

## 💱 Currency Handling

Currency is implemented as a controlled dropdown.

Reasoning:
- Prevent invalid manual input
- Improve UX clarity
- Reduce backend validation noise
- Maintain deterministic behavior

The current implementation uses a static list.
This can be upgraded to an API-driven source if required.

---

## 🔄 State Handling

The form implements:

- EditContext-based validation
- OnValidSubmit / OnInvalidSubmit handlers
- Controlled loading state
- Focus-first-invalid behavior
- Toast feedback abstraction

---

## 🌍 Responsive Strategy

Bootstrap grid system used:

- Mobile: full width
- Tablet: medium width
- Desktop: centered card layout

Design goals:
- Readability
- Touch optimization
- Minimal cognitive load
- Financial-system UI consistency

---

## 🧪 Testing Strategy

- Unit tests for Application layer
- Separation allows:
  - Domain logic testing
  - Service orchestration testing
  - Future bUnit component testing

---

## 🌱 Branching Strategy (Real-World Workflow)

This repository follows a multi-branch deployment strategy:

| Branch   | Purpose              |
|----------|---------------------|
| main     | Production          |
| staging  | Pre-production/UAT  |
| develop  | Active development  |
| feature/*| Feature branches    |
| hotfix/* | Emergency fixes     |

Workflow:

1. Create feature branch from `develop`
2. Open PR → `develop`
3. Promote to `staging`
4. Promote to `main` for production release

Direct pushes to `main` are restricted.

---

## 🚀 Running the Project

### 1️⃣ Run WebAPI
cd src/Transcard.WebAPI
dotnet run

### 2️⃣ Run WebApp
cd src/Transcard.WebApp
dotnet run

Login Details
username: admin
password: admin

---

## 🔒 Environment & Secrets

Sensitive configuration is not committed.

Secrets should be supplied via:

- Environment variables
- Secure configuration providers
- CI/CD secret storage

---

## 📌 Future Improvements

- Global HTTP interceptor for automatic 401 redirect
- Centralized ToastService
- CI/CD with GitHub Actions
- Docker multi-environment setup
- Role-based UI authorization
- Refresh token implementation

---

## 📜 License

Internal project / Assessment implementation.







└── README.md

