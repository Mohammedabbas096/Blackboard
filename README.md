# Group1Blackboard

A team project built for **Internet Programming 2026FA** — Phase 1.

This is an ASP.NET Core MVC web application that simulates a Blackboard-style
student portal. It's organized into two separate areas: one for students and
one for admins, each with their own navigation and layout.

---

## What's inside

**Student Area** — the default view when you open the app
- Home (Student Dashboard)
- Catalog (placeholder)
- Calendar (placeholder)

**Admin Area** — accessible via the Admin link in the navbar
- Major management (placeholder)
- Course management (placeholder)
- Student management (placeholder)
- View Site — takes you back to the student dashboard

---

## How to run it

Make sure you have the [.NET 10 SDK](https://dotnet.microsoft.com/download) installed.

```bash
cd Group1Blackboard
dotnet run
```

Then open your browser and go to `http://localhost:5069` (or whatever port the terminal shows).

---

## Project structure

```
Group1Blackboard/
├── Areas/
│   ├── Admin/          ← Admin area (Major, Course, Student)
│   └── Student/        ← Student area (Home, Catalog, Calendar)
├── Controllers/        ← Root controller (redirects to Student area)
├── Views/
│   └── Shared/
│       ├── _RootLayout.cshtml      ← Outermost layout (Bootstrap, JS)
│       ├── _StudentLayout.cshtml   ← Student navbar (inherits Root)
│       └── _AdminLayout.cshtml     ← Admin navbar (inherits Root)
├── wwwroot/            ← Static files (Bootstrap, CSS, JS)
└── Program.cs          ← App entry point and routing config
```

---

## Tech stack

- ASP.NET Core MVC (.NET 10)
- Bootstrap 5
- Razor Views
- MVC Areas for Student / Admin separation

---

## Team

Group 1 — Internet Programming 2026FA

Mohammed Ghouse Mohiuddin (Student ID: 1529883)

---

## Notes

This is Phase 1. No database or authentication is implemented yet.
Catalog, Calendar, Major, Course, and Student pages are placeholders
that will be built out in future phases.
