using System.Collections.Generic;

namespace IdleSim.scenes.skill_tree.components;

public class SkillData
{
    
    public int Id { get; set; }
    public List<int> Child { get; set; }
    public string Image { get; set; } = "";
    public string SkillName { get; set; } = "";
    public string SkillDescription { get; set; } = "";
    public int Cost { get; set; }
    public int 	BoughtLevels { get; set; }
    public int Levels { get; set; }
    public int Value { get; set; }
}