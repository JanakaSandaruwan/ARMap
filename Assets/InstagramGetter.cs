using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using Mapbox.Unity.Map;

public class InstagramGetter : MonoBehaviour {

    public AbstractMap _map;
	// Use this for initialization
	IEnumerator Start () {
        string mainPic_url = "http://www.morahiking.com/images/headers/unilogo.png";
        Debug.Log(mainPic_url);

        WWW mainPic = new WWW(mainPic_url);
        yield return mainPic;

        double lat = 6.795562;
        double lon = 79.9006054;

        GameObject instaPost = Instantiate(Resources.Load("ARBanner")) as GameObject;
        instaPost.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = mainPic.texture;
        instaPost.transform.position = _map.GeoToWorldPosition(new Mapbox.Utils.Vector2d(lat, lon)) + new Vector3(0, 2f, 0);
        instaPost.transform.SetParent(_map.transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
