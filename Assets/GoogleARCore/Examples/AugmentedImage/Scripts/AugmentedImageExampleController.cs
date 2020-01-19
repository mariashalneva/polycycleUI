//-----------------------------------------------------------------------
// <copyright file="AugmentedImageExampleController.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
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

namespace GoogleARCore.Examples.AugmentedImage
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using GoogleARCore;
    using UnityEngine;
    using System.Linq;
    using UnityEngine.UI;
    using TMPro;

    /// <summary>
    /// Controller for AugmentedImage example.
    /// </summary>
    /// <remarks>
    /// In this sample, we assume all images are static or moving slowly with
    /// a large occupation of the screen. If the target is actively moving,
    /// we recommend to check <see cref="AugmentedImage.TrackingMethod"/> and
    /// render only when the tracking method equals to
    /// <see cref="AugmentedImageTrackingMethod"/>.<c>FullTracking</c>.
    /// See details in <a href="https://developers.google.com/ar/develop/c/augmented-images/">
    /// Recognize and Augment Images</a>
    /// </remarks>
    public class AugmentedImageExampleController : MonoBehaviour
    {
        /// <summary>
        /// A prefab for visualizing an AugmentedImage.
        /// </summary>
        public AugmentedImageVisualizer[] AugmentedImageVisualizerPrefab;

        public AugmentedImageVisualizer ImageVisualizerPrefab1;
        public AugmentedImageVisualizer ImageVisualizerPrefab2;

        /// <summary>
        /// The overlay containing the fit to scan user guide.
        /// </summary>
        public GameObject FitToScanOverlay;

        private Dictionary<int, AugmentedImageVisualizer> m_Visualizers
            = new Dictionary<int, AugmentedImageVisualizer>();

        public AugmentedImage[] m_TempAugmentedImagePREFRABS;
        public List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();


        public AugmentedImageVisualizer currentSelected;
        /// <summary>
        /// The Unity Awake() method.
        /// </summary>
        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
        }

        /// <summary>
        /// The Unity Update method.
        /// </summary>
        public void Update()
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

            // Get updated augmented images for this frame.
            Session.GetTrackables<AugmentedImage>(
                m_TempAugmentedImages, TrackableQueryFilter.Updated);

            // Create visualizers and anchors for updated augmented images that are tracking and do
            // not previously have a visualizer. Remove visualizers for stopped images.
            //for (int i = 0; i< m_TempAugmentedImages.Count;  i++)// image in m_TempAugmentedImages)
            //{
            //    var image1 = m_TempAugmentedImages[0];
            //    AugmentedImageVisualizer visualizer1 = null;
            //    m_Visualizers.TryGetValue(image1.DatabaseIndex, out visualizer1);

            //    if (image1.TrackingState == TrackingState.Tracking && visualizer1 == null)
            //    {
            //        Create an anchor to ensure that ARCore keeps tracking this augmented image.
            //        Anchor anchor = image1.CreateAnchor(image1.CenterPose);
            //        visualizer1 = (AugmentedImageVisualizer)Instantiate(
            //            ImageVisualizerPrefab1, anchor.transform);
            //        visualizer1.Image = image1;
            //        m_Visualizers.Add(image1.DatabaseIndex, visualizer1);
            //    }
            //    else if (image1.TrackingState == TrackingState.Stopped && visualizer1 != null)
            //    {
            //        m_Visualizers.Remove(image1.DatabaseIndex);
            //        GameObject.Destroy(visualizer1.gameObject);
            //    }

            //    var image2 = m_TempAugmentedImages[1];
            //    AugmentedImageVisualizer visualizer2 = null;
            //    m_Visualizers.TryGetValue(image2.DatabaseIndex, out visualizer2);

            //    if (image2.TrackingState == TrackingState.Tracking && visualizer2 == null)
            //    {
            //        Create an anchor to ensure that ARCore keeps tracking this augmented image.
            //    Anchor anchor = image2.CreateAnchor(image2.CenterPose);
            //        visualizer2 = (AugmentedImageVisualizer)Instantiate(
            //            ImageVisualizerPrefab1, anchor.transform);
            //        visualizer2.Image = image2;
            //        m_Visualizers.Add(image2.DatabaseIndex, visualizer2);
            //    }
            //    else if (image2.TrackingState == TrackingState.Stopped && visualizer2 != null)
            //    {
            //        m_Visualizers.Remove(image2.DatabaseIndex);
            //        GameObject.Destroy(visualizer2.gameObject);
            //    }
            //}

            foreach (var image in m_TempAugmentedImages)
            {
                int i = image.DatabaseIndex;
                AugmentedImageVisualizer visualizer = null;
                m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
                if (image.TrackingMethod == AugmentedImageTrackingMethod.FullTracking && visualizer == null)
                {
                    // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                    Anchor anchor = image.CreateAnchor(image.CenterPose);

                    visualizer = (AugmentedImageVisualizer)Instantiate(
                        AugmentedImageVisualizerPrefab[i], anchor.transform);
                    visualizer.Image = image;
                    m_Visualizers.Add(image.DatabaseIndex, visualizer);
                }
                else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
                {
                    m_Visualizers.Remove(image.DatabaseIndex);
                    GameObject.Destroy(visualizer.gameObject);


                }

            }


            // Show the fit-to-scan overlay if there are no images that are Tracking.
            foreach (var visualizer in m_Visualizers.Values)
            {
                if (visualizer.Image.TrackingState == TrackingState.Tracking)
                {
                    FitToScanOverlay.SetActive(false);
                    return;
                }
            }
            FitToScanOverlay.SetActive(true);
        }

    }
}
