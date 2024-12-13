# AI Chat back

## Overview

Audio Processing Service is a robust and scalable application for handling audio processing tasks. It supports functionalities such as chat-based audio transcription, text-based chat response generation, and seamless integration with cloud services for storage and retrieval. This service leverages modern frameworks, including .NET, Docker, and Azure Blob Storage, to provide efficient and modular solutions.

## Key Features

- **Chat Response Streaming**: Real-time generation of chat responses for text prompts.
- **Audio Transcription**: High-quality audio-to-text transcription with support for various audio formats.
- **Blob Storage Integration**: Secure and efficient file upload, download, and management with Azure Blob Storage.
- **Google Drive Support**: Fetch audio files directly from Google Drive.
- **Retry Policies**: Built-in resilience for handling API rate limits using Polly.
- **Clean Architecture**: Follows the principles of Clean Architecture, separating concerns into distinct layers, such as Application, Domain, and Infrastructure, ensuring maintainability, testability, and scalability. The core business logic is isolated from external dependencies, promoting a clear separation of concerns.
- **Containerized Deployment**: Uses Docker for seamless deployment and scalability.

## Architecture

The system follows **Clean Architecture**, which emphasizes separation of concerns into distinct layers. This approach is designed to ensure scalability, maintainability, and testability by isolating business logic from external frameworks and dependencies. The core of the system remains independent of implementation details, enabling flexibility and easy adaptation to future changes.

### Core Components:

- **Presentation/API Layer**: Exposes the API endpoints and acts as the interface between the system and external consumers.
- **Application Layer**: Handles commands and queries, interacting with domain logic.
- **Domain Layer**: Contains business rules and logic, including entities like Chat and ChatResponse.
- **Infrastructure Layer**: Manages external integrations, such as Blob Storage, Google Drive, and OpenAI services.

### Technologies Used:

- **.NET 8**: Core framework for building the application.
- **Docker Compose**: Manages dependencies like PostgreSQL and the application container.
- **OpenTelemetry**: Observability stack for metrics and tracing (optional).

## System Requirements

### Prerequisites:

- **Docker**: Ensure Docker and Docker Compose are installed.
- **OpenAI API Key**: Required for transcription and chat-based tasks.
- **Ollama Llama** 3.2: Ensure Ollama Llama 3.2 is installed and running.

## Setup 

### How to Run with Docker Compose

1. Clone the repository:
 
   ```bash
   git clone <repository-url>
   ```

2. Build and Run with Docker Compose
   ```bash
   docker-compose up --build
   ```




