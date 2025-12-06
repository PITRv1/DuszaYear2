class_name Player extends StaticBody3D


signal place_card(card : UIPointCard)
signal modif_deck_selected

@onready var camera : Camera3D = $Camera3D
@export var player_ui : PlayerUI

var player_class : PlayerClass = PlayerClass.new()
var selected_deck : int = 0

func _ready() -> void:
	player_ui.player = self
	player_ui.player_class = player_class
	

func _physics_process(_delta: float) -> void:
	if Input.is_action_just_pressed("left_click"):
		var result : Area3D = project_raycast_from_mouse_position()
		
		if result:
			match result.get_groups()[0]:
				"modif_deck":
					if selected_deck != 0: return
					
					if result.get_parent().name.contains("2"): selected_deck = 2
					else: selected_deck = 1
					
					modif_deck_selected.emit()
				"play_pile":
					if player_ui.selected_point_card:
						# TODO: More check bc it causes bugs!!!!
						
						var card := player_ui.selected_point_card
						
						for modif_card : ModifierCardInterface in player_ui.selected_point_card.card.modifiers:
							player_class.modif_cards.erase(modif_card)
						
						player_ui.selected_point_card = null
						
						place_card.emit(card)
						
		


func project_raycast_from_mouse_position() -> Area3D:
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
	if len(result) == 0: return null
	
	return result["collider"]
	
	
