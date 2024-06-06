using Godot;
using System.Text.Json;
using System.Text.Json.Serialization;

class Wallet
{
	[JsonPropertyName("c-coins")]
	public int Coins { get; set; }
}

public partial class Login : Control
{
	public override void _Ready()
	{
		// Personally I don't like the default button, so I'll hide it for now.
		GetNode<AcceptDialog>("Container/AcceptDialog").GetOkButton().Hide();

		GetNode<Button>("Container/LoginButton").Pressed += HandleLoginButtonPressed;
		GetNode<Button>("Container/RegisterButton").Pressed += HandleRegisterButtonPressed;
	}

	private async void HandleLoginButtonPressed()
	{
		string username = GetNode<LineEdit>("Container/Username").Text;
		string password = GetNode<LineEdit>("Container/Password").Text;

		// Maybe we can make something fancy later and only enable the button when both fields are okay.
		// This should be enough for now.
		if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
		{
			GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "Usuario y contraseña son requeridos";
			GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
			return;
		}

		try
		{
			// Disable button to prevent multiple requests.
			GetNode<Button>("Container/LoginButton").Disabled = true;

			// (Enrique) Maybe I should proxy this trough nginx on ealpizar.com to setup SSL.
			// Nakama can also handle SSL negotiation itself, but it's not recommended for production.
			Nakama.Client client = new Nakama.Client("http", "nakama-api.ealpizar.com", 7350, "defaultkey");

			// AuthenticateEmailAsync also works with usernames, we just need to pass in a blank email.
			// See https://github.com/heroiclabs/nakama/issues/235.
			Nakama.ISession session = await client.AuthenticateEmailAsync("", password, username: username, create: false);

			GlobalState.Instance.Username = session.Username;
			GlobalState.Instance.AuthToken = session.AuthToken;
			
			// Very ugly way to get the coins. We should move this to a proper server request system.
            GlobalState.Instance.Coins = JsonSerializer.Deserialize<Wallet>((await client.GetAccountAsync(session)).Wallet).Coins;
			
			GetTree().ChangeSceneToFile("res://scenes/game.tscn");
		}
		catch (Nakama.ApiResponseException e)
		{
			GD.PrintErr(e);
			string errorMessage = "Error al conectar con el servidor de autenticación";

			// 400: Bad Request is returned when the password has less than 8 characters.
			// 401: Unauthorized is returned when the email or password is incorrect.
			// 404: Not Found is returned when the user does not exist.
			// We don't want to expose this information to the user so we just show a generic message.
			if (e.StatusCode == 400 || e.StatusCode == 401 || e.StatusCode == 404)
			{
				errorMessage = "Correo o contraseña incorrectos";
			}

			GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = errorMessage;
			GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();

			// Don't forget to enable the button again.
			GetNode<Button>("Container/LoginButton").Disabled = false;
		}
	}

	private void HandleRegisterButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/register.tscn");
	}
}
