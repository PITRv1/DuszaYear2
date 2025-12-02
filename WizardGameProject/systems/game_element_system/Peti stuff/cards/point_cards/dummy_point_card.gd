class_name DummyPointCard extends Node

@export_range(1, 9, 1.0) var value : int

var rarity : GameData.PointCardRarities = GameData.PointCardRarities.COMMON


func _init(card_value : int, card_rarity : GameData.PointCardRarities) -> void:
	value = card_value
	rarity = card_rarity
