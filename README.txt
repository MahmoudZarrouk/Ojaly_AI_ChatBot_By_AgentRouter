# Ojaly Chat Bot
v

Ojaly Chat Bot is a simple AI-powered chatbot application built with ASP.NET Core MVC and integrated with an external AI API provider.

The project demonstrates how to connect a .NET web application with an AI model through API requests, send user messages from the frontend to the backend, process the response, and display it in a clean chat interface.

---

## Project Overview

Ojaly Chat Bot allows users to write messages in a web-based chat interface and receive AI-generated responses in real time.

The application is built using ASP.NET Core MVC, where the frontend sends the user message to a backend API route. The backend then communicates with the AI provider using `HttpClient`, receives the response, and returns it back to the frontend.

---

## Features

- Simple and modern chatbot interface
- ASP.NET Core MVC architecture
- Backend API route for handling chat messages
- AI API integration using `HttpClient`
- JSON request and response handling
- Secure API key usage through `appsettings.json`
- Clean and organized project structure
- Responsive dark-themed UI
- Improved error handling
- Separate API controller for chat requests

---

## Technologies Used

- ASP.NET Core MVC
- C#
- JavaScript
- HTML
- CSS
- Bootstrap
- HttpClient
- System.Text.Json
- Groq API / OpenAI-Compatible API

---

## Project Structure

```text
OjalyChatBot/
│
├── Controllers/
│   ├── ChatController.cs
│   └── ChatApiController.cs
│
├── Models/
│   └── ChatMessageRequest.cs
│
├── Services/
│   ├── IAiService.cs
│   └── AiService.cs
│
├── Views/
│   ├── Chat/
│   │   └── Index.cshtml
│   │
│   └── Shared/
│       └── _Layout.cshtml
│
├── wwwroot/
│   ├── css/
│   │   └── site.css
│   │
│   └── js/
│       └── chat.js
│
├── appsettings.json
├── Program.cs
└── README.md
