using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Define
{
	public enum SceneNum
	{
		Awake,
		Main,
	}

	public enum UIEvent
	{
		Click,
		Pressed,
		PointerDown,
		PointerUp,
	}

	public enum SceneType
	{
		Unknown,
		Main,
		Navigation,
		ARZone,
		Street,
	}

	public enum Sound
	{
		Bgm,
		Effect,
		Max,
	}
}
