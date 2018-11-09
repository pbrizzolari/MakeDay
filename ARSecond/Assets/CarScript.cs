using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;


public class CarScript: MonoBehaviour {

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Collision Detected");
    //    Debug.Log(collision);
    //    if(collision.gameObject.name=="house"){
    //        Destroy(collision.gameObject);
    //        var particleComponents = collision.collider.GetComponentsInParent<ParticleSystem>(true);
    //        foreach (var component in particleComponents)
    //            component.enableEmission = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        Debug.Log(other);
        if(other.gameObject.name == "house"){
            //Destroy(other.gameObject);
            var particleComponents = GetComponentsInChildren<ParticleSystem>(true);
            foreach (var component in particleComponents)
            {
                component.enableEmission = true;

                Quotes quotes = new Quotes();
                quotes.GetQuote();
                //StartCoroutine(GetQuote("test"));
            }
        }
    }

    private void OnEnable()
    {
        var particleComponents = GetComponentsInChildren<ParticleSystem>(true);
        foreach (var component in particleComponents)
        {
            Debug.Log("Disable Emission");
            component.enableEmission = false;
        }
    }

    private IEnumerator GetQuote(string obj){
        var BodyText = "{\"type\":\"root_term\",\"cover_amount\":100000000,\"cover_period\":\"2_years\",\"basic_income_per_month\":2000000,\"education_status\":\"undergraduate_degree\",\"smoker\":false,\"gender\":\"male\",\"age\":29}";
        using (UnityWebRequest www = UnityWebRequest.Post(@"https://sandbox.root.co.za/v1/insurance/quote", BodyText)){
            byte[] bytesToEncode = Encoding.UTF8.GetBytes("sandbox_NTExYWRhN2YtYTM5My00ODM4LTgyYmUtNzNlYjY4YjU5YjI3LkpGenlScE5OQUpOdGJxRVpVRnZNOEo5LWhnWUZMSU9M:");
            string encodedText = Convert.ToBase64String(bytesToEncode);
            www.SetRequestHeader("authorization", "basic "+encodedText);
            www.SetRequestHeader("Content-Type", "application/json");
            //Debug.Log(www.GetRequestHeader("basic"));
            Debug.Log(www.GetRequestHeader("Content-Type"));
            Debug.Log(www.uploadedBytes.ToString());
            www.chunkedTransfer = false;
            yield return www.SendWebRequest();

            Debug.Log(www.responseCode);
            if(www.isNetworkError || www.isHttpError){
                Debug.Log(www.error);
            }else{
                Debug.Log("form upload completed");
            }
        }

    }
}
