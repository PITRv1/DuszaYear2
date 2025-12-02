class_name PlayPile extends Area3D

var item_list : Array[int]
var visual_template: PackedScene = preload("uid://bjnqdsim5yh6i")
@onready var card_visual_place_pos: Marker3D = $CardVisualPlacePos

func recive_actual_card(card : ActualPointCard):
	item_list.push_back(card.value)
	add_card_visual_to_stack()

func add_card_visual_to_stack():
	var new_card_visual : CSGBox3D = visual_template.instantiate()
	self.add_child(new_card_visual)
	new_card_visual.global_position = card_visual_place_pos.global_position
	
	new_card_visual.position.y += .2 * item_list.size()
	

func _on_input_event(camera: Node, event: InputEvent, event_position: Vector3, normal: Vector3, shape_idx: int) -> void:
	if event is InputEventMouseButton:
		if event.is_action_pressed("left_click"):
			var interacting_player : Player = camera.get_parent()
			interacting_player._on_play_pile_clicked(self)
