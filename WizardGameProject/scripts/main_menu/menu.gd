extends Control

@onready var animation_player = $"../AnimationPlayer"
@onready var path = $"../../OutSidePath/PathFollow3D"
@onready var main_menu_screen = $"."

@export var map : PackedScene
func _on_play_pressed():
	path.progress_ratio = 0.7
	animation_player.play("start_game")
	main_menu_screen.visible = false


func _on_exit_pressed():
	get_tree().quit()


func _on_animation_player_animation_finished(anim_name):
	if anim_name == "start_game":
		get_tree().change_scene_to_packed(map)
