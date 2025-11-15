class_name Deck extends Area3D


func select() -> void:
	var mesh : StandardMaterial3D = StandardMaterial3D.new()
	mesh.albedo_color = Color.YELLOW
	$MeshInstance3D.mesh.material = mesh
	
