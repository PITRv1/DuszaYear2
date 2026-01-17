extends Node3D

## Reference to the AnimationTree node
@export var animation_tree: AnimationTree



## Path to the AnimationNodeStateMachine inside the AnimationTree
## Example: "parameters/StateMachine"
@export var state_machine_path: StringName = "parameters/BootomGear/playback"
@onready var main_menu_cam: Camera3D = $MainMenuCam
@onready var intro_cam: Camera3D = $IntroCam
@onready var rich_text_label: RichTextLabel = $RichTextLabel

var _playback: AnimationNodeStateMachinePlayback
var has_started : bool = false

func _ready() -> void:
	if animation_tree == null:
		push_error("AnimationTree is not assigned.")
		return

	# Ensure the AnimationTree is active
	animation_tree.active = true

	# Get the state machine playback object
	_playback = animation_tree.get(state_machine_path)

	if _playback == null:
		push_error("Failed to get AnimationNodeStateMachinePlayback. Check the state_machine_path.")
		return


# -----------------------------
# Viewpoint transition methods
# -----------------------------

func _unhandled_input(event: InputEvent) -> void:
	if has_started == true: return
	
	if event.is_pressed():
		
		has_started = true
		_playback.travel("intro")


func go_to_main_view_point() -> void:
	_playback.travel("mainViewPoint")


func go_to_bar_view_point() -> void:
	_playback.travel("barViewPoint")


func go_to_shelf_view_point() -> void:
	_playback.travel("shelfViewPoint")


func go_to_play_view_point() -> void:
	_playback.travel("playViewPoint")

func change_cam():
	intro_cam.clear_current()
	main_menu_cam.make_current()
	
	rich_text_label.queue_free()
