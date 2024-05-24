using Godot;
using System;

public partial class Login : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AcceptDialog>("Container/AcceptDialog").GetOkButton().Hide();
		GetNode<Button>("Container/LoginButton").Pressed += HandleLoginButtonPressed;
		GetNode<Button>("Container/RegisterButton").Pressed += HandleRegisterButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private async void HandleLoginButtonPressed()
	{
		string email = GetNode<LineEdit>("Container/Email").Text;
		string password = GetNode<LineEdit>("Container/Password").Text;

		if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
		{
			GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "Correo y contraseña son requeridos";
			GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
			return;
		}

		try
		{
			Nakama.Client client = new Nakama.Client("http", "nakama-api.ealpizar.com", 7350, "defaultkey");
			Nakama.ISession session = await client.AuthenticateEmailAsync(email, password, create: true);
			GetTree().ChangeSceneToFile("res://scenes/Game.tscn");
		}
		catch (Nakama.ApiResponseException e)
		{
			if (e.StatusCode == 400 || e.StatusCode == 401)
			{
				GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "Correo o contraseña incorrectos";
				GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
				return;
			}

			GD.PrintErr(e);
			GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "Error al conectar con el servidor de autenticación";
			GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
		}
	}

	private void HandleRegisterButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/register.tscn");
	}
}
