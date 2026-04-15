# 🏥 MedCore - Smart Hospital Management System

**MedCore** is a comprehensive, enterprise-level Hospital Management System (HMS) built using the **Onion Architecture** pattern. It is designed to streamline healthcare workflows, manage patient records, and optimize doctor-patient scheduling through a modern, secure, and highly scalable web interface.

---

## 🏛️ Architectural Overview (Onion Architecture)

This project strictly adheres to the **Onion Architecture** (also known as hexagonal architecture), ensuring that the core business logic is independent of external frameworks, UI, and databases.

### **Project Layers:**
1.  **`MedCore.Domain` (The Core):** Contains the domain entities, value objects, and repository interfaces. It has no dependencies on any other layer.
2.  **`MedCore.Application` (Business Logic):** Contains the service interfaces and implementations. It handles the application-specific business rules.
3.  **`MedCore.Infrastructure` (Data & External Tools):** Implements the repository interfaces using Entity Framework Core, handles migrations, and manages the `ApplicationDbContext`.
4.  **`MedCore.Web` (Presentation):** The ASP.NET Core MVC project containing Controllers, ViewModels, and UI components.

---

## ✨ Key Features

### 🔐 Advanced Role-Based Access Control (RBAC)
The system distinguishes between three distinct roles to ensure data security and operational integrity:
* **Admin:** Full system authority. Can manage medical specialties, doctors' profiles, system settings, and user roles.
* **Receptionist:** Dedicated access to patient management and appointment booking. Administrative menus are automatically hidden.
* **User/Patient:** Access to the medical directory, viewing doctor profiles, and basic system navigation.

### 📅 Smart Appointment Management
* **Real-time Availability:** Prevents double-booking by checking doctor availability programmatically.
* **AJAX Integration:** Dynamic loading of doctors based on selected specialties without page refreshes.
* **Full CRUD:** Receptionists can book, reschedule, or cancel appointments with intuitive workflows.

### 📂 Medical Record Management
* **Doctors & Specialties:** Manage medical departments and clinical staff with data integrity protection (prevents deleting specialties with active doctors).
* **Patient Database:** Secure storage for patient contact info with validation to prevent duplicate records.

### 🎨 Modern UI/UX
* Responsive Dashboard overview.
* Interactive notifications using **SweetAlert2**.
* Auto-active smart sidebar that adapts to the current user's role.

---

## 🛠️ Tech Stack

* **Backend:** .NET 10.0, ASP.NET Core MVC (C#).
* **Database:** Microsoft SQL Server.
* **ORM:** Entity Framework Core (Code First).
* **Identity:** ASP.NET Core Identity for Authentication & Authorization.
* **Design Patterns:** Onion Architecture, Generic Repository Pattern, Dependency Injection.
* **Frontend:** HTML5, CSS3 (Inter Font), JavaScript, jQuery, Bootstrap 5, FontAwesome, SweetAlert2.

---

## 🚀 Getting Started

### 1. Database Setup
1.  Clone the repository.
2.  Update the connection string in `appsettings.json` within the `MedCore.Web` project.
3.  Open the **Package Manager Console**, set the default project to `MedCore.Infrastructure`.
4.  Run the migration command:
    ```bash
    Update-Database
    ```

### 2. Data Seeding
On the first run, the system automatically seeds the required roles and default administrative accounts to allow immediate testing.

| Role | Email | Password |
| :--- | :--- | :--- |
| **Admin** | `admin@medcore.com` | `123456` |
| **Receptionist** | `reception@medcore.com` | `123456` |

> **Note:** It is highly recommended to change these default passwords before deploying to a production environment.

---

## 📸 Screenshots

*(Add your project screenshots here to showcase the UI)*

![Dashboard](link-to-your-screenshot)
![Appointment List](link-to-your-screenshot)
![Booking System](link-to-your-screenshot)

---

## 🤝 Contact & Support

This system was designed and developed by **Eng. [Write Your Name Here]**. For inquiries or collaboration requests, please feel free to reach out:

* **Email:** [Your Email Address]
* **LinkedIn:** [Your LinkedIn Profile Link]
* **GitHub:** [Your GitHub Profile Link]

---
**Developed with ❤️ using Onion Architecture.**