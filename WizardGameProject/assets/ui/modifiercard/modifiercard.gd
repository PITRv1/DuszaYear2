class_name UIModifierCard extends Control

@warning_ignore("unused_signal")
signal selected(card : UIModifierCard)

@onready var top_left: Label = $MarginContainer/VBoxContainer/HBoxContainer/VBoxContainer/TopLeft
@onready var bottom_right: Label = $MarginContainer/VBoxContainer/HBoxContainer2/VBoxContainer/BottomRight
@onready var border: ColorRect = $border
@onready var rich_text_label: RichTextLabel = $CenterContainer/RichTextLabel

var card : ModifierCardInterface
var is_hovered : bool = false
var is_selected : bool = false
var is_maximized : bool = true
var parent_card : UIPointCard

func minimize_card() -> void:
	top_left.add_theme_font_size_override("font_size", 30)
	rich_text_label.visible = false
	bottom_right.visible = false
	custom_minimum_size = Vector2(59.85, 82.95)
	size = Vector2(59.85, 82.95)
	is_maximized = false

func maximize_card() -> void:
	top_left.add_theme_font_size_override("font_size", 55)
	rich_text_label.visible = true
	bottom_right.visible = true
	custom_minimum_size = Vector2(171.0, 237.0)
	size = Vector2(171.0, 237.0)
	is_maximized = true
	

func _ready() -> void:
	custom_minimum_size = size
	
	if card is ModifierCardMultiplier: 
		top_left.text = "*"+str(card.amount)
		bottom_right.text = "*"+str(card.amount)
		
	
func _process(_delta: float) -> void:
	if is_selected: return
	
	if is_hovered:
		border.color = Color.YELLOW
	elif border.color != Color.from_string("#a3a3a3", Color.RED):
		border.color = Color.from_string("#a3a3a3", Color.RED)
	

func _on_mouse_entered() -> void:
	is_hovered = true
	

func _on_mouse_exited() -> void:
	is_hovered = false
	

func _on_selected() -> void:
	selected.emit(self)
	
