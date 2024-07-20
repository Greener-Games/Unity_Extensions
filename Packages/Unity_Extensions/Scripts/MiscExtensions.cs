using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GG.Extensions
{
    public static class MiscExtensions
    {
        public delegate void UnityAction<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        /// <summary>
        /// An actual quit that will stop play mode in editor as well
        /// </summary>
        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
        }

        /// <summary>
        ///     Ensure the resources directory lives at 'Assets' level
        /// </summary>
        public static void EnsureResourcesExists()
        {
            string path = Path.Combine("Assets", "Resources");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Finds all objects of type T in all loaded scenes, including inactive objects.
        /// This method is an alternative to Resources.FindObjectsOfTypeAll (which returns project assets, including prefabs),
        /// and GameObject.FindObjectsOfTypeAll (which is deprecated).
        /// </summary>
        /// <typeparam name="T">The type of the objects to find.</typeparam>
        /// <returns>A list of objects of type T found in all loaded scenes.</returns>
        public static List<T> FindObjectsOfTypeAll<T>()
        {
            List<T> results = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var s = SceneManager.GetSceneAt(i);
                results.AddRange(FindObjectsOfTypeAllInScene<T>(s, false));
            }

            foreach (GameObject savedObject in GameObjectExtensions.GetDontDestroyOnLoadObjects())
            {
                results.AddRange(savedObject.GetComponentsInChildren<T>(true));
            }

            return results;
        }

        /// <summary>
        /// Finds all objects of type T in a specific scene, including inactive objects.
        /// Optionally includes objects marked as DontDestroyOnLoad.
        /// </summary>
        /// <typeparam name="T">The type of the objects to find.</typeparam>
        /// <param name="scene">The scene to search within.</param>
        /// <param name="includeDontDestroyOnLoad">Whether to include objects marked as DontDestroyOnLoad.</param>
        /// <returns>A list of objects of type T found in the specified scene.</returns>
        public static List<T> FindObjectsOfTypeAllInScene<T>(Scene scene, bool includeDontDestroyOnLoad = true)
        {
            List<T> results = new List<T>();

            var allGameObjects = scene.GetRootGameObjects();
            for (int j = 0; j < allGameObjects.Length; j++)
            {
                var go = allGameObjects[j];
                results.AddRange(go.GetComponentsInChildren<T>(true));
            }

            if (includeDontDestroyOnLoad)
            {
                foreach (GameObject savedObject in GameObjectExtensions.GetDontDestroyOnLoadObjects())
                {
                    results.AddRange(savedObject.GetComponentsInChildren<T>(true));
                }
            }

            return results;
        }

        /// <summary>
        /// Asynchronously loads a scene by name and provides progress updates and a completion callback.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="loadSceneMode">Specifies whether to load the scene additively or replace the current scene.</param>
        /// <param name="updateAction">An action to perform with the loading progress (0.0 to 0.9).</param>
        /// <param name="OnComplete">An action to perform once the scene is fully loaded and activated.</param>
        /// <returns>An IEnumerator for coroutine support, allowing this method to yield until the scene has loaded.</returns>
        public static IEnumerator WaitForSceneToLoad(string sceneName, LoadSceneMode loadSceneMode,
            UnityAction<float> updateAction, UnityAction OnComplete)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
            {
                updateAction?.Invoke(async.progress);
                yield return null;
            }

            async.allowSceneActivation = true;

            while (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                yield return new WaitForSeconds(0.1f);
            }

            OnComplete?.Invoke();
        }

        /// <summary>
        /// Clone data from an object into a new version of it
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Clone<T>(T obj)
        {
            DataContractSerializer dcSer = new DataContractSerializer(obj.GetType());
            MemoryStream memoryStream = new MemoryStream();

            dcSer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;

            T newObject = (T)dcSer.ReadObject(memoryStream);
            Color32 c = new Color32();
            c.ColorToHex();
            return newObject;
        }

        /// <summary>
        /// Use Reflection from a source class to an inhearated class
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        public static void CopyAll<T, T2>(this T source, T2 target)
        {
            Type type = typeof(T);
            foreach (FieldInfo sourceField in type.GetFields())
            {
                FieldInfo targetField = type.GetField(sourceField.Name);
                targetField.SetValue(target, sourceField.GetValue(source));
            }
        }

        /// <summary>
        /// Calculates the number of columns and rows in a vertical grid layout based on the active children.
        /// </summary>
        /// <param name="glg">The GridLayoutGroup for which to calculate columns and rows.</param>
        /// <param name="column">The number of columns in the grid. Output parameter.</param>
        /// <param name="row">The number of rows in the grid. Output parameter.</param>
        /// <remarks>
        /// This method assumes that the grid starts with at least one column and one row.
        /// It iterates through all child objects of the GridLayoutGroup, counting the number of columns and rows
        /// based on the positions of the child objects. It only considers active child objects.
        /// </remarks>
        public static void GetColumnAndRowForVerticalGrid(this GridLayoutGroup glg, out int column, out int row)
        {
            column = 0;
            row = 0;

            if (glg.transform.childCount == 0)
                return;

            //Column and row are now 1
            column = 1;
            row = 1;

            //Get the first child GameObject of the GridLayoutGroup
            RectTransform firstChildObj = glg.transform.GetChild(0).GetComponent<RectTransform>();

            float currentY = firstChildObj.anchoredPosition.y;

            //Loop through the rest of the child object
            for (int i = 1; i < glg.transform.childCount; i++)
            {
                if (!glg.transform.GetChild(i).gameObject.activeSelf)
                {
                    continue;
                }

                //Get the next child
                RectTransform currentChildObj = glg.transform.GetChild(i).GetComponent<RectTransform>();

                Vector2 currentChildPos = currentChildObj.anchoredPosition;

                //if first child.x == otherchild.x, it is a row, else it's a column
                if (Math.Abs(currentY - currentChildPos.y) > Mathf.Epsilon)
                {
                    row++;
                    currentY = currentChildPos.y;
                }
                else
                {
                    column++;
                }
            }
        }

        /// <summary>
        /// Finds the index of the first occurrence of an object in an array.
        /// </summary>
        /// <param name="array">The array to search.</param>
        /// <param name="obj">The object to find the index of.</param>
        /// <typeparam name="T">The type of the objects in the array.</typeparam>
        /// <returns>The index of the first occurrence of the object in the array; -1 if not found.</returns>
        public static int IndexOf<T>(this T[] array, T obj)
        {
            return Array.IndexOf(array, obj);
        }

        /// <summary>
        /// Trims the milliseconds from a DateTime object, returning a new DateTime object.
        /// </summary>
        /// <param name="dt">The DateTime object to trim milliseconds from.</param>
        /// <returns>A new DateTime object with the milliseconds set to 0.</returns>
        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0, dt.Kind);
        }

        /// <summary>
        /// Wait a set number of frames in a coroutine
        /// </summary>
        /// <param name="frameCount"></param>
        /// <returns></returns>
        public static IEnumerator Frames(int frameCount)
        {
            WaitForEndOfFrame frame = new WaitForEndOfFrame();
            while (frameCount > 0)
            {
                frameCount--;
                yield return frame;
            }
        }

        /// <summary>
        /// Creates a runtime sprite from a texture2D
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Sprite CreateSprite(this Texture2D t)
        {
            Rect r = new Rect(0, 0, t.width, t.height);
            Sprite s = Sprite.Create(t, r, Vector2.zero);
            return s;
        }
    }
}