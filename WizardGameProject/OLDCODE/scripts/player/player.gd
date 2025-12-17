class_name PlayerClass extends Node


var point_cards : Array[PointCard] = []
var modif_cards : Array[ModifierCardInterface] = []
var chosen_class : PlayerPlayableClassInterface
var effect_status : String = "none"


func decrease_cooldown()  -> void:
	chosen_class.active_cooldown -= 1
	chosen_class.passive_cooldown -= 1
	


func pull_card_from_deck(deck : DeckInterface) -> bool:
	if deck is PointCardDeck:
		if len(point_cards) == 4: return false
		point_cards.append(deck.draw_card())
	elif deck is ModifierCardDeck:
		if len(modif_cards) == 4: return false
		modif_cards.append(deck.draw_card())
	
	return true
	

func add_modifier_to_card(point_card : PointCard, modif_card : ModifierCardInterface) -> bool:
	var result := point_card.add_modifier(modif_card)
	
	if result: modif_cards.erase(modif_card)
	
	return result
	

func remove_modifier_from_card(point_card : PointCard, modif_card : ModifierCardInterface) -> void:
	point_card.remove_modifier(modif_card)
	

func play_card(card : PointCard, play_pile : PlayPile) -> void: 
	point_cards.erase(card)
	play_pile.add_card(card)
	
