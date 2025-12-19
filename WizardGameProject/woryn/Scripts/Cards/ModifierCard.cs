using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public  abstract partial class ModifierCard : Node, CardInterface
{
	public string CardName { get; private set; }
}
