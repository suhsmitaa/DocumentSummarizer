using Microsoft.Extensions.AI;

namespace DocumentSummarizer.Services;

public class AiService
{
    private readonly IChatClient _chatClient;

    public AiService(IChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<string> SummarizeAsync(string documentText)
    {
        try
        {
            var messages = new List<ChatMessage>
            {
                new(ChatRole.System, 
                    "You are a helpful multilingual assistant. " +
                    "Create a clear and concise summary of the given document. " +
                    "If the document is in Nepali, write the summary in Nepali. " +
                    "If the document is in English, write the summary in English. " +
                    "Use bullet points when useful. Keep the summary short and accurate."),
                new(ChatRole.User, $"Please summarize the following document:\n\n{documentText}")
            };

            var response = await _chatClient.GetResponseAsync(messages);
            return response.Text ?? "No summary generated.";
        }
        catch (Exception ex)
        {
            return $"Error while summarizing: {ex.Message}";
        }
    }

    public async Task<string> AskQuestionAsync(string documentText, string question)
    {
        try
        {
            var messages = new List<ChatMessage>
            {
                new(ChatRole.System, 
                    "You are a helpful multilingual assistant. " +
                    "Answer the user's question ONLY using the provided document content. " +
                    "If the document is in Nepali, answer in Nepali. " +
                    "If the document is in English, answer in English. " +
                    "If the answer is not in the document, say so clearly."),
                new(ChatRole.User, 
                    $"Document content:\n{documentText}\n\nQuestion: {question}")
            };

            var response = await _chatClient.GetResponseAsync(messages);
            return response.Text ?? "No answer generated.";
        }
        catch (Exception ex)
        {
            return $"Error while answering: {ex.Message}";
        }
    }
}