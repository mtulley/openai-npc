using Godot;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


public partial class NpcDialogueGenerator : Node
{
    // Signal emitted when a response is received from the LLM
    [Signal] public delegate void ResponseReceivedEventHandler(string response);

    private readonly System.Net.Http.HttpClient _httpClient = new System.Net.Http.HttpClient();
    private const string OllamaUrl = "http://localhost:11434/api/generate";
    private const string ModelName = "llama2";  // Change if using a different model

    public override void _Ready()
    {
        GD.Print("LLM Client Ready.");
    }

    public async void SendPrompt(string prompt)
    {
        var requestBody = new
		{
			model = ModelName,
			prompt = prompt,
			stream = false
		};

		string jsonBody = JsonSerializer.Serialize(requestBody);

		var content = new StringContent(
			jsonBody,
			Encoding.UTF8,
			"application/json"
		);

        try
        {
            var response = await _httpClient.PostAsync(OllamaUrl, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseDict = Json.ParseString(responseBody).As<Godot.Collections.Dictionary>();

            if (responseDict.ContainsKey("response"))
            {
                string llmResponse = responseDict["response"].ToString();
                EmitSignal(SignalName.ResponseReceived, llmResponse);
            }
            else
            {
                GD.PrintErr("No 'response' key in LLM response.");
            }
        }
        catch (Exception ex)
        {
            GD.PrintErr($"Error connecting to LLM: {ex.Message}");
        }
    }
}
