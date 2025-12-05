extends Node

@onready var ui_player_point_cards: HBoxContainer = $Control/VBoxContainer/HBoxContainer/PointCards
@onready var ui_player_modifier_cards: HBoxContainer = $Control/VBoxContainer/HBoxContainer/ModifierCards


func _ready() -> void:
	var pointdeck := PointCardDeck.new()
	var modifdeck := ModifierCardDeck.new()
	
	pointdeck.generate_deck()
	modifdeck.generate_deck()
	
	var player1 : = PlayerClass.new()
	
	for o in range(0,2):
		for i in range(0,4):
			if o == 0:
				player1.modif_cards.append(modifdeck.draw_card())
			else:
				player1.point_cards.append(pointdeck.draw_card())
				
	
	#for card : PointCard in player1.point_cards:
		#var ui_point_card_instance : UIPointCard = ui_point_card_scene.instantiate()
		#ui_point_card_instance.card = card
		#ui_player_point_cards.add_child(ui_point_card_instance)
	#
	#for card in player1.modif_cards:
		#var ui_card_instance : UIModifierCard = ui_modifier_card_scene.instantiate()
		#ui_card_instance.card = card
		#ui_player_modifier_cards.add_child(ui_card_instance)
	
