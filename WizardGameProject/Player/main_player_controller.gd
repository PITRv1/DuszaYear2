class_name Player extends StaticBody3D

@onready var camera: Camera3D = $Camera3D

var table_pos : Vector3
var player_hand : Array[BasePointCard]
var player_hand_size := 4

func _ready() -> void:
	$Camera3D.look_at(table_pos, Vector3.UP)

func _on_point_card_deck_clicked(point_card_deck : PointCardDeck):
	if player_hand.size() < player_hand_size:
		var card : BasePointCard = point_card_deck.pull_card()
		if card: 
			player_hand.append(card)
		

		

func _physics_process(delta: float) -> void:
	if Input.is_key_pressed(KEY_0):
		print(player_hand)
