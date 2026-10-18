# AI Document Summarizer

A simple one-page web application that allows users to upload a PDF or paste text and receive an AI-generated summary. Users can also ask questions about the document.

**Live Demo:** https://documentsummarizer-qncf.onrender.com 

## Features

- Upload PDF or paste text
- AI-powered document summarization
- Ask questions about the document (Q&A)
- Clean and simple user interface
- Built with modern .NET and free AI (Groq)

## Tech Stack

- **.NET 10** + Blazor Server
- **Microsoft.Extensions.AI**
- **PdfPig** (PDF text extraction)
- **Groq** (free high-speed AI)

## How to Run Locally

1. Install [.NET 10 SDK](https://dotnet.microsoft.com/download)
2. Clone the repository
3. Add your Groq API key in `appsettings.json` or as an environment variable
4. Run:
   ```bash
   dotnet run