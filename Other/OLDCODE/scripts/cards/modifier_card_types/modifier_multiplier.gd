class_name ModifierCardMultiplier extends ModifierCardInterface


var amount : int = 2

func apply_card_modifier(card : PointCard) -> void:
	card.value *= amount
	

func randomize_properties() -> void:
	amount = randi_range(2, 6)
	
