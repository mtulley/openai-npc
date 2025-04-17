using Godot;

public partial class LLMDialogueManager : Node
{
    [Export] public NpcDialogueGenerator npcDialogueGenerator;

    string general_instructions = "Generate NPC dialogue in the format: 'NPC: dialogue here'.";
    string context = "";

    public override void _Ready()
    {
        npcDialogueGenerator.Connect(NpcDialogueGenerator.SignalName.ResponseReceived, new Callable(this, nameof(OnLlmResponse)));
    }

    public void ChangeContext(string context) 
    {
        this.context = context;
    }

    public void GetLlmResponse(string player_dialogue)
    {
        npcDialogueGenerator.SendPrompt("Follow these instructions: " + general_instructions + "Context: " + context + "Player dialogue: " + player_dialogue);
    }

    private void OnLlmResponse(string response)
    {
        GD.Print($"LLM says: {response}");
    }
}
