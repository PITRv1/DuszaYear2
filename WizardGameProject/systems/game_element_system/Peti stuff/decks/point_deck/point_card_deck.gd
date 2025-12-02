class_name PointCardDeck extends Node

@export var size : int = 52

@export var interaction_area : Area3D

var card_list : Array[DummyPointCard]


func _ready() -> void:
	for i in range(size):
		card_list.append(DummyPointCard.new(randi_range(1,9), randi_range(GameData.PointCardRarities.COMMON, GameData.PointCardRarities.LEGENDARY) ))

func pull_card() -> DummyPointCard:
	if card_list.is_empty(): return
	
	return card_list.pop_front()


func _on_input_event_recieved(camera: Node, event: InputEvent, event_position: Vector3, normal: Vector3, shape_idx: int) -> void:
	if event is InputEventMouseButton:
		if event.is_action_pressed("left_click"):
			var interacting_player : Player = camera.get_parent()
			interacting_player._on_point_card_deck_clicked(self)
