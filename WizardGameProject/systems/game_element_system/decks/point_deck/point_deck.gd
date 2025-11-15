class_name PointCardDeck extends Node

@export var size : int = 52
var card_list : Array[BasePointCard]


func _ready() -> void:
	for i in range(size):
		card_list.append(BasePointCard.new(randi_range(1,9), BasePointCard.PointCardRarities.COMMON ))

func pull_card() -> BasePointCard:
	return card_list.pop_front()
