class_name Player extends StaticBody3D

@onready var camera: Camera3D = $Camera3D

var table_pos : Vector3
var player_point_card_hand : Array[ActualPointCard]
var player_point_card_hand_size := 4

@onready var left_player_hand: Node3D = %LeftPlayerHand

var selected_point_card : ActualPointCard

var selected_height_offset : float = .2

func _ready() -> void:
	$Camera3D.look_at(table_pos, Vector3.UP)

# FIXME This is ultra jank the way I implamented it but it works and first make it exist then make it good
func add_point_card_to_hand(actual_card : ActualPointCard):
	left_player_hand.add_child(actual_card)
	var offset = 0.3 * player_point_card_hand.find(actual_card)
	actual_card.position.x += offset


func _on_point_card_deck_clicked(point_card_deck : PointCardDeck):
	if player_point_card_hand.size() < player_point_card_hand_size:
		var dummy_card : DummyPointCard = point_card_deck.pull_card()
		if not dummy_card : return
		
		var actual_card := GameData.convert_dummy_to_actual_card(dummy_card)
		
		player_point_card_hand.append(actual_card)
		add_point_card_to_hand(actual_card)


func _on_point_card_clicked(point_card : ActualPointCard):
	if selected_point_card:
		selected_point_card.position.y = 0
	
	point_card.position.y += selected_height_offset
	selected_point_card = point_card
	
	
func _on_play_pile_clicked(play_pile : PlayPile):
	play_pile.recive_actual_card(selected_point_card)
	player_point_card_hand.erase(selected_point_card)
	selected_point_card.queue_free()
