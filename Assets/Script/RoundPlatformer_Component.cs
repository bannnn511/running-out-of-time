using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class RoundPlatformer_Component : MonoBehaviour {

	/// <summary>
	/// Control the Position of the platform. From 1 to 100. 1 = closer from the center, 100 = far from the center.
	/// </summary>
	[Range(1, 100)]
	[Tooltip("Position of the platform from the center")]
	public float Position;

	/// <summary>
	/// Control the Rotation of the platform. In degree.
	/// </summary>
	[Range(-360, 360)]
	[Tooltip("Rotation of the platform")]
	public float Rotation;


	/// <summary>
	/// The width of the platform. From 1 to 360. In Degree. 1 = Small platform. 360 = a complete circle.
	/// </summary>
	[Range(1, 365)]
	[Tooltip("Width of the platform")]
	public float Width;


	/// <summary>
	/// The height of the platform. from 0.1f to 10.
	/// </summary>
	//[Tooltip("Height of the platform")]
	//[Range(0.1f, 10)]
	//public float Height;


	[Tooltip("Control the Z position of the visual render of the platform. Tweak this value if you want to have your platform in front or behind another element.")]
	[Range(-10, 10)]
	public float Z_Position;


	/// <summary>
	/// Number of segments. Used for tilable texture.
	/// </summary>
	[Space] [Space]
	[Range(1,100)]
	[Tooltip("")]
	public int Segments;

	/// <summary>
	/// If true, the script will automaticly calculate the number of segments depending to the width and position of the platform. So the texture will tile correctly.
	/// </summary>
	[Tooltip("If true, the script will automaticly calculate the number of segments so the texture will tile correctly")]
	public bool AutoSegments = true;



	private LineRenderer Linerender;
	private EdgeCollider2D EdgeCol2D;



	private void OnEnable() {
		Initialize();
	}



	private void Initialize(){
		Linerender = GetComponent<LineRenderer>();
		EdgeCol2D = GetComponent<EdgeCollider2D>();

		Linerender.positionCount = Segments + 1;

		CreatePoints(Width, Rotation);
	}



	//Update the Platform graphics in EditorTime//
	public void OnValidate() {

		if(Linerender == null){Linerender = GetComponent<LineRenderer>();}
		if(EdgeCol2D == null){EdgeCol2D = GetComponent<EdgeCollider2D>();}
		if(Width > 365){ Width = 365;}

		if(Linerender != null){
			if(AutoSegments == true){
				RespectTextureRatio();
			}

			Linerender.positionCount = Segments + 1;
			CreatePoints(Width, Rotation);
			EdgeCollider_Generate(Width, Rotation);
		}
	}



	//RespectTextureRatio will Automaticly calculate the number of segments needed so the texture will not stretch//
	//Called only if the boolean "AutoSegment" is used. 
	private const float AngleToSegments = 2.91F; //Don't touch this unless the AutoSegments fail to correctly calculate the number of segments for a correct tile ratio//
	private const float RadiusToSegments = 6F; //Don't touch this either.

	public void RespectTextureRatio(){
		Segments = Mathf.RoundToInt(Width / AngleToSegments);
		Segments = Mathf.RoundToInt(((Width / RadiusToSegments)/10) * Position);
	}



	//Create the visual of the platform
	private void CreatePoints(float InputAngle, float Rotation){
		float X;
		float Y;
		float angle = (-(InputAngle / 2)) + Rotation;

		for (int i = 0; i < (Segments + 1); i++){
			X = Mathf.Sin (Mathf.Deg2Rad * angle) * Position;
			Y = Mathf.Cos (Mathf.Deg2Rad * angle) * Position;

			Linerender.SetPosition(i, new Vector3(X, Y, Z_Position)); //The Position of the LineRenderer Point. 

			angle += (InputAngle / Segments);
		}
	}



	//Create the collider shape of the platform
	private Vector2[] LineCoordPos;

	public void EdgeCollider_Generate(float InputAngle, float rotation){
		LineCoordPos = new Vector2[(Linerender.positionCount)];

		float X;
		float Y;
		float angle = (-(InputAngle / 2 )) + rotation;

		for (int i = 0; i < (Linerender.positionCount); i++){ //For every position of the line; Generate one point for the Edge Collider 2D Shape.
			X = Mathf.Sin(Mathf.Deg2Rad * angle) * Position;
			Y = Mathf.Cos(Mathf.Deg2Rad * angle) * Position;

			LineCoordPos[i] = new Vector2(X, Y);
			angle += (InputAngle/Segments);
		}

		if(EdgeCol2D != null){
			EdgeCol2D.points = LineCoordPos;
		}
	}
}
