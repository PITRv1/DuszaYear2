class_name PlayerPlayableClassInterface extends Node

@warning_ignore("unused_signal")
signal passive_used
@warning_ignore("unused_signal")
signal active_used

var classname : String
var active_cooldown : int
var passive_cooldown : int

@warning_ignore("unused_parameter")
func use_passive(player : PlayerClass, modif_deck : ModifierCardDeck, point_deck : PointCardDeck) -> void: pass

@warning_ignore("unused_parameter")
func use_active(player : PlayerClass, modif_deck : ModifierCardDeck, point_deck : PointCardDeck) -> void: pass
