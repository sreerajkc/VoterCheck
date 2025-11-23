using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ToiletUIManager : MonoBehaviour
{
    [Header("Appearance Customizer UI Properties")]
    [SerializeField] private GameObject appearanceCustomizerPanel;

    [SerializeField] private AppearanceCustomizerUIController hairCustomizerUIController;
    [SerializeField] private AppearanceCustomizerUIController noseCustomizerUIController;
    [SerializeField] private AppearanceCustomizerUIController beardCustomizerUIController;
    [SerializeField] private AppearanceCustomizerUIController moustacheCustomizerUIController;
    [SerializeField] private AppearanceCustomizerUIController eyeColorCustomizerUIController;
    [SerializeField] private AppearanceCustomizerUIController skinColorCustomizerUIController;

    [Header("Identity Card UI Properties")]
    [SerializeField] private VoterIdInfo fakeVoterIdInfo;
    [SerializeField] private VoterIdInfo makingVoterIdInfo;

    string[] variantNames = { "item1", "item2", "item3", "item4", "item5", "item6" };

    private void OnEnable()
    {
        Toilet.OnVoterEntered += ToggleAppearanceCustomizerForLocal;
        Toilet.OnVoterExit += ToggleAppearanceCustomizerForLocal;
    }
    private void OnDisable()
    {
        Toilet.OnVoterEntered -= ToggleAppearanceCustomizerForLocal;
        Toilet.OnVoterExit -= ToggleAppearanceCustomizerForLocal;
    }

    private void Start()
    {
        makingVoterIdInfo = new VoterIdInfo();

        hairCustomizerUIController.Initialize("Hair", variantNames, (int id) => makingVoterIdInfo.HairId = id);
        noseCustomizerUIController.Initialize("Nose", variantNames, (int id) => makingVoterIdInfo.NoseId = id);
        beardCustomizerUIController.Initialize("Beard", variantNames, (int id) => makingVoterIdInfo.BeardId = id);
        moustacheCustomizerUIController.Initialize("Moustache", variantNames, (int id) => makingVoterIdInfo.MoustacheId = id);
        eyeColorCustomizerUIController.Initialize("Eye Color", variantNames, (int id) => makingVoterIdInfo.EyeColorInt = id);
        skinColorCustomizerUIController.Initialize("Skin Color", variantNames, (int id) => makingVoterIdInfo.SkinColorInt = id);
    }

    private void ToggleAppearanceCustomizerForLocal()
    {
        appearanceCustomizerPanel.SetActive(!appearanceCustomizerPanel.activeInHierarchy);
    }

    [Button]
    private void CheckDetails()
    {
        if (makingVoterIdInfo == fakeVoterIdInfo)
        {
            Debug.Log("PASSED");
        }
    }
}


