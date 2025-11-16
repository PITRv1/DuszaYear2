class_name ActualPointCard extends Node3D

var value : int
var rarity : GameData.PointCardRarities
var modifier_list : Array[BaseModifier]


func _on_card_clicked_input_event(camera: Node, event: InputEvent, event_position: Vector3, normal: Vector3, shape_idx: int) -> void:
	if event is InputEventMouseButton:
		if event.is_action_pressed("left_click"):
			var interacting_player : Player = camera.get_parent()
			interacting_player._on_point_card_clicked(self)
