using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    protected Player player;
    protected SpriteRenderer sr;

    [Header("Pop Up Text")]
    [SerializeField] private GameObject popUpTextPrefab;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;

    private GameObject myHealthBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = PlayerManager.instance.player;

        originalMat = sr.material;        
    }
    public void CreatePopUpText(string _text)
    {
        float randomX = Random.Range(0, 0.5f);
        float randomY = Random.Range(0, 0.5f);

        Vector3 positionOffset = new Vector3(randomX, randomY, 0);

        GameObject newText = Instantiate(popUpTextPrefab, transform.position + positionOffset, Quaternion.identity);

        newText.GetComponent<TextMeshPro>().text = _text;

    }

    public void MakeTransparent(bool _transparent)
    {
        if(_transparent)
        {
            myHealthBar.SetActive(false);
            sr.color = Color.clear;
        } else
        {
            myHealthBar.SetActive(true);
            sr.color = Color.white;
        }
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currentColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currentColor;
        sr.material = originalMat;
    }

    private void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
