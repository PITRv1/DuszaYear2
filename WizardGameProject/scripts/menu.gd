extends VBoxContainer

@export var map : PackedScene
func _on_play_pressed():
	get_tree().change_scene_to_packed(map)


func _on_exit_pressed():
	get_tree().quit()
