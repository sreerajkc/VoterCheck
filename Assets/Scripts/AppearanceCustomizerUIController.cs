using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppearanceCustomizerUIController : MonoBehaviour
{
    private int id = 1;
    private int maxVariantsCount;
    private string[] variantNames;
    private Action<int> onUpdateId;

    [Header("UI Elements")]
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;

    [SerializeField] private TextMeshProUGUI partTitleText;
    [SerializeField] private TextMeshProUGUI partVariantNameText;


    public int Id => id;

    private void Awake()
    {
        rightButton.onClick.AddListener(() => CycleVariants(+1));
        leftButton.onClick.AddListener(() => CycleVariants(-1));
    }

    public void Initialize(string partName, string[] variantNames, Action<int> onUpdateId)
    {
        partTitleText.text = partName;

        this.variantNames = variantNames;
        maxVariantsCount = variantNames.Length;

        partVariantNameText.text = variantNames[0];

        this.onUpdateId = onUpdateId;
    }

    private void CycleVariants(int direction)
    {
        id += direction;
        if (id > maxVariantsCount)
        {
            id = 1;
        }
        else if(id <= 0)
        {
            id = maxVariantsCount;
        }

        onUpdateId?.Invoke(id);
        partVariantNameText.text = variantNames[id - 1];
    }

}
