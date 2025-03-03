using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
     // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Label testMessageText = root.Q<Label>("TestLabel");
        testMessageText.text = "Hi mom";

        testMessageText.SetBinding("text", new DataBinding
        {
            dataSource = PlayerData.Instance,
            dataSourcePath = new PropertyPath(nameof(PlayerData.currentHealth)),
            bindingMode = BindingMode.ToTarget,
        });
    }
}
