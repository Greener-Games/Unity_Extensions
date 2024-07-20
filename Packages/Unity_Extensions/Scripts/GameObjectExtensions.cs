using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GG.Extensions
{
	public static class GameObjectExtensions
	{
		static List<GameObject> dontDestoryOnLoadObjects = new List<GameObject>();

        /// <summary>
        /// Gets an existing component of type T from the child or adds one if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">The type of the component to get or add.</typeparam>
        /// <param name="child">The component from which to get or add the component.</param>
        /// <returns>The existing or newly added component.</returns>
        static public T GetOrAddComponent<T>(this Component child) where T : Component
        {
            T result = child.GetComponent<T>();
            if (result == null)
            {
                result = child.gameObject.AddComponent<T>();
            }
            return result;
        }

        /// <summary>
        /// Gets an existing component of type T from the GameObject or adds one if it doesn't exist.
        /// </summary>
        /// <typeparam name="T">The type of the component to get or add.</typeparam>
        /// <param name="child">The GameObject from which to get or add the component.</param>
        /// <returns>The existing or newly added component.</returns>
        static public T GetOrAddComponent<T>(this GameObject child) where T : Component
        {
            return GetOrAddComponent<T>(child.transform);
        }

        /// <summary>
        /// Changes the material of all Renderer components in the GameObject and its children.
        /// </summary>
        /// <param name="go">The GameObject whose materials to change.</param>
        /// <param name="newMat">The new material to apply.</param>
        public static void ChangeMaterial(this GameObject go, Material newMat)
        {
            Renderer[] children = go.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer rend in children)
            {
                Material[] mats = new Material[rend.materials.Length];
                for (int j = 0; j < rend.materials.Length; j++)
                {
                    mats[j] = newMat;
                }
                rend.materials = mats;
            }
        }

        /// <summary>
        /// Gets a component of type T from the parent of the GameObject, ignoring any components of type T on the GameObject itself.
        /// </summary>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <param name="go">The GameObject from which to start the search.</param>
        /// <returns>The component of type T from the parent GameObject.</returns>
        public static T GetComponentInParentIgnoreSelf<T>(this GameObject go)
        {
            return go.transform.parent.GetComponentInParent<T>();
        }
		
		/// <summary>
/// Marks the specified GameObject to not be destroyed when loading a new scene.
/// </summary>
/// <param name="obj">The GameObject to preserve across scenes.</param>
/// <remarks>
/// Adds the GameObject to a static list to keep track of objects that shouldn't be destroyed on load.
/// </remarks>
public static void DontDestroyOnLoad(this GameObject obj)
{
    dontDestoryOnLoadObjects.Add(obj);
    Object.DontDestroyOnLoad(obj);
}

/// <summary>
/// Destroys a GameObject that was previously marked with DontDestroyOnLoad.
/// </summary>
/// <param name="obj">The GameObject to destroy.</param>
/// <remarks>
/// Removes the GameObject from the static list that tracks objects preserved across scenes before destroying it.
/// </remarks>
public static void DestoryDontDestroyOnLoad(this GameObject obj)
{
    dontDestoryOnLoadObjects.Remove(obj);
    Object.Destroy(obj);
}

/// <summary>
/// Retrieves a list of all GameObjects that have been marked to not be destroyed on scene loads.
/// </summary>
/// <returns>A list of GameObjects that are preserved across scenes.</returns>
/// <remarks>
/// Filters out any null references in the static list before returning it to ensure all returned objects are valid.
/// </remarks>
public static List<GameObject> GetDontDestroyOnLoadObjects()
{
    dontDestoryOnLoadObjects = dontDestoryOnLoadObjects.Where(x => x != null).ToList();
    return new List<GameObject>(dontDestoryOnLoadObjects);
}

/// <summary>
/// Returns a list of all child GameObjects of the specified GameObject.
/// </summary>
/// <param name="gameObject">The parent GameObject.</param>
/// <returns>A list of all child GameObjects.</returns>
/// <remarks>
/// Does not include the parent GameObject in the list, only its children.
/// </remarks>
public static List<GameObject> GetAllChildren(this GameObject gameObject)
{
    Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();
    List<GameObject> allChildren = new List<GameObject>(childTransforms.Length);

    foreach(Transform child in childTransforms)
    {
        if(child.gameObject != gameObject) allChildren.Add(child.gameObject);
    }

    return allChildren;
}

/// <summary>
/// Returns a list of all child GameObjects of the specified GameObject, including the GameObject itself.
/// </summary>
/// <param name="gameObject">The GameObject to retrieve children from.</param>
/// <returns>A list of GameObjects representing all children and the GameObject itself.</returns>
public static List<GameObject> GetAllChildrenAndSelf(this GameObject gameObject)
{
    Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();
    List<GameObject> allChildren = new List<GameObject>(childTransforms.Length);

    for (int transformIndex = 0; transformIndex < childTransforms.Length; ++transformIndex)
    {
        allChildren.Add(childTransforms[transformIndex].gameObject);
    }

    return allChildren;
}
	}
}
