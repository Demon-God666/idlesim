namespace IdleSim.scenes.main.components;

public class PlayerDataJson
{

    public PlayerDataJson(int money, int skillPoints)
    {
        Money = money;
        SkillPoints = skillPoints;
    }

    public int Money { get; }
    public int SkillPoints { get; }

}


