extends Control

@onready var main_menu: VBoxContainer = $MainMenu
@onready var server_message: VBoxContainer = $ServerMessage


func _on_server_button_pressed() -> void:
	NetworkHandler.start_server()
	main_menu.hide()
	server_message.visible = true
	

func _on_client_button_pressed() -> void:
	NetworkHandler.start_client()
	if self != null:
		queue_free()
	
