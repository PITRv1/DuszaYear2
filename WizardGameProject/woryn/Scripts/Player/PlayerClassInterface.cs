using Godot;
using System;

public interface PlayerClassInterface
{
	public string ClassName { get; }
    public byte ActiveCooldown { get; }
    public byte PassiveCooldown { get; }
    public void UseActive();
    public void UsePassive();
}