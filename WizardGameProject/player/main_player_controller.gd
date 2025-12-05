class_name Player extends StaticBody3D

@onready var camera : Camera3D = $Camera3D
@export var player_ui : PlayerUI

var player_class : PlayerClass = PlayerClass.new()

func _ready() -> void:
	player_ui.player = self
	player_ui.player_class = player_class
	
