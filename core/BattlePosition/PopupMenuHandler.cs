using Godot;

public partial class PopupMenuHandler: Node {

    public PopupMenu BattleSelectionMenu;

    public override void _Ready() {
        BattleSelectionMenu = GetNode<PopupMenu>("PopupMenu");

        // add items
        BattleSelectionMenu.AddItem("Option 1");
        BattleSelectionMenu.AddItem("Option 2");
        BattleSelectionMenu.AddItem("Option 3");

        BattleSelectionMenu.Connect("id_pressed", new Callable(this, nameof(OnBattleSelectionMenuPressed)));
    }

    public void OnInput(InputEvent e) {
        GD.Print("Input event");
        if (e is InputEventMouseButton mouseButton) {
            if (mouseButton.ButtonIndex == MouseButton.Right && e.IsPressed()) {
                BattleSelectionMenu.Popup();
                GD.Print("Popup menu opened");
                BattleSelectionMenu.Popup();
            }
        }
    }

    public void OnBattleSelectionMenuPressed(int id) {
        GD.Print("BattleSelectionMenu pressed: " + id + " " + BattleSelectionMenu.GetItemText(id));
    }
}