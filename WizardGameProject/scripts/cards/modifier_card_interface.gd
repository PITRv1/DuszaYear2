@abstract
class_name ModifierCardInterface extends CardInterface

var turn_until_activition : int

func randomize_properties() -> void: pass

@warning_ignore("unused_parameter")
func apply_card_modifier(card : PointCard) -> void: pass

func activate_effect() -> void: pass

func apply_deck_modifier() -> void: pass
