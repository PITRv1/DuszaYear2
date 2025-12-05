extends Control

@onready var inside_camera = $"../inside_camera"
@onready var main_screen = $MainScreen
@onready var main_menu_screen = $MainMenuScreen
@onready var animation_player = $AnimationPlayer


var key_pressed : bool = false
	
func _input(event):
	if event is not InputEventMouseMotion and !key_pressed:
		animation_player.play("key_pressed")
		main_screen.visible = false
		key_pressed = true

func _on_animation_player_animation_finished(_anim_name):
	main_menu_screen.visible = true
