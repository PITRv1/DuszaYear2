class_name DeckDEBRECATED extends Area3D

@export var meshinstance : MeshInstance3D

func select() -> void:
	var material : StandardMaterial3D = StandardMaterial3D.new()
	material.albedo_color = Color.YELLOW
	
	meshinstance.set_surface_override_material(0, material)
	
