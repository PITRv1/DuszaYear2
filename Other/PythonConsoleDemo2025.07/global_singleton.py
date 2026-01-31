from game_elements import *;

class Global:
    game_manager = None
    deck : Deck
    sample_card : Card
    play_pile : list[Card] = []
    players : list = []
    global_turn_count : int = 1

    turn_done : bool = False
    current_player_index : int = 0

global_instance : Global = Global()