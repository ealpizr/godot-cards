using Godot;
using System;
using System.Runtime.Intrinsics.Arm;

public partial class Register : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AcceptDialog>("Container/AcceptDialog").GetOkButton().Hide();
		GetNode<Button>("Container/RegisterButton").Pressed += HandleRegisterButtonPressed;
		GetNode<Button>("Container/LoginButton").Pressed += HandleLoginButtonPressed;
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
			GetTree().ChangeSceneToFile("res://scenes/login.tscn");
		}
		catch (Nakama.ApiResponseException e)
		{
			if (e.StatusCode == 400)
			{
				GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "Datos inválidos. Verifique que el correo sea válido y la contraseña tenga al menos 8 caracteres";
				GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
				return;
			}

			// Nakama tries to login the user if the email is already registered, returning a 401 status code
			if (e.StatusCode == 401)
			{
				GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "El correo ya está registrado";
				GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
				return;
			}

			GD.PrintErr(e);
			GetNode<AcceptDialog>("Container/AcceptDialog").DialogText = "Error al conectar con el servidor de autenticación";
			GetNode<AcceptDialog>("Container/AcceptDialog").PopupCentered();
		}
	}

	private void HandleLoginButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/login.tscn");
	}
}
