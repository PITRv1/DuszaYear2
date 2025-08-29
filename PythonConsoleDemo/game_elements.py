import random;
from enum import Enum;

class CardType(Enum):
    TYPE_A = 0
    TYPE_B = 1
    TYPE_C = 2
    TYPE_D = 3


class Card:
    def __init__(self, global_id : int, value : int, type : CardType):
        self.global_id = global_id
        
        self.value = value
        self.type = type

class Deck:
    cards = []

    def __init__(self, cards_in_symbol : int):
        self.cards_in_symbol = cards_in_symbol
        
        self.fill()


    def fill(self):
        full_deck : list[Card] = []


        # reset deck before updating it again
        global_card_id = 0
        self.cards = []

        for i in range(len(CardType)):
            curr_card_type : CardType = list(CardType)[i]

            for j in range(self.cards_in_symbol):
                global_card_id += 1
                new_card = Card(global_card_id, j + 1, curr_card_type)
                full_deck.append(new_card)

        self.cards = full_deck


    def draw(self, amount : int):
        result : list[Card] = []

        for i in range(amount):
            result.append(self.cards.pop(-1))

        return result
    

    def shuffle(self):
        if len(self.cards) == 0: return

        for i in range(len(self.cards) - 1, 0, -1):
            j = random.randint(0, i)
            self.cards[i], self.cards[j] = self.cards[j], self.cards[i]