class_name Player extends StaticBody3D

@onready var camera: Camera3D = $Camera3D

var selected_deck : DeckDEBRECATED = null
var table_pos : Vector3


func _ready() -> void:
	$Camera3D.look_at(table_pos, Vector3.UP)
	

func _physics_process(_delta: float) -> void:
	if Input.is_action_just_pressed("left_click"):
		if selected_deck != null: return
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
	
	var deck : DeckDEBRECATED = result["collider"]
	print(result, "     ", deck)
	deck.select()
	selected_deck = deck
