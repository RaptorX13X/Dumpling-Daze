using UnityEngine;
using UnityEngine.UI;

public class WallpaperChange : MonoBehaviour
{
    [SerializeField] private RawImage wallpaper;
    [SerializeField] private Texture2D[] wallpaperTextures; // Array of different materials for wallpapers
    private int currentTextureIndex = 0; // Index to keep track of the current material

    [SerializeField] private ComputerTrigger computerScript;

    //private void OnMouseDown()
    //{
    //    if (computerScript.isComputering)
    //    {
    //        // Check if there are materials assigned
    //        if (wallpaperMaterials != null && wallpaperMaterials.Length > 0)
    //        {
    //            // Increment the current material index
    //            currentMaterialIndex = (currentMaterialIndex + 1) % wallpaperMaterials.Length;

    //            // Change the texture of the RawImage to the texture of the new material
    //            wallpaper.material = wallpaperMaterials[currentMaterialIndex];
    //        }
    //    }
    //}

    public void ChangeWallpaper()
    {
        if (computerScript.isComputering)
        {
            // Check if there are materials assigned
            if (wallpaperTextures != null && wallpaperTextures.Length > 0)
            {
                // Increment the current material index
                currentTextureIndex = (currentTextureIndex + 1) % wallpaperTextures.Length;

                // Change the texture of the RawImage to the texture of the new material
                wallpaper.texture = wallpaperTextures[currentTextureIndex];
            }
        }
    }
}
