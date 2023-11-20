using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy_Rotation : MonoBehaviour
{

    public Transform TargetLimb;
    ConfigurableJoint Cj;

    public bool mirror;

	public Quaternion startrot;

	[SerializeReference] bool LocalCopy;
    void Start()
    {
        Cj = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        //always copy the motion and rotation of our model dummy
        if (!mirror)
        {
            //make sure the limb does not want to copy the localrotation of the gameobject
            if (LocalCopy)
            {
                Cj.targetRotation = TargetLimb.localRotation;
            }
            else
            {
                Cj.targetRotation = TargetLimb.rotation;
            }
        }
        else
        {
            if (LocalCopy)
            {
                Cj.targetRotation = Quaternion.Inverse(TargetLimb.localRotation);
            }
            else
            {
                Cj.targetRotation = Quaternion.Inverse(TargetLimb.rotation);
            }
        }
    }

	static void SetTargetRotationInternal(ConfigurableJoint joint, Quaternion targetRotation, Quaternion startRotation, Space space)
	{
		// Calculate the rotation expressed by the joint's axis and secondary axis
		var right = joint.axis;
		var forward = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
		var up = Vector3.Cross(forward, right).normalized;
		Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);

		// Transform into world space
		Quaternion resultRotation = Quaternion.Inverse(worldToJointSpace);

		// Counter-rotate and apply the new local rotation.
		// Joint space is the inverse of world space, so we need to invert our value
		if (space == Space.World)
		{
			resultRotation *= startRotation * Quaternion.Inverse(targetRotation);
		}
		else
		{
			resultRotation *= Quaternion.Inverse(targetRotation) * startRotation;
		}

		// Transform back into joint space
		resultRotation *= worldToJointSpace;

		// Set target rotation to our newly calculated rotation
		joint.targetRotation = resultRotation;
	}
}
