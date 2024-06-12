using Godot;
using System;

// This class is loaded automatically by the engine when the game starts.
// See https://docs.godotengine.org/en/stable/tutorials/scripting/singletons_autoload.html

// Also, this is a Singleton! ;)

public partial class GlobalState : Node
{
    private static GlobalState _instance;

    public static GlobalState Instance => _instance;

    public override void _EnterTree()
    {
        if (_instance != null)
        {
            // Someone is trying to create a second instance of the class. :(
            // There can be only one!
            this.QueueFree();
        }

        // This is the first instance of the class.
        _instance = this;
    }

    // (Enrique) Maybe I should proxy this trough nginx on ealpizar.com to setup SSL.
    // Nakama can also handle SSL negotiation itself, but it's not recommended for production.
    public Nakama.Client NakamaClient { get; } = new Nakama.Client("http", "nakama-api.ealpizar.com", 7350, "defaultkey");
    public Nakama.ISession Session { get; set; }

    // This is just temporary. We should move this to a proper server request system.
    public int Coins { get; set; }

    // Do we keep this here?
    public string Version { get; } = "pre-alpha v0.1";
}
