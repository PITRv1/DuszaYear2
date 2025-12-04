extends Node


func _ready() -> void:
	var pointdeck := PointCardDeck.new()
	var modifdeck := ModifierCardDeck.new()
	
	pointdeck.generate_deck()
	modifdeck.generate_deck()
	
	var point_card : PointCard = pointdeck.draw_card()
	print(point_card.value, " ", point_card.rarity)
	
	var modif_card : ModifierCardMultiplier = modifdeck.draw_card()
	modif_card.randomize_properties()
	print(modif_card.amount)
	
	point_card.add_modifier(modif_card)
	print(point_card.value, " ", point_card.rarity)
	
	
