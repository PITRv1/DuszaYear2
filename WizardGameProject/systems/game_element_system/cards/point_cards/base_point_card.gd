class_name BasePointCard extends Node

enum PointCardRarities {
	COMMON = 1,
	EPIC = 2,
	LEGENDARY = 3
}


@export_range(1, 9, 1.0) var value : int
@export var rarity : PointCardRarities = PointCardRarities.COMMON


var modifier_list : Array[BaseModifier]


@onready var max_modifiers : int = rarity


func _init(card_value : int, card_rarity : PointCardRarities) -> void:
	value = card_value
	rarity = card_rarity


func apply_modifier( modfier : BaseModifier ): 
	if len(modifier_list) >= max_modifiers: return
	modifier_list.push_front(modfier)

func remove_modifier( modifier : BaseModifier ):
	if !modifier_list.has(modifier): return
	modifier_list.erase(modifier)

# TODO Figure out how to make modifiers:
# Could use resources to represent cards 
# 
# Possible issue: I want to make modif decks with a few modifiers in such a way that you can specify the card and then set the amount of it that should be in that deck
# Idk how to do this yet


# HACK sry Dani kivettem mindent es megkerulom azt ami idaig volt
