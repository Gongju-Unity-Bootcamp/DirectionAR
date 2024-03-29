using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Define
{
    public enum SceneNum
	{
		Consent,
		Awake,
		Main,
		ARCamera,
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
		ARCamera,
		Street,
		ARNavigation,
		Call,
		Consent,
	}

	public enum Sound
	{
		Bgm,
		Effect,
		Max,
	}

	public struct Permission
	{
		public const string Call = "android.permission.CALL_PHONE";
		public const string Media = "android.permission.READ_MEDIA_IMAGES";
	}

	public struct Path
	{
		public const string FloderName = "KCCI-C";
		public const string FileName = "AR체험존";
		public const string ExtName = "png";
	}

	public struct Call
	{	
		public const string TestNumber = "01087491220";
		public const string TestNumber2 = "01028552569";
		public const string EmergencyNumber = "112";
	}
}
