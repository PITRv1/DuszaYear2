from global_singleton import global_instance;
from game_elements import Card;

class Player:
    def __init__(self, hand_size : int, magic_amount : int, max_item_count : int):
        self.score = 0
        self.hand_size = hand_size
        self.magic_amount = magic_amount
        self.hand : list[Card] = global_instance.deck.draw(5)
        self.can_use_items : bool = True

    def play_card(self):
        
        added_text : str =  " or type PLAY if you're done"
        selected_cards : list[Card] = []

        while True:
            global_instance.game_manager.clear_screen()
            print("---------------| Action : Card selection |---------------------")
            print("")
            
            print("Cards in your hand :")
            for index, card in enumerate(self.hand):
                not_selected_text = f"     {index+1}.   {card.value} of {card.type.name}s"
                selected_text = f"       {index+1}.   {card.value} of {card.type.name}s"

                print(selected_text if card in selected_cards else not_selected_text)
            
            print("")
            


            player_input = input(f"Select the cards by typing the index of the card{ added_text if len(selected_cards) > 0 else "" }. Go back to action screen with BACK.\n> ")
            
            match player_input:
                case "PLAY":
                    if len(selected_cards) == 0: continue

                    global_instance.turn_done = True

                    self.hand.extend(global_instance.deck.draw(len(selected_cards)))
                    global_instance.play_pile.extend(selected_cards)
                    self.can_use_items = True
                    break

                case "BACK":
                    break

                case _:
                    try:
                        if int(player_input) > len(self.hand):
                            print("------------------------------------------------")
                            print("There aren't enough cards in your hand for this.")
                            print("------------------------------------------------")

                        for index, card in enumerate(self.hand):
                            if int(player_input) == index + 1:

                                if card in selected_cards:
                                    selected_cards.remove(card)
                                    continue

                                selected_cards.append(card)
                                print(selected_cards)

                    except:

                        print("--------------------------------------------")
                        print("ERROR:> Please type a valid index (eg.: 1).")
                        print("--------------------------------------------")
                        
                        continue

            

    def blame_player(self):
        prev_player_index : int = (global_instance.current_player_index - 1) % len(global_instance.players)
        previous_player : Player =  global_instance.players[prev_player_index] 
      
        while True:
            global_instance.game_manager.clear_screen()
            print("---------------| Action : Blaming |---------------------")
            print("")
            print(f"You are blaming Player {prev_player_index + 1}.")
            print("")
            print("Confirm you action by typing BLAME or type BACK to go back to the action screen.")
            player_input : str = input("> ")

            match player_input:
                case "BACK":
                    break
                case "BLAME":
                    print("")
                    print("#/----------------------------------\#")
                    print(f"|    The current sample card was:   |")
                    print(f"|           {global_instance.sample_card.value}  of {global_instance.sample_card.type.name}s           |")
                    print(f"|                                   |")
                    print(f"|      Player {prev_player_index}'s top card was:     |")
                    print(f"|           {global_instance.play_pile[-1].value}  of {global_instance.play_pile[-1].type.name}s           |")
                    print(f"|                                   |")
                    print(f"#\---------------------------------/#")
                    print(f"")

                    verdict_text : str = ""
                    punishment_text : str = ""

                    #RESULTS
                    #Case 3
                    if global_instance.sample_card.type != global_instance.play_pile[-1].type and global_instance.sample_card.value > global_instance.play_pile[-1].value:
                        verdict_text = "The cards don't match at all."
                        punishment_text = "The liar looses 1 bar of magic."
                        previous_player.take_magic(1)
                        
                        self.change_sample_card()

                    #Case 2
                    elif global_instance.sample_card.value > global_instance.play_pile[-1].value:
                        verdict_text = "Only the symbol is correct."
                        punishment_text = "The cather redeems the reward."
                        
                        self.reward_player()
                        self.change_sample_card()
                    
                    #Case 1
                    elif global_instance.sample_card.type != global_instance.play_pile[-1].type:
                        verdict_text = "Only the value is correct."
                        punishment_text = "The liar can't use their items in their next turn. The cather redeems the reward."
                        self.can_use_items = False
                        self.reward_player()
                        self.change_sample_card()

                    #Case 4
                    else:
                        verdict_text = "The card was correct."
                        punishment_text = "The blamer looses 1 bar of magic."
                        self.take_magic(1)

                    print("---------== RESULT ==---------")
                    print(f"VERDICT: {verdict_text}")
                    print(f"PUNISHMENT: {punishment_text}")
                    print(f"")


                    print("Type anything to continue")
                    input("> ")

                    global_instance.turn_done = True
                    break


                case _:
                    continue



    def use_item(self):
        pass

    def take_magic(self, amount):
        self.magic_amount -= amount

    def reward_player(self):
        for card in global_instance.play_pile:
            self.score += card.value
            global_instance.play_pile.remove(card)

    def change_sample_card(self):
        global_instance.deck.cards.append(global_instance.sample_card)
        global_instance.deck.shuffle()
        global_instance.sample_card = global_instance.deck.draw(1)[0]

    def get_player_move(self, first_turn : bool = False):

        print("Options:")
        print("        - PLAY CARD")
        if not first_turn: print("        - BLAME PLAYER")
        if self.can_use_items: print("        - USE ITEM")

        player_input : str = input("> ")

        match player_input:
            case "PLAY CARD":
                self.play_card()
                return

            case "BLAME PLAYER":
                if first_turn:
                    print("You can't blame a player on the first turn!")
                    return

                self.blame_player()
                return

            case "USE ITEM":
                if not self.can_use_items:
                    print("You can't use your items on this turn because you got caught")
                self.use_item()
                return

            case _:
                print("Invalid choice!")
                return
    