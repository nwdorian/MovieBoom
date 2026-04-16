[![Build](https://github.com/nwdorian/MovieBoom/actions/workflows/build.yml/badge.svg)](https://github.com/nwdorian/MovieBoom/actions/workflows/build.yml)
[![Architecture Tests](https://github.com/nwdorian/MovieBoom/actions/workflows/test-architecture.yml/badge.svg)](https://github.com/nwdorian/MovieBoom/actions/workflows/test-architecture.yml)
[![Integration Tests](https://github.com/nwdorian/MovieBoom/actions/workflows/test-integration.yml/badge.svg)](https://github.com/nwdorian/MovieBoom/actions/workflows/test-integration.yml)

# MovieBoom

A modern ASP.NET Core MVC web application built with Clean Architecture principles, featuring movie management, user authentication, and comprehensive test coverage.

## Features

- **Movie Management** - Browse, search, and manage movie data
- **User Authentication** - Secure user registration and login with ASP.NET Core Identity
- **Email Integration** - Send emails using MailKit (SMTP)
- **Clean Architecture** - Scalable design with clear layer separation
- **Containerized** - Full Docker support for easy deployment
- **Tested** - Integration tests and architecture validation

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

Local development without Docker

- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [MailPit](https://mailpit.axllent.org/docs/install/)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/nwdorian/MovieBoom.git
cd MovieBoom
```

### 2. Run with Docker (Recommended)

The fastest way to get started:

```bash
docker compose up --build
```

This will start:

- **Web Application** - <http://localhost:8080>
- **SQL Server Database** - localhost:1433
- **MailPit Email Server** - localhost:1025 (SMTP), <http://localhost:8025> (admin UI)

### 3. Run Locally (Without Docker)

1. Update the connection string in `src/Web/appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MovieBoom; Integrated Security=SSPI"
   }
   ```

2. Start the MailPit service

   - Access the email testing UI at <http://localhost:8025> to view all emails sent by the application.

3. Build and run:

   ```bash
   dotnet build
   cd src/Web
   dotnet run
   ```

## Project Structure

```plaintext
MovieBoom/
├── src/
│   ├── Application/                    # Application layer - services, queries, DTOs
│   ├── Domain/                         # Domain layer - entities, errors, interfaces
│   ├── Infrastructure/                 # Infrastructure layer - EF Core, auth, email
│   └── Web/                            # Presentation layer - MVC controllers, views
├── tests/
│   ├── Application.IntegrationTests/   # Integration tests
│   └── ArchitectureTests/              # Architecture validation tests
├── .github/
│   └── workflows/                      # GitHub Actions CI/CD
│
├── compose.yaml                        # Docker Compose production config
├── compose.dev.yaml                    # Docker Compose development config
├── global.json                         # .NET SDK version
├── Directory.Build.props               # MSBuild properties
├── Directory.Packages.props            # Central package management
├── Directory.Build.targets             # Shared analyzer package references
├── .editorconfig                       # Code style rules
└── MovieBoom.slnx                      # Solution file
```

### Architecture Layers

| Layer | Description |
| ------- | ------------- |
| **Domain** | Core business entities and rules. No external dependencies. |
| **Application** | Application services and abstractions. Depends only on Domain. |
| **Infrastructure** | External concerns (database, email, identity). Implements Application interfaces. |
| **Web** | ASP.NET Core MVC presentation layer. Depends on Application and Infrastructure. |

## Running Tests

### Run All Tests

```bash
dotnet test
```

### Run Architecture Tests

Architecture tests validate that the project follows Clean Architecture principles:

```bash
cd tests/ArchitectureTests
dotnet test
```

## Docker Development

### Development Environment

```bash
# Start external services with development configuration
docker compose -f compose.dev.yaml up --build

# Stop services
docker compose down -v
```

## Key Technologies

| Technology | Version | Purpose |
| ------------ | --------- | --------- |
| .NET | 10.0 | Runtime and SDK |
| ASP.NET Core | 10.0.x | Web framework |
| Entity Framework Core | 10.0.x | ORM and data access |
| Microsoft Identity | 10.0.x | Authentication & authorization |
| xUnit | 3.2.x | Unit testing framework |
| Shouldly | 4.3.x | Assertion library |
| Testcontainers | 4.11.x | Container-based testing |
| Serilog | 10.0.x | Structured logging |
| MailKit | 4.15.x | Email sending |
| SonarAnalyzer | 10.21.x | Static code analysis |

## Configuration

### appsettings.json

Configuration is layered:

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development overrides

## CI/CD

GitHub Actions workflows are provided in `.github/workflows/`:

- **build.yml** - Builds the solution on multiple .NET versions
- **test-unit.yml** - Runs unit tests
- **test-integration.yml** - Runs integration tests with Testcontainers

## Code Quality

The project includes:

- **SonarAnalyzer** - Code quality and security analysis
- **IDisposableAnalyzers** - Resource disposal validation
- **TreatWarningsAsErrors** - Build fails on warnings
- **EnforceCodeStyleInBuild** - Code style enforcement

## Personal thoughts

- This is my first project working with identity. I didn't like the scaffolded identity pages, code structure is quite unreadable so I followed a [CodeMaze Identity Series](https://code-maze.com/asp-net-core-identity-series/) and structured the code they way I prefer. Not having scaffolded Identity pages does mean there are certain features lacking like deleting the account. I plan to implement this in next projects.
- Clean Architecture also uncovered some friction when trying to keep the pattern of implementing external concerns like Identity in Infrastructure project. Web project comes with Identity libraries plugged in so it made more sense to keep some Identity stuff in the Web project instead of pulling dependencies into Infrastructure.
- In this project I also tried out Docker setup. Docker compose is super convenient because we don't have to install services (like DB and MailPit) on the machine. Plus they can be run with a single command.
- When it comes to Docker I am unsure how to handle the production VS development setup. Debugging inside a container is noticably slower compared to running the app on host machine. It's not unusable but it feels like online gaming on 200ms with small delay on every step of the debugger. I decided to have a development compose file that starts all external services and running the app on host.
- Another problem with Docker setup for production is running migrations. From the articles I've read it's not good to run migrations and seeding on app startup but instead to generate SQL scripts from migrations and run them manually. My goal with Docker compose is to have a single command to run everything. So instead of having more steps for running the application I decided to have migrations and seeding run on startup after the SQL Server is ready (with healthcheck). For future applications that will be deployed I will run SQL scripts manually, but for now I'm happy with this setup as it's more important that it's easy and simple to clone and run the project locally.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

## Contact

For any questions or feedback, please open an issue.
