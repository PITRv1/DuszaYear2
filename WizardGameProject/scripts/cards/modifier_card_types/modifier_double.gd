class_name ModifierCardmultiplier extends ModifierCardInterface

var amount : int = 1

func apply_card_modifier(card : PointCard) -> void:
	card.value *= amount
	
