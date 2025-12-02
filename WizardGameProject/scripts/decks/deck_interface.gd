@abstract
class_name DeckInterface extends Node

var cards : Array[CardInterface]
var card_types : Array[CardInterface]
var number_of_cards_per_type : int


func _ready() -> void:
	pass

func generate_deck() -> void:
	for i in range(number_of_cards_per_type):
		for card in card_types:
			pass
