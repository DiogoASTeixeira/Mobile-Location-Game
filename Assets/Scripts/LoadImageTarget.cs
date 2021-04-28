using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LoadImageTarget : MonoBehaviour
{
    void Start()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(CreateImageTargetFromSideloadedTexture);
      //  Vuforia.DataSet.StorageType.
    }

    void CreateImageTargetFromSideloadedTexture()
    {
        var objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        // get the runtime image source and set the texture to load
        var runtimeImageSource = objectTracker.RuntimeImageSource;
        runtimeImageSource.SetFile(VuforiaUnity.StorageType., "Vuforia/myTarget.jpg", 0.15f, "myTargetName");

        // create a new dataset and use the source to create a new trackable
        var dataset = objectTracker.CreateDataSet();
        var trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, "myTargetName");

        // add the DefaultTrackableEventHandler to the newly created game object
        trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();

        // activate the dataset
        objectTracker.ActivateDataSet(dataset);

        // TODO: add virtual content as child object(s)
    }
}
