@abstract
class_name DeckInterface extends Node

@warning_ignore("unused_signal")
signal out_of_cards

var cards : Array[CardInterface]

func generate_deck() -> void: pass

func draw_card() -> CardInterface:
	var card : CardInterface = cards.pop_front()
	
	if len(cards) == 0: out_of_cards.emit()
	
	return card
	
