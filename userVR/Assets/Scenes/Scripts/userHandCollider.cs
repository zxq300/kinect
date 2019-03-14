using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userHandCollider : MonoBehaviour {
    public int playerIndex = 0;
    public bool mirroredMovement = false;

    public static Vector3 rHandPos;
    public static Vector3 lHandPos;
    public static Vector3 rElbowPos;
    public static Vector3 rShoulderPos;

    private GameObject[] jointColliders = null;
    private KinectManager manager;
    private Vector3 userHandLeftPos = Vector3.zero;
    private Vector3 userHandRightPos = Vector3.zero;
    private Vector3 userElbowRightPos = Vector3.zero;
    private Vector3 userShoulderRightPos = Vector3.zero;
    private Matrix4x4 kinectToWorld = Matrix4x4.identity;

    // Use this for initialization
    void Start()
    {
        manager = KinectManager.Instance;
        kinectToWorld = manager.GetKinectToWorldMatrix();

        lHandPos = new Vector3();
        rHandPos = new Vector3();
        

        if (manager && manager.IsInitialized())
        {
            KinectInterop.SensorData sensorData = manager.GetSensorData();
            if (sensorData != null && sensorData.sensorInterface != null)
            {
                jointColliders = new GameObject[4]; //HandLeft7 HandRight11 ElbowRight9 ShoulderRight8


                string sHandLeft = ((KinectInterop.JointType)7).ToString() + "Collider";
                string sHandRight = ((KinectInterop.JointType)11).ToString() + "Collider";
                string sElbowRight = ((KinectInterop.JointType)9).ToString() + "Collider";
                string sShoulderRight = ((KinectInterop.JointType)8).ToString() + "Collider";

                jointColliders[0] = new GameObject(sHandLeft);
                jointColliders[1] = new GameObject(sHandRight);
                jointColliders[2] = new GameObject(sElbowRight);
                jointColliders[3] = new GameObject(sShoulderRight);

                SphereCollider colliderl = jointColliders[0].AddComponent<SphereCollider>();
                SphereCollider colliderr = jointColliders[1].AddComponent<SphereCollider>();

                jointColliders[0].AddComponent<Rigidbody>();
                jointColliders[1].AddComponent<Rigidbody>();

                colliderl.isTrigger = true;
                colliderr.isTrigger = true;

                colliderl.radius = 0.1f;
                colliderr.radius = 0.1f;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (manager && manager.IsUserDetected())
        {
            long userId = manager.GetUserIdByIndex(playerIndex);  // manager.GetPrimaryUserID();
            
            // update colliders
            userHandLeftPos = manager.GetJointKinectPosition(userId, (int)KinectInterop.JointType.HandLeft);
            userHandRightPos = manager.GetJointKinectPosition(userId, (int)KinectInterop.JointType.HandRight);
            userElbowRightPos = manager.GetJointKinectPosition(userId, (int)KinectInterop.JointType.ElbowRight);
            userShoulderRightPos = manager.GetJointKinectPosition(userId, (int)KinectInterop.JointType.ShoulderRight);

            if (!mirroredMovement)
            {
                userHandLeftPos.x = -userHandLeftPos.x;
                userHandRightPos.x = -userHandRightPos.x;
                userElbowRightPos.x = -userElbowRightPos.x;
                userShoulderRightPos.x = -userShoulderRightPos.x;
            }
            
            userHandLeftPos = kinectToWorld.MultiplyPoint3x4(userHandLeftPos);
            userHandRightPos = kinectToWorld.MultiplyPoint3x4(userHandRightPos);
            userElbowRightPos = kinectToWorld.MultiplyPoint3x4(userElbowRightPos);
            userShoulderRightPos = kinectToWorld.MultiplyPoint3x4(userShoulderRightPos);

            // the scale of userMesh
            userHandLeftPos.Scale(transform.localScale);
            userHandRightPos.Scale(transform.localScale);
            userElbowRightPos.Scale(transform.localScale);
            userShoulderRightPos.Scale(transform.localScale);

            // update the position of colliders
            jointColliders[0].transform.position = transform.position + userHandLeftPos;
            jointColliders[1].transform.position = transform.position + userHandRightPos;
            jointColliders[2].transform.position = transform.position + userElbowRightPos;
            jointColliders[3].transform.position = transform.position + userShoulderRightPos;

            lHandPos = jointColliders[0].transform.position;
            rHandPos = jointColliders[1].transform.position;
            rElbowPos = jointColliders[2].transform.position;
            rShoulderPos = jointColliders[3].transform.position;
        }
    }
}
