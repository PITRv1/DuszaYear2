class_name PlayerUI extends Control

@export var ui_point_card : PackedScene
@export var ui_modifier_card : PackedScene
@export var pointcard_container : Control
@export var modifiercard_container : Control

var player : Player
var player_class := PlayerClass.new()

var _selected_point_card : UIPointCard
var selected_point_card : UIPointCard :
	set(value):
		if _selected_point_card != null: 
			_selected_point_card.border.color = Color.from_string("#a3a3a3", Color.RED)
			_selected_point_card.is_selected = false
			
		
		_selected_point_card = value
		
		if value == null: return
		_selected_point_card.is_selected = true
		_selected_point_card.border.color = Color.CYAN
	get(): return _selected_point_card


func add_card_to_ui(card : CardInterface):
	if card is PointCard:
		var ui_card : UIPointCard = create_point_card_ui(card)
		ui_card.selected.connect(handle_card_selection)
		
		pointcard_container.add_child(ui_card)
		player_class.point_cards.append(card)
	else: 
		var ui_card : UIModifierCard = create_modifier_card_ui(card)
		ui_card.selected.connect(handle_card_selection)
		print(ui_card)
		modifiercard_container.add_child(ui_card)
		player_class.modif_cards.append(card)
		

func remove_card_from_ui(card : UIPointCard) -> void:
	if pointcard_container.get_children().has(card):
		pointcard_container.remove_child(card)
	

func create_point_card_ui(card : PointCard) -> UIPointCard:
	var ui_point_card_instance : UIPointCard = ui_point_card.instantiate()
	ui_point_card_instance.card = card
	
	return ui_point_card_instance
	

func create_modifier_card_ui(card : ModifierCardInterface) -> UIModifierCard:
	var ui_modifier_card_instance : UIModifierCard = ui_modifier_card.instantiate()
	ui_modifier_card_instance.card = card
	
	return ui_modifier_card_instance
	

func handle_card_selection(card : Control) -> void:
	if card is UIPointCard: 
		if selected_point_card == card:
			selected_point_card = null
			return
		selected_point_card = card
	elif card is UIModifierCard && selected_point_card != null:
		if card.parent_card == null:
			selected_point_card.ui_add_modifier(card)
		else:
			card.parent_card.ui_remove_modifier(card)
			card.reparent(modifiercard_container)
		
		
