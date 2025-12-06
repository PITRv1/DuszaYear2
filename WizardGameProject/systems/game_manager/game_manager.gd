class_name GameManager extends Node

@export var player1 : Player
@export var play_pile_3d : PlayPile3D

var point_card_deck := PointCardDeck.new()
var modifier_card_deck1 := ModifierCardDeck.new()
var modifier_card_deck2 := ModifierCardDeck.new()
var play_pile := PlayPile.new()

func _process(_delta: float) -> void:
	play_pile_3d.current_value_label.text = str(play_pile.current_card.value)
	

func _ready() -> void:
	player1.place_card.connect(receive_card_from_player)
	player1.modif_deck_selected.connect(func(): give_cards_to_player(player1))
	
	point_card_deck.generate_deck()
	modifier_card_deck1.generate_deck()
	modifier_card_deck2.generate_deck()
	
	point_card_deck.out_of_cards.connect(func(): print("Ayo hayo ayoo!!"))
	

func give_cards_to_player(player : Player) -> void:
	print(len(player.player_class.modif_cards))
	for i in range(0, 4-len(player.player_class.point_cards)):
		player.player_ui.add_card_to_ui(point_card_deck.draw_card())
	
	for i in range(0, 4-len(player.player_class.modif_cards)):
		var current_modif_deck := modifier_card_deck1 if player1.selected_deck == 1 else modifier_card_deck2
		player.player_ui.add_card_to_ui(current_modif_deck.draw_card())
	


func receive_card_from_player(card : UIPointCard):
	if play_pile.add_card(card.card):
		player1.player_class.point_cards.erase(card.card)
		player1.player_ui.remove_card_from_ui(card)
		give_cards_to_player(player1)
	else: 
		print("Too bad!")
		
