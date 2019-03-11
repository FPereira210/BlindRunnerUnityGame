using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAdmin : MonoBehaviour {

    private Admin admin;
	void Start () {
        admin = Admin.instance;
	}

    public void ResetData()
    {
        admin.ResetData();
    }
}
