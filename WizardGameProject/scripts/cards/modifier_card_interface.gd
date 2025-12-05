@abstract
class_name ModifierCardInterface extends CardInterface

var turns_until_activition : int

func randomize_properties() -> void: pass

@warning_ignore("unused_parameter")
func apply_card_modifier(card : PointCard) -> void: pass

func activate_effect() -> void: pass

@warning_ignore("unused_parameter")
func apply_deck_modifier(playpile : PlayPile) -> void: pass
