using TMPro;
using UnityEngine;


public class GunSlot : WeaponSlot
{
    public TextMeshProUGUI bulletType;
    public TextMeshProUGUI clipInfo;

    /// <summary>
    /// 子弹数量还需要从仓库中读取，需要修改
    /// </summary>
    /// <param name="newItem"></param>
    public override void AddToSlot(Item newItem)
    {
        base.AddToSlot(newItem);
        bulletType.text = item.bulletType;
        clipInfo.text = "30 / 60";
    }
    




}
