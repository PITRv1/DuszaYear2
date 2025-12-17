class_name PlayerSpawner extends Node

@export var player_template_scene : PackedScene = preload("uid://ctvoi5v6q42fp")
@export var player_seat_group_name : StringName = "player_seat_position"

@export var player_look_pos : Marker3D

func spawn_player() -> Player:
	var new_player : Player = player_template_scene.instantiate() as Player
	var player_seat : PlayerSeat

	var seats := get_tree().get_nodes_in_group(player_seat_group_name)
	
	for seat : PlayerSeat in seats:
		if seat.is_open:
			player_seat = seat
	
	new_player.table_pos = player_look_pos.global_position
	player_seat.add_child(new_player)
	player_seat.is_open = false
	
	return new_player
