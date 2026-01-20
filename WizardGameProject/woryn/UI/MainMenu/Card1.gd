extends NinePatchRect

var tween
func _process(_delta):
	if(Input.is_action_just_pressed("ui_accept")):
		tween = get_tree().create_tween()
		tween.set_parallel(true).set_trans(Tween.TRANS_EXPO).set_ease(Tween.EASE_OUT)
		tween.tween_property(self, "modulate", Color.RED, 1.0)
		tween.tween_property(self, "position", self.position + Vector2(-200.0,-110.0), 1.0)
		tween.tween_property(self, "rotation", self.rotation + -.5, 1.0)
