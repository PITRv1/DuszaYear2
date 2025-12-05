class_name UIPointCard extends Control

@onready var top_left: Label = $MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/TopLeft
@onready var bottom_right: Label = $MarginContainer/VBoxContainer/HBoxContainer2/VBoxContainer/BottomRight
@onready var background: ColorRect = $Background
@onready var modifier_container: HBoxContainer = $ModifierContainer
@onready var border: ColorRect = $Border

var card : PointCard
var colors : Array[Color] = [Color.WHITE, Color.AQUAMARINE, Color.MEDIUM_SLATE_BLUE, Color.GOLD]
var is_hovered : bool = false

func _ready() -> void:
	custom_minimum_size = size
	if card != null:
		background.color = colors[card.rarity as int]
		top_left.text = str(card.value)
		bottom_right.text = str(card.value)
		
	

func ui_add_modifier(modifier : UIModifierCard):
	modifier.reparent(modifier_container, false)
	

func _process(_delta: float) -> void:
	if is_hovered:
		border.color = Color.YELLOW
	elif border.color != Color.from_string("#a3a3a3", Color.RED):
		border.color = Color.from_string("#a3a3a3", Color.RED)
	


func _on_mouse_entered() -> void:
	is_hovered = true


func _on_mouse_exited() -> void:
	is_hovered = false
