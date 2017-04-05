using UnityEngine;
using System.Collections;

namespace ChrsUtils
{
    namespace ChrsExtensionMethods
    { 
		/*--------------------------------------------------------------------------------------*/
		/*																						*/
		/*	ExtensionMethods: Convenience methods												*/
		/*																						*/
		/*		Functions:																		*/
		/* 			public:																		*/					
		/*				static Vector2 ToVector2(this Vector3 vec3)								*/
		/*				static float ToDegree(this float radians)								*/
		/*				static float ToRadians (this float degrees)								*/
		/*																						*/
		/*--------------------------------------------------------------------------------------*/
		public static class ExtensionMethods
		{
			/*--------------------------------------------------------------------------------------*/
			/*																						*/
			/*	toVector2: turns a Vector3 into a Vecotr2											*/
			/*		param: Vector3 vec3																*/
			/*		returns: the same Vector 3 without the z value in a Vector2						*/
			/*																						*/
			/*--------------------------------------------------------------------------------------*/
			public static Vector2 ToVector2(this Vector3 vec3)
			{
				return new Vector2(vec3.x, vec3.y);
			}

			/*--------------------------------------------------------------------------------------*/
			/*																						*/
			/*	ToDegree: turns a Vector3 into a Vecotr2											*/
			/*		param: 																			*/
			/* 			float radians																*/
			/*																						*/
			/*		returns: 																		*/
			/* 			float: radians in degrees													*/
			/*																						*/
			/*--------------------------------------------------------------------------------------*/
			public static float ToDegree(this float radians)
			{
				return radians * (180.0f / Mathf.PI);
			}

			/*--------------------------------------------------------------------------------------*/
			/*																						*/
			/*	ToRadians: turns a Vector3 into a Vecotr2											*/
			/*		param: 																			*/
			/* 			float degrees																*/
			/*																						*/
			/*		returns: 																		*/
			/* 			float: degress in radians													*/
			/*																						*/
			/*--------------------------------------------------------------------------------------*/
			public static float ToRadians (this float degrees)
			{
				return degrees * (Mathf.PI / 180.0f);
			}
		}
	}
}
