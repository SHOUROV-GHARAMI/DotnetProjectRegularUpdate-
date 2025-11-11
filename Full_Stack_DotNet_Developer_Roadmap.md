# Recreate the Markdown roadmap file

md_content = """# Full-Stack .NET Developer Roadmap (Project-Driven)

A step-by-step guide to becoming a professional full-stack .NET developer through hands-on projects.

---

## **Stage 1 — .NET Fundamentals (Console Apps)**
**Project:** Personal Expense Tracker (Console App)

**Learn:**
- C# syntax, OOP, LINQ, and EF basics
- Use File I/O or SQLite for persistence
- Practice LINQ filtering, summing, grouping

---

## **Stage 2 — ASP.NET Core API Basics**
**Project:** Task Management API

**Learn:**
- Create RESTful CRUD APIs
- Use EF Core with PostgreSQL
- Implement proper HTTP response codes

---

## **Stage 3 — Authentication & Authorization**
**Project:** User Management + JWT Auth System

**Learn:**
- Implement JWT login, registration
- Use BCrypt for password hashing
- Add role-based access control

---

## **Stage 4 — Frontend Integration (Full-Stack App #1)**
**Project:** Task Manager Web App

**Learn:**
- Build React or Angular frontend
- Integrate with ASP.NET Core API
- Implement JWT authentication frontend flow

---

## **Stage 5 — Advanced Database & Query Skills**
**Project:** Blog Platform API

**Learn:**
- Learn EF Core relationships (1:N, N:N)
- Write raw SQL joins, subqueries
- Implement pagination and filtering

---

## **Stage 6 — Clean Architecture & Domain-Driven Design**
**Project:** E-Commerce Backend

**Learn:**
- Learn Clean Architecture principles
- Use CQRS, MediatR, AutoMapper
- Apply Repository + Unit of Work patterns

---

## **Stage 7 — Full-Stack App #2**
**Project:** E-Commerce Web App

**Learn:**
- Combine backend and frontend
- Add role-based admin features
- Use Redux or NgRx for state management

---

## **Stage 8 — Advanced Topics**
**Project:** Event Booking Platform

**Learn:**
- Learn SignalR (real-time updates)
- Implement payments (Stripe)
- Use Redis caching, deploy with Docker

---

## **Stage 9 — Deployment & DevOps**
**Goal:** Deploy to Azure using CI/CD

**Learn:**
- Containerize apps using Docker
- Set up GitHub Actions pipelines
- Host API + DB on Azure

---

## **Stage 10 — Capstone Project**
**Project:** TeamCollab (SaaS App)

**Learn:**
- Multi-tenancy, real-time chat (SignalR)
- JWT + refresh tokens
- Stripe subscriptions, Clean Architecture

---

### ✅ Summary Table

| Stage | Project | Focus |
|--------|----------|--------|
| 1 | Expense Tracker | .NET Basics |
| 2 | Task API | Web API CRUD |
| 3 | JWT Auth | Authentication |
| 4 | Task Manager | Full-stack Basics |
| 5 | Blog API | Database & Query Skills |
| 6 | E-Commerce Backend | Clean Architecture |
| 7 | E-Commerce Full-stack | Advanced Full-stack |
| 8 | Event Booking | Real-time + Payments |
| 9 | Deployment | DevOps & CI/CD |
| 10 | TeamCollab | SaaS Capstone |

---

**Next Step:** Start with Project #1 – Expense Tracker (Console App).
"""

# Save the Markdown file
md_path = "/mnt/data/Full_Stack_DotNet_Developer_Roadmap.md"
with open(md_path, "w") as f:
    f.write(md_content)

md_path
