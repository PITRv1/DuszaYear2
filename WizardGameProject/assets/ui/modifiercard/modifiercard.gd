class_name UIModifierCard extends Control

@onready var top_left: Label = $MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/TopLeft
@onready var bottom_right: Label = $MarginContainer/VBoxContainer/HBoxContainer2/VBoxContainer/BottomRight

var card : ModifierCardInterface

func _ready() -> void:
	custom_minimum_size = size
	
	if card is ModifierCardMultiplier: 
		top_left.text = "*"+str(card.amount)
		bottom_right.text = "*"+str(card.amount)
		
	
