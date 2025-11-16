class_name GameManager extends Node



@export var game_settings : GameSettings

@export_group("Map elements")
@export var world_holder : Node

var current_3d_scene : Node3D

var point_card_deck : PointCardDeck = null
var player_spawner : PlayerSpawner = null


func _init() -> void:
	Global.game_manager = self

func _ready() -> void:
	if world_holder.get_child_count():
		current_3d_scene = world_holder.get_child(0)
	
	# HACK this is unholy and should be made better
	for child in current_3d_scene.get_children(true):
		
		if (not point_card_deck) and (child is PointCardDeck):
			point_card_deck = child
			continue
		
		if (not player_spawner) and (child is PlayerSpawner):
			player_spawner = child
			continue
			
		if point_card_deck and player_spawner:
			break
		
	assert(point_card_deck != null)
	assert(player_spawner != null)
	
	setup_players()
	

func game_cycle_update() : pass



func setup_players():
	for player in range(game_settings.player_count + 1):
		player_spawner.spawn_player()




func change_3d_scene(new_scene : StringName, delete : bool = true, keep_running : bool = false) -> void :
	if current_3d_scene != null:
		if delete:
			current_3d_scene.queue_free()
		elif keep_running:
			current_3d_scene.visible = false
		else:
			world_holder.remove_child(current_3d_scene)
	
	var new = load(new_scene).instantiate()
	world_holder.add_child(new)
	current_3d_scene = new
