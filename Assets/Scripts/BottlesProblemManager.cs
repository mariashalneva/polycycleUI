//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    public class BottlesProblemManager : MonoBehaviour
    {
        public int counter = 0;
        public TextMeshProUGUI text;
        public GameObject infoPanel;
        public GameObject textPanel;
        public GameObject infoPanel2;

        private List<GameObject> bottleObjects;

        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR
        /// background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a vertical plane.
        /// </summary>
        //public GameObject GameObjectVerticalPlanePrefab;

        ///// <summary>
        ///// A prefab to place when a raycast from a user touch hits a horizontal plane.
        ///// </summary>
        //public GameObject GameObjectHorizontalPlanePrefab;

        /// <summary>
        /// A prefab to place when a raycast from a user touch hits a feature point.
        /// </summary>
        public GameObject GameObjectPointPrefab;

        /// <summary>
        /// The rotation in degrees need to apply to prefab when it is placed.
        /// </summary>
        private const float k_PrefabRotation = 180.0f;

        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error,
        /// otherwise false.
        /// </summary>
        private bool m_IsQuitting = false;

        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            bottleObjects = new List<GameObject>();
            textPanel.gameObject.SetActive(true);
            Invoke("InstantiateBottles", 0.2f);
        }

        public void Update()
        {

            //_UpdateApplicationLifecycle();

            //// If the player has not touched the screen, we are done with this update.
            //Touch touch;
            //if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            //{
            //    return;
            //}

            //// Should not handle input if the player is pointing on UI.
            //if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            //{
            //    return;
            //}

            //// Raycast against the location the player touched to search for planes.
            //TrackableHit hit;
            //TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            //    TrackableHitFlags.FeaturePointWithSurfaceNormal;
            //if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            //{
            //    return;
            //}

            ////下記コードによりタップする度にCubeを生成して前え飛ばす。サイズとか勢いは適当
            //GameObject cube = GameObjectPointPrefab;
            //cube.transform.position = FirstPersonCamera.transform.TransformPoint(0, 0, 0.5f);
            //cube.GetComponentInChildren<Rigidbody>().AddForce(FirstPersonCamera.transform.TransformDirection(0, 1f, 2f), ForceMode.Impulse);
            //Instantiate(cube);

        }
        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void InstantiateBottles()
        {
            if (counter >= 346)
            {

                textPanel.gameObject.SetActive(false);
                ///
                //shpw message

                infoPanel.SetActive(true);
                return;
            }

            _UpdateApplicationLifecycle();

            // If the player has not touched the screen, we are done with this update.
            //Touch touch;
            //if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            //{
            //    return;
            //}

            // Should not handle input if the player is pointing on UI.
            //if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            //{
            //    return;
            //}

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
            GameObject prefab = new GameObject();


            float spawnY = Random.Range
               (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            if (Frame.Raycast(spawnPosition.x, spawnPosition.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    // Choose the prefab based on the Trackable that got hit.
                    //if (hit.Trackable is FeaturePoint)
                    //{
                    prefab = GameObjectPointPrefab;
                    prefab.AddComponent<Rigidbody>();
                }

                counter++;
                var cube = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);
                cube.transform.position = FirstPersonCamera.transform.TransformPoint(0, 0, 0.5f);
                cube.GetComponentInChildren<Rigidbody>().AddForce(FirstPersonCamera.transform.TransformDirection(0, 1f, 2f), ForceMode.Impulse);
                // Instantiate(cube);

                bottleObjects.Add(cube);
            }

            text.text = counter + "/346";
            Invoke("InstantiateBottles", 0.05f);
        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to
            // appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        /// <summary>
        /// Actually quit the application.
        /// </summary>
        private void _DoQuit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Show an Android toast message.
        /// </summary>
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>(
                            "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }


        public void ChangeColors()
        {

            infoPanel.SetActive(false);
            for (int i = 0; i < bottleObjects.Count; i++)
            {
                if (i % 5 == 0)
                    bottleObjects[i].GetComponent<BottleItem>().ChangeColor(true);
                else
                    bottleObjects[i].GetComponent<BottleItem>().ChangeColor(false);
            }

            Invoke("set2panelact", 5);
        }

        private void set2panelact()
        {
            infoPanel2.SetActive(true);
        }

        public void TryAgain()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
