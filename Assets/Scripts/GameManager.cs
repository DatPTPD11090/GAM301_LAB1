using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject[] objectsToMove;  
    private int cubeCount = 0;
    private int maxCubes = 10;
    private Vector3[] targetPositions = { new Vector3(5, 0, 0), new Vector3(0, 5, 0), new Vector3(0, 0, 5) };

    void Start()
    {
        StartCoroutine(SpawnCubes());
        StartCoroutine(MoveObjectsSequentially());
    }

    
    IEnumerator SpawnCubes()
    {
        while (cubeCount < maxCubes)
        {
            yield return new WaitForSeconds(2);

            // tao vi tri ngau nhien
            float randomX = Random.Range(-5f, 5f);
            float randomY = Random.Range(-5f, 5f);
            float randomZ = Random.Range(-5f, 5f);
            GameObject cubeClone = Instantiate(cubePrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);

            cubeCount++; 
        }
    }

    
    IEnumerator FadeOut(GameObject obj)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        Material objMaterial = objRenderer.material;

        
        objMaterial.SetFloat("_Mode", 3);
        objMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        objMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        objMaterial.SetInt("_ZWrite", 0);
        objMaterial.DisableKeyword("_ALPHATEST_ON");
        objMaterial.EnableKeyword("_ALPHABLEND_ON");
        objMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        objMaterial.renderQueue = 3000;

        float elapsedTime = 0f;
        float fadeDuration = 5f;
        Color startColor = objMaterial.color;

        while (elapsedTime < fadeDuration)
        {
            
            float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / fadeDuration);
            objMaterial.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    void Update()
    {
        // an space lam mo doi tuong
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject obj in objectsToMove)
            {
                StartCoroutine(FadeOut(obj));
            }
        }
    }

    
    IEnumerator MoveObjectsSequentially()
    {
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            yield return new WaitForSeconds(1f); 
            StartCoroutine(MoveObject(objectsToMove[i], targetPositions[i]));
        }
    }

    // di chuyen doi tuong toi vi tri moi
    IEnumerator MoveObject(GameObject obj, Vector3 target)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = obj.transform.position;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = target; 
    }
}
