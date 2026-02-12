using UnityEngine;
using UnityEngine.SceneManagement;
public static class SceneLoader // this is a static class because we want to be able to call the LoadScene method from anywhere in our code without having to create an instance of the SceneLoader class
{

    /// <summary>
    /// Loads the scene -- new fucntionality ///

    public static void Load(SceneID scene)
    {
        // No strings. Just a clear mapping.
        SceneManager.LoadScene(GetBuildIndex(scene));
    }

    private static int GetBuildIndex(SceneID scene) // sceneid enum is called to create a scene switch
    {
        return scene switch
        {
            SceneID.Main => 0,
            SceneID.GameScene => 1,
            SceneID.Win => 2,
            _ => 0,
        };
    }
}
