using Godot;
using System;

public partial class Register : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AcceptDialog>("Container/AcceptDialog").GetOkButton().Hide();
		GetNode<Button>("Container/RegisterButton").Pressed += HandleRegisterButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private async void HandleRegisterButtonPressed()
	{
		string email = GetNode<LineEdit>("Container/Email").Text;
		string username = GetNode<LineEdit>("Container/Username").Text;
		string password = GetNode<LineEdit>("Container/Password").Text;

		if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
		{
			GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "Todos los campos son requeridos";
			GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
			return;
		}

		try
		{
			Nakama.Client client = new Nakama.Client("http", "nakama-api.ealpizar.com", 7350, "defaultkey");
			Nakama.ISession session = await client.AuthenticateEmailAsync(email, password, username, true);
			GetTree().ChangeSceneToFile("res://scenes/Login.tscn");
		}
		catch (Nakama.ApiResponseException e)
		{
			if (e.StatusCode == 400)
			{
				GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "Datos inválidos";
				GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
			}
		}
	}
}
