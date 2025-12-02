extends Node


func _ready() -> void:
	var pointcard := PointCard.new()
	pointcard.value = 2
	pointcard.rarity = CardInterface.RARITIES.LEGENDARY
	
	print(pointcard.value)
	
	
	var modif1 : = ModifierCardmultiplier.new()
	modif1.amount = 6
	pointcard.add_modifier(modif1)
	
	print(pointcard.value)
	
