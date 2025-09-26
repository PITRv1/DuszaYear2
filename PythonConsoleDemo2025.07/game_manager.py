from global_singleton import global_instance;
from game_elements import *;
from player import Player;
import os;



class GameManager:
    def __init__(self, cards_in_symbol : int, player_count : int, player_handsize : int, player_magic_amount : int, player_max_item_count : int):
        global_instance.game_manager = self
        global_instance.deck = Deck(cards_in_symbol)
        global_instance.deck.shuffle()
        global_instance.sample_card = global_instance.deck.draw(1)[0]

        for i in range(player_count):
            global_instance.players.append(Player(player_handsize, player_magic_amount, player_max_item_count))

    def start_game(self):
        self.clear_screen()
        print("")
        print("===============| The Wizard Card game |==================")
        print("README:")
        print("This is a prototype for the undefined team's project for 2025/2026")
        print("If I care enough I will write down how to play here but I can't be bothered rn.")
        print("=========================================================")
        print("")

        global_instance.current_player_index = 0

        print("Type anything to continue")
        input("> ")
        self.turn(is_first=True)

    def turn(self, is_first : bool = False):

        curr_player : Player = global_instance.players[global_instance.current_player_index]
        global_instance.turn_done = False

        while not global_instance.turn_done:
            self.clear_screen()
            print(f"###############| Turn {global_instance.global_turn_count} |###############")
            print("")
            print("Turn information:")
            print(f"It's Player {global_instance.current_player_index + 1}'s turn.")
            print(f"The current sample card is {global_instance.sample_card.value} of {global_instance.sample_card.type.name}s.")
            print(f"Number of cards on the Play pile: {len(global_instance.play_pile)}")
            print(f"Player's magic amount: {curr_player.magic_amount}")
            print(f"Player's score is amount: {curr_player.score}")

            print("")
            curr_player.get_player_move(is_first)
        

        global_instance.global_turn_count += 1

        # Handle choosing next player and overflowing from the 4th to the 1st player.
        global_instance.current_player_index = (global_instance.current_player_index + 1) % len(global_instance.players)
        
        self.start_next_turn()

    def start_next_turn(self):
        self.turn()

    def end_game(self):
        pass

    
    def clear_screen(self):
        os.system('cls' if os.name == 'nt' else 'clear')
