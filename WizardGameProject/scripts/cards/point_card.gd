class_name PointCard extends CardInterface


signal value_changed(value : int)
signal rarity_changed(value : RARITIES)

var modifiers : Array[ModifierCardInterface]

var _value : int
var value : int : 
	set(new_value):
		_value = new_value
		value_changed.emit(new_value)
	get(): return _value

var _rarity : RARITIES
var rarity : RARITIES :
	set(new_value):
		_rarity = new_value
		rarity_changed.emit(new_value)
	get(): return _rarity

var _default_value : int
var _default_rarity : RARITIES

func _ready() -> void:
	_default_value = value
	_default_rarity = rarity
	

func add_modifier(modifier : ModifierCardInterface) -> bool:
	if len(modifiers) < rarity as int:
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
