extends Node


func _ready() -> void:
	var pointdeck := PointCardDeck.new()
	pointdeck.generate_deck()
	
	
