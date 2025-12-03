class_name PointCardDeck extends DeckInterface

@export_range(1, 9, 1) var card_types : int = 9
@export_range(1, 9, 1) var cards_per_type : int = 3

func generate_deck() -> void:
	for point_card_num in range(1, card_types):
		for per_type in range(0, cards_per_type):
			var point_card := PointCard.new()
			point_card.value = point_card_num
			point_card.randomize_rarity()
			cards.append(point_card)
			
	
	cards.shuffle()
	
