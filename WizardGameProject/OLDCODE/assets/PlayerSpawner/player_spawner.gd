extends Node

# @export var player : PackedScene

# @export var look_pos: Marker3D
# @export var seats : Array[Marker3D]

# func _ready() -> void:
# 	NetworkHandler.on_peer_connected.connect(spawn_player_lobotomized)
# 	MultiplayerClientGlobals.handle_local_id_assignment.connect(spawn_player)
# 	MultiplayerClientGlobals.handle_remote_id_assignment.connect(spawn_player_lobotomized)
	

# func spawn_player_lobotomized(id : int):
# 	var mesh_instance : MeshInstance3D = MeshInstance3D.new()
# 	mesh_instance.mesh = CapsuleMesh.new()
# 	mesh_instance.position.y += 1.0
	
# 	seats[id].call_deferred("add_child", mesh_instance)
	

# func spawn_player(id: int) -> void:
# 	var player_instance : Player = player.instantiate()
# 	player_instance.owner_id = id
# 	player_instance.name = str(id)
# 	player_instance.table_pos = look_pos.global_position
	
# 	seats[id].add_child(player_instance)
	
# 	await player_instance.ready
# 	await player_instance.camera.ready
	
# 	player_instance.camera.current = true
	
