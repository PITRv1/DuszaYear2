class_name GameManager extends Node

@export var player : Player

var point_card_deck := PointCardDeck.new()
var modifier_card_deck1 := ModifierCardDeck.new()
var modifier_card_deck2 := ModifierCardDeck.new()
var play_pile := PlayPile.new()

func _ready() -> void:
	point_card_deck.generate_deck()
	modifier_card_deck1.generate_deck()
	modifier_card_deck2.generate_deck()
	
	for i in range(0, 4):
		player.player_ui.add_card_to_ui(point_card_deck.draw_card())
		player.player_ui.add_card_to_ui(modifier_card_deck1.draw_card())
	
