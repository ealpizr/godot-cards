using Godot;

public partial class PopupMenuHandler: Node {

    public PopupMenu BattleSelectionMenu;

    private Card cardHandled;

    public override void _Ready() {
        BattleSelectionMenu = GetNode<PopupMenu>("PopupMenu");

        // add items
        BattleSelectionMenu.AddItem("Ataque", 1);
        BattleSelectionMenu.AddItem("Defense", 2);

        BattleSelectionMenu.Connect("id_pressed", new Callable(this, nameof(OnBattleSelectionMenuPressed)));
    }

    public void OnInput(InputEvent e) {
        GD.Print("Input event");
        if (e is InputEventMouseButton mouseButton) {
            if (mouseButton.ButtonIndex == MouseButton.Right && e.IsPressed()) {
                BattleSelectionMenu.Popup();
                GD.Print("Popup menu opened");
            }
        }
    }

    public void OnBattleSelectionMenuPressed(int id) {
        GD.Print("BattleSelectionMenu pressed: " + id + " " + BattleSelectionMenu.GetItemText(id));
        cardHandled.Label.Text = "Battle Postiion";
    }

    public void HandleCard(Card card) {
        this.cardHandled = card;
    }
}