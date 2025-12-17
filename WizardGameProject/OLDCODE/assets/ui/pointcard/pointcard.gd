class_name UIPointCard extends Control

signal selected(card : UIPointCard)

@onready var top_left: Label = $MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/TopLeft
@onready var bottom_right: Label = $MarginContainer/VBoxContainer/HBoxContainer2/VBoxContainer/BottomRight
@onready var background: ColorRect = $Background
@onready var modifier_container: VBoxContainer = $ModifierContainer
@onready var border: ColorRect = $Border

var card : PointCard
var colors : Array[Color] = [Color.WHITE, Color.AQUAMARINE, Color.MEDIUM_SLATE_BLUE, Color.GOLD]
var is_hovered : bool = false
var is_selected : bool = false

func _ready() -> void:
	custom_minimum_size = size
	if card != null:
		background.color = colors[card.rarity as int]
		top_left.text = str(card.value)
		bottom_right.text = str(card.value)
		card.value_changed.connect(edit_value_information)
		
	

func ui_add_modifier(modifier : UIModifierCard):
	if card.add_modifier(modifier.card):
		modifier.parent_card = self
		modifier.minimize_card()
		modifier.reparent(modifier_container)
		

func ui_remove_modifier(modifier : UIModifierCard):
	card.remove_modifier(modifier.card)
	modifier.parent_card = null
	modifier.maximize_card()
	

func _process(_delta: float) -> void:
	modifier_container.mouse_filter = MOUSE_FILTER_STOP if modifier_container.get_child_count() > 0 else MOUSE_FILTER_IGNORE  
	
	if is_selected: return
	
	if is_hovered:
		border.color = Color.YELLOW
	elif border.color != Color.from_string("#a3a3a3", Color.RED):
		border.color = Color.from_string("#a3a3a3", Color.RED)
	

func edit_value_information(new : int):
	top_left.text = str(new)
	bottom_right.text = str(new)
	

func _on_mouse_entered() -> void:
	is_hovered = true
	

func _on_mouse_exited() -> void:
	is_hovered = false
	

func _on_selected() -> void:
	selected.emit(self)
	
