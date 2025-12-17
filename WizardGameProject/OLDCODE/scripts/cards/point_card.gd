class_name PointCard extends CardInterface


signal value_changed(value : int)
signal rarity_changed(value : RARITIES)

var modifiers : Array[ModifierCardInterface] = []

var _default_value : int
var _value : int
var value : int : 
	set(new_value):
		_value = new_value
		value_changed.emit(new_value)
		
		if _default_value == 0:
			_default_value = new_value
	get(): return _value

var _default_rarity : RARITIES
var _rarity : RARITIES
var rarity : RARITIES :
	set(new_value):
		_rarity = new_value
		rarity_changed.emit(new_value)
	get(): return _rarity


func _ready() -> void:
	_default_value = value
	_default_rarity = rarity
	

func randomize_properties() -> void:
	randomize_rarity()
	randomize_value()
	

func randomize_rarity() -> void:
	rarity = [0,0,0,0,0,0,0,0,0,0,1,1,1,1,2,2,3].pick_random() as RARITIES
	_default_rarity = rarity
	

func randomize_value() -> void:
	value = randi_range(1, 9)
	_default_value = value
	

func add_modifier(modifier : ModifierCardInterface) -> bool:
	if len(modifiers) < int(rarity):
		modifier.apply_card_modifier(self)
		modifiers.insert(0, modifier)
		return true
	else:
		return false
	

func remove_modifier(modifier : ModifierCardInterface) -> void:
	if modifiers.has(modifier):
		modifiers.erase(modifier)
		reapply_modifiers()
	

func reapply_modifiers() -> void:
	value = _default_value
	rarity = _default_rarity
	
	for modifier : ModifierCardInterface in modifiers:
		modifier.apply_card_modifier(self)
