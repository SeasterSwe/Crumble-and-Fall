using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusFromHight : MonoBehaviour
{
    [Header("Sources")]
    private ElevationCheck elevationCheck;
    private GenerateBlockToInventoryOverTime generator;
    private Cannon cannon;
    
    public Image uiBonusRotation;
    public Image uibonusVelocity;
    public Image uiBonusGenerator;
    
    [Header ("Give Bonus At")]
    public float bonusRotationAt = 7;
    public float bonusVelocityAt = 9;
    public float bonusGeneratorAt = 11;

    [Header("Bonus Values")]
    public float cannonRotationBonus = 2;
    public float cannonVelocityBonus = 2;
    public float blockGenBonus = 2;

    private float cannonRotationBaseValue;
    private float cannonVelocityBaseValue;
    private float blockGenBaseValue;

    public Color inaktiveCol = new Color(1, 0, 0, 0.5f);

    // Start is called before the first frame update
    void Awake()
    {
        Transform parent = transform.parent;
        elevationCheck = parent.GetComponentInChildren<ElevationCheck>();
        cannon = parent.GetComponentInChildren<Cannon>();
        generator = parent.GetComponentInChildren<GenerateBlockToInventoryOverTime>();

        cannonRotationBaseValue = cannon.bonunsRotationSpeed;
        cannonVelocityBaseValue = cannon.velBouns;
        blockGenBaseValue = generator.bonus;

        bonusRotationAt = bonusRotationAt + elevationCheck.groundlevel;
        bonusVelocityAt = bonusVelocityAt + elevationCheck.groundlevel;
        bonusGeneratorAt = bonusGeneratorAt + elevationCheck.groundlevel;
    }
    
    // Update is called once per frame
    void Update()
    {
        cannon.rotationSpeed = Bonus(bonusRotationAt, uiBonusRotation, cannonRotationBaseValue, cannonRotationBonus);
        cannon.velBouns = Bonus(bonusVelocityAt, uibonusVelocity, cannonVelocityBaseValue, cannonVelocityBonus);
        generator.bonus = Bonus(bonusGeneratorAt, uiBonusGenerator, blockGenBaseValue, blockGenBonus);
    }

    float Bonus(float restictValue, Image img, float baseValue, float bonus)
    {
        if (elevationCheck.towerHight > restictValue)
        {
            img.color = Color.white;
            return bonus;
        }
        else
        {
            img.color = inaktiveCol;
            return baseValue;
        }
    }
}
