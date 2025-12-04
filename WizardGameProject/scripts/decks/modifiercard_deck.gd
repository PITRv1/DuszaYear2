class_name ModifierCardDeck extends DeckInterface

@export var amount : int = 27

func generate_deck() -> void:
	for i in range(0, 27):
		
		var modifcard := ModifierCardMultiplier.new()
		modifcard.randomize_properties()
		
		cards.append(modifcard)
	
	
