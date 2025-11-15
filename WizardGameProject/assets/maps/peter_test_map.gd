extends Node

@export var curr_deck : PointCardDeck

var test_card : BasePointCard

func _ready() -> void:
	test_card = curr_deck.pull_card()
