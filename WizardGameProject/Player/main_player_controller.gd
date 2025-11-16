class_name Player extends StaticBody3D

@onready var camera: Camera3D = $Camera3D

var table_pos : Vector3

func _ready() -> void:
	$Camera3D.look_at(table_pos, Vector3.UP)

func _on_point_card_deck_clicked(point_card_deck : PointCardDeck):
	print(point_card_deck.pull_card().value)
	print(point_card_deck.pull_card().rarity)
	
