using UnityEngine;
using UnityEngine.UI;

public class MultilingualText : Text {

    [SerializeField]
    private string _key;

    public string Key
    {
        get { return _key; }
        set { _key = value; }
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnKeyValueChanged()
    {
        //text = FIND_VALUE(Key);
    }
}
