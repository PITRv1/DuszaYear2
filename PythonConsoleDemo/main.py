from global_singleton import global_instance;
from game_manager import GameManager;
from game_elements import *;

game_manager : GameManager = GameManager(
    cards_in_symbol=10,
    player_count=2,
    player_handsize=5,
    player_magic_amount=3,
    player_max_item_count=4
    )

game_manager.start_game()
