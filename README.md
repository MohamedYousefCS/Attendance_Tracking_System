# Attendance/Time Tracking System

## Description

The Attendance/Time Tracking System is a simple and basic solution designed to streamline the process of monitoring attendance for students, employees, and instructors. It provides a user-friendly interface for tracking check-in and check-out times, making attendance management efficient for businesses. The system caters to various types of users including students, instructors, employees, supervisors, and security personnel.

## Technologies Used

This web application is developed using the following technologies:
- **ASP.NET Core MVC:** Provides the framework for building web applications.
- **SQL Server:** Used for storing and managing data.
- **Entity Framework Core (Code First):** Facilitates database operations within the application.
- **HTML, JavaScript, jQuery:** Used for frontend development.
- **Bootstrap:** Provides responsive design components for the user interface.

## Main Features

### 1. User Registration
- **Register Student:** Allows administrators to register students with their personal details.
- **Register Instructor:** Administrators can register instructors.
- **Register Employee:** Admins can register employees, specifying their roles (e.g., student affairs, security).

### 2. Profile Management
- **Manage User Profiles:** Enables students to view their personal data and attendance records.
- **Instructor Profile:** Instructors can access and manage their personal information.

### 3. Student Management
- **CRUD Operations:** Provides functionalities for managing student records.
- **Training Programs:** Manages various training programs (PTP, ITP, ST), including program, intake, and track details.

### 4. Department Management
- **Manage Employee and Instructor Departments:** Facilitates CRUD operations for managing departments, with supervisors assigned for each track.

### 5. Permission Requests
- **Late or Absence Permission:** Students can request permissions for late arrival or absence, which are then approved or rejected by the track supervisor.

### 6. Attendance Recording
- **Daily Attendance:** Records daily attendance of students, capturing check-in and check-out times.
- **Attendance Analysis:** Generates reports on late arrivals, absences, and punctuality using schedule and attendance data.

### 7. Schedule Management
- **Student Schedule:** Students can view their schedules and manage permissions within the system.
- **Track Supervisor Schedule:** Allows track supervisors to record student schedules for better organization.

## Installation

To install and run the application locally, follow these steps:
1. Clone the repository to your local machine.
2. Set up the database using SQL Server and execute the necessary scripts provided.
3. Configure the connection string in the application settings.
4. Build and run the application using ASP.NET Core.

## Usage

1. Log in as an administrator to access admin functionalities.
2. Register students, instructors, and employees as required.
3. Manage user profiles and department assignments.
4. Track attendance, handle permissions, and manage student schedules.
5. Generate reports and analyze attendance data for decision-making.

## Contributors

- Mohamed Yousef Ragab
- Eslam Abdelaziz Abdelwanis Momeh
- Habiba Helmy Mohamed Badawy
- Ebrahim Osama Dawoud
- Bothina Ahmed Ahmed 
