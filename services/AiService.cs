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
                new(ChatRole.System, "You are a helpful assistant that creates clear, concise summaries of documents. Use bullet points when useful. Keep the summary short."),
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
                    "You are a helpful assistant. Answer the user's question ONLY using the provided document content. " +
                    "If the answer is not in the document, say 'I could not find that information in the document.'"),
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