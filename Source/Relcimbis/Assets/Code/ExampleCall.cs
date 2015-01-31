using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class ExampleCall : MonoBehaviour
{
	
	public float animationSpeed = 1.0f;
	public int firstAnimationFrame = 0;
	public int lastAnimationFrame = 9;
	
	public int singleFrameSize = 32;
	
	string spriteSheetPath = null;
	public Texture2D playerSpriteSheet = null;
	private Texture2D[] playerFrames;
	private int imageIndex = 0;


	void Start ()
	{

		playerFrames = GameObject.FindGameObjectWithTag ( "SpriteCreator" ).GetComponent<SpriteCreator> ().SpritesFromTexture ( singleFrameSize, spriteSheetPath, playerSpriteSheet );
		
		imageIndex = firstAnimationFrame;
		if ( lastAnimationFrame == 0 )
		{
			
			lastAnimationFrame = playerFrames.Length - 1;
		}
		
		InvokeRepeating ( "Animate", 0, animationSpeed );
	}
	
	
	void Animate ()
	{
		
		gameObject.renderer.material.mainTexture = playerFrames[imageIndex];
		
		if ( imageIndex >= lastAnimationFrame )
		{
			
			imageIndex = firstAnimationFrame;
		} else {
			
			imageIndex += 1;
		}
	}
}
