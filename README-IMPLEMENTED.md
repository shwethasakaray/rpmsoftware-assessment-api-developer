# README-IMPLEMENTED.md

## Overview

This document provides an overview of the changes made to the project, including the implementation of RESTful APIs, dependency injection, and validation. 
It also explains the design principles and best practices used in the project.

## Changes Made

### 1. Implementation of RESTful APIs

- Created a new .Net Core Web API project with `CustomersController` class to handle CRUD operations for customers.
- Implemented the following endpoints:
  - `GET /api/customers`: Get all customers.
  - `GET /api/customers/{id}`: Get a customer by ID.
  - `POST /api/customers`: Create a new customer.
  - `PUT /api/customers/{id}`: Update an existing customer.
  - `DELETE /api/customers/{id}`: Delete a customer by ID.

### 2. Dependency Injection

- Registered the `ICustomerService`, `CustomerService`, `ICustomerValidator`, `CustomerValidator`, `ICustomerRepository`, and `CustomerRepository` in the dependency injection container.
- Used SimpleInjector for dependency injection in the ASP.NET Web Forms project.

### 3. Validation

- Created a `CustomerValidator` class to validate customer data.
- Used the `CustomerValidator` class in the `CustomersController` and `Customers.aspx.cs` to validate customer data before processing.
- Added validation messages for required fields and data formats.
- Implemented functionality for checking the Country and populating the provinces based on the selected country.

### 3. CSS Styling

- Add button was not placed correctly, added margin top for proper alignment.

### 5. Testing

- Created testing projects for the `CustomerService`, `CustomerValidator`, and `CustomerController` classes.
- Implemented unit tests for the `CustomerService`, `CustomerValidator`, and `CustomersController` classes using NUnit and Moq.

## Design Principles and Best Practices

### SOLID Principles

1. **Single Responsibility Principle (SRP)**:
   - Each class has a single responsibility. For example, the `CustomerValidator` class is responsible for validating customer data.

2. **Open/Closed Principle (OCP)**:
   - Classes are open for extension but closed for modification. This is achieved using interfaces and dependency injection.

3. **Liskov Substitution Principle (LSP)**:
   - Subtypes are substitutable for their base types. This is ensured by using interfaces for services and validators.

4. **Interface Segregation Principle (ISP)**:
   - Interfaces are specific to the needs of the clients. For example, the `ICustomerValidator` interface is specific to customer validation.

5. **Dependency Inversion Principle (DIP)**:
   - High-level modules depend on abstractions, not on low-level modules. This is achieved using dependency injection.

### Best Practices

1. **Dependency Injection**:
   - Used SimpleInjector for dependency injection to manage dependencies and improve testability.

2. **Asynchronous Programming**:
   - Implemented asynchronous methods in the `CustomerService` and `CustomerRepository` classes to improve scalability and responsiveness.

3. **Validation**:
   - Used a dedicated `CustomerValidator` class to validate customer data, ensuring data integrity and consistency.

4. **Swagger Documentation**:
   - Configured Swagger to document the RESTful APIs, making it easier for developers to understand and use the APIs.

## Conclusion

The changes made to the project follow SOLID design principles and best practices, resulting in a more maintainable, extensible, and testable codebase. 
The implementation of RESTful APIs, dependency injection, and validation ensures a robust and scalable application.