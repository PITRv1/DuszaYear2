using Godot;
using System;

// In C#, you call interfaces like IPlayerClass
// BTW, class is very counterintuitive, as class is a general word in programming. I know
// class here means wizard and such, but I'd somehow differentiate between these to make it
// 100% clear.
public interface PlayerClassInterface
{
	public string ClassName { get; set; }
    public int ActiveCooldown { get; set; }
    public int PassiveCooldown { get; set; }
    public void UseActive();
    public void UsePassive();
}