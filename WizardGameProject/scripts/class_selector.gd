extends Control

var player_class : String
@onready var class_selector = $"."
@onready var title = $title
@onready var classes = $Classes


func _ready():
	pass # Replace with function body.

func _process(_delta):
	if player_class != "":
		title.text = player_class


func _on_thief_pressed():
	player_class = "Thief"
	classes.visible = false


func _on_magicer_pressed():
	player_class = "Magicer"
	classes.visible = false
