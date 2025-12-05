class_name PlayPile extends Node

var total_value : int = 0
var current_card : PointCard
var modif_cards : Array[CardInterface] = []

func next_turn() -> void:
	for card : ModifierCardInterface in modif_cards:
		card.turns_until_activition -= 1
		
		if card.turns_until_activition <= 0:
			card.activate_effect()
			modif_cards.erase(card)
	

func get_value_and_reset() -> int:
	var value := total_value
	
	total_value = 0
	
	return value
	

func add_card(card : PointCard) -> bool:
	if current_card.value > card.value: return false
	
	current_card = card
	total_value += card.value
	
	for modif_card : ModifierCardInterface in card.modifiers:
		modif_cards.append(modif_card)
		modif_card.apply_deck_modifier(self)
		card.modifiers.erase((modif_card))
	
	return true
	
