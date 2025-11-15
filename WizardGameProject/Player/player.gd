class_name Player extends StaticBody3D

@onready var camera: Camera3D = $Camera3D

var selected_deck : Deck = null
var table_pos : Vector3
var owner_id : int = -1
var is_authority : bool:
	get: return !NetworkHandler.is_server && owner_id == MultiplayerClientGlobals.id

func _ready() -> void:
	$Camera3D.look_at(table_pos, Vector3.UP)
	

func _physics_process(_delta: float) -> void:
	if Input.is_action_just_pressed("left_click"):
		if selected_deck: return
		project_raycast_from_mouse_position()
		
	

func project_raycast_from_mouse_position() -> void:
	var mouse_pos : Vector2 = get_viewport().get_mouse_position()
	var ray_length : int = 100
	var from : Vector3 = camera.project_ray_origin(mouse_pos)
	var to : Vector3 = from + camera.project_ray_normal(mouse_pos) * ray_length
	var space := get_world_3d().direct_space_state
	var ray_query := PhysicsRayQueryParameters3D.new()
	
	ray_query.from = from
	ray_query.to = to
	ray_query.collide_with_areas = true
	ray_query.collide_with_bodies = false
	
	var result := space.intersect_ray(ray_query)
	if len(result) == 0: return
	
	var deck : Deck = result["collider"]
	print(result, "     ", deck)
	deck.select()
	selected_deck = deck
	

#region Legacy, but will be useful later
#func _enter_tree() -> void:
	#MultiplayerServerGlobals.handle_player_position.connect(server_handle_player_position)
	#MultiplayerClientGlobals.handle_player_position.connect(client_handle_player_position)
	#
#
#func _exit_tree() -> void:
	#MultiplayerServerGlobals.handle_player_position.disconnect(server_handle_player_position)
	#MultiplayerClientGlobals.handle_player_position.disconnect(client_handle_player_position)
	#

#func _physics_process(_delta: float) -> void:
	#if !is_authority: return
	#
	#velocity = Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down").normalized() * SPEED
	#
	#move_and_slide()
	#
	#PlayerPosition.create(owner_id, global_position).send(NetworkHandler.server_peer)
	

#func server_handle_player_position(peer_id: int, player_position : PlayerPosition) -> void:
	#if owner_id != peer_id: return
	#
	#global_position = player_position.position
	#
	#PlayerPosition.create(owner_id, global_position).broadcast(NetworkHandler.connection)
	#
#
#func client_handle_player_position(player_position : PlayerPosition) -> void:
	#if is_authority || owner_id != player_position.id: return
	#
	#global_position = player_position.position
#endregion
