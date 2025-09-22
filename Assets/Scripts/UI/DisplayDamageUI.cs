using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamageUI : MonoBehaviour
{
    // Reference to Damage Text Label
    public Text damageTextLabel;

    // Reference to Damage Label
    public Text damageLabel;

    class DamageLabelStats
    {
        public GameObject labelGO = null;
        public float currentLifetime = 3.0f;
    }

    // List of Alive Damage Labels
    List<DamageLabelStats> aliveDamageLabels;
    
    public void DisplayDamage(int damageAmt)
    {
        DamageLabelStats newDamageLabelStats = new DamageLabelStats();

        GameObject newLabelGO = Instantiate(damageTextLabel.gameObject, this.gameObject.transform);
        
        newLabelGO.gameObject.SetActive(true);
        newLabelGO.gameObject.transform.position = damageTextLabel.gameObject.transform.position;
        newLabelGO.gameObject.transform.GetChild(0).GetComponent<Text>().text = damageAmt.ToString();

        newDamageLabelStats.labelGO = newLabelGO;
        newDamageLabelStats.currentLifetime = 3.0f;
        aliveDamageLabels.Add(newDamageLabelStats);
    }

    void Start()
    {
        if (damageTextLabel != null && damageLabel != null)
        {
            // Deactivate on Start, will only be used when damage is done
            damageTextLabel.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("DisplayDamageUI.cs: Start() => DamageTextLabel and DamageText references not provided!");
        }

        aliveDamageLabels = new List<DamageLabelStats>();
    }

    void Update()
    {
        // Display Damage Labels moving Downward
        for (int i = 0; i < aliveDamageLabels.Count; i++)
        {
            var labelStats = aliveDamageLabels[i];

            // Move Downward
            labelStats.labelGO.transform.position += -Vector3.up * 10.0f * Time.deltaTime;

            // Apply a Fade Effect
            UnityEngine.Color labelColor = labelStats.labelGO.GetComponent<Text>().color;
            labelColor.a = Mathf.Lerp(labelColor.a, 0.0f, Time.deltaTime);
            labelStats.labelGO.GetComponent<Text>().color = labelColor;
            labelStats.labelGO.gameObject.transform.GetChild(0).GetComponent<Text>().color = labelColor;

            // Remove Labels after some time
            labelStats.currentLifetime -= Time.deltaTime;
            if (labelStats.currentLifetime < 0.0f)
            {
                Destroy(labelStats.labelGO);
                aliveDamageLabels[i] = null;
            }
        }

        aliveDamageLabels.RemoveAll(x => x == null);
    }
}
