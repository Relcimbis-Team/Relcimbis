using System;
using System.IO;
using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class SpriteCreator : MonoBehaviour
{


	internal Texture2D[] SpritesFromTexture ( int widthOfFrame, string fileLocation, Texture2D directTexture )
	{
		
		Texture2D sourceTexture = null;
		Texture2D[] sprites = null;
		
		
		if ( fileLocation != null )
		{
			
			sourceTexture = FetchTexture ( fileLocation );
		} else {
			
			sourceTexture = directTexture;
		}
		
			
		if ( VerifyTexture ( sourceTexture ) == true )
		{
		
			sprites = new Texture2D[Mathf.ClosestPowerOfTwo (( sourceTexture.width / widthOfFrame ) * ( sourceTexture.height / widthOfFrame ))];
			
			int textureIndex = 0;
			while ( textureIndex < sprites.Length )
			{
				
				sprites[textureIndex] = CreateSprite ( sourceTexture, widthOfFrame, ( textureIndex * widthOfFrame ));
				
				textureIndex += 1;
			}
		}
		
		return sprites;
	}
	
	
	private Texture2D FetchTexture ( string path )
	{
		
		Texture2D texture = null;
		
		if ( File.Exists ( path ) == false )
		{
			
			UnityEngine.Debug.LogError ( "Please provide a valid path! " + path + " does not exist!" );
			return null;
		} else {
			
			byte[] externalImageBytes = File.ReadAllBytes ( path );
		
			texture = new Texture2D ( 1, 1 );
			texture.LoadImage ( externalImageBytes );
		}
		
		return texture;
	}
	
	
	private bool VerifyTexture ( Texture2D texture )
	{
		
		if ( texture == null )
		{
			
			UnityEngine.Debug.LogError ( "Please provide source for sprite generation!" );
			return false;
		} else {
		
			if ( !Mathf.IsPowerOfTwo ( texture.width ) || !Mathf.IsPowerOfTwo ( texture.height ))
			{
				
				UnityEngine.Debug.LogError ( "Source dimensions must be a power of two!" ); //Why?
				return false;
			} else {
				
				if ( texture.width != texture.height )
				{
					
					UnityEngine.Debug.LogError ( "The image must be a square!" ); //Why?
					return false;
				} else {
					
					return true;
				}
			}
		}
	}
	
	
	private Texture2D CreateSprite ( Texture2D sourceTexture, int sizeOfFrame, int totalOffset )
	{
		
		int xOffset = 0;
		int yOffset = 0;
		
		int tempOffset = 0;
		int tempXOffset = 0;
		
		while ( tempOffset < totalOffset )
		{
			
			tempOffset += sizeOfFrame;
			tempXOffset += sizeOfFrame;
			
			if ( tempXOffset == sourceTexture.width )
			{
				
				yOffset += sizeOfFrame;
				
				tempXOffset = 0;
			}
		}
		
		xOffset = tempXOffset;
		
		Color[] pixels = sourceTexture.GetPixels ( xOffset, yOffset, sizeOfFrame, sizeOfFrame );
		Texture2D newSprite = new Texture2D ( sizeOfFrame, sizeOfFrame );
		newSprite.wrapMode = TextureWrapMode.Clamp;
		newSprite.filterMode = FilterMode.Point;
		newSprite.SetPixels ( pixels );
		newSprite.Apply ();
		
		return newSprite;
	}
}
