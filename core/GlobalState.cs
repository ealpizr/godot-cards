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

    public string Username { get; set; }

    public string AuthToken { get; set; }
}
