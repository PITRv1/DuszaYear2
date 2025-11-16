extends Node

enum PointCardRarities {
	COMMON = 1,
	EPIC = 2,
	LEGENDARY = 3
}

const actual_point_card_template : PackedScene = preload("uid://cfh3o72mn10g")


func convert_dummy_to_actual_card(dummy_card : DummyPointCard) -> ActualPointCard:
	var new_actual_point_card : ActualPointCard = actual_point_card_template.instantiate()
	new_actual_point_card.value = dummy_card.value
	new_actual_point_card.rarity = dummy_card.rarity

	return new_actual_point_card
