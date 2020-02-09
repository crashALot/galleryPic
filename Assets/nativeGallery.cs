using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class nativeGallery : MonoBehaviour
{
	void Update()
	{
		if( Input.GetMouseButtonDown( 0 ) )
		{
			// Don't attempt to pick media from Gallery/Photos if
			// another media pick operation is already in progress
			if( NativeGallery.IsMediaPickerBusy() )
				return;
			else
			{
				// Pick a PNG image from Gallery/Photos
				// If the selected image's width and/or height is greater than 512px, down-scale the image
				PickImage( 512 );
			}
		}
	}

	// Sprite variables that will be attached user selected texture
	private Sprite mySprite;
	
	private void PickImage( int maxSize )
	{
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
		{
			Debug.Log( "Image path: " + path );
			if( path != null )
			{	
				// Create Texture from selected image
				Texture2D texture = NativeGallery.LoadImageAtPath( path, maxSize );
				if( texture == null )
				{
					Debug.Log( "Couldn't load texture from " + path );
					return;
				}
				
				mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
				GetComponent<Image>().sprite = mySprite;
			}
		}, "Select a PNG image", "image/png" );

		Debug.Log( "Permission result: " + permission );
	}
}