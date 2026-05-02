# Newsletter

An AI-powered newsletter generation and management system built with .NET.

## Overview

This project provides a comprehensive solution for creating, managing, and distributing newsletters. It leverages AI agents to generate engaging content and titles, making the newsletter creation process efficient and automated.

## Project Structure

The solution is organized into four main projects:

- **Newsletter.Api**: ASP.NET Core Web API project that exposes endpoints for newsletter operations
- **Newsletter.Core**: Contains core models, abstractions, and service interfaces
- **Newsletter.Infra**: Infrastructure layer with concrete implementations of repositories and services
- **Newsletter.Ai**: AI-powered agents for content generation and title creation

## Technologies Used

- .NET 9.0
- ASP.NET Core
- OpenAI API for AI content generation
- Entity Framework (likely, based on repository pattern)

## Features

- Automated newsletter content generation using AI
- Subscriber management
- Article management
- Email service integration
- Modular architecture with clean separation of concerns

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- OpenAI API key (for AI features)

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd Newsletter
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Configure your OpenAI API key using user-secrets

5. Run the API:
   ```bash
   dotnet run --project Newsletter.Api
   ```

